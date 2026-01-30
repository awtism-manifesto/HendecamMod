
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class FirstHalf : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Grants immunity to Penetrated, Blood Butchered, Midas, and Dryad's Bane");
        tooltips.Add(line);
    }
    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.BoneJavelin] = true;
        player.buffImmune[BuffID.BloodButcherer] = true;
        player.buffImmune[BuffID.Midas] = true;
        player.buffImmune[BuffID.DryadsWardDebuff] = true;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<LostFragment1>(1);
        recipe.AddIngredient<LostFragment2>(1);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}