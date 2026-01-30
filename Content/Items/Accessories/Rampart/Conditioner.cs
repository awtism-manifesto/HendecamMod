using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class Conditioner : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 500);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Grants immunity to Wet, Lovestruck, and Stinky");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.Wet] = true;
        player.buffImmune[BuffID.Lovestruck] = true;
        player.buffImmune[BuffID.Stinky] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.CritterShampoo);
        recipe.AddIngredient(ItemID.LovePotion);
        recipe.AddIngredient(ItemID.StinkPotion);
        recipe.AddTile(TileID.Bottles);
        recipe.Register();
    }
}