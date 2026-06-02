using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HendecamMod.Common.Systems;  

namespace HendecamMod.Common.Global;

public class ProjectileRadifier : GlobalProjectile
{
   
    public bool duplicated = false;

    public override bool InstancePerEntity => true;

    public override void AI(Projectile projectile)
    {
        
        var config = ModContent.GetInstance<HendecamConfig>();
        if (!config.Enabled) return;

      
        if (duplicated) return;

      
        duplicated = true;

        int count = config.ProjectileCount;
        if (count < 2) count = 2;
        if (count > 20) count = 20;

      
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
                newProj.GetGlobalProjectile<ProjectileRadifier>().duplicated = true;
            }
        }
    }
}