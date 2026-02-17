namespace HendecamMod.Content.Poop;

public class PooShit : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 6; // The width of projectile hitbox
        Projectile.height = 6; // The height of projectile hitbox

        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Melee; // Is the projectile shoot by a ranged weapon?
        Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 1125;
        Projectile.extraUpdates = 1;
        Projectile.light = 0.1f;
        Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = true; // Can the projectile collide with tiles?
        AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        Projectile.aiStyle = 1;
        Projectile.alpha = 255;
    }

    public override void AI()
    {
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 17f)
        {
            Projectile.ai[0] = 17f;
            Projectile.velocity.Y += 0.22f;
        }

        // The projectile is rotated to face the direction of travel
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

        // Cap downward velocity
        if (Projectile.velocity.Y > 18f)
        {
            Projectile.velocity.Y = 18f;
        }

        if (Projectile.alpha < 205)
        {
            for (int i = 0; i < 1; i++)
            {
                float posOffsetX = 0f;
                float posOffsetY = 0f;
                if (i == 1)
                {
                    posOffsetX = Projectile.velocity.X * 2.5f;
                    posOffsetY = Projectile.velocity.Y * 2.5f;
                }

                Dust fire2Dust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 8, Projectile.height - 8, DustID.Poop, 0f, 0f, 100, default, 0.95f);
                fire2Dust.fadeIn = 0.2f + Main.rand.Next(4) * 0.1f;
                fire2Dust.noGravity = true;
                fire2Dust.velocity *= 0.35f;
                Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 8, Projectile.height - 8, DustID.Poop, 0f, 0f, 100, default, 0.95f);
                fireDust.fadeIn = 0.2f + Main.rand.Next(4) * 0.1f;
                fireDust.noGravity = true;
                fireDust.velocity *= 0.35f;
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

        target.AddBuff(BuffID.Poisoned, 180);
        target.AddBuff(BuffID.Stinky, 900);
    }
}