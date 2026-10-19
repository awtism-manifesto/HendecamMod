using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Ranger;

public class MintalShuriken : ModItem
{
    public override void SetDefaults()
    {
        // Alter any of these values as you see fit, but you should probably keep useStyle on 1, as well as the noUseGraphic and noMelee bools

        // Common Properties
        Item.rare = ItemRarityID.Orange;
        Item.value = 888;
        Item.maxStack = 9999;

        // Use Properties
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 13;
        Item.useTime = 13;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.consumable = true;

        // Weapon Properties			
        Item.damage = 32;
        Item.knockBack = 5f;
        Item.noUseGraphic = true; // The item should not be visible when used
        Item.noMelee = true; // The projectile will do the damage and not the item
        Item.DamageType = DamageClass.Throwing;

        // Projectile Properties
        Item.shootSpeed = 15.95f;
        Item.shoot = ProjectileType<MintalShurikenProj>(); // The projectile that will be thrown
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Homes in on enemies and bounces between them");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);


    }
    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(250);

        recipe.AddIngredient(ItemID.Shuriken, 250);
        recipe.AddIngredient<MintalBar>(3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}