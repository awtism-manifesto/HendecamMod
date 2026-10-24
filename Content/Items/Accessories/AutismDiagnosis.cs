using HendecamMod.Common.Systems;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

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
        recipe.AddIngredient<AutismOrb>(6);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
    public static readonly int AdditiveStupidDamageBonus = 12;
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
       
       
        player.endurance = 1f - 0.92f * (1f - player.endurance); 

        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();



        if (loboPlayer.Current <= loboPlayer.Max/3 || loboPlayer.Current == loboPlayer.Max)
        {

            player.GetDamage(DamageClass.Generic) += AdditiveStupidDamageBonus / 100f;
        }

    }
}