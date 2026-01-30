using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class Ushanka : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Grants immunity to Chilled, Frozen, Frostbite, Obstructed, and Electrified");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.Chilled] = true;
        player.buffImmune[BuffID.Frozen] = true;
        player.buffImmune[BuffID.Frostburn2] = true;
        player.buffImmune[BuffID.Obstructed] = true;
        player.buffImmune[BuffID.Electrified] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.HandWarmer);
        recipe.AddIngredient(ItemID.EngineeringHelmet);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}