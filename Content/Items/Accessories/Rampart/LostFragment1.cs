
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class LostFragment1 : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Grants immunity to Penetrated and Blood Butchered");
        tooltips.Add(line);
    }
    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.BloodButcherer] = true;
        player.buffImmune[BuffID.BoneJavelin] = true;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BloodButcherer);
        recipe.AddIngredient(ItemID.BoneJavelin);
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.Tables);
        recipe.Register();
    }
}