using HendecamMod.Content.Items.Placeables;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Materials;

public class EbonceramicSheet : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.22f;
        Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
        Item.value = 2250;
        Item.maxStack = 9999;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);




    }


    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(5);

        recipe.AddIngredient(ItemID.Bone, 2);
        recipe.AddIngredient<Ebonclay>(10);
        recipe.AddTile(TileID.AdamantiteForge);
        recipe.Register();

        recipe = CreateRecipe(10);

        recipe.AddIngredient(ItemID.SoulofNight);
        recipe.AddIngredient<Ebonclay>();
        recipe.AddIngredient<CeramicSheet>(10);
        recipe.AddTile(TileID.AdamantiteForge);
        recipe.Register();



    }
}