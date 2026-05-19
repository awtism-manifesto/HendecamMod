using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace HendecamMod.Content.Projectiles.Friendly.Ranged;

public class MantleEnergyArrow : ModProjectile
{
    

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6; // The length of old position to be recorded
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
    }

    public override void SetDefaults()
    {
        Projectile.width = 22; // The width of projectile hitbox
        Projectile.height = 22; // The height of projectile hitbox

        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
        Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 500;

        Projectile.light = 0.1f;
        Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = true; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 1; 
        Projectile.usesLocalNPCImmunity = true;
        AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        Projectile.aiStyle = 1;
        Projectile.alpha = 255;
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

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {

        modifiers.SourceDamage = modifiers.SourceDamage + (target.defense * 0.025f);
    }
    public override void AI()
    {
      

        Lighting.AddLight(Projectile.Center, 0.7f, 0.25f, 0.25f);
       
    }
}