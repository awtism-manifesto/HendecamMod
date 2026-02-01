using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;
using Terraria.Audio;

namespace HendecamMod.Content.Projectiles;

public class AstaBoomDelayed : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.PlayerHurtDamageIgnoresDifficultyScaling[Type] = true; // Damage dealt to players does not scale with difficulty in vanilla.

        // This set handles some things for us already:
        // Sets the timeLeft to 3 and the projectile direction when colliding with an NPC or player in PVP (so the explosive can detonate).
        // Explosives also bounce off the top of Shimmer, detonate with no blast damage when touching the bottom or sides of Shimmer, and damage other players in For the Worthy worlds.
        ProjectileID.Sets.Explosive[Type] = true;
        ProjectileID.Sets.RocketsSkipDamageForPlayers[Type] = true;

        // This set makes it so the rocket doesn't deal damage to players. Only used for vanilla rockets.
        // Simply remove the Projectile.HurtPlayer() part to stop the projectile from damaging its user.
        // ProjectileID.Sets.RocketsSkipDamageForPlayers[Type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.width = 30;
        Projectile.height = 30;
        Projectile.friendly = true;
        Projectile.penetrate = -1; // Infinite penetration so that the blast can hit all enemies within its radius.
        Projectile.DamageType = DamageClass.Magic;
        Projectile.light = 0f; // How much light emit around the projectile
       
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.timeLeft = 28;
        Projectile.extraUpdates = 3;
        Projectile.tileCollide = false;
        // Rockets use explosive AI, ProjAIStyleID.Explosive (16). You could use that instead here with the correct AIType.
        // But, using our own AI allows us to customize things like the dusts that the rocket creates.
        // Projectile.aiStyle = ProjAIStyleID.Explosive;
        // AIType = ProjectileID.RocketI;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.immune[Projectile.owner] = 2;
        target.AddBuff(ModContent.BuffType<RadPoisoning3>(), 255);
    }

    public override void AI()
    {
        // Apply gravity after a quarter of a second
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 7f)
        {
            Projectile.ai[0] = 29f;
            Projectile.velocity.Y += 0.5f;
        }

        // The projectile is rotated to face the direction of travel
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

        // Cap downward velocity
        if (Projectile.velocity.Y > 32f)
        {
            Projectile.velocity.Y = 32f;
        }

        // If timeLeft is <= 3, then explode the rocket.
        if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
        {
            Projectile.PrepareBombToBlow();
        }
        else
        {
            // Spawn dusts if the rocket is moving at or greater than half of its max speed.
            if (Math.Abs(Projectile.velocity.X) >= 8f || Math.Abs(Projectile.velocity.Y) >= 8f)
            {
                for (int i = 0; i < 2; i++)
                {
                    float posOffsetX = 0f;
                    float posOffsetY = 0f;
                    if (i == 1)
                    {
                        posOffsetX = Projectile.velocity.X * 0.5f;
                        posOffsetY = Projectile.velocity.Y * 0.5f;
                    }

                    // Used by the liquid rockets which leave trails of their liquid instead of fire.
                    // if (fireDust.type == Dust.dustWater()) {
                    //	fireDust.scale *= 0.65f;
                    //	fireDust.velocity += Projectile.velocity * 0.1f;
                    // }
                }
            }

            // Increase the speed of the rocket if it is moving less than 1 block per second.
            // It is not recommended to increase the number past 16f to increase the speed of the rocket. It could start no clipping through blocks.
            // Instead, increase extraUpdates in SetDefaults() to make the rocket move faster.
            if (Math.Abs(Projectile.velocity.X) <= 15f && Math.Abs(Projectile.velocity.Y) <= 15f)
            {
                Projectile.velocity *= 1.1f;
            }
        }

        // Rotate the rocket in the direction that it is moving.
        if (Projectile.velocity != Vector2.Zero)
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + MathHelper.PiOver2;
        }
    }

    // When the rocket hits a tile, NPC, or player, get ready to explode.
   

    public override void PrepareBombToBlow()
    {
        Projectile.tileCollide = false; // This is important or the explosion will be in the wrong place if the rocket explodes on slopes.
        Projectile.alpha = 255; // Make the rocket invisible.

        // Resize the hitbox of the projectile for the blast "radius".
        // Rocket I: 128, Rocket III: 200, Mini Nuke Rocket: 250
        // Measurements are in pixels, so 128 / 16 = 8 tiles.
        Projectile.Resize(375, 375);
        // Set the knockback of the blast.
        // Rocket I: 8f, Rocket III: 10f, Mini Nuke Rocket: 12f
        Projectile.knockBack = 11f;
    }

    public override void OnKill(int timeLeft)
    {

        for (int i = 0; i < 8; i++)
        {
            float rotation = MathHelper.ToRadians(i * 45f);
            Vector2 velocity = Projectile.velocity.RotatedBy(rotation);
            Vector2 position = Projectile.Center;

            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                position,
                velocity,
                ModContent.ProjectileType<AstaBoomRecurse>(),
                (int)(Projectile.damage *0.33f),
                Projectile.knockBack,
                Projectile.owner
            );
        }


        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        // Resize the projectile again so the explosion dust and gore spawn from the middle.
        // Rocket I: 22, Rocket III: 80, Mini Nuke Rocket: 50
        Projectile.Resize(310, 310);

        // Spawn a bunch of fire dusts.
        for (int j = 0; j < 30; j++)
        {
            Dust fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AstatineDust>(), 0f, 0f, 100, default, 2.75f);
            fireDust.noGravity = true;
            fireDust.velocity *= 7f;
            fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AstatineDust>(), 0f, 0f, 100, default, 1.2f);
            fireDust.velocity *= 6f;
            fireDust.noGravity = true;
        }

       
    }
}