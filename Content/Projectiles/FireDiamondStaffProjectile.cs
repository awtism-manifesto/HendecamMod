namespace HendecamMod.Content.Projectiles;

/// <summary>
///     This the class that clones the vanilla Meowmere projectile using CloneDefaults().
///     Make sure to check out <see cref="ExampleCloneWeapon" />, which fires this projectile; it itself is a cloned
///     version of the Meowmere.
/// </summary>
public class FireDiamondStaffProjectile : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 12; // The width of projectile hitbox
        Projectile.height = 12; // The height of projectile hitbox

        Projectile.scale = 1f;
        Projectile.timeLeft = 150;
        Projectile.tileCollide = true;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.penetrate = 3;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.alpha = 55;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.OnFire3, 120);
        Vector2 Peanits = Projectile.Center - new Vector2(Main.rand.Next(-1, 1), 2);
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits,
            new Vector2(1, 0).RotatedBy((Peanits).DirectionTo(Projectile.Center).ToRotation()),
            ModContent.ProjectileType<BoomSmall>(), (int)(Projectile.damage * 0.75), Projectile.knockBack, Projectile.owner);
    }

    public override void AI()
    {
        Projectile.rotation += 0.33f;
        if (Math.Abs(Projectile.velocity.X) >= 4f || Math.Abs(Projectile.velocity.Y) >= 4f)
        {
            for (int i = 0; i < 2; i++)
            {
                float posOffsetX = 0f;
                float posOffsetY = 0f;
                if (i == 1)
                {
                    posOffsetX = Projectile.velocity.X * 0f;
                    posOffsetY = Projectile.velocity.Y * 0f;
                }

                Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 8, Projectile.height - 8, DustID.Torch, 0f, 0f, 100, default, 0.67f);
                fireDust.fadeIn = 0.2f + Main.rand.Next(4) * 0.1f;
                fireDust.velocity *= 0.15f;
                fireDust.noGravity = true;
            }
        }
    }
}