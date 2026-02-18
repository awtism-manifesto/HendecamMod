using HendecamMod.Content.NPCs.Bosses;
using HendecamMod.Content.Projectiles.Enemies;
using Terraria.DataStructures;

namespace HendecamMod.Content.Global;


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
public class FuckRedDevilsHarder : GlobalProjectile
    {
    public bool frommRedDevil;

    public override bool InstancePerEntity => true;

    public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            {
            if (projectile.type == ProjectileID.HolyArrow && target.type == NPCID.RedDevil)
                {
                modifiers.SourceDamage *= 777f;
                }
            }
        }
    }
public class ProjKill : GlobalProjectile
    {
    public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
        if (NPC.AnyNPCs(ModContent.NPCType<EyesOfGabriel>()) && projectile.type == ProjectileID.CursedFlameHostile)
            {
            projectile.Kill();
            }
        if (NPC.AnyNPCs(ModContent.NPCType<EyesOfGabriel>()) && projectile.type == ProjectileID.EyeFire)
            {
            projectile.Kill();
            }
        if (NPC.AnyNPCs(ModContent.NPCType<EyesOfRaphael>()) && projectile.type == ProjectileID.EyeLaser)
            {
            projectile.Kill();
            }
        if (NPC.AnyNPCs(ModContent.NPCType<EyesOfRaphael>()) && projectile.type == ProjectileID.DeathLaser)
            {
            projectile.Kill();
            }
        }
    }
public class DustKill : ModSystem
    {
    public override void PostUpdateDusts()
        {
        for (int i = 0; i < Main.maxDust; i++)
            {
            Dust d = Main.dust[i];
            if (d.active && d.type == 75 && NPC.AnyNPCs(ModContent.NPCType<EyesOfGabriel>()))
                {
                d.active = false;
                }
            if (d.active && d.type == 5 && NPC.AnyNPCs(ModContent.NPCType<EyesOfGabriel>()))
                {
                d.active = false;
                }
            if (d.active && d.type == 5 && NPC.AnyNPCs(ModContent.NPCType<EyesOfRaphael>()))
                {
                d.active = false;
                }
            if (d.active && d.type == 5 && NPC.AnyNPCs(ModContent.NPCType<EyesOfMichael>()))
                {
                d.active = false;
                }
            }
        }
    }
public class GoreKill : ModSystem
    {
    public override void PostUpdateGores()
        {
        for (int i = 0; i < Main.maxGore; i++)
            {
            Gore g = Main.gore[i];
            if (g.active)
                {
                if (g.type == 2 && NPC.AnyNPCs(ModContent.NPCType<EyesOfGabriel>()))
                    {
                    g.active = false;
                    }
                if (g.type == 2 && NPC.AnyNPCs(ModContent.NPCType<EyesOfRaphael>()))
                    {
                    g.active = false;
                    }
                if (g.type == 2 && NPC.AnyNPCs(ModContent.NPCType<EyesOfMichael>()))
                    {
                    g.active = false;
                    }
                if (g.type >= 7 && g.type <= 10 && NPC.AnyNPCs(ModContent.NPCType<EyesOfGabriel>()))
                    {
                    g.active = false;
                    }
                if (g.type >= 7 && g.type <= 10 && NPC.AnyNPCs(ModContent.NPCType<EyesOfRaphael>()))
                    {
                    g.active = false;
                    }
                if (g.type >= 7 && g.type <= 10 && NPC.AnyNPCs(ModContent.NPCType<EyesOfMichael>()))
                    {
                    g.active = false;
                    }
                if (g.type >= 143 && g.type <= 146 && NPC.AnyNPCs(ModContent.NPCType<EyesOfGabriel>()))
                    {
                    g.active = false;
                    }
                if (g.type >= 143 && g.type <= 146 && NPC.AnyNPCs(ModContent.NPCType<EyesOfRaphael>()))
                    {
                    g.active = false;
                    }
                if (g.type >= 143 && g.type <= 146 && NPC.AnyNPCs(ModContent.NPCType<EyesOfMichael>()))
                    {
                    g.active = false;
                    }
                }
            }
        }
    }
public class HarpyBuff : GlobalProjectile
    {
    public bool fromHarpy;

    public override bool InstancePerEntity => true;

    private int tickCounter = 0;
    private int nextSpawnTick = 0;
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

    public override bool InstancePerEntity => true;

    private int tickCounter = 0;
    private int nextSpawnTick = 0;
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

