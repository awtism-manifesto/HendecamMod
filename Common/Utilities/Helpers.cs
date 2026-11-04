using HendecamMod.Common.Systems.Players;
using MonoMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Map;

namespace HendecamMod.Common.Utilities
{
    public readonly struct RaycastData(Vector2 end, bool hit, float lengthSQ, Tile? tile = null)
    {
        public readonly Vector2 End = end;
        public readonly bool Hit = hit;
        public readonly float LengthSQ = lengthSQ;
        public readonly float Length { get { return MathF.Sqrt(LengthSQ); } }
        public readonly Tile? Tile = tile;
    }
    public static class Helpers
    {
        public static HendecamPlayer GetHCPlayer(this Player player) => player.GetModPlayer<HendecamPlayer>();

        public static void ExpandHitboxBy(this Projectile projectile, int width, int height)
        {
            projectile.position = projectile.Center;
            projectile.width = width;
            projectile.height = height;
            projectile.position -= projectile.Size * 0.5f;
        }
        public static void ExpandHitboxBy(this Projectile projectile, int newSize) => projectile.ExpandHitboxBy(newSize, newSize);
        public static void ExpandHitboxBy(this Projectile projectile, Vector2 newSize) => projectile.ExpandHitboxBy((int)newSize.X, (int)newSize.Y);
        public static void ExpandHitboxBy(this Projectile projectile, float expandRatio) => projectile.ExpandHitboxBy((int)(projectile.width * expandRatio), (int)(projectile.height * expandRatio));
        public static Vector3 ToVector3(this Vector2 v) => new(v.X, v.Y, 0);

        public static bool IsLocalPlayer(this Player player) => player.whoAmI == Main.myPlayer;

        public static RaycastData QuickRaycast(Vector2 origin, Vector2 direction, Func<Point, bool> shouldContinue = null, float maxDistTiles = 64f, bool visualize = false)
        {
            shouldContinue ??= p => false;
            origin /= 16f;
            direction = direction.SafeNormalize(Vector2.Zero);
            Vector2 unitStepSize = new(
                (float)Math.Sqrt(1 + direction.Y / direction.X * (direction.Y / direction.X)),
                (float)Math.Sqrt(1 + direction.X / direction.Y * (direction.X / direction.Y))
                );
            Point tileCheck = origin.ToPoint();
            Vector2 rayLength1D;
            Point step;
            float curDistPixels = 0f;
            if (direction.X < 0)
            {
                step.X = -1;
                rayLength1D.X = (origin.X - tileCheck.X) * unitStepSize.X;
            }
            else
            {
                step.X = 1;
                rayLength1D.X = (tileCheck.X + 1 - origin.X) * unitStepSize.X;
            }
            if (direction.Y < 0)
            {
                step.Y = -1;
                rayLength1D.Y = (origin.Y - tileCheck.Y) * unitStepSize.Y;
            }
            else
            {
                step.Y = 1;
                rayLength1D.Y = (tileCheck.Y + 1 - origin.Y) * unitStepSize.Y;
            }
            bool tileFound = false;
            Tile? intersectTile = null;
            while (!tileFound && curDistPixels < maxDistTiles)
            {
                // Walk
                if (rayLength1D.X < rayLength1D.Y)
                {
                    tileCheck.X += step.X;
                    curDistPixels = rayLength1D.X;
                    rayLength1D.X += unitStepSize.X;
                }
                else
                {
                    tileCheck.Y += step.Y;
                    curDistPixels = rayLength1D.Y;
                    rayLength1D.Y += unitStepSize.Y;
                }
                if (visualize)
                {
                    Dust d = Dust.NewDustPerfect(tileCheck.ToWorldCoordinates(), DustID.WhiteTorch);
                    d.noGravity = true;
                }
                if (TileHelpers.SolidTile(tileCheck, out Tile tile))
                {
                    // set to false to skip this tile
                    bool isHit = true;
                    // vector2 that goes from vector2.zero on the top left of the tile to new vector2.one on the bottom right
                    Vector2 localIntersection = origin + direction * curDistPixels - tileCheck.ToVector2();
                    if (tile.IsHalfBlock)
                    {
                        bool isTopHalf = localIntersection.Y < 0.5f;
                        // if the local intersection is on the top half:
                        if (isTopHalf)
                        {
                            // if the raycast is going downwards, run all our logic.
                            if (direction.Y > 0f)
                            {
                                float yRemaining = 0.5f - localIntersection.Y;
                                float distToNextIntersection = yRemaining / Math.Abs(direction.Y);

                                // now, we could easily just:
                                // - add distToNextIntersection to curDistPixels
                                // - recalculate localIntersection
                                // - see if localIntersection.X is less than 0f, or more than 1f
                                // but that's a whole one extra flop (probably with more overhead since it's vector math)!
                                // so instead let's just recalculate x, which is all we need anyway.
                                float nextX = localIntersection.X + distToNextIntersection * direction.X;

                                if (nextX < 0f || nextX > 1f)
                                    // out of bounds? that means we missed, so skip this tile.
                                    isHit = false;
                                else
                                    // finally, if we're not out of bounds, add the distance to the ray's pixel distance.
                                    curDistPixels += distToNextIntersection;
                            }
                            // if it's going upwards, that means we missed, so skip this tile.
                            else
                            {
                                isHit = false;
                            }
                        }
                        // we don't need to add extra logic for the bottom half because it would just collide like a regular tile then.
                    }
                    else if (tile.Slope != SlopeType.Solid)
                    {
                        // we can actually calculate all that we need using this. as you can see it's basically the way we did half blocks except we calculate both x and y.
                        void CalculateSlopedCollision(bool rising)
                        {
                            // for a rising slope, the ray's vertical offset decreases as it moves along the slope
                            // for a falling slope, the ray's vertical offset increases as it moves along the slope

                            // this little function took me about 4 hours to make because i fucking suck at math
                            // now i can rest easy
                            float localDiff = rising ? 1f - localIntersection.X - localIntersection.Y : localIntersection.Y - localIntersection.X;
                            float localDistFactor = rising ? direction.X + direction.Y : direction.X - direction.Y;

                            if (localDistFactor == 0f)
                            {
                                isHit = false;
                                return;
                            }

                            float distToNextIntersection = localDiff / localDistFactor;

                            float nextX = localIntersection.X + distToNextIntersection * direction.X;
                            float nextY = localIntersection.Y + distToNextIntersection * direction.Y;

                            if (nextX >= 0f && nextX <= 1f && nextY >= 0f && nextY <= 1f)
                                curDistPixels += distToNextIntersection;
                            else
                                isHit = false;
                        }

                        switch (tile.Slope)
                        {
                            // imagine a square of 4 tiles, sloped to look like a diamond. (image below) (i swear it's not an ip grabber i would never do that i just don't like long links)
                            // https://bit.ly/4hyqy6N

                            // bottom and right are solid and aligned with tile grid. this would be tile [0, 0] on the diamond. it is a rising slope.
                            case SlopeType.SlopeDownRight:
                                // check if custom collision should be done in the first place
                                if (localIntersection.Y < 1f && localIntersection.X < 1f)
                                    CalculateSlopedCollision(true);
                                break;

                            // bottom and left are solid and aligned with tile grid. this would be tile [1, 0] on the diamond. it is a falling slope.
                            case SlopeType.SlopeDownLeft:

                                // check if custom collision should be done in the first place
                                if (localIntersection.Y < 1f && localIntersection.X > 0f)
                                    CalculateSlopedCollision(false);
                                break;

                            // top and right are solid and aligned with tile grid. this would be tile [0, 1] on the diamond. it is a falling slope.
                            case SlopeType.SlopeUpRight:
                                // check if custom collision should be done in the first place
                                if (localIntersection.Y > 0f && localIntersection.X < 1f)
                                    CalculateSlopedCollision(false);
                                break;

                            // top and left are solid and aligned with tile grid. this would be tile [1, 1] on the diamond. it is a rising slope.
                            case SlopeType.SlopeUpLeft:
                                // check if custom collision should be done in the first place
                                if (localIntersection.Y > 0f && localIntersection.X > 0f)
                                    CalculateSlopedCollision(true);
                                break;
                        }
                    }

                    if (isHit && !shouldContinue(tileCheck))
                    {
                        tileFound = true;
                        intersectTile = tile;
                    }
                }
            }
            Vector2 intersection;
            if (tileFound)
            {
                intersection = origin + direction * curDistPixels;
            }
            else
            {
                intersection = origin + direction * maxDistTiles;
            }
            return new RaycastData(intersection * 16f, tileFound, Vector2.DistanceSquared(origin * 16f, intersection * 16f), intersectTile);
        }
    }
}
