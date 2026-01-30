using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles;

public class Electrobomb : ModProjectile
{
    private NPC HomingTarget
    {
        get => Projectile.ai[0] == 0 ? null : Main.npc[(int)Projectile.ai[0] - 1];
        set { Projectile.ai[0] = value == null ? 0 : value.whoAmI + 1; }
    }

    public ref float DelayTimer => ref Projectile.ai[1];

    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 4;
    }

    public override void SetDefaults()
    {
        Projectile.width = 13; // The width of projectile hitbox
        Projectile.height = 13; // The height of projectile hitbox

        Projectile.friendly = true; // Can the projectile deal damage to enemies?
        Projectile.hostile = false; // Can the projectile deal damage to the player?
        Projectile.DamageType = DamageClass.Magic; // Is the projectile shoot by a ranged weapon?
        Projectile.penetrate = -1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
        Projectile.timeLeft = 200;

        Projectile.light = 0.1f;
        Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
        Projectile.tileCollide = true; // Can the projectile collide with tiles?
        Projectile.extraUpdates = 0; // Set to above 0 if you want the projectile to update multiple time in a frame
        Projectile.usesLocalNPCImmunity = true;
        AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        Projectile.aiStyle = 1;
        Projectile.alpha = 255;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Projectile.PrepareBombToBlow();
        Projectile.timeLeft = 2;
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

        float maxDetectRadius = 305f; // The maximum radius at which a projectile can detect a target

        // A short delay to homing behavior after being fired
        if (DelayTimer < 14)
        {
            DelayTimer += 1;
            return;
        }

        // First, we find a homing target if we don't have one
        if (HomingTarget == null)
        {
            HomingTarget = FindClosestNPC(maxDetectRadius);
        }

        // If we have a homing target, make sure it is still valid. If the NPC dies or moves away, we'll want to find a new target
        if (HomingTarget != null && !IsValidTarget(HomingTarget))
        {
            HomingTarget = null;
        }

        // If we don't have a target, don't adjust trajectory
        if (HomingTarget == null)
            return;

        // If found, we rotate the projectile velocity in the direction of the target.
        // We only rotate by 3 degrees an update to give it a smooth trajectory. Increase the rotation speed here to make tighter turns
        float length = Projectile.velocity.Length();
        float targetAngle = Projectile.AngleTo(HomingTarget.Center);
        Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(5.5f)).ToRotationVector2() * length;
        if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
        {
            Projectile.PrepareBombToBlow();
        }
    }

    public NPC FindClosestNPC(float maxDetectDistance)
    {
        NPC closestNPC = null;

        // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
        float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

        // Loop through all NPCs
        foreach (var target in Main.ActiveNPCs)
        {
            // Check if NPC able to be targeted. 
            if (IsValidTarget(target))
            {
                // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                // Check if it is within the radius
                if (sqrDistanceToTarget < sqrMaxDetectDistance)
                {
                    sqrMaxDetectDistance = sqrDistanceToTarget;
                    closestNPC = target;
                }
            }
        }

        return closestNPC;
    }

    public bool IsValidTarget(NPC target)
    {
        // This method checks that the NPC is:
        // 1. active (alive)
        // 2. chaseable (e.g. not a cultist archer)
        // 3. max life bigger than 5 (e.g. not a critter)
        // 4. can take damage (e.g. moonlord core after all it's parts are downed)
        // 5. hostile (!friendly)
        // 6. not immortal (e.g. not a target dummy)
        // 7. doesn't have solid tiles blocking a line of sight between the projectile and NPC
        return target.CanBeChasedBy() && Collision.CanHit(Projectile.Center, 1, 1, target.position, target.width, target.height);
    }

    public override void PrepareBombToBlow()
    {
        Projectile.tileCollide = false; // This is important or the explosion will be in the wrong place if the rocket explodes on slopes.
        Projectile.alpha = 255; // Make the rocket invisible.

        // Resize the hitbox of the projectile for the blast "radius".
        // Rocket I: 128, Rocket III: 200, Mini Nuke Rocket: 250
        // Measurements are in pixels, so 128 / 16 = 8 tiles.
        Projectile.Resize(90, 90);
        // Set the knockback of the blast.
        // Rocket I: 8f, Rocket III: 10f, Mini Nuke Rocket: 12f
        Projectile.knockBack = 6f;
    }

    public override void OnKill(int timeLeft)
    {
        // Vanilla code takes care ensuring that in For the Worthy or Get Fixed Boi worlds the blast can damage other players because
        // this projectile is ProjectileID.Sets.Explosive[Type] = true;. It also takes care of hurting the owner. The Projectile.PrepareBombToBlow
        // and Projectile.HurtPlayer methods can be used directly if needed for a projectile not using ProjectileID.Sets.Explosive

        // Play an exploding sound.
        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        SoundEngine.PlaySound(SoundID.DD2_LightningAuraZap, Projectile.position);

        // Resize the projectile again so the explosion dust and gore spawn from the middle.
        // Rocket I: 22, Rocket III: 80, Mini Nuke Rocket: 50
        Projectile.Resize(75, 75);
        // Spawn a bunch of fire dusts.
        for (int j = 0; j < 25; j++)
        {
            Dust fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 100, default, 0.7f);
            fireDust.noGravity = true;
            fireDust.velocity *= 3f;
            fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 100, default, 0.66f);
            fireDust.velocity *= 3.5f;
        }
    }
}