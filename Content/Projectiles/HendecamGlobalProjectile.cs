using HendecamMod.Common.Global;
using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Items.Weapons.Magic;
using HendecamMod.Content.Projectiles.Items;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace HendecamMod.Content.Projectiles
{
    public class HendecamGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public bool duplicated = false;
        public bool ContentXenon = false;
        public bool firstFrame = true;
        public bool secondFrame = false;

        public enum ProjectileItemSource : short
        {
            None,
            GoldenAK,
            CompoundBow,
            BeeSniper,
            Hitscan,
            MechGun,
            RedneckGun,
            VP70,
            VP70Setup,
            Blackshard,
            DeliriantDagger,
            VerdantClaymore,
            VerdantClaymoreSetup,
            PyrrhicClaymore,
            PyrrhicClaymoreSetup
        }

        public ProjectileItemSource ProjectileSource = ProjectileItemSource.None;

        private static readonly SoundStyle CritSound = new($"{nameof(HendecamMod)}/Assets/Sounds/Crit")
        {
            Volume = 2.5f,
            Pitch = 0f,
            MaxInstances = 100
        };

        private static readonly SoundStyle ClientCritSound = new($"{nameof(HendecamMod)}/Assets/Sounds/Crit")
        {
            Volume = 1.8f,
            Pitch = 0f,
            MaxInstances = 133
        };

        public override void SetDefaults(Projectile projectile)
        {
            switch (projectile.type)
            {
                case ProjectileID.ThunderStaffShot:
                    projectile.extraUpdates += 1;
                    break;
            }
        }

        public override void AI(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];

            switch (ProjectileSource)
            {
                case ProjectileItemSource.GoldenAK:
                    projectile.scale = 2.66f;
                    break;

                case ProjectileItemSource.CompoundBow:
                    projectile.extraUpdates = 1;
                    break;

                case ProjectileItemSource.BeeSniper:
                    projectile.extraUpdates = 3;
                    break;

                case ProjectileItemSource.Hitscan:
                    if (firstFrame)
                    {
                        projectile.extraUpdates += 3;
                        secondFrame = true;
                    }
                    if (secondFrame)
                    {
                        projectile.extraUpdates *= 5;
                        secondFrame = false;
                    }
                    break;

                case ProjectileItemSource.MechGun:
                    projectile.extraUpdates = 5;
                    var laserPlayer = player.GetModPlayer<LaserCD>();
                    if (firstFrame && laserPlayer.LaserTimer > 0)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Vector2 velocity = projectile.velocity.RotatedByRandom(MathHelper.ToRadians(12f));
                            Vector2 spawnOffset = projectile.Center - new Vector2(Main.rand.NextFloat(-36, 36), Main.rand.NextFloat(-36, 36));
                            Projectile.NewProjectile(
                                projectile.GetSource_FromThis(),
                                spawnOffset,
                                velocity,
                                ProjectileType<ChargeLaser>(),
                                (int)(projectile.damage * 0.3f),
                                projectile.knockBack,
                                projectile.owner
                            );
                        }
                    }
                    break;

                case ProjectileItemSource.VP70:
                    projectile.extraUpdates = 4;
                    break;

                case ProjectileItemSource.VP70Setup:
                    projectile.extraUpdates = 3;
                    break;
            }

            if (firstFrame && player.GetModPlayer<Sharpened>().Sharp && projectile.penetrate >= 1)
            {
                if (!projectile.usesLocalNPCImmunity || !projectile.usesIDStaticNPCImmunity)
                {
                    projectile.usesLocalNPCImmunity = true;
                    projectile.localNPCHitCooldown = 30;
                    projectile.usesOwnerMeleeHitCD = false;
                    projectile.usesIDStaticNPCImmunity = false;
                }
                projectile.penetrate += 2;
            }

            if (firstFrame)
            {
                firstFrame = false;
            }

            // 3. Experimental Projectile Duplication Config
            var config = GetInstance<HendecamExperimentalConfig>();
            if (config.Enabled && !duplicated)
            {
                duplicated = true;
                int count = Math.Clamp(config.ProjectileCount, 2, 20);
                float angleStep = MathHelper.TwoPi / count;

                for (int i = 0; i < count; i++)
                {
                    float angle = i * angleStep;
                    Vector2 newVelocity = projectile.velocity.RotatedBy(angle);
                    Vector2 spawnPos = projectile.Center;

                    int newProjIndex = Projectile.NewProjectile(
                        projectile.GetSource_FromThis(),
                        spawnPos,
                        newVelocity,
                        projectile.type,
                        projectile.damage,
                        projectile.knockBack,
                        projectile.owner
                    );

                    if (newProjIndex >= 0 && newProjIndex < Main.maxProjectiles)
                    {
                        Projectile newProj = Main.projectile[newProjIndex];
                        newProj.GetGlobalProjectile<HendecamGlobalProjectile>().duplicated = true;
                    }
                }
            }
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (projectile.type == ProjectileID.HolyArrow && target.type == NPCID.RedDevil)
            {
                modifiers.SourceDamage *= 777f;
            }
            
            switch (ProjectileSource)
            {
                case ProjectileItemSource.RedneckGun:
                    if (target.HasBuff(BuffType<RedneckTag>()))
                        modifiers.SourceDamage *= 1.5f;
                    break;

                case ProjectileItemSource.VP70:
                    if (target.HasBuff(BuffType<VpTag>()))
                        modifiers.SourceDamage *= 1.65f;
                    break;

                case ProjectileItemSource.VerdantClaymore:
                    if (target.HasBuff(BuffType<VerdantTag>()))
                        modifiers.SourceDamage *= 1.85f;
                    break;

                case ProjectileItemSource.PyrrhicClaymore:
                    if (target.HasBuff(BuffType<PyrrhicTag>()))
                        modifiers.SourceDamage *= 2.25f;
                    break;
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[projectile.owner];

            switch (ProjectileSource)
            {
                case ProjectileItemSource.Hitscan:
                    SoundEngine.PlaySound(CritSound);
                    break;

                case ProjectileItemSource.RedneckGun:
                    if (target.HasBuff(BuffType<RedneckTag>()))
                    {
                        ParticleOrchestrator.RequestParticleSpawn(
                            clientOnly: false,
                            ParticleOrchestraType.TrueNightsEdge,
                            new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                            projectile.owner
                        );
                        SoundEngine.PlaySound(CritSound);
                    }
                    break;

                case ProjectileItemSource.VP70:
                    if (target.HasBuff(BuffType<VpTag>()))
                    {
                        ParticleOrchestrator.RequestParticleSpawn(
                            clientOnly: false,
                            ParticleOrchestraType.Excalibur,
                            new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                            projectile.owner
                        );
                        SoundEngine.PlaySound(CritSound);
                    }
                    break;

                case ProjectileItemSource.VP70Setup:
                    target.AddBuff(BuffType<VpTag>(), 95);
                    break;

                case ProjectileItemSource.Blackshard:
                    target.AddBuff(BuffType<BlackshardDebuff>(), 196);
                    break;

                case ProjectileItemSource.DeliriantDagger:
                    target.AddBuff(BuffType<DeliriantTag>(), 155);
                    break;

                case ProjectileItemSource.VerdantClaymoreSetup:
                    target.AddBuff(BuffType<VerdantTag>(), 125);
                    break;

                case ProjectileItemSource.VerdantClaymore:
                    if (target.HasBuff(BuffType<VerdantTag>()))
                    {
                        ParticleOrchestrator.RequestParticleSpawn(
                            clientOnly: false,
                            ParticleOrchestraType.TrueNightsEdge,
                            new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                            projectile.owner
                        );
                        player.AddBuff(BuffType<JungleHealing>(), 100);
                        SoundEngine.PlaySound(CritSound);
                    }
                    break;

                case ProjectileItemSource.PyrrhicClaymoreSetup:
                    target.AddBuff(BuffType<PyrrhicTag>(), 175);
                    break;

                case ProjectileItemSource.PyrrhicClaymore:
                    if (target.HasBuff(BuffType<PyrrhicTag>()))
                    {
                        ParticleOrchestrator.RequestParticleSpawn(
                            clientOnly: false,
                            ParticleOrchestraType.TrueExcalibur,
                            new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                            projectile.owner
                        );
                        player.AddBuff(BuffType<PyrrhicDefense>(), 120);
                        SoundEngine.PlaySound(CritSound);
                    }
                    break;
            }

            if (ContentXenon)
            {
                Vector2 velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(0));
                Projectile.NewProjectile(
                    projectile.GetSource_FromThis(),
                    target.Center,
                    velocity,
                    ProjectileType<XenonX>(),
                    (int)(projectile.damage * 2f),
                    projectile.knockBack,
                    projectile.owner
                );
                SoundEngine.PlaySound(CritSound);
            }

            if (GetInstance<HendecamClientConfig>().EverythingMakesCritNoise)
            {
                SoundEngine.PlaySound(ClientCritSound);
            }
        }

        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            if (GetInstance<HendecamExperimentalConfig>().Bouncy)
            {
                if (projectile.penetrate >= 2)
                {
                    projectile.penetrate--;
                }

                if (projectile.penetrate <= 0)
                {
                    projectile.Kill();
                }
                else
                {
                    Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);

                    if (Math.Abs(projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                    {
                        projectile.velocity.X = -oldVelocity.X;
                    }

                    if (Math.Abs(projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                    {
                        projectile.velocity.Y = -oldVelocity.Y;
                    }
                }
                return false;
            }

            return true;
        }
        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
        {
            if (projectile.type == ProjectileID.UnholyTridentHostile)
            {
                modifiers.SourceDamage *= 0.67f;
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            if (projectile.type == ProjectileID.DeerclopsIceSpike)
            {
                target.AddBuff(BuffID.Frostburn, 150);
            }
            if (projectile.type == ProjectileID.UnholyTridentHostile)
            {
                target.AddBuff(BuffID.ShadowFlame, 360);
            }
        }
        public override Color? GetAlpha(Projectile projectile, Color lightColor)
        {
            if (ProjectileSource == ProjectileItemSource.MechGun)
                return Color.Red;

            return null;
        }
    }
}