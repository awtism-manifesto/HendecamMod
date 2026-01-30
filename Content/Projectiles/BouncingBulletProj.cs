using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace HendecamMod.Content.Projectiles;

public class BouncingBulletProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20; // The length of old position to be recorded
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
    }

    public override void SetDefaults()
    {
        Projectile.width = 6;
        Projectile.height = 6;
        Projectile.friendly = true;
        Projectile.penetrate = 1; // Infinite penetration so that the blast can hit all enemies within its radius.
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.light = 0.25f; // How much light emit around the projectile
        Projectile.usesLocalNPCImmunity = true;
        Projectile.extraUpdates = 2;
        Projectile.timeLeft = 330;
        Projectile.usesIDStaticNPCImmunity = true;
        Projectile.idStaticNPCHitCooldown = 10;
        Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
        AIType = ProjectileID.CrystalBullet; // Act exactly like default Bullet
        // Rockets use explosive AI, ProjAIStyleID.Explosive (16). You could use that instead here with the correct AIType.
        // But, using our own AI allows us to customize things like the dusts that the rocket creates.
        // Projectile.aiStyle = ProjAIStyleID.Explosive;
        // AIType = ProjectileID.RocketI;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        // If the projectile hits the left or right side of the tile, reverse the X velocity
        if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
        {
            Projectile.velocity.X = -oldVelocity.X;
        }

        // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
        if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
        {
            Projectile.velocity.Y = -oldVelocity.Y;
        }

        return false;
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

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.immune[Projectile.owner] = 2;
        Vector2 velocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(360));
        Vector2 Peanits = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits, velocity,
            ModContent.ProjectileType<BouncingBulletProj>(), (int)(Projectile.damage * 0.95f), Projectile.knockBack, Projectile.owner);
    }

    public override void AI()
    {
    }

    // When the rocket hits a tile, NPC, or player, get ready to explode.
}