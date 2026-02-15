using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;

namespace HendecamMod.Content.Projectiles;

// This example is similar to the Wooden Arrow projectile
public class BigCannonBall : ModProjectile
{
   

    public override void SetDefaults()
    {
        Projectile.width = 32; // The width of projectile hitbox
        Projectile.height = 32; // The height of projectile hitbox
        Projectile.penetrate = 1;
        Projectile.arrow = true;
        Projectile.friendly = true;
        Projectile.DamageType = ModContent.GetInstance<StupidDamage>();
        Projectile.timeLeft = 225;
       
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Projectile.Kill();
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
            }

            // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
        }

        return false;
    }

    public override void AI()
    {

        if (Projectile.timeLeft <= 215)
        {
            Projectile.Resize(72, 72);
        }

        Projectile.rotation += 0.2f;
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 17f)
        {
            Projectile.ai[0] = 17f;
            Projectile.velocity.Y += 0.285f;
        }

        // Cap downward velocity
        if (Projectile.velocity.Y > 26f)
        {
            Projectile.velocity.Y = 26f;
        }
    }

    

   
}