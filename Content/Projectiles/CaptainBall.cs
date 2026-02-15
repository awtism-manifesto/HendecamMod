using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;

namespace HendecamMod.Content.Projectiles;

// This example is similar to the Wooden Arrow projectile
public class CaptainBall : ModProjectile
{
   
    public override void SetDefaults()
    {
        Projectile.width = 20; // The width of projectile hitbox
        Projectile.height = 20; // The height of projectile hitbox
        Projectile.penetrate = 7;
        Projectile.arrow = true;
        Projectile.friendly = true;
        Projectile.DamageType =  DamageClass.Ranged;
        Projectile.timeLeft = 330;
        Projectile.scale = 0.75f;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 18;
    }

   

   

    public override void AI()
    {
        // The code below was adapted from the ProjAIStyleID.Arrow behavior. Rather than copy an existing aiStyle using Projectile.aiStyle and AIType,
        // like some examples do, this example has custom AI code that is better suited for modifying directly.
        // See https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#what-is-ai for more information on custom projectile AI.

        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 13f)
        {
            Projectile.ai[0] = 13f;
            Projectile.velocity.Y += 0.3f;
        }

        // Cap downward velocity
        if (Projectile.velocity.Y > 31f)
        {
            Projectile.velocity.Y = 31f;
        }
        Projectile.rotation += 0.25f;
        if (Projectile.timeLeft > 326)
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

                Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 1, Projectile.height - 1, DustID.Smoke, 0f, 0f, 50, default, 1.5f);
                fireDust.fadeIn = 0.2f + Main.rand.Next(5) * 0.1f;
                fireDust.noGravity = true;
                fireDust.velocity *= 1.75f;
            }
        }

    }

  

  
}