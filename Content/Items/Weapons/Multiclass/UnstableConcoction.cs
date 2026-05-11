using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles.Items;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Multiclass;

public class UnstableConcoction : ModItem
{
    public override void SetStaticDefaults()
    {
        // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)
        Main.RegisterItemAnimation(Type, new DrawAnimationVertical(4, 4));
        ItemID.Sets.AnimatesAsSoul[Type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation


      


    }
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 0.9f;
        Item.rare = ItemRarityID.Red; // The color that the item's name will be in-game.
        Item.value = 869000;
        // Use Properties
        // Use Properties
        Item.useTime = 24; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 24; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Swing; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        Item.noUseGraphic = true;

        // The sound that this item plays when used.
        Item.UseSound = SoundID.Item1;
        // Weapon Properties
        Item.DamageType = GetInstance<OmniDamage>(); // Sets the damage type to ranged.
        Item.damage = 148; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 9.9f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.
        Item.mana = 6;
        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shoot = ProjectileType<UnstablePotion>();

        Item.shootSpeed = 16.75f; // The speed of the projectile (measured in pixels per frame.)
    }
    public float LobotometerCost = 6f;
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
   

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Uses 6 Braincells");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Throws highly unstable acid potions that occasionally explode midair")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Hitting enemies splashes acid so potent it ignores enemy debuff immunities")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Killing an acidified enemy releases a massive explosion that does more damage the stronger the enemy killed was")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "These explosions also acidify enemies")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient(ItemID.ToxicFlask);
        recipe.AddIngredient<AstatineBar>(15);
        recipe.AddIngredient(ItemID.VialofVenom, 75);
        recipe.AddIngredient<IED>(150);

        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }

    // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-28.5f, -2.5f);
    }
}