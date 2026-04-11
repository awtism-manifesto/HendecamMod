using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Projectiles.Items;

public class FireBlast : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 45;
        Projectile.height = 45;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.penetrate = 1;
        Projectile.timeLeft = 60;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = false;
        Projectile.extraUpdates = 1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 12;
    }
   

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {

        target.AddBuff(BuffID.OnFire, 240);

        target.AddBuff(BuffID.OnFire3, 240);
    }
    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 5; i++)
        {
            float rotation = MathHelper.ToRadians(i * 72f) + Projectile.rotation;
            Vector2 velocity = (Projectile.velocity *14).RotatedBy(rotation);
            Vector2 position = Projectile.Center;

            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                position,
                velocity,
                ProjectileType<FireJet>(),
                Projectile.damage,
                Projectile.knockBack,
                Projectile.owner
            );
        }
    }
   
    public override void AI()
    {

        Projectile.velocity = new Vector2(0.03f, 0.03f);
       
        Projectile.rotation += 0.175f;

        if (Projectile.timeLeft <= 2)
        {

            Projectile.rotation = Main.rand.NextFloat(-36f, 36f);
        }

        for (int i = 0; i < 2; i++)
        {
            float posOffsetX = 0f;
            float posOffsetY = 0f;
            if (i == 1)
            {
                posOffsetX = Projectile.velocity.X * 2.5f;
                posOffsetY = Projectile.velocity.Y * 2.5f;
            }

            Dust chudDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 7, Projectile.height - 7, DustID.Torch, 0f, 0f, 100, default, 1.25f);
            chudDust.fadeIn = 0.1f + Main.rand.Next(3) * 0.1f;
            chudDust.velocity *= 0.115f;
            Dust chud2Dust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 7, Projectile.height - 7, DustType<LycopiteDust>(), 0f, 0f, 100, default, 0.33f);
            chud2Dust.fadeIn = 0.1f + Main.rand.Next(3) * 0.1f;
            chud2Dust.velocity *= 0.15f;
        }
    }
}