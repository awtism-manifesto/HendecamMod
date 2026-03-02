using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Global;
using Terraria.DataStructures;
using static HendecamMod.Content.Items.Accessories.IronLung;
using static HendecamMod.Content.Items.Accessories.VapeDyes.Red40VapeDye;

namespace HendecamMod.Content.Projectiles.Items.VapeProjectiles;

public class TorchGodVapeSmoke : ModProjectile
{
    public override void SetStaticDefaults()
    {
       
    }

    public override void SetDefaults()
    {
        Projectile.width = 18; // The width of projectile hitbox
        Projectile.height = 18; // The height of projectile hitbox
        Projectile.usesLocalNPCImmunity = true;
        Projectile.penetrate = 3;
        Projectile.localNPCHitCooldown = 30;
        Projectile.alpha = 55;
        Projectile.friendly = true;
        Projectile.DamageType = ModContent.GetInstance<StupidDamage>();
        Projectile.timeLeft = 90;
        Projectile.GetGlobalProjectile<VapeMark>().VapeProj = true;
    }
    public override void OnSpawn(IEntitySource source)
    {
        var vapeMark = Projectile.GetGlobalProjectile<VapeMark>();
        vapeMark.VapeProj = true;
        vapeMark.DustScale = 1.67f;


    }
    public override void AI()
    {


        Player player = Main.player[Projectile.owner];
        if (player.GetModPlayer<IronLungPlayer>().IronLungs == true)
        {
            Projectile.extraUpdates = 1;
        }
        if (player.GetModPlayer<IronLungPlayer>().IronLungs == false)
        {
            // Apply gravity after a quarter of a second
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 8f)
            {
                Projectile.ai[0] = 8f;
                Projectile.velocity.Y -= 0.125f;
            }
        }

        // The projectile is rotated to face the direction of travel
        Projectile.rotation += 0.265f;

        if (Projectile.position == Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY))
        {
            Projectile.velocity = Vector2.Zero;
        }

        for (int i = 0; i < 2; i++)
        {
            float posOffsetX = 0f;
            float posOffsetY = 0f;
            if (i == 1)
            {
                posOffsetX = Projectile.velocity.X * 2.5f;
                posOffsetY = Projectile.velocity.Y * 2.5f;
            }

           
           

            Vector2 targetPos = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float length = Projectile.velocity.Length();
            float targetAngle = Projectile.AngleTo(targetPos);
            Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(5f)).ToRotationVector2() * length;
        }

    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.OnFire, 210);
        target.AddBuff(BuffID.Frostburn, 210);

        Projectile.damage = (int)(Projectile.damage * 0.67f);
    }

   
}