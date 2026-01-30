
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class IdeaGenerator : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Grants immunity to Creative Shock");
        tooltips.Add(line);
    }
    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.NoBuilding] = true;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<Iphone>();

        recipe.AddIngredient(ItemID.Book, 1);
        recipe.AddTile(TileID.HeavyWorkBench);
        recipe.Register();
    }
}