using Terraria.Audio;

namespace HendecamMod.Content.Projectiles.Items.UnarmedProjectiles;

public class ClawProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        // Prevents jitter when stepping up and down blocks and half blocks
        ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
        Main.projFrames[Projectile.type] = 4; //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT
    }

    public override void SetDefaults()
    {
        Projectile.width = 22;
        Projectile.height = 22;
        Projectile.friendly = true; //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT
        Projectile.tileCollide = false; //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT
        Projectile.penetrate = -1; //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT
        Projectile.DamageType = DamageClass.Melee;
        Projectile.ownerHitCheck = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 6;
       
        Projectile.aiStyle = -1; // Replace with 20 if you do not want custom code
        Projectile.hide = true; // Hides the projectile, so it will draw in the player's hand when we set the player's heldProj to this one.
    }

    // This code is adapted and simplified from aiStyle 20 to use a different dust and more noises. If you want to use aiStyle 20, you do not need to do any of this.
    // It should be noted that this projectile has no effect on mining and is mostly visual.
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];

        Projectile.timeLeft = 24;

       
        int frameSpeed = 6; //TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT TAKE CURRENT

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


        Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter);
        if (Main.myPlayer == Projectile.owner)
        {
            // This code must only be ran on the client of the projectile owner
            if (player.channel)
            {
                float holdoutDistance = player.HeldItem.shootSpeed * Projectile.scale;
                // Calculate a normalized vector from player to mouse and multiply by holdoutDistance to determine resulting holdoutOffset
                Vector2 holdoutOffset = holdoutDistance * Vector2.Normalize(Main.MouseWorld - playerCenter);
                if (holdoutOffset.X != Projectile.velocity.X || holdoutOffset.Y != Projectile.velocity.Y)
                {
                    // This will sync the projectile, most importantly, the velocity.
                    Projectile.netUpdate = true;
                }

                // Projectile.velocity acts as a holdoutOffset for held projectiles.
                Projectile.velocity = holdoutOffset;
            }
            else
            {
                Projectile.Kill();
            }
        }

        if (Projectile.velocity.X > 0f)
        {
            player.ChangeDir(1);
        }
        else if (Projectile.velocity.X < 0f)
        {
            player.ChangeDir(-1);
        }

        Projectile.spriteDirection = Projectile.direction;
        player.ChangeDir(Projectile.direction); // Change the player's direction based on the projectile's own
        player.heldProj = Projectile.whoAmI; // We tell the player that the drill is the held projectile, so it will draw in their hand
        player.SetDummyItemTime(2); // Make sure the player's item time does not change while the projectile is out
        Projectile.Center = playerCenter; // Centers the projectile on the player. Projectile.velocity will be added to this in later Terraria code causing the projectile to be held away from the player at a set distance.
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();

        

       
    }
}