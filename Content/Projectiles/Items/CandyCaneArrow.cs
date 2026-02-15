using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;

namespace HendecamMod.Content.Projectiles.Items;

// This example is similar to the Wooden Arrow projectile
public class CandyCaneArrow : ModProjectile
{
    

    public override void SetDefaults()
    {
        Projectile.width = 14; // The width of projectile hitbox
        Projectile.height = 14; // The height of projectile hitbox
        Projectile.penetrate = 1;
        Projectile.arrow = true;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.timeLeft = 330;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        
    }

   

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
       
        Projectile.damage = (int)(Projectile.damage * 0.75f);
    }

    public override void AI()
    {

        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 17f)
        {
            Projectile.ai[0] = 17f;
            Projectile.velocity.Y += 0.27f;
        }

        // Cap downward velocity
        if (Projectile.velocity.Y > 17f)
        {
            Projectile.velocity.Y = 25f;
        }

       
    }

    public override void OnKill(int timeLeft)
    {
       
            float rotation = MathHelper.ToRadians(10f);
            Vector2 velocity = Projectile.velocity.RotatedBy(rotation);
            Vector2 position = Projectile.Center;

            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                position,
                velocity,
                ModContent.ProjectileType<CandyCaneShard>(),
                Projectile.damage,
                Projectile.knockBack,
                Projectile.owner
            );
        float rotation2 = MathHelper.ToRadians(0f);
        Vector2 velocity2 = Projectile.velocity.RotatedBy(rotation2);
        Vector2 position2 = Projectile.Center;

        Projectile.NewProjectile(
            Projectile.GetSource_FromThis(),
            position2,
            velocity2,
            ModContent.ProjectileType<CandyCaneShard>(),
            Projectile.damage,
            Projectile.knockBack,
            Projectile.owner
        );
        float rotation3 = MathHelper.ToRadians(-10f);
        Vector2 velocity3 = Projectile.velocity.RotatedBy(rotation3);
        Vector2 position3 = Projectile.Center;

        Projectile.NewProjectile(
            Projectile.GetSource_FromThis(),
            position3,
            velocity3,
            ModContent.ProjectileType<CandyCaneShard>(),
            Projectile.damage,
            Projectile.knockBack,
            Projectile.owner
        );


        SoundEngine.PlaySound(SoundID.Item10, Projectile.position); // Plays the basic sound most projectiles make when hitting blocks.
       
    }
}