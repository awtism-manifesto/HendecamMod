using HendecamMod.Content.Buffs;
using HendecamMod.Content.Projectiles.Enemies;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

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

public class FuckRedDevils : GlobalProjectile
{
    public bool fromRedDevil;

    public override bool InstancePerEntity => true;

    public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
    {
        if (projectile.type == ProjectileID.UnholyTridentHostile)
        {
            modifiers.SourceDamage *= 0.67f;
        }
    }

    public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
    {
        if (projectile.type == ProjectileID.UnholyTridentHostile)
        {
            target.AddBuff(BuffID.ShadowFlame, 360);
        }
    }
}

public class HarpyBuff : GlobalProjectile
{
    public bool fromHarpy;
    private int nextSpawnTick;

    private int tickCounter;

    public override bool InstancePerEntity => true;

    public override void AI(Projectile projectile)
    {
        if (nextSpawnTick == 0)
        {
            tickCounter++;
            nextSpawnTick = 1;
        }

        if (tickCounter >= nextSpawnTick && projectile.type == ProjectileID.HarpyFeather && Main.expertMode)
        {
            Vector2 velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(20));
            Vector2 Peanits = projectile.Center;
            Projectile.NewProjectile(projectile.GetSource_FromThis(), Peanits, velocity,
                ModContent.ProjectileType<HarpyFeatherClone>(), (int)(projectile.damage * 0.75f), projectile.knockBack, projectile.owner);
            Vector2 velocity2 = projectile.velocity.RotatedBy(MathHelper.ToRadians(-20));
            Vector2 Peanits2 = projectile.Center;
            Projectile.NewProjectile(projectile.GetSource_FromThis(), Peanits2, velocity2,
                ModContent.ProjectileType<HarpyFeatherClone>(), (int)(projectile.damage * 0.75f), projectile.knockBack, projectile.owner);

            tickCounter = 0;
            nextSpawnTick = 99999;
        }
    }
}

public class FrostCoreTack : GlobalProjectile
{
    public bool fromIceGolem;
    private int nextSpawnTick;

    private int tickCounter;

    public override bool InstancePerEntity => true;

    public override void AI(Projectile projectile)
    {
        if (nextSpawnTick == 0)
        {
            tickCounter++;
            nextSpawnTick = 1;
        }

        if (tickCounter >= nextSpawnTick && projectile.type == ProjectileID.FrostBeam && Main.masterMode)
        {
            for (int i = 0; i < 8; i++)
            {
                float rotation = MathHelper.ToRadians(i * 45f);
                Vector2 velocity = projectile.velocity.RotatedBy(rotation);
                Vector2 position = projectile.Center;

                Projectile.NewProjectile(
                    projectile.GetSource_FromThis(),
                    position,
                    velocity,
                    ModContent.ProjectileType<FrostBeamClone>(),
                    projectile.damage,
                    projectile.knockBack,
                    projectile.owner
                );
            }

            tickCounter = 0;
            nextSpawnTick = 99999;
        }
    }
}

public class DeerBurn : GlobalProjectile
{
    public bool DeerclopsAttack;

    public override bool InstancePerEntity => true;

    public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
    {
        if (projectile.type == ProjectileID.DeerclopsIceSpike)
        {
            target.AddBuff(BuffID.Frostburn, 150);
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