using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Armor;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using static HendecamMod.Content.Items.Accessories.VapeDyes.Red40VapeDye;

namespace HendecamMod.Content.Items.Accessories.VapeDyes;

public class Green3VapeDye : ModItem
{
   

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = 60000;
      
    }
   

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
       
        var line = new TooltipLine(Mod, "Face", "Vapes now emit green vapor");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Can be combined with other vape dyes")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Works in inventory and vanity slots")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();


        recipe.AddIngredient(ItemID.GreenDye);
        recipe.AddIngredient<Polymer>(15);
        recipe.AddIngredient<CrudeOil>(25);
        recipe.AddTile(TileID.DyeVat);
        recipe.Register();
    }
    public override void UpdateInventory(Player player)
    {
        player.GetModPlayer<VapeDyePlayer>().Green3Active = true;
    }
    public override void UpdateVanity(Player player)
    {
        player.GetModPlayer<VapeDyePlayer>().Green3Active = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<VapeDyePlayer>().Green3Active = true;

    }
   
}