
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class TwoInOne : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Grants immunity to Slimed, Sparkle Slime, Wet, Lovestruck, and Stinky");
        tooltips.Add(line);
    }
    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.Slimed] = true;
        player.buffImmune[BuffID.GelBalloonBuff] = true;
        player.buffImmune[BuffID.Wet] = true;
        player.buffImmune[BuffID.Lovestruck] = true;
        player.buffImmune[BuffID.Stinky] = true;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<Shampoo>(1);
        recipe.AddIngredient<Conditioner>(1);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}