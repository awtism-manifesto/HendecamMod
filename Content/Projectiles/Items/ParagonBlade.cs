using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;

namespace HendecamMod.Content.Projectiles.Items;

public class ParagonBlade : ModProjectile
{
    public override void SetStaticDefaults()
    {
      
        ProjectileID.Sets.SentryShot[Type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.width = 40; // The width of projectile hitbox
        Projectile.height = 40; // The height of projectile hitbox
        Projectile.aiStyle = -1; // The ai style of the projectile, please reference the source code of Terraria
        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Summon;
        Projectile.penetrate = 10; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 40; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
        Projectile.alpha = 100; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
       
        Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = true; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        AIType = ProjectileID.Bullet; // Act exactly like default Bullet
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.OnFire, 750);
        target.AddBuff(BuffID.OnFire3, 750);
        target.AddBuff(BuffID.Oiled, 750);
    }

    public override void AI()
    {
        Lighting.AddLight(Projectile.Center, 1.25f, 0.65f, 0.15f);

        Projectile.rotation += 0.25f;
    }
    public override void OnKill(int timeLeft)
    {
      

        for (int i = 0; i < 6; i++)
        {
            float rotation = MathHelper.ToRadians(i * 60f);
            Vector2 velocity = Projectile.velocity.RotatedBy(rotation);
            Vector2 position = Projectile.Center;

            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                position,
                velocity,
                ModContent.ProjectileType<ParagonTack>(),
                (int)(Projectile.damage * 0.67f),
                Projectile.knockBack,
                Projectile.owner
            );
        }

       
    }

}