using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;

namespace HendecamMod.Physics;

public class VerletPoint
{
    public int ID { get; set; }
    public Vector2 pos { get; set; }
    public Vector2 oldPos { get; set; }
    public bool isVerletPinned { get; set; }

    public void Update()
    {
        if (!isVerletPinned)
        {
            Vector2 velocity = pos - oldPos;
            oldPos = pos;
            pos += velocity + new Vector2(0f, PhysicsMethods.gravity);
        }
    }
}

public class VerletStick
{
    public int ID { get; set; }
    public VerletPoint pointA { get; set; }
    public VerletPoint pointB { get; set; }
    public float length { get; set; }

    public void Update()
    {
        if (pointA == null || pointB == null) return;

        Vector2 diff = pointB.pos - pointA.pos;
        float distance = diff.Length();
        if (distance <= 0f) return;

        float percent = (length - distance) / distance * 0.5f;
        Vector2 offset = diff * percent;

        if (!pointA.isVerletPinned)
        {
            pointA.pos -= offset;
        }
        if (!pointB.isVerletPinned)
        {
            pointB.pos += offset;
        }
    }
}

public class VerletChain
{
    public VerletStick startStick { get; set; }
    public VerletStick endStick { get; set; }
    public List<VerletStick> allSticks { get; set; }

    public void Update(Vector2 startPos, Vector2 endPos)
    {
        if (allSticks == null) return;

        startStick.pointA.pos = startPos;
        endStick.pointB.pos = endPos;

        for (int i = 0; i < PhysicsMethods.ConstraintIterations; i++)
        {
            foreach (VerletStick stick in allSticks)
            {
                stick.Update();
            }
        }
    }

    public void UpdateStart(Vector2 startPos)
    {
        if (startStick?.pointA != null)
        {
            startStick.pointA.pos = startPos;
        }
    }

    public void Kill()
    {
        if (allSticks != null)
        {
            foreach (VerletStick stick in allSticks)
            {
                stick.pointA = null;
                stick.pointB = null;
            }
            allSticks.Clear();
            allSticks = null;
        }
        endStick = null;
        startStick = null;
        PhysicsMethods.GetChains().Remove(this);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 screenPos, Texture2D texture, Color drawColor, bool useLighting, Texture2D texture2 = null, Texture2D startTexture = null, Texture2D endTexture = null)
    {
        if (allSticks == null) return;

        bool hasAlternatingTexture = texture2 != null;
        bool hasStartTexture = startTexture != null;
        bool hasEndTexture = endTexture != null;

        for (int i = 0; i < allSticks.Count; i++)
        {
            bool isStart = hasStartTexture && i == 0;
            bool isEnd = hasEndTexture && i == allSticks.Count - 1;
            if (hasAlternatingTexture && i % 2 == 0 && !isStart && !isEnd)
                continue;

            VerletStick stick = allSticks[i];
            Vector2 chainCenter = Vector2.Lerp(stick.pointA.pos, stick.pointB.pos, 0.5f);
            float angle = (stick.pointB.pos - stick.pointA.pos).ToRotation();
            Texture2D textureToDraw = isStart ? startTexture : (isEnd ? endTexture : texture);
            Color color = useLighting ? Lighting.GetColor(chainCenter.ToTileCoordinates()) : drawColor;

            spriteBatch.Draw(textureToDraw, chainCenter - screenPos, null, color, angle, textureToDraw.Size() / 2f, 1f, SpriteEffects.None, 0f);
        }

        if (hasAlternatingTexture)
        {
            for (int i = 0; i < allSticks.Count; i += 2)
            {
                VerletStick stick = allSticks[i];
                Vector2 chainCenter = Vector2.Lerp(stick.pointA.pos, stick.pointB.pos, 0.5f);
                float angle = (stick.pointB.pos - stick.pointA.pos).ToRotation();
                spriteBatch.Draw(texture2, chainCenter - screenPos, null, drawColor, angle, texture2.Size() / 2f, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}

public static class PhysicsMethods
{
    public static readonly float gravity = 0.5f;
    public const int ConstraintIterations = 10;
    private static readonly List<VerletPoint> points = [];
    private static readonly List<VerletStick> sticks = [];
    private static readonly List<VerletChain> chains = [];

    public static VerletPoint CreateVerletPoint(int ID, Vector2 pos, bool isVerletPinned)
    {
        var point = new VerletPoint { ID = ID, pos = pos, oldPos = pos, isVerletPinned = isVerletPinned };
        points.Add(point);
        return point;
    }

    public static List<VerletPoint> GetPoints() => points;

    public static VerletStick CreateVerletStick(int ID, VerletPoint pointA, VerletPoint pointB, float length)
    {
        var stick = new VerletStick { ID = ID, pointA = pointA, pointB = pointB, length = length };
        sticks.Add(stick);
        return stick;
    }

    public static List<VerletStick> GetSticks() => sticks;

    public static VerletChain CreateVerletChain(int segmentsNum, float segmentLength, Vector2 posStart, Vector2 posEnd, bool pinStart = true, bool pinEnd = false, float startLength = 0f, float endLength = 0f)
    {
        startLength = startLength == 0f ? segmentLength : startLength;
        endLength = endLength == 0f ? segmentLength : endLength;
        if (chains.Count == 0)
        {
            ClearAll();
        }

        Vector2 segmentVector = (posEnd - posStart) / segmentsNum;
        List<VerletPoint> chainPoints = [];
        List<VerletStick> chainSticks = [];
        VerletStick startStick = null;
        VerletStick endStick = null;

        for (int i = 0; i <= segmentsNum; i++)
        {
            Vector2 pointPos = posStart + (segmentVector * i);
            bool shouldBePinned = (i == 0 && pinStart) || (i == segmentsNum && pinEnd);
            var point = new VerletPoint { ID = 1, pos = pointPos, oldPos = pointPos, isVerletPinned = shouldBePinned };
            points.Add(point);
            chainPoints.Add(point);
        }

        for (int i = 0; i < segmentsNum; i++)
        {
            var stick = new VerletStick { ID = i % 2 == 0 ? 1 : 2, length = segmentLength, pointA = chainPoints[i], pointB = chainPoints[i + 1] };
            if (i == 0)
            {
                stick.length = startLength;
                startStick = stick;
            }
            else if (i == segmentsNum - 1)
            {
                stick.length = endLength;
                endStick = stick;
            }
            sticks.Add(stick);
            chainSticks.Add(stick);
        }

        var chain = new VerletChain { endStick = endStick, startStick = startStick, allSticks = chainSticks };
        chains.Add(chain);
        return chain;
    }

    public static List<VerletChain> GetChains() => chains;

    public static void ClearAll()
    {
        sticks.Clear();
        points.Clear();
        chains.Clear();
    }
}