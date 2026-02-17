using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items;

public class ImpulseBow : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1f;
        Item.rare = ItemRarityID.Red; // The color that the item's name will be in-game.
        Item.value = 2000000; // The number and type of coins item can be sold for to an NPC
        // Use Properties
        Item.useTime = 15; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 15; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        // The sound that this item plays when used.
        Item.UseSound = SoundID.Item75;
        // Weapon Properties
        Item.DamageType = ModContent.GetInstance<RangedMagicDamage>(); // Sets the damage type to ranged.
        Item.damage = 98; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 3.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.
        Item.crit = 10;
        Item.mana = 6;

        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 28.5f; // The speed of the projectile (measured in pixels per frame.)
        Item.useAmmo = ItemID.WoodenArrow;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Shoots an astatine pulse shot along with your arrow");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

      
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.PulseBow);
        recipe.AddIngredient<AstatineBar>(15);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-19f, -1f);
    }
}