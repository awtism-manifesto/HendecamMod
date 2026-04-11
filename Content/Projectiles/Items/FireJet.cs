namespace HendecamMod.Content.Projectiles.Items;

public class FireJet : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 18; // The width of projectile hitbox
        Projectile.height = 18; // The height of projectile hitbox

        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Magic; // Is the projectile shoot by a ranged weapon?
        Projectile.penetrate = 5; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 30;

        Projectile.light = 0f;
        Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = false; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 0; // Set to above 0 if you want the projectile to update multiple time in a frame
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
      
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        modifiers.SourceDamage *= 0.67f;
    }

    public override void AI()
    {
        float length = Projectile.velocity.Length();
        float targetAngle = Projectile.AngleTo(Projectile.Center);
        Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(1.5f)).ToRotationVector2() * length;
        Projectile.rotation = Projectile.velocity.ToRotation();
        if (Math.Abs(Projectile.velocity.X) <= 12f && Math.Abs(Projectile.velocity.Y) <= 12f)
        {
            Projectile.velocity *= 1.11f;
        }
       
        Projectile.rotation += 0.175f;
        if (Projectile.alpha < 182)
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

                Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.2f, Projectile.width - 1, Projectile.height - 1, DustID.Torch, 0f, 0f, 100, default, 1.75f);
                fireDust.fadeIn = 0.3f + Main.rand.Next(5) * 0.2f;
                fireDust.noGravity = true;
                fireDust.velocity *= 1.66f;
            }
        }
    }
    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 5; i++)
        {
            float rotation = MathHelper.ToRadians(i * 72f);
            Vector2 velocity = Projectile.velocity.RotatedBy(rotation);
            Vector2 position = Projectile.Center;

            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                position,
                velocity,
                ProjectileType<FireJetRecurse>(),
                Projectile.damage,
                Projectile.knockBack,
                Projectile.owner
            );
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Projectile.damage = (int)(Projectile.damage * 0.8f);

        target.AddBuff(BuffID.OnFire, 240);

        target.AddBuff(BuffID.OnFire3, 240);
    }
}