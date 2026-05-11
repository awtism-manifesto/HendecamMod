using Terraria.Utilities;

namespace HendecamMod.Common.Utils;

/// <summary>
/// Contains a lot of utillities and global usings
/// </summary>
public static partial class LemonUtils
{
    public static bool NotClient() => Main.netMode != NetmodeID.MultiplayerClient;

    /// <summary>
    /// Accelerates an entity towards a position.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="pos">The position to accelerate towards</param>
    /// <param name="xDecel">The "turning speed" on the X axis. Increase this value if you want the NPC to decelerate faster if its not moving in the desired direction</param>
    /// <param name="yDecel">Same as xDecel, just on the Y axis</param>
    /// <param name="xAccel">The desired acceleration on the X axis</param>
    /// <param name="yAccel">The desired acceleration on the Y axis</param>
    public static void MoveToPos(this Entity entity, Vector2 pos, float xDecel = 1f, float yDecel = 1f, float xAccel = 1f, float yAccel = 1f)
    {
        Vector2 direction = entity.Center.DirectionTo(pos);
        if (direction.HasNaNs())
        {
            return;
        }
        float XaccelMod = Math.Sign(direction.X) - Math.Sign(entity.velocity.X);
        float YaccelMod = Math.Sign(direction.Y) - Math.Sign(entity.velocity.Y);
        entity.velocity += new Vector2(XaccelMod * xDecel + xAccel * Math.Sign(direction.X), YaccelMod * yDecel + yAccel * Math.Sign(direction.Y));
    }

    /// <summary>
    /// Returns 1 one out of "consequent" times, otherwise -1.
    /// </summary>
    /// <param name="rand"></param>
    /// <param name="consequent"></param>
    /// <returns></returns>
    public static int NextDirectionInt(this UnifiedRandom rand, int consequent = 1)
    {
        return rand.NextBool(consequent).ToDirectionInt();
    }

    public static bool IntersectsExact(this Rectangle rect, Rectangle other)
    {
        return (other.Left <= rect.Right &&
                    rect.Left <= other.Right &&
                    other.Top <= rect.Bottom &&
                    rect.Top <= other.Bottom);
    }

    public static float AngleBetween(Vector2 v1, Vector2 v2)
    {
        return MathF.Atan2(v1.X * v2.Y - v2.X * v1.Y, v1.X * v2.X + v1.Y * v2.Y);
    }

    /// <summary>
    /// Similar to Main.rand.NextVector2Circular(), but allows you to specify minimum distance from center
    /// </summary>
    /// <param name="circleHalfWidth"></param>
    /// <param name="circleHalfHeight"></param>
    /// <param name="minWidth"></param>
    /// <param name="minHeight"></param>
    /// <returns></returns>
    public static Vector2 RandomVector2Circular(float circleHalfWidth, float circleHalfHeight, float minWidth = 0, float minHeight = 0)
    {
        float width = 0;
        float height = 0;
        do
        {
            width = Main.rand.NextFloat(-circleHalfWidth, circleHalfWidth);
        }
        while (Math.Abs(width) <= minWidth);

        do
        {
            height = Main.rand.NextFloat(-circleHalfHeight, circleHalfHeight);
        }
        while (Math.Abs(height) <= minHeight);

        return new Vector2(width, height);
    }

    public static void DebugPlayerCenter(Player player)
    {
        Main.NewText("Player Center: " + player.Center);
    }

    public static void DebugPlayerTileCoords(Player player)
    {
        Main.NewText("Player Tile Coords: " + player.Center.ToTileCoordinates());
    }

    public static float ClosenessToMidpoint(int length, int index)
    {
        if (index >= length || index < 0)
        {
            throw new IndexOutOfRangeException();
        }
        int mid = length / 2;
        int distanceToMid = (int)MathF.Abs(index - mid);
        int closeness = 1 - (distanceToMid / mid);
        return closeness;
    }

    /// <summary>
    /// Returns the sign of num, or zeroDefault if num = 0
    /// </summary>
    /// <param name="num"></param>
    /// <param name="zeroDefault"></param>
    /// <returns></returns>
    public static int Sign(float num, int zeroDefault = 0)
    {
        if (num > 0)
        {
            return 1;
        }
        else if (num < 0)
        {
            return -1;
        }
        else
        {
            return zeroDefault;
        }
    }

    /// <summary>
    /// Returns the sign of num, or zeroDefault if num = 0
    /// Casts zeroDefault to int
    /// </summary>
    public static int Sign(float num, float zeroDefault = 0)
    {
        int _zeroDefault = (int)zeroDefault;
        if (num > 0)
        {
            return 1;
        }
        else if (num < 0)
        {
            return -1;
        }
        else
        {
            return _zeroDefault;
        }
    }
}
