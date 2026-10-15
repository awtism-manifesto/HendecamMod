using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Accessories;

public class CatScarf : ModItem
{
   
    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = 110000;
        Item.defense = 4;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "10% reduced mana cost");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+1 max minion, +1 max sentry")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "May attract a feline visitor to your base....")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);


    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.maxTurrets += 1;
        player.maxMinions += 1;
        player.manaCost *= 0.9f;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Silk, 12);
        recipe.AddIngredient(ItemID.NaturesGift);

        recipe.AddTile(TileID.Loom);

        recipe.Register();
        
    }
}

