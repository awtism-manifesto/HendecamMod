using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;

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
        player.GetArmorPenetration<StupidDamage>() += 4;

        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();

        if (loboPlayer.Current == loboPlayer.Max)
        { 
            player.GetModPlayer<BaseSpike>().Spiked = true;
        }
       

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