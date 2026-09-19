using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Projectiles;

public class AstaGlaive : ModProjectile
{
    public override void SetStaticDefaults()
    {
    }

    public override void SetDefaults()
    {
        Projectile.width = 44;
        Projectile.height = 44;
        Projectile.tileCollide = true;
        Projectile.arrow = false;
        Projectile.friendly = true;
        Projectile.DamageType = GetInstance<MeleeRangedDamage>();
        Projectile.timeLeft = 60;
        Projectile.penetrate = 7;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 10;
        Projectile.extraUpdates = 1;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int i = 0; i < 5; i++)
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustType<AstatineDust>());
            dust.noGravity = true;
            dust.velocity *= 5.5f;
            dust.scale *= 1f;
        }

        target.AddBuff(BuffType<RadPoisoning3>(), 180);
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
        Projectile.penetrate--;
        return false;
    }
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        Projectile.rotation += 0.425f;
        Lighting.AddLight(Projectile.Center, 0.5f, 0.05f, 0.05f);
        for (int i = 0; i < 2; i++)
        {
            float posOffsetX = 0f;
            float posOffsetY = 0f;
            if (i == 1)
            {
                posOffsetX = Projectile.velocity.X * 2.5f;
                posOffsetY = Projectile.velocity.Y * 2.5f;
            }

            Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 1, Projectile.height - 1, DustType<AstatineDust>(), 0f, 0f, 100, default, 0.95f);
            fireDust.fadeIn = 0.1f + Main.rand.Next(1) * 0.1f;
            fireDust.noGravity = true;
            fireDust.velocity *= 1.35f;
        }

        if (Projectile.ai[0] == 0f)
        {
            if (Projectile.timeLeft <= 30)
            {
                Projectile.ai[0] = 1f;
                Projectile.tileCollide = false;
            }
        }

        else
        {
            Projectile.timeLeft = 10;

            Vector2 toPlayer = player.Center - Projectile.Center;
            float distance = toPlayer.Length();
            if (distance < 24f)
            {
                Projectile.Kill();
                return;
            }

            toPlayer.Normalize();

            float returnSpeed = 23f;
            float acceleration = 1.25f;

            Projectile.velocity = (Projectile.velocity * acceleration + toPlayer * returnSpeed) / (acceleration + 1f);
            if (Projectile.velocity.Length() < returnSpeed)
                Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero) * returnSpeed;
        }
    }
}