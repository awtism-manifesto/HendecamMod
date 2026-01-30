using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles;

public class StarfishYoyo : ModProjectile
{
    public override void SetStaticDefaults()
    {
        // The following sets are only applicable to yoyo that use aiStyle 99.

        // YoyosLifeTimeMultiplier is how long in seconds the yoyo will stay out before automatically returning to the player. 
        // Vanilla values range from 3f (Wood) to 16f (Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
        ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 10.5f;

        // YoyosMaximumRange is the maximum distance the yoyo sleep away from the player. 
        // Vanilla values range from 130f (Wood) to 400f (Terrarian), and defaults to 200f.
        ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 235f;

        // YoyosTopSpeed is top speed of the yoyo Projectile.
        // Vanilla values range from 9f (Wood) to 17.5f (Terrarian), and defaults to 10f.
        ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 12.33f;
    }
    public override void AI()
    {
        // The code below was adapted from the ProjAIStyleID.Arrow behavior. Rather than copy an existing aiStyle using Projectile.aiStyle and AIType,
        // like some examples do, this example has custom AI code that is better suited for modifying directly.
        // See https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#what-is-ai for more information on custom projectile AI.


        // dust
        if (Math.Abs(Projectile.velocity.X) >= 0f || Math.Abs(Projectile.velocity.Y) >= 0f)
        {
            for (int i = 0; i < 2; i++)
            {
                float posOffsetX = 0f;
                float posOffsetY = 0f;
                if (i == 1)
                {
                    posOffsetX = Projectile.velocity.X * 2.5f;
                    posOffsetY = Projectile.velocity.Y * 2.5f;
                }


                Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + -5f + posOffsetX, Projectile.position.Y + 5f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 8, Projectile.height - 8, DustID.Water, 0f, 0f, 100, default, 0.75f);
                fireDust.fadeIn = 0.2f + Main.rand.Next(5) * 0.1f;
                fireDust.noGravity = true;
                fireDust.velocity *= 1f;
            }
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {

        if (Main.rand.NextBool(5))
        {

            Vector2 velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(60));
            Vector2 Spawn = Projectile.Center - new Vector2(Main.rand.NextFloat(-4, 4));
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Spawn, velocity,
                ModContent.ProjectileType<WaterJetMelee>(), (int)(Projectile.damage * 0.425f), Projectile.knockBack, Projectile.owner);
        }
        if (Main.rand.NextBool(5))
        {
            Vector2 velocity2 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(120));
            Vector2 Spawn2 = Projectile.Center - new Vector2(Main.rand.NextFloat(-4, 4));
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Spawn2, velocity2,
            ModContent.ProjectileType<WaterJetMelee>(), (int)(Projectile.damage * 0.425f), Projectile.knockBack, Projectile.owner);
        }
        if (Main.rand.NextBool(5))
        {
            Vector2 velocity3 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(180));
            Vector2 Spawn3 = Projectile.Center - new Vector2(Main.rand.NextFloat(-4, 4));
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Spawn3, velocity3,
            ModContent.ProjectileType<WaterJetMelee>(), (int)(Projectile.damage * 0.425f), Projectile.knockBack, Projectile.owner);
        }
        if (Main.rand.NextBool(5))
        {
            Vector2 velocity4 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(240));
            Vector2 Spawn4 = Projectile.Center - new Vector2(Main.rand.NextFloat(-4, 4));
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Spawn4, velocity4,
            ModContent.ProjectileType<WaterJetMelee>(), (int)(Projectile.damage * 0.425f), Projectile.knockBack, Projectile.owner);
        }
        if (Main.rand.NextBool(5))
        {
            Vector2 velocity5 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(300));
            Vector2 Spawn5 = Projectile.Center - new Vector2(Main.rand.NextFloat(-4, 4));
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Spawn5, velocity5,
            ModContent.ProjectileType<WaterJetMelee>(), (int)(Projectile.damage * 0.425f), Projectile.knockBack, Projectile.owner);
        }




    }
    public override Color? GetAlpha(Color lightColor)
    {
        return Color.AliceBlue;
    }
    public override void SetDefaults()
    {
        Projectile.width = 16; // The width of the projectile's hitbox.
        Projectile.height = 16; // The height of the projectile's hitbox.

        Projectile.aiStyle = ProjAIStyleID.Yoyo; // The projectile's ai style. Yoyos use aiStyle 99 (ProjAIStyleID.Yoyo). A lot of yoyo code checks for this aiStyle to work properly.
        Projectile.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
        Projectile.friendly = true; // Player shot projectile. Does damage to enemies but not to friendly Town NPCs.

        Projectile.penetrate = -1; // All vanilla yoyos have infinite penetration. The number of enemies the yoyo can hit before being pulled back in is based on YoyosLifeTimeMultiplier.
                                   // Projectile.scale = 1f; // The scale of the projectile. Most yoyos are 1f, but a few are larger. The Kraken is the largest at 1.2f
    }
}