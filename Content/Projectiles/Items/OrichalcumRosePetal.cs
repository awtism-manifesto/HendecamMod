using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;

namespace HendecamMod.Content.Projectiles.Items;

public class OrichalcumRosePetal : ModProjectile
{
   
    private NPC HomingTarget
    {
        get => Projectile.ai[0] == 0 ? null : Main.npc[(int)Projectile.ai[0] - 1];
        set { Projectile.ai[0] = value == null ? 0 : value.whoAmI + 1; }
    }

    public ref float DelayTimer => ref Projectile.ai[1];
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 3;

    }
    public override void SetDefaults()
    {
        Projectile.width = 12; // The width of projectile hitbox
        Projectile.height = 12; // The height of projectile hitbox
        Projectile.scale = 0.75f;
        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Magic; // Is the projectile shoot by a ranged weapon?
        Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 300; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
        Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
        Projectile.light = 0.05f; // How much light emit around the projectile
        Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = true; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
    }

   

    public override void AI()
    {
        int frameSpeed = 12;

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
        Lighting.AddLight(Projectile.Center, 0.8f, 0.22f, 0.75f);
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.Venom, 125);
      
    }

    


   
}