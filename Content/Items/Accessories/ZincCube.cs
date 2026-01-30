using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Rarities;

namespace HendecamMod.Content.Items.Accessories;

public class ZincCube : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.
        
            // Common Properties
            Item.width = 26; // Hitbox width of the item.
        Item.height = 26; // Hitbox height of the item.
        Item.rare = ModContent.RarityType<BurntOrange>();
        Item.value = 36250;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.defense = 3;
        if (ModLoader.TryGetMod("Avalon", out Mod Avalon))
        {
            Item.rare = ItemRarityID.Blue;
        }
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.maxFallSpeed = player.maxFallSpeed * 2f;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Doubles your fall speed");
        tooltips.Add(line);
        if (!ModLoader.TryGetMod("Avalon", out Mod Avalon))
        {
            line = new TooltipLine(Mod, "Face", "Avalon Compatibility Item")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
       
    }

    public override void AddRecipes()
    {
        if (ModLoader.TryGetMod("Avalon", out Mod Avalon) && Avalon.TryFind("ZincBar", out ModItem ZincBar))
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<CubicMold>();
            recipe.AddIngredient(ZincBar.Type, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}