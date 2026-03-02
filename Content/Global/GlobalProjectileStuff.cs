using HendecamMod.Content.Buffs;
using HendecamMod.Content.Projectiles.Enemies;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;

namespace HendecamMod.Content.Global;

public class CrystalBeef : GlobalProjectile
{
    public bool fromGoldenAK;

    public override bool InstancePerEntity => true;

    public override void AI(Projectile projectile)
    {
        if (fromGoldenAK)
        {
            projectile.scale = 1.66f;
        }
    }
}


public class FastBow : GlobalProjectile
{
    public bool fromCompoundBow;

    public override bool InstancePerEntity => true;

    public override void AI(Projectile projectile)
    {
        if (fromCompoundBow)
        {
            projectile.extraUpdates = 1;
        }
    }
}

public class FastBees : GlobalProjectile
{
    public bool fromBeeSniper;

    public override bool InstancePerEntity => true;

    public override void AI(Projectile projectile)
    {
        if (fromBeeSniper)
        {
            projectile.extraUpdates = 3;
        }
    }
}

public class FastLaserSwords : GlobalProjectile
{
    public bool fromMechGun;

    public override bool InstancePerEntity => true;

    public override void AI(Projectile projectile)
    {
        if (fromMechGun)
        {
            projectile.extraUpdates = 2;
        }
    }
}

public class RedneckCombo : GlobalProjectile
{
    public bool fromRedneckGun;

    public override bool InstancePerEntity => true;

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!fromRedneckGun)
            return; // Don't run if this isn't a right-click combo shot

        if (target.HasBuff(ModContent.BuffType<RedneckTag>()))
        {
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.TrueNightsEdge,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                projectile.owner);
            SoundEngine.PlaySound(SoundID.Item37, projectile.position);
        }
    }

    public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
    {
        if (fromRedneckGun && target.HasBuff(ModContent.BuffType<RedneckTag>()))
        {
            modifiers.SourceDamage *= 1.5f;
        }
    }
}

public class VPCombo : GlobalProjectile
{
    public bool fromVP70;

    public override bool InstancePerEntity => true;

    public override void AI(Projectile projectile)
    {
        if (fromVP70)
        {
            projectile.extraUpdates = 4;
        }
    }

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!fromVP70)
            return; // Don't run if this isn't a right-click combo shot

        if (target.HasBuff(ModContent.BuffType<VpTag>()))
        {
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Excalibur,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                projectile.owner);

            SoundEngine.PlaySound(SoundID.Item37, projectile.position);
        }
    }

    public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
    {
        if (fromVP70 && target.HasBuff(ModContent.BuffType<VpTag>()))
        {
            modifiers.SourceDamage *= 1.65f;
        }
    }
}

public class VPComboSetup : GlobalProjectile
{
    public bool fromtheVP70;

    public override bool InstancePerEntity => true;

    public override void AI(Projectile projectile)
    {
        if (fromtheVP70)
        {
            projectile.extraUpdates = 3;
        }
    }

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!fromtheVP70)
            return;

        target.AddBuff(ModContent.BuffType<VpTag>(), 95);
    }
}

public class KnightComboSetup : GlobalProjectile
{
    public bool fromtheBlackshard;

    public override bool InstancePerEntity => true;

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!fromtheBlackshard)
            return;

        target.AddBuff(ModContent.BuffType<BlackshardDebuff>(), 196);
    }
}

public class DeliriantComboSetup : GlobalProjectile
{
    public bool fromtheDeliriantDagger;

    public override bool InstancePerEntity => true;

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!fromtheDeliriantDagger)
            return;
        target.AddBuff(ModContent.BuffType<DeliriantTag>(), 155);
    }
}

public class VerdantComboSetup : GlobalProjectile
{
    public bool fromtheVerdantClaymore;

    public override bool InstancePerEntity => true;

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!fromtheVerdantClaymore)
            return; // Only apply buff if this is a left-click setup shot

        target.AddBuff(ModContent.BuffType<VerdantTag>(), 125);
    }
}

public class VerdantCombo : GlobalProjectile
{
    public bool fromVerdantClaymore;

    public override bool InstancePerEntity => true;

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!fromVerdantClaymore)
            return; // Don't run if this isn't a right-click combo shot

        if (target.HasBuff(ModContent.BuffType<VerdantTag>()))
        {
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.TrueNightsEdge,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                projectile.owner);

            // Apply a buff to the player
            Player player = Main.player[projectile.owner];
            player.AddBuff(ModContent.BuffType<JungleHealing>(), 100); // 300 = 5 seconds (60 ticks per second)
        }
    }

    public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
    {
        if (fromVerdantClaymore && target.HasBuff(ModContent.BuffType<VerdantTag>()))
        {
            modifiers.SourceDamage *= 1.85f;
        }
    }
}

public class MagnetSphereActuallyGoodNow : GlobalProjectile //shoutout to my clanka deepseek for this one
{
    public override bool InstancePerEntity => true;

    private const int MAX_SPHERES = 8;
    private bool isModified = false;

    // Static counter to track total spheres globally
    private static int totalSpheres = 0;

    public override void SetDefaults(Projectile projectile)
    {
        if (projectile.type == ProjectileID.MagnetSphereBall)
        {
            projectile.timeLeft = 3600;
            projectile.penetrate = -1;
            projectile.maxPenetrate = -1;
            projectile.alpha = 0;
            isModified = true;
        }
    }
    private int timeLeft2 = 3600; 

    public override void OnSpawn(Projectile projectile, IEntitySource source)
    {
        if (projectile.type == ProjectileID.MagnetSphereBall)
        {
            totalSpheres++;

            // If we have too many spheres, immediately despawn the oldest
            if (totalSpheres > MAX_SPHERES)
            {
                // Find and kill the oldest sphere
                int oldestTime = int.MaxValue;
                int oldestIndex = -1;

                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p != null && p.active && p.type == ProjectileID.MagnetSphereBall && p.whoAmI != projectile.whoAmI)
                    {
                        if (p.timeLeft < oldestTime)
                        {
                            oldestTime = p.timeLeft;
                            oldestIndex = i;
                        }
                    }
                }

                if (oldestIndex != -1)
                {
                    Main.projectile[oldestIndex].Kill();
                    totalSpheres--;
                }
            }
        }
    }

    public override void AI(Projectile projectile)
    {
        if (projectile.type == ProjectileID.MagnetSphereBall)
        {
            timeLeft2--;
            if (timeLeft2 < 3000)
            {
                projectile.Kill();
            }

            // Always keep the projectile alive if we're under the cap
            if (projectile.timeLeft < 60)
            {
                // Verify we're still under the cap
                int currentCount = 0;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile other = Main.projectile[i];
                    if (other != null && other.active && other.type == ProjectileID.MagnetSphereBall)
                    {
                        currentCount++;
                    }
                }

                totalSpheres = currentCount;

                if (currentCount <= MAX_SPHERES)
                {
                    projectile.timeLeft = 3600;
                    projectile.alpha = 0;
                }
            }
        }
    }

    public override void OnKill(Projectile projectile, int timeLeft)
    {
        if (projectile.type == ProjectileID.MagnetSphereBall)
        {
            totalSpheres--;
            if (totalSpheres < 0) totalSpheres = 0;
        }
    }

    public override bool PreKill(Projectile projectile, int timeLeft)
    {
        if (projectile.type == ProjectileID.MagnetSphereBall)
        {
            // Only prevent vanilla despawns, let our cap system handle excess kills
            if (totalSpheres <= MAX_SPHERES && projectile.timeLeft < 60)
            {
                projectile.timeLeft = 3600;
                projectile.alpha = 0;
                projectile.active = true;
                return false;
            }
        }

        return base.PreKill(projectile, timeLeft);
    }
}