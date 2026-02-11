using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class LobotomySpike : ModItem
{

  

    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = 22500;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Increases Stupid damage by 4%");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Lobotometer can no longer decay until accessory is removed")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
       
        recipe.AddIngredient<SteelBar>(8);
        recipe.AddIngredient(ItemID.TissueSample, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();

        recipe.AddIngredient<SteelBar>(8);
        recipe.AddIngredient(ItemID.ShadowScale, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public static readonly int AdditiveStupidDamageBonus = 4;
    public override void UpdateAccessory(Player player, bool hideVisual)
    {

        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 100f;



        player.GetModPlayer<BaseSpike>().Spiked = true;

    }
   
}
public class BaseSpike : ModPlayer
{



    public bool Spiked;

   
    public override void ResetEffects()
    {
        Spiked = false;
    }

    public override void PostUpdate()
    {
       

    }


}