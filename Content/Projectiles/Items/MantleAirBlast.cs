using Terraria.Audio;

namespace HendecamMod.Content.Projectiles.Items;

// This example is similar to the Wooden Arrow projectile
public class MantleAirBlast : ModProjectile
{
    

    public override void SetDefaults()
    {
        Projectile.width = 60; // The width of projectile hitbox
        Projectile.height = 60; // The height of projectile hitbox
        Projectile.scale = 0.5f;
        Projectile.penetrate = 3;
       
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.timeLeft = 36;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        Projectile.extraUpdates = 2;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
    }

   public Color mantiusColor = Color.White;
    

    
    public override Color? GetAlpha(Color lightColor)
    {
        return mantiusColor;
    }
  
    public override void AI()
    {

        if (Math.Abs(Projectile.velocity.X) >= 5f || Math.Abs(Projectile.velocity.Y) >= 5f)
        {
            Projectile.velocity *= 0.9f;
        }


        if (Projectile.timeLeft <= 6)
        {
            mantiusColor = new Color(185, 15, 90);
        }


    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {

        modifiers.SourceDamage = modifiers.SourceDamage + (target.defense * 0.036f);
    }
    public override void OnKill(int timeLeft)
    {
       
            float rotation = MathHelper.ToRadians(13.5f);
            Vector2 velocity = Projectile.velocity.RotatedBy(rotation);
            Vector2 position = Projectile.Center;

            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                position,
                velocity,
                ProjectileType<MantleBurstLaser>(),
                 (int)(Projectile.damage * 0.63f),
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
            ProjectileType<MantleBurstLaser>(),
            (int)(Projectile.damage * 0.63f),
            Projectile.knockBack,
            Projectile.owner
        );
        float rotation3 = MathHelper.ToRadians(-13.5f);
        Vector2 velocity3 = Projectile.velocity.RotatedBy(rotation3);
        Vector2 position3 = Projectile.Center;

        Projectile.NewProjectile(
            Projectile.GetSource_FromThis(),
            position3,
            velocity3,
            ProjectileType<MantleBurstLaser>(),
             (int)(Projectile.damage * 0.63f),
            Projectile.knockBack,
            Projectile.owner
        );


       
       
    }
}