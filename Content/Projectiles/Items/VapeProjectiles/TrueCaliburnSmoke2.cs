using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Global;
using Terraria;
using Terraria.DataStructures;
using static HendecamMod.Content.Items.Accessories.IronLung;
using static HendecamMod.Content.Items.Accessories.VapeDyes.Red40VapeDye;

namespace HendecamMod.Content.Projectiles.Items.VapeProjectiles;

public class TrueCaliburnSmoke2 : ModProjectile
{
    public override void SetStaticDefaults()
    {
       
    }

    public override void SetDefaults()
    {
        Projectile.width = 25; // The width of projectile hitbox
        Projectile.height = 25; // The height of projectile hitbox
        Projectile.usesLocalNPCImmunity = true;
        Projectile.penetrate = 4;
        Projectile.localNPCHitCooldown = 17;
        Projectile.tileCollide = false;
        Projectile.friendly = true;
        Projectile.DamageType = GetInstance<StupidDamage>();
        Projectile.timeLeft = 85;
        Projectile.GetGlobalProjectile<VapeMark>().VapeProj = true;
        Projectile.tileCollide = false;

       
    }
    public override void OnSpawn(IEntitySource source)
    {
        var vapeMark = Projectile.GetGlobalProjectile<VapeMark>();
        vapeMark.VapeProj = true;
        vapeMark.DustScale = 3.4f;

        if (Main.rand.NextBool(2))
        {
            Projectile.velocity.X = (Main.rand.NextFloat(0.1f, 0.6f));
        }
        else
        {
            Projectile.velocity.X = (Main.rand.NextFloat(-0.6f, -0.1f));
        }
       


    }
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        if (player.GetModPlayer<IronLungPlayer>().IronLungs == true)
        {
            Projectile.extraUpdates = 1;
        }

        // Apply velocity based on spawn direction (check ai[0])
        if (Projectile.ai[0] == 1f) // Spawned above player
        {
            Projectile.velocity.Y = 12.5f; // Moving downward
        }
        else // Spawned below player
        {
            Projectile.velocity.Y = -12.5f; // Moving upward
        }

        // The projectile is rotated to face the direction of travel
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

        for (int i = 0; i < 2; i++)
        {
            float posOffsetX = 0f;
            float posOffsetY = 0f;
            if (i == 1)
            {
                posOffsetX = Projectile.velocity.X * 2.5f;
                posOffsetY = Projectile.velocity.Y * 2.5f;
            }

            Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 10, Projectile.height - 10, DustID.HallowedWeapons, 0f, 0f, 100, default, 1.5f);
            fireDust.fadeIn = 0.1f + Main.rand.Next(2) * 0.1f;
            fireDust.noGravity = true;
            fireDust.velocity *= 1.25f;

            Dust fire2Dust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 10, Projectile.height - 10, DustID.Terragrim, 0f, 0f, 100, Color.LightSkyBlue, 2.25f);
            fire2Dust.fadeIn = 0.1f + Main.rand.Next(2) * 0.1f;
            fire2Dust.noGravity = true;
            fire2Dust.velocity *= 1.25f;

            Dust fire23Dust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 10, Projectile.height - 10, DustID.Gastropod, 0f, 0f, 100, Color.Pink, 0.9f);
            fire23Dust.fadeIn = 0.1f + Main.rand.Next(2) * 0.1f;
            fire23Dust.noGravity = true;
            fire23Dust.velocity *= 1.25f;
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
       

        Projectile.damage = (int)(Projectile.damage * 0.925f);
    }

   
}