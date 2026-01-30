using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles.Items;

// This example is similar to the Wooden Arrow projectile
public class SpectralArrowProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        // If this arrow would have strong effects (like Holy Arrow pierce), we can make it fire fewer projectiles from Daedalus Stormbow for game balance considerations like this:
        //ProjectileID.Sets.FiresFewerFromDaedalusStormbow[Type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.width = 12; // The width of projectile hitbox
        Projectile.height = 12; // The height of projectile hitbox
        Projectile.light = 1f;
        Projectile.extraUpdates = 1;
        Projectile.arrow = true;
        Projectile.friendly = true;
        Projectile.DamageType = ModContent.GetInstance<RangedSummonDamage>();
        Projectile.timeLeft = 1200;
    }

    public override void AI()
    {
        // The code below was adapted from the ProjAIStyleID.Arrow behavior. Rather than copy an existing aiStyle using Projectile.aiStyle and AIType,
        // like some examples do, this example has custom AI code that is better suited for modifying directly.
        // See https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#what-is-ai for more information on custom projectile AI.

        // Apply gravity after a quarter of a second
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 18f)
        {
            Projectile.ai[0] = 17f;
            Projectile.velocity.Y += 0.17f;
        }

        // The projectile is rotated to face the direction of travel
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

        // Cap downward velocity
        if (Projectile.velocity.Y > 17f)
        {
            Projectile.velocity.Y = 21f;
        }
        // dust
        if (Math.Abs(Projectile.velocity.X) >= 4f || Math.Abs(Projectile.velocity.Y) >= 4f)
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

                Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 8, Projectile.height - 8, DustID.YellowStarDust, 0f, 0f, 100, default, 0.75f);
                fireDust.fadeIn = 0.1f + Main.rand.Next(5) * 0.1f;
                fireDust.velocity *= 0.05f;
                Dust fire2Dust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 8, Projectile.height - 8, DustID.CrystalSerpent_Pink, 0f, 0f, 100, default, 0.95f);
                fire2Dust.fadeIn = 0.1f + Main.rand.Next(5) * 0.1f;
                fire2Dust.velocity *= 0.25f;
                fire2Dust.noGravity = true;
            }
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(ModContent.BuffType<ShinyTag2>(), 300);
    }
    public override void OnKill(int timeLeft)
    {

        for (int i = 0; i < 5; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.CrystalSerpent_Pink);
            dust.noGravity = true;
            dust.velocity *= 1.5f;
            dust.scale *= 0.9f;
        }
        for (int i = 0; i < 5; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.YellowStarDust);
            dust.noGravity = true;
            dust.velocity *= 1.5f;
            dust.scale *= 0.9f;
        }
    }
}