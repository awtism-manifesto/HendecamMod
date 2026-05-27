using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using Terraria.Audio;

namespace HendecamMod.Content.Projectiles.Items;


public class ScaldStream : ModProjectile
{
    public override void SetDefaults()
    {
        // This method right here is the backbone of what we're doing here; by using this method, we copy all of
        // the Meowmere Projectile's SetDefault stats (such as projectile.friendly and projectile.penetrate) on to our projectile,
        // so we don't have to go into the source and copy the stats ourselves. It saves a lot of time and looks much cleaner;
        // if you're going to copy the stats of a projectile, use CloneDefaults().

        Projectile.CloneDefaults(ProjectileID.WaterStream);

        // To further the Cloning process, we can also copy the ai of any given projectile using AIType, since we want
        // the projectile to essentially behave the same way as the vanilla projectile.
        AIType = ProjectileID.WaterStream;
        Projectile.timeLeft = 110;
       Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 20;
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {

        modifiers.SourceDamage = modifiers.SourceDamage + (target.defense * 0.036f);
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffType<ScaldBurn>(), 300);

       
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        target.AddBuff(BuffType<ScaldBurn>(), 300);
    }

    public override void AI()
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

            Dust fireDust = Dust.NewDustDirect(new Vector2(Projectile.position.X + 1f + posOffsetX, Projectile.position.Y + 1f + posOffsetY) - Projectile.velocity * 0.1f, Projectile.width - 8, Projectile.height - 8, DustID.RedTorch, 0f, 0f, 100, default, 1f);
            fireDust.fadeIn = 0.1f + Main.rand.Next(2) * 0.05f;
            fireDust.velocity *= 0.05f;
        }
    }

   
}