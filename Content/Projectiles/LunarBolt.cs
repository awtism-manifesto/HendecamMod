using HendecamMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles;

/// <summary>
/// This the class that clones the vanilla Meowmere projectile using CloneDefaults().
/// Make sure to check out <see cref="ExampleCloneWeapon" />, which fires this projectile; it itself is a cloned version of the Meowmere.
/// </summary>
public class LunarBolt : ModProjectile
{

    public override void SetDefaults()
    {


        Projectile.width = 3; // The width of projectile hitbox
        Projectile.height = 3; // The height of projectile hitbox

        Projectile.scale = 1.66f;
        Projectile.timeLeft = 200;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;

        Projectile.tileCollide = true;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.penetrate = 1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.alpha = 255;
        Projectile.extraUpdates = 1;

    }

    private int tickCounter = 0;
    private int nextSpawnTick = 0;
    public override void AI()
    {
        if (Projectile.alpha < 195)
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
                Dust chudDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 7, Projectile.height - 7, DustID.Vortex, 0f, 0f, 100, default, 0.65f);
                chudDust.fadeIn = 0.1f + Main.rand.Next(3) * 0.1f;
                chudDust.velocity *= 0.1f;
                chudDust.noGravity = true;
                Dust chud2Dust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 7, Projectile.height - 7, ModContent.DustType<MoonburnDust>(), 0f, 0f, 100, default, 0.55f);
                chud2Dust.fadeIn = 0.1f + Main.rand.Next(3) * 0.1f;
                chud2Dust.velocity *= 0.1f;
                chud2Dust.noGravity = true;

            }
        }

        if (Projectile.timeLeft > 175)
        {

            if (nextSpawnTick == 0)
            {
                nextSpawnTick = Main.rand.Next(20, 25);
            }

            tickCounter++;

            if (tickCounter >= nextSpawnTick)
            {
                Vector2 velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(0));
                Vector2 Peanits = Projectile.Center;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Peanits, velocity,
                    ModContent.ProjectileType<LunarContrail>(), (int)(Projectile.damage * 0.67f), Projectile.knockBack, Projectile.owner);

                tickCounter = 0;
                nextSpawnTick = Main.rand.Next(265, 315);


                Projectile.netUpdate = true;
            }
        }

    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {


        for (int i = 0; i < 8; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Vortex);
            dust.noGravity = true;
            dust.velocity *= 7.5f;
            dust.scale *= 1.5f;

        }



        hit.HitDirection = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
    }

}