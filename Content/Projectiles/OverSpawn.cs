using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace HendecamMod.Content.Projectiles;

public class OverSpawn : ModProjectile
{

    public override void SetStaticDefaults()
    {

        ProjectileID.Sets.SentryShot[Type] = true;
    }
    public override void SetDefaults()
    {
        Projectile.width = 1; // The width of projectile hitbox
        Projectile.height = 1; // The height of projectile hitbox
        Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Summon; // Is the projectile shoot by a ranged weapon?
        Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 1; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
        Projectile.alpha = 255;
        Projectile.light = 0f; // How much light emit around the projectile
        Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = true; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 0; // Set to above 0 if you want the projectile to update multiple time in a frame

        AIType = ProjectileID.Bullet; // Act exactly like default Bullet
    }

    public override void OnKill(int timeLeft)
    {

        for (int i = 0; i < 14; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke);
            dust.noGravity = true;
            dust.velocity *= 14.5f;
            dust.scale *= 1.75f;
        }

        for (int i = 0; i < 24; i++)
        {
            float rotation = MathHelper.ToRadians(i * 15f);
            Vector2 velocity = Projectile.velocity.RotatedBy(rotation);
            Vector2 position = Projectile.Center;

            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                position,
                velocity,
                ModContent.ProjectileType<Tack>(),
                Projectile.damage,
                Projectile.knockBack,
                Projectile.owner
            );
        }
    }

}

