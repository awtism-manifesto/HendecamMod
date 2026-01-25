
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class SecondHalf : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 1000);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Grants immunity to Betsy's Curse, Oiled, Daybroken, and Celled");
        tooltips.Add(line);
    }
    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.BetsysCurse] = true;
        player.buffImmune[BuffID.Oiled] = true;
        player.buffImmune[BuffID.Daybreak] = true;
        player.buffImmune[BuffID.StardustMinionBleed] = true;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<LostFragment3>(1);
        recipe.AddIngredient<LostFragment4>(1);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}