
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class MukbangStarterKit : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Grants immunity to Peckish, Hungry, and Starving");
        tooltips.Add(line);
    }
    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.NeutralHunger] = true;
        player.buffImmune[BuffID.Hunger] = true;
        player.buffImmune[BuffID.Starving] = true;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.CreamSoda);
        recipe.AddIngredient(ItemID.Milkshake);
        recipe.AddIngredient(ItemID.ChickenNugget, 10);
        recipe.AddIngredient(ItemID.Burger);
        recipe.AddIngredient(ItemID.Fries);
        recipe.AddTile(TileID.CookingPots);
        recipe.Register();
    }
}