using HendecamMod.Content.Buffs;

namespace HendecamMod.Content.Projectiles.Friendly.Ranged;

public class GarandMag : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.penetrate = 1;
        Projectile.timeLeft = 200;
        Projectile.light = 0.33f;
        Projectile.tileCollide = true;
        Projectile.ignoreWater = false;
        Projectile.extraUpdates = 1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 15;
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        if (Main.zenithWorld)
        {
            modifiers.SourceDamage *= 25f;
        }
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.penetrate <= 0)
        {
            Projectile.Kill();
        }
        else
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            // If the projectile hits the left or right side of the tile, reverse the X velocity
            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
            {
                Projectile.velocity.X = -oldVelocity.X * 0.5f;
            }

            // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
            {
                Projectile.velocity.Y = -oldVelocity.Y * 0.5f;
            }
        }

        return false;
    }
    public override void AI()
    {
        Projectile.rotation += 0.21f;
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 19f)
        {
            Projectile.ai[0] = 19f;
            Projectile.velocity.Y += 0.21f;
        }
        else
        {
            Projectile.velocity.Y -= 0.15f;
        }

        if (Projectile.velocity.Y > 15f)
        {
            Projectile.velocity.Y = 17f;
        }
    }
}