using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace HendecamMod.Content.Projectiles.Friendly.Magic;

// This example is similar to the Wooden Arrow projectile
public class ZygardeArrow : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 28; // The length of old position to be recorded
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
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
   
    public override void SetDefaults()
    {
        Projectile.width = 15; // The width of projectile hitbox
        Projectile.height = 15; // The height of projectile hitbox
       
        Projectile.penetrate = 3;
        Projectile.extraUpdates = 1;
        Projectile.arrow = true;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.timeLeft = 1200;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 20;
    }

    public override void AI()
    {
        // The code below was adapted from the ProjAIStyleID.Arrow behavior. Rather than copy an existing aiStyle using Projectile.aiStyle and AIType,
        // like some examples do, this example has custom AI code that is better suited for modifying directly.
        // See https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#what-is-ai for more information on custom projectile AI.

        // Apply gravity after a quarter of a second
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 22f)
        {
            Projectile.ai[0] = 22f;
            Projectile.velocity.Y += 0.16f;
        }

        // The projectile is rotated to face the direction of travel
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        Lighting.AddLight(Projectile.Center, 0.3f, 0.85f, 0.3f);
        // Cap downward velocity
        if (Projectile.velocity.Y > 18f)
        {
            Projectile.velocity.Y = 19f;
        }

       
    }

   

   
}