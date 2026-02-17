using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class UnhygenicLobotomySpike : ModItem
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

        line = new TooltipLine(Mod, "Face", "Converts Lobotometer decay rate into stupid damage")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Does not stack with Haemophilic Lobotomy Spike")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<EbonceramicSheet>(15);
        recipe.AddIngredient(ItemID.DemoniteBar, 5);
        recipe.AddIngredient(ItemID.SoulofNight, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
       
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {


        player.GetModPlayer<GrossSpike>().GrossSpiked = true;

        player.GetModPlayer<BaseSpike>().Spiked = true;

    }
   
}
public class GrossSpike : ModPlayer
{



    public bool GrossSpiked;


    public override void ResetEffects()
    {
        GrossSpiked = false;
    }

    public override void PostUpdate()
    {
        if (GrossSpiked && !Player.GetModPlayer<BloodSpike>().BloodySpiked)
        {

            var loboPlayer = Player.GetModPlayer<LobotometerPlayer>();


            float damageBonus = loboPlayer.BaseDecayRate * loboPlayer.DecayRateMultiplier / 600;

            Player.GetDamage(ModContent.GetInstance<StupidDamage>()) += damageBonus;
        }
        else return;

    }


}