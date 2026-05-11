using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Global;
using Terraria.DataStructures;
using static HendecamMod.Content.Items.Accessories.IronLung;

namespace HendecamMod.Content.Projectiles.Items.VapeProjectiles;

public class TerraVapeSmoke2 : ModProjectile
{
    public override void SetStaticDefaults()
    {
       
    }

    public override void SetDefaults()
    {
        Projectile.width = 28; // The width of projectile hitbox
        Projectile.height = 28; // The height of projectile hitbox
        Projectile.usesLocalNPCImmunity = true;
        Projectile.penetrate = 8;
        Projectile.localNPCHitCooldown = 15;

        Projectile.friendly = true;
        Projectile.DamageType = GetInstance<StupidDamage>();
        Projectile.timeLeft = 67;
        Projectile.GetGlobalProjectile<VapeMark>().VapeProj = true;
        Projectile.extraUpdates = 1;
        Projectile.tileCollide = false;
       
    }

    public override void OnSpawn(IEntitySource source)
    {
        var vapeMark = Projectile.GetGlobalProjectile<VapeMark>();
        vapeMark.VapeProj = true;
        vapeMark.DustScale = 3.5f;

        

    }
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        if (player.GetModPlayer<IronLungPlayer>().IronLungs == true)
        {
            Projectile.extraUpdates = 1;
        }


        
        if (Projectile.ai[0] == 1f)
        {
            Projectile.velocity.Y = 14.5f;
            Projectile.velocity.X = 14.5f;  // Moving right
        }
        else if (Projectile.ai[0] == 2f)
        {
            Projectile.velocity.X = 14.5f;
            Projectile.velocity.Y = -14.5f;  // Moving down
        }
        else if (Projectile.ai[0] == 3f)
        {
            Projectile.velocity.X = -14.5f;
            Projectile.velocity.Y = -14.5f; // Moving up
        }
        else 
        {
            Projectile.velocity.X = -14.5f; // Moving left
            Projectile.velocity.Y = 14.5f;
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


            Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 10, Projectile.height - 10, DustID.TerraBlade, 0f, 0f, 100, default, 1.5f);
            fireDust.fadeIn = 0.1f + Main.rand.Next(2) * 0.1f;
            fireDust.noGravity = true;
            fireDust.velocity *= 1.25f;
            Dust fire2Dust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 10, Projectile.height - 10, DustID.ChlorophyteWeapon, 0f, 0f, 100, default, 1.5f);
            fire2Dust.fadeIn = 0.1f + Main.rand.Next(2) * 0.1f;
            fire2Dust.noGravity = true;
            fire2Dust.velocity *= 1.25f;
        }

    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
       



            Projectile.damage = (int)(Projectile.damage * 0.901f);
    }

   
}