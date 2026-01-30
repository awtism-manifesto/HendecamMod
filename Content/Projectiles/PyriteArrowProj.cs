using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles;

// This example is similar to the Wooden Arrow projectile
public class PyriteArrowProj : ModProjectile
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

        Projectile.arrow = true;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.timeLeft = 660;
    }
    private int tickCounter = 0;
    private int nextSpawnTick = 0;
    public override void AI()
    {
        // The code below was adapted from the ProjAIStyleID.Arrow behavior. Rather than copy an existing aiStyle using Projectile.aiStyle and AIType,
        // like some examples do, this example has custom AI code that is better suited for modifying directly.
        // See https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#what-is-ai for more information on custom projectile AI.

        // Apply gravity after a quarter of a second
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 13f)
        {
            Projectile.ai[0] = 8f;
            Projectile.velocity.Y += 0.15f;
        }

        // The projectile is rotated to face the direction of travel
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

        // Cap downward velocity
        if (Projectile.velocity.Y > 13f)
        {
            Projectile.velocity.Y = 19f;
        }
        if (nextSpawnTick == 0)
        {
            nextSpawnTick = Main.rand.Next(22, 23);
        }

        tickCounter++;

        if (tickCounter >= nextSpawnTick)
        {
            Vector2 velocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(5));
            Vector2 Peanits = Projectile.Center - new Vector2(0, 0);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits, velocity,
                ModContent.ProjectileType<IchSpark>(), (int)(Projectile.damage * 0.35f), Projectile.knockBack, Projectile.owner);

            tickCounter = 0;
            nextSpawnTick = Main.rand.Next(22, 23);
            Projectile.netUpdate = true;
        }

    }
}