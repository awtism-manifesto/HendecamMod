using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Armor;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories.VapeDyes;

public class Red40VapeDye : ModItem
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
       
        var line = new TooltipLine(Mod, "Face", "Vapes now emit red vapor");
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


        recipe.AddIngredient(ItemID.RedDye);
        recipe.AddIngredient<Polymer>(15);
        recipe.AddIngredient<CrudeOil>(25);
        recipe.AddTile(TileID.DyeVat);
        recipe.Register();
    }
    public override void UpdateInventory(Player player)
    {
        player.GetModPlayer<VapeDyePlayer>().Red40Active = true;
    }
    public override void UpdateVanity(Player player)
    {
        player.GetModPlayer<VapeDyePlayer>().Red40Active = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<VapeDyePlayer>().Red40Active = true;

    }
    public class VapeDyePlayer : ModPlayer
    {
        // Track which dyes are active with simple bools
        public bool Red40Active;
        public bool Yellow5Active;
        public bool Blue1Active;
        public bool Green3Active;

        public override void ResetEffects()
        {
            // Reset all bools - they'll be set again by items in UpdateInventory/UpdateAccessory
            Red40Active = false;
            Yellow5Active = false;
            Blue1Active = false;
            Green3Active = false;
        }

        // Helper method to get currently active dyes
        public List<VapeDye> GetActiveDyes()
        {
            var dyes = new List<VapeDye>();

            if (Red40Active) dyes.Add(VapeDyes.Red40);
            if (Yellow5Active) dyes.Add(VapeDyes.Yellow5);
            if (Blue1Active) dyes.Add(VapeDyes.Blue1);
            if (Green3Active) dyes.Add(VapeDyes.Green3);

            return dyes;
        }
    }
}