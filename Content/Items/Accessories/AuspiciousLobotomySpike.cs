using HendecamMod.Common.Systems;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

public class AuspiciousLobotomySpike : ModItem
{

  

    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.LightRed;
        Item.value = 150000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Lobotometer can no longer decay until accessory is removed");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Converts Lobotometer decay rate into defense")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Does not stack with Haemophilic or Unhygenic Lobotomy Spike")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<PearlceramicSheet>(15);
        recipe.AddIngredient<MintalBar>(5);
        recipe.AddIngredient(ItemID.SoulofLight, 5);
        recipe.AddIngredient<LobotomySpike>();
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
       
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();

        player.GetModPlayer<HallowSpike>().HallowSpiked = true;

        if (loboPlayer.Current == loboPlayer.Max)
        {
            player.GetModPlayer<BaseSpike>().Spiked = true;
        }

    }
   
}
public class HallowSpike : ModPlayer
{



    public bool HallowSpiked;


    public override void ResetEffects()
    {
        HallowSpiked = false;
    }

    public override void UpdateEquips()
    {
        if (HallowSpiked && !Player.GetModPlayer<BloodSpike>().BloodySpiked && !Player.GetModPlayer<GrossSpike>().GrossSpiked)
        {

            var loboPlayer = Player.GetModPlayer<LobotometerPlayer>();


            float defenseBonus = (loboPlayer.DecayRateMultiplier * 6.67f) + (loboPlayer.Max * 0.015f);

            int defenseRounded = (int)defenseBonus;

            Player.statDefense += defenseRounded;
        }
      

    }


}