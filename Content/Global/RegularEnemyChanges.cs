using HendecamMod.Content.NPCs.Bosses;
using HendecamMod.Content.Projectiles.Enemies;
using Terraria.DataStructures;

namespace HendecamMod.Content.Global;

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
                ProjectileType<HarpyFeatherClone>(), (int)(projectile.damage * 0.75f), projectile.knockBack, projectile.owner);
            Vector2 velocity2 = projectile.velocity.RotatedBy(MathHelper.ToRadians(-20));
            Vector2 Peanits2 = projectile.Center;
            Projectile.NewProjectile(projectile.GetSource_FromThis(), Peanits2, velocity2,
            ProjectileType<HarpyFeatherClone>(), (int)(projectile.damage * 0.75f), projectile.knockBack, projectile.owner);

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
                    ProjectileType<FrostBeamClone>(),
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
