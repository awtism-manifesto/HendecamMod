using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Global;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent;
using static HendecamMod.Content.Items.Accessories.IronLung;
using static HendecamMod.Content.Items.Accessories.VapeDyes.Red40VapeDye;

namespace HendecamMod.Content.Projectiles.Items.VapeProjectiles;

// This example is similar to the Wooden Arrow projectile
public class DragonVapeSmoke : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8; // The length of old position to be recorded
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
    }

    public override void SetDefaults()
    {
        Projectile.width = 17; // The width of projectile hitbox
        Projectile.height = 17; // The height of projectile hitbox
        Projectile.usesLocalNPCImmunity = true;
        Projectile.penetrate = 3;
        Projectile.localNPCHitCooldown = 20;
       
        Projectile.friendly = true;
        Projectile.DamageType = GetInstance<StupidDamage>();
        Projectile.timeLeft = 67;
        Projectile.GetGlobalProjectile<VapeMark>().VapeProj = true;
    }
    public override void OnSpawn(IEntitySource source)
    {
        var vapeMark = Projectile.GetGlobalProjectile<VapeMark>();
        vapeMark.VapeProj = true;
        vapeMark.DustScale = 3.06f;


    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = TextureAssets.Projectile[Type].Value;

        // Redraw the projectile with the color not influenced by light
        Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
        for (int k = 0; k < Projectile.oldPos.Length; k++)
        {
            Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
            Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None);
        }

        return true;
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
            if (Projectile.ai[0] >= 5f)
            {
                Projectile.ai[0] = 5f;
                Projectile.velocity.Y -= 0.052f;
            }
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

            Dust fire2Dust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 10, Projectile.height - 10, DustID.Fireworks, 0f, 0f, 100, Color.Orange, 1.25f);
            fire2Dust.fadeIn = 0.1f + Main.rand.Next(2) * 0.1f;
            fire2Dust.noGravity = true;
            fire2Dust.velocity *= 1.33f;

            Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 10, Projectile.height - 10, DustID.Torch, 0f, 0f, 100, default, 1.25f);
            fireDust.fadeIn = 0.1f + Main.rand.Next(2) * 0.1f;
            fireDust.noGravity = true;
            fireDust.velocity *= 1.33f;
        }

    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.OnFire, 300);
        target.AddBuff(BuffID.OnFire3, 150);

        Projectile.damage = (int)(Projectile.damage * 0.91f);
    }

   
}