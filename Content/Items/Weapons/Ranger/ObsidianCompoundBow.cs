using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Global;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Ranger;

public class ObsidianCompoundBow : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1f;
        Item.rare = ItemRarityID.Orange; // The color that the item's name will be in-game.
        Item.value = 99900;
        // Use Properties
        Item.useTime = 26; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 26; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        Item.UseSound = new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/BowShot")
        {
            Volume = 2f,
            PitchVariance = 0.2f,
            MaxInstances = 10,
        };
        // Weapon Properties
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 42; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 4.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.

        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 9.5f; // The speed of the projectile (measured in pixels per frame.)
        Item.useAmmo = ItemID.WoodenArrow;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        type = ProjectileType<ObsidianArrow>();
        int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        Main.projectile[proj].GetGlobalProjectile<FastBow>().fromCompoundBow = true;
        return false; // Prevent vanilla projectile spawn
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Shoots obsidian arrows at an extremely high velocity");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Getting hit with the weapon in hand causes shattering obsidian shards to fly everywhere")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }
    public override void HoldItem(Player player)
    {
        player.GetModPlayer<ObsidiShat>().Shatty = true;
    }
    public override void UpdateInventory(Player player)
    {
        if (player.HeldItem.type == ItemType<ObsidianCompoundBow>())
        {
            player.GetModPlayer<ObsidiShat>().Shatty = true;
        }
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<CompoundBow>();
        recipe.AddIngredient<ObsidianGreatbow>();

        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
       
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-6f, -1f);
    }
}
public class ObsidiShat : ModPlayer
{


    public bool Shatty;


    public override void ResetEffects()
    {
        Shatty = false;
    }



    public override void OnHurt(Player.HurtInfo info)
    {
        if (!Shatty)
            return;


        int baseDamage = 21;

        float totalMultiplier = 1f; // Base 100%

        // Generic damage (100% effectiveness)
        totalMultiplier += Player.GetDamage(DamageClass.Generic).Additive - 1f;

        // Get all damage classes and check if they should contribute to OmniDamage
        // OmniDamage gets 67% of specialized class bonuses
        DamageClass[] specializedClasses = new DamageClass[] {
        DamageClass.Magic,
        DamageClass.Melee,
        DamageClass.Ranged,
        DamageClass.Summon,
        DamageClass.Throwing,
        GetInstance<StupidDamage>()
        };
        foreach (var damageClass in specializedClasses)
        {
            // Add 67% of the class's bonus
            float classBonus = Player.GetDamage(damageClass).Additive - 1f;
            if (classBonus > 0)
            {
                totalMultiplier += classBonus * 0.67f;
            }
        }

        // Calculate final damage
        int calculatedDamage = (int)(baseDamage * totalMultiplier);


        Projectile.NewProjectile(
            Player.GetSource_FromThis(),
            Player.Center,
            new Vector2(0f, -5f),
            ProjectileType<ObsidiSpawn>(),
            calculatedDamage,
            3f,
            Player.whoAmI
        );


    }
}