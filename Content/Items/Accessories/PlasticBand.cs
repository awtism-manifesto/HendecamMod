using HendecamMod.Common.Systems;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Accessories;


public class PlasticBand : ModItem
{
    

    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 20;
        Item.value = 2250;
        Item.rare = ItemRarityID.Blue;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "+50 max Braincells");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'Are you headed to the water park or the psych ward?'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
       
        recipe.AddIngredient<PlasticScrap>(14);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
        loboPlayer.TemporaryBonus += 50f;
    }
}