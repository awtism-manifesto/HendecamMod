using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Poop
{
    /// <summary>
    /// This the class that clones the vanilla Meowmere projectile using CloneDefaults().
    /// Make sure to check out <see cref="ExampleCloneWeapon" />, which fires this projectile; it itself is a cloned version of the Meowmere.
    /// </summary>
    public class PoopRang : ModProjectile
    {
        public override void SetDefaults()
        {
            // This method right here is the backbone of what we're doing here; by using this method, we copy all of
            // the Meowmere Projectile's SetDefault stats (such as projectile.friendly and projectile.penetrate) on to our projectile,
            // so we don't have to go into the source and copy the stats ourselves. It saves a lot of time and looks much cleaner;
            // if you're going to copy the stats of a projectile, use CloneDefaults().

            Projectile.CloneDefaults(ProjectileID.WoodenBoomerang);

            // To further the Cloning process, we can also copy the ai of any given projectile using AIType, since we want
            // the projectile to essentially behave the same way as the vanilla projectile.
            AIType = ProjectileID.WoodenBoomerang;
           

            // After CloneDefaults has been called, we can now modify the stats to our wishes, or keep them as they are.
            // For the sake of example, lets make our projectile penetrate enemies a few more times than the vanilla projectile.
            // This can be done by modifying projectile.penetrate

        }
        public override void AI()
        {
            // The code below was adapted from the ProjAIStyleID.Arrow behavior. Rather than copy an existing aiStyle using Projectile.aiStyle and AIType,
            // like some examples do, this example has custom AI code that is better suited for modifying directly.
            // See https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#what-is-ai for more information on custom projectile AI.

           
            // dust, all dust
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



                    Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 12, Projectile.height - 12, DustID.Poop, 0f, 0f, 100, default, 0.25f);
                    fireDust.fadeIn = 0.2f + Main.rand.Next(4) * 0.1f;
                    fireDust.velocity *= 0.15f;
                    fireDust.noGravity = true;
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 7; i++) // Creates a splash of dust around the position the projectile dies.
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Poop);
                dust.noGravity = true;
                dust.velocity *= 7.5f;
                dust.scale *= 1.25f;
            }

            target.AddBuff(BuffID.Poisoned, 80);
            target.AddBuff(BuffID.Stinky, 900);

        }
        public override void OnKill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            
        }
    }
}