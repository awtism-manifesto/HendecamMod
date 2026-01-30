
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace HendecamMod.Content.Poop;

public class ThrownPoop : ModProjectile
{
    public override void SetStaticDefaults()
    {

        Main.projFrames[Projectile.type] = 4;
    }

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.Shuriken);

        // To further the Cloning process, we can also copy the ai of any given projectile using AIType, since we want
        // the projectile to essentially behave the same way as the vanilla projectile.
        AIType = ProjectileID.Shuriken;
        Projectile.penetrate += -3;
        Projectile.timeLeft = 1500;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {



        Projectile.Kill();



        return false;
    }
    public override void AI()
    {

        int frameSpeed = 7;

        Projectile.frameCounter++;

        if (Projectile.frameCounter >= frameSpeed)
        {
            Projectile.frameCounter = 0;
            Projectile.frame++;

            if (Projectile.frame >= Main.projFrames[Projectile.type])
            {
                Projectile.frame = 0;


            }
        }




        for (int i = 0; i < 1; i++)
        {
            float posOffsetX = 0f;
            float posOffsetY = 0f;
            if (i == 1)
            {
                posOffsetX = Projectile.velocity.X * 2.5f;
                posOffsetY = Projectile.velocity.Y * 2.5f;
            }

            Dust fire2Dust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 5, Projectile.height - 5, DustID.Poop, 0f, 0f, 100, default, 0.67f);
            fire2Dust.fadeIn = 0.2f + Main.rand.Next(3) * 0.1f;
            fire2Dust.noGravity = true;
            fire2Dust.velocity *= 0.25f;


        }


    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int i = 0; i < 7; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Poop);
            dust.noGravity = true;
            dust.velocity *= 7.5f;
            dust.scale *= 1.25f;
        }

        target.AddBuff(BuffID.Poisoned, 90);
        target.AddBuff(BuffID.Stinky, 900);
    }


}


