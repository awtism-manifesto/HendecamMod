using HendecamMod.Content.DamageClasses;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Projectiles.Items;

public class LoadedDieProj : ModProjectile
{
    // Store the original ammo type that was used to create this projectile
    private int originalAmmoType;

    public override void SetDefaults()
    {
        Projectile.width = 30;
        Projectile.height = 30;
        Projectile.friendly = true;
        Projectile.DamageType = GetInstance<RangedStupidDamage>();
        Projectile.penetrate = 1;
        Projectile.timeLeft = 600;
        Projectile.tileCollide = true;
        Projectile.ignoreWater = false;
        Projectile.extraUpdates = 1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 15;
    }

    // Store the ammo type when the projectile is created
    public override void OnSpawn(IEntitySource source)
    {
        // Try to get the ammo type from the source if it's from a player
        if (source is EntitySource_ItemUse itemSource && itemSource.Item?.useAmmo == AmmoID.Bullet)
        {
            // Get the player who used the item
            Player player = Main.player[Projectile.owner];

            // First, check dedicated ammo slots (slots 54-58 in vanilla)
            // These are the 4 ammo slots in the inventory UI
            for (int i = 54; i <= 58; i++)
            {
                if (i < player.inventory.Length)
                {
                    Item item = player.inventory[i];
                    if (!item.IsAir && item.ammo == AmmoID.Bullet && item.stack > 0)
                    {
                        // Convert the ammo item to its corresponding projectile type
                        originalAmmoType = GetProjectileFromAmmo(item.type);
                        break;
                    }
                }
            }

            // If no ammo found in dedicated slots, check the main inventory
            if (originalAmmoType == 0)
            {
                for (int i = 0; i < 54; i++) // Only check main inventory slots (0-53)
                {
                    Item item = player.inventory[i];
                    if (!item.IsAir && item.ammo == AmmoID.Bullet && item.stack > 0)
                    {
                        // Convert the ammo item to its corresponding projectile type
                        originalAmmoType = GetProjectileFromAmmo(item.type);
                        break;
                    }
                }
            }

            // If still no ammo found, check hotbar and other slots as a fallback
            if (originalAmmoType == 0)
            {
                for (int i = 0; i < player.inventory.Length; i++)
                {
                    Item item = player.inventory[i];
                    if (!item.IsAir && item.ammo == AmmoID.Bullet && item.stack > 0)
                    {
                        // Convert the ammo item to its corresponding projectile type
                        originalAmmoType = GetProjectileFromAmmo(item.type);
                        break;
                    }
                }
            }
        }

        // If we couldn't find the ammo type, default to musket ball
        if (originalAmmoType == 0)
            originalAmmoType = ProjectileID.Bullet;
    }

    // Helper method to get the projectile type from ammo item type
    private int GetProjectileFromAmmo(int ammoItemType)
    {
        // Handle vanilla ammo types
        if (ammoItemType == ItemID.MusketBall)
            return ProjectileID.Bullet;
        else if (ammoItemType == ItemID.SilverBullet)
            return ProjectileID.SilverBullet;
        else if (ammoItemType == ItemID.MeteorShot)
            return ProjectileID.MeteorShot;
        else if (ammoItemType == ItemID.CrystalBullet)
            return ProjectileID.CrystalBullet;
        else if (ammoItemType == ItemID.CursedBullet)
            return ProjectileID.CursedBullet;
        else if (ammoItemType == ItemID.IchorBullet)
            return ProjectileID.IchorBullet;
        else if (ammoItemType == ItemID.ChlorophyteBullet)
            return ProjectileID.ChlorophyteBullet;
        else if (ammoItemType == ItemID.HighVelocityBullet)
            return ProjectileID.BulletHighVelocity;
        else if (ammoItemType == ItemID.PartyBullet)
            return ProjectileID.PartyBullet;
        else if (ammoItemType == ItemID.NanoBullet)
            return ProjectileID.NanoBullet;
        else if (ammoItemType == ItemID.ExplodingBullet)
            return ProjectileID.ExplosiveBullet;
        else if (ammoItemType == ItemID.GoldenBullet)
            return ProjectileID.GoldenBullet;
        else if (ammoItemType == ItemID.VenomBullet)
            return ProjectileID.VenomBullet;
        else if (ammoItemType == ItemID.MoonlordBullet)
            return ProjectileID.MoonlordBullet;

        // Check for modded ammo types
        if (ammoItemType > ItemID.Count)
        {
            Item ammoItem = ContentSamples.ItemsByType[ammoItemType];
            if (ammoItem != null && ammoItem.shoot > 0)
                return ammoItem.shoot;
        }

        // Default to musket ball projectile if no match found
        return ProjectileID.Bullet;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        // Determine how many bullets to spawn (1-6)
        int bulletCount = Main.rand.Next(1, 7); // Next(1, 7) gives 1-6

        // Calculate the angle between bullets for a full 360-degree spread
        float angleStep = 360f / bulletCount;

        // Play a satisfying sound when the die splits
        SoundEngine.PlaySound(SoundID.Item14, Projectile.Center); // Explosion sound

        // Spawn dust effects
        for (int i = 0; i < 20; i++)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
                DustID.Torch, 0f, 0f, 100, default, 1.5f);
        }

        // Spawn the bullets
        for (int i = 0; i < bulletCount; i++)
        {
            // Calculate angle for this bullet
            float angle = MathHelper.ToRadians(angleStep * i);

            // Randomize the velocity a bit for more natural spread
            Vector2 velocity = Projectile.velocity.RotatedBy(angle);
            velocity *= 0.8f; // Slightly slower than the original die
            velocity += new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f));

            Vector2 position = Projectile.Center;

            // Create the projectile using the stored ammo type
            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                position,
                velocity,
                originalAmmoType, // Use the original ammo projectile type
                (int)(Projectile.damage * 0.67f),
                Projectile.knockBack * 0.5f,
                Projectile.owner
            );
            
        }
    }

    public override void AI()
    {
        Projectile.rotation += 0.225f;
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 13f)
        {
            Projectile.ai[0] = 13f;
            Projectile.velocity.Y += 0.242f;
        }

        if (Projectile.velocity.Y > 22f)
        {
            Projectile.velocity.Y = 25f;
        }
    }
}