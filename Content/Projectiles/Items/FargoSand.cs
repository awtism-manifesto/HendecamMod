using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;

namespace HendecamMod.Content.Projectiles.Items;

public class FargoSand : ModProjectile
{
   
    public override void SetDefaults()
    {
        Projectile.width = 18; // The width of projectile hitbox
        Projectile.height = 18; // The height of projectile hitbox
        Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = ModContent.GetInstance<OmniDamage>();
      
        Projectile.penetrate = 2; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 280; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
        Projectile.alpha = 72; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.

        Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = true; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        AIType = ProjectileID.Bullet; // Act exactly like default Bullet
    }

    public override void AI()
    {
        if (Projectile.alpha < 222)
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

                Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 16, Projectile.height - 16, DustID.Sand, 0f, 0f, 100, default, 1.25f);
                fireDust.fadeIn = 0.2f + Main.rand.Next(4) * 0.1f;
                fireDust.noGravity = true;
                fireDust.velocity *= 1f;
            }
        }
    }

   

    

    
}