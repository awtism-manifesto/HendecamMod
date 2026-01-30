using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles;

public class TackSpraySpawn : ModProjectile
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
        for (int i = 0; i < 8; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke);
            dust.noGravity = true;
            dust.velocity *= 12.5f;
            dust.scale *= 1.75f;
        }

        Vector2 velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(0));
        Vector2 Peanits = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits, velocity,
            ModContent.ProjectileType<Tack>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity2 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(45));
        Vector2 Peanits2 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits2, velocity2,
            ModContent.ProjectileType<Tack>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity3 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(90));
        Vector2 Peanits3 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits3, velocity3,
            ModContent.ProjectileType<Tack>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity4 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(135));
        Vector2 Peanits4 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits4, velocity4,
            ModContent.ProjectileType<Tack>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity5 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(180));
        Vector2 Peanits5 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits5, velocity5,
            ModContent.ProjectileType<Tack>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity6 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(225));
        Vector2 Peanits6 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits6, velocity6,
            ModContent.ProjectileType<Tack>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity7 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(270));
        Vector2 Peanits7 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits7, velocity7,
            ModContent.ProjectileType<Tack>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity8 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(315));
        Vector2 Peanits8 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits8, velocity8,
            ModContent.ProjectileType<Tack>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity9 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(22.5f));
        Vector2 Peanits9 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits9, velocity9,
            ModContent.ProjectileType<Tack>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity12 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(67.5f));
        Vector2 Peanits12 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits12, velocity12,
            ModContent.ProjectileType<Tack>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity13 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(112.5f));
        Vector2 Peanits13 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits13, velocity13,
            ModContent.ProjectileType<Tack>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity14 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(157.5f));
        Vector2 Peanits14 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits14, velocity14,
            ModContent.ProjectileType<Tack>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity15 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(202.5f));
        Vector2 Peanits15 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits15, velocity15,
            ModContent.ProjectileType<Tack>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity16 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(247.5f));
        Vector2 Peanits16 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits16, velocity16,
            ModContent.ProjectileType<Tack>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity17 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(292.5f));
        Vector2 Peanits17 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits17, velocity17,
            ModContent.ProjectileType<Tack>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
        Vector2 velocity18 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(337.5f));
        Vector2 Peanits18 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits18, velocity18,
            ModContent.ProjectileType<Tack>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
    }
}