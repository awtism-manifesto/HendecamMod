using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace HendecamMod.Content.Projectiles;

// This example is similar to the Wooden Arrow projectile
public class AvalancheBoulder : ModProjectile
{
   
    public override void SetDefaults()
    {
        Projectile.width = 45; // The width of projectile hitbox
        Projectile.height = 45; // The height of projectile hitbox
        Projectile.aiStyle = 1;
        Projectile.extraUpdates = 3;
        Projectile.friendly = true;
        Projectile.penetrate = 6;

        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 25;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.timeLeft = 195;
        AIType = ProjectileID.Bullet;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.Frostburn, 390);
        target.AddBuff(BuffID.Frostburn2, 150);
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
                Projectile.velocity.X = -oldVelocity.X;
                Projectile.extraUpdates = 1;
            }

            // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
                Projectile.extraUpdates = 1;
            }
        }

        return false;
    }

    

    public override void AI()
    {
        if (Projectile.timeLeft <= 115)

            Projectile.ai[0] += 8.5f;
        if (Projectile.ai[0] >= 8.5f)
        {
            Projectile.ai[0] = 8.5f;
            Projectile.velocity.Y += 1.05f;
        }

        // The projectile is rotated to face the direction of travel
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

        // Cap downward velocity
        if (Projectile.velocity.Y > 47f)
        {
            Projectile.velocity.Y = 47f;
        }

        if (Math.Abs(Projectile.velocity.X) >= 4f || Math.Abs(Projectile.velocity.Y) >= 4f)
        {
            for (int i = 0; i < 2; i++)
            {
                float posOffsetX = 0f;
                float posOffsetY = 0f;
                if (i == 1)
                {
                    posOffsetX = Projectile.velocity.X * 2.5f;
                    posOffsetY = Projectile.velocity.Y * 2.5f;
                }

                Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 8, Projectile.height - 8, DustID.Snow, 0f, 0f, 100, default, 0.4f);
                fireDust.fadeIn = 0.2f + Main.rand.Next(5) * 0.1f;
                fireDust.velocity *= 0.2f;
            }
        }
    }

    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 7; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Snow);
            dust.noGravity = true;
            dust.velocity *= 5.5f;
            dust.scale *= 1.9f;
            Dust dusty = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Ice);
            dusty.noGravity = true;
            dusty.velocity *= 3.5f;
            dusty.scale *= 1.1f;
        }
    }
}