using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class AutismDiagnosis : ModItem
{

   

   

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Lime;
        Item.value = 400000;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<Paper>();
        recipe.AddIngredient<AutismOrb>(2);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "8% increased damage reduction");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Increases stupid damage as your Lobotometer goes up, up to a 20% boost at max")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "'Woah, this is worthless!'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }
   
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
       
       
        player.endurance = 1f - 0.92f * (1f - player.endurance); 

        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
      

        
        float lobotometerPercent = loboPlayer.Current / loboPlayer.Max;
        float damageBonus = lobotometerPercent * 0.20f;

        player.GetDamage<StupidDamage>() += damageBonus;

    }
}