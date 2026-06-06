using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Weapons.Ammo;
using HendecamMod.Content.Items.Weapons.Ranger;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.ImprovisedLaserSight;
using static HendecamMod.Content.Items.Accessories.MarksmanLaserSight;

namespace HendecamMod.Content.Items.Weapons.Multiclass;

public class AmmoRecycler : ModItem
{
    private int shotCounter = 0;
    private int[] ammoTypes = { AmmoID.Bullet, AmmoID.Arrow, AmmoID.Rocket, AmmoID.Dart };

    public override void SetDefaults()
    {
        Item.width = 104;
        Item.height = 70;
        Item.rare = ItemRarityID.LightRed;
        Item.scale = 0.9f;
        Item.value = 100000;
        Item.useTime = 29;
        Item.useAnimation = 29;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Ranged;
        Item.damage = 1;
        Item.knockBack = 5.75f;
        Item.noMelee = true;
        Item.ArmorPenetration = 5;
       
        Item.useAmmo = AmmoID.None;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 12.75f;
    }

  
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        return false; 
    }
   
    public int currentAmmoType;
   
   public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/ThiccShotgun")
        {
            Volume = 0.67f,

            MaxInstances = 100,
        });
        SoundEngine.PlaySound(new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/AmmoGun2")
        {
            Volume = 1.15f,

            MaxInstances = 100,
        });
        SoundEngine.PlaySound(new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/AmmoGun1")
        {
            Volume = 1.15f,

            MaxInstances = 100,
        });
        // Get the current ammo type we should use
        int currentAmmoType = ammoTypes[shotCounter];

        // Find the ammo in player's inventory
        Item ammoItem = FindAmmo(player, currentAmmoType);

       

        // Get the projectile type from the ammo
        int projectileType = GetProjectileTypeFromAmmo(ammoItem);

        // Calculate damage based on ammo
        int finalDamage = damage * GetAmmoDamage(player, ammoItem);
        int originalDamage = finalDamage;
     

        // Get knockback from ammo
        float finalKnockback = ammoItem.knockBack;
        if (finalKnockback <= 0) finalKnockback = knockback;

        

        // Create multiple projectiles
        const int NumProjectiles = 5;
        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(9.25f));
            newVelocity *= 1f - Main.rand.NextFloat(0.2f);

            Projectile.NewProjectileDirect(source, position, newVelocity, projectileType, finalDamage, finalKnockback, player.whoAmI);
        }

        // Consume ammo (50% chance)
        if (Main.rand.NextFloat() >= 0.5f)
        {
            // Check if it's an endless ammo item
            bool isEndlessAmmo = ammoItem.type == ItemID.EndlessMusketPouch ||
                                  ammoItem.type == ItemID.EndlessQuiver ||
                                  ammoItem.type == ItemType<CIAJournalismAward>() ||
                                  ammoItem.stack >= 3996;

            if (!isEndlessAmmo)
            {
                ammoItem.stack--;
                if (ammoItem.stack <= 0)
                {
                    ammoItem.TurnToAir();
                }
            }
        }

        // Cycle to next ammo type for the NEXT shot
        shotCounter++;
        if (shotCounter >= ammoTypes.Length)
        {
            shotCounter = 0;
        }

        return false;
    }

    // Helper method to find ammo in player's inventory
    private Item FindAmmo(Player player, int ammoType)
    {
        // First, check the dedicated ammo slots (slots 54-58)
        // These are the 5 slots below the main inventory
        for (int i = 54; i <= 58; i++)
        {
            Item item = player.inventory[i];
            if (!item.IsAir && item.ammo == ammoType && item.stack > 0 )
            {
                return item;
            }
        }

      

        // If no ammo found in ammo slots, check the main inventory (slots 0-53)
        for (int i = 0; i < 54; i++)
        {
            Item item = player.inventory[i];
            if (!item.IsAir && item.ammo == ammoType && item.stack > 0)
            {
                return item;
            }
        }

        return null;
    }

   
    // Helper method to get projectile type from ammo
    private int GetProjectileTypeFromAmmo(Item ammoItem)
    {
        // This is a simplified mapping - you might need to expand this
        if (ammoItem.shoot > ProjectileID.None)
        {
            return ammoItem.shoot;
        }

        // Fallback mapping for common ammo types
        switch (ammoItem.type)
        {
            case ItemID.MusketBall:
                return ProjectileID.Bullet;
            case ItemID.SilverBullet:
                return ProjectileID.SilverBullet;
            case ItemID.MeteorShot:
                return ProjectileID.MeteorShot;
            case ItemID.WoodenArrow:
                return ProjectileID.WoodenArrowFriendly;
            case ItemID.FlamingArrow:
                return ProjectileID.FireArrow;
            case ItemID.RocketI:
            case ItemID.RocketII:
            case ItemID.RocketIII:
            case ItemID.RocketIV:
            case ItemID.ClusterRocketI:
            case ItemID.ClusterRocketII:
                return ProjectileID.RocketI;
            case ItemID.PoisonDart:
                return ProjectileID.PoisonDart;
            default:
                return ProjectileID.Bullet; // Default fallback
        }
    }

    // Helper method to calculate damage from ammo
    // Helper method to calculate damage from ammo
    private int GetAmmoDamage(Player player, Item ammoItem)
    {
        // Get the base damage from the ammo
        float damage = ammoItem.damage;
        if (damage <= 0) damage = 10; // Default fallback


        // Manually calculate total multiplier
        float totalMultiplier = 1f; // Base 100%

       
        totalMultiplier += player.GetDamage(DamageClass.Generic).Additive - 1f;

        totalMultiplier += player.GetDamage(DamageClass.Ranged).Additive - 1f;

        // Get all damage classes and check if they should contribute to OmniDamage
        // OmniDamage gets 67% of specialized class bonuses
        DamageClass[] specializedClasses = new DamageClass[] {
        DamageClass.Magic,
        DamageClass.Melee,
        
        DamageClass.Summon,
        DamageClass.Throwing,
        GetInstance<StupidDamage>()
        };
        foreach (var damageClass in specializedClasses)
        {
            // Add 67% of the class's bonus
            float classBonus = player.GetDamage(damageClass).Additive - 1f;
            if (classBonus > 0)
            {
                totalMultiplier += classBonus * 0.67f;
            }
        }

        // Calculate final damage
        int calculatedDamage = (int)(damage * totalMultiplier);

        int boostedDamage;


        if (Main.hardMode)
        {
             boostedDamage = (int)(calculatedDamage * 2.25f);
        }
        else
        {
            boostedDamage = (int)(calculatedDamage * 1.75f);
        }

        if (boostedDamage < 1) boostedDamage = 1;

        return boostedDamage;
    }

    public override void HoldItem(Player player)
    {
        player.GetModPlayer<LaserDrawFire>().Laser = true;
    }


    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Cycles between using Bullets, Arrows, Rockets and Darts with every shot");
        tooltips.Add(line);

        var line1 = new TooltipLine(Mod, "Face", "Right click to zoom");
        tooltips.Add(line1);

        line = new TooltipLine(Mod, "Face", "50% chance to not consume ammo, increases all ammo base damage by 1.75x");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Does not work unless you have all four types of ammo");
        tooltips.Add(line);
        if (ModLoader.TryGetMod("Fargowiltas", out Mod Fargo))
        {
        line = new TooltipLine(Mod, "Face", "WARNING: This weapon may accidentally consume modded endless ammo items.");
        tooltips.Add(line);
            line = new TooltipLine(Mod, "Face", "It will not consume ammo if the stack size is 3996 or larger.");
            tooltips.Add(line);
        }
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<ZazaNerfGun>();
        recipe.AddIngredient<Boomshark>();
        recipe.AddIngredient<ImprovisedMortarLauncher>();
        recipe.AddIngredient<ObsidianCompoundBow>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-28f, -3f);
    }
}
public class LaserDrawFire : ModPlayer
{
    public override void ModifyZoom(ref float zoom)
    {
        Player player = Main.LocalPlayer;
        if (Main.mouseRight == true && Laser)
        {
                zoom = player.scope ? 0.6666667f : 0.5f;
        }
        base.ModifyZoom(ref zoom);
    }


    public bool Laser;
    public Vector2 laserEndPosition;

    public override void ResetEffects()
    {
        Laser = false;
    }
    public override void PreUpdate()
    {
        if (Laser)
        {
            // Get mouse position in world coordinates
            laserEndPosition = Main.MouseWorld;
        }
    }

}
public class LaserSightFireDrawLayer : PlayerDrawLayer
{
    // Define where in the draw order this layer appears
    public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.BackAcc);

    // Control when this layer is visible
    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
        Player player = drawInfo.drawPlayer;

        
        if (player.whoAmI != Main.myPlayer)
            return false;

        var modPlayer = player.GetModPlayer<LaserDrawFire>();
        return modPlayer.Laser && !player.dead;
    }

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        Player player = drawInfo.drawPlayer;
        var modPlayer = player.GetModPlayer<LaserDrawFire>();

        if (modPlayer.laserEndPosition == Vector2.Zero)
            return;

        // Calculate laser start position (from player center)
        Vector2 startPos = player.Center;
        Vector2 endPos = modPlayer.laserEndPosition;

        // Calculate direction and distance
        Vector2 direction = endPos - startPos;
        float distance = direction.Length();
        direction.Normalize();

        // Don't draw if too far
        if (distance > 1850f) return;

        // Get screen position
        Vector2 screenStart = startPos - Main.screenPosition;
        Vector2 screenEnd = endPos - Main.screenPosition;

        // Draw the laser beam
        DrawLaserBeam(drawInfo, screenStart, screenEnd, distance);
    }

    private void DrawLaserBeam(PlayerDrawSet drawInfo, Vector2 start, Vector2 end, float distance)
    {
        // You'll need these textures - create them in your Assets folder
        Texture2D laserTexture = Request<Texture2D>("HendecamMod/Content/Effects/LaserBeamFire").Value;
        Texture2D circleTexture = Request<Texture2D>("HendecamMod/Content/Effects/LaserEndFire").Value;

        // Fallback if textures don't exist - creates simple textures
        if (laserTexture == null || laserTexture.IsDisposed)
        {
            laserTexture = new Texture2D(Main.graphics.GraphicsDevice, 1, 1);
            laserTexture.SetData(new[] { Color.White });
        }

        if (circleTexture == null || circleTexture.IsDisposed)
        {
            circleTexture = new Texture2D(Main.graphics.GraphicsDevice, 5, 5);
            Color[] data = new Color[25];
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.White;
            circleTexture.SetData(data);
        }

        Color laserColor = new Color(255, 75, 20) * 0.6f; 

        Vector2 direction = end - start;
        float rotation = direction.ToRotation();

        // Draw main beam
        drawInfo.DrawDataCache.Add(new DrawData(
            laserTexture,
            new Vector2(start.X, start.Y),
            null,
            laserColor,
            rotation,
            new Vector2(0, laserTexture.Height / 2),
            new Vector2(distance, 2f),
            SpriteEffects.None,
            0
        ));

        // Draw glow effect (wider, more transparent)
        drawInfo.DrawDataCache.Add(new DrawData(
            laserTexture,
            new Vector2(start.X, start.Y),
            null,
            laserColor * 0.3f,
            rotation,
            new Vector2(0, laserTexture.Height / 2),
            new Vector2(distance, 6f),
            SpriteEffects.None,
            0
        ));

        // Draw end cap (circle at the end)
        drawInfo.DrawDataCache.Add(new DrawData(
            circleTexture,
            end,
            null,
            laserColor,
            0f,
            new Vector2(circleTexture.Width / 2, circleTexture.Height / 2),
            1f,
            SpriteEffects.None,
            0
        ));
    }
}