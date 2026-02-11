using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class HaemophilicLobotomySpike : ModItem
{

  

    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.LightRed;
        Item.value = 165000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Lobotometer can no longer decay until accessory is removed");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Converts Lobotometer decay rate into max life")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Does not stack with Unhygenic Lobotomy Spike")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<CrimceramicSheet>(15);
        recipe.AddIngredient(ItemID.CrimtaneBar, 5);
        recipe.AddIngredient(ItemID.SoulofNight, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
       
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {


        player.GetModPlayer<BloodSpike>().BloodySpiked = true;

        player.GetModPlayer<BaseSpike>().Spiked = true;

    }
   
}
public class BloodSpike : ModPlayer
{
    public bool BloodySpiked;

    public override void ResetEffects()
    {
        BloodySpiked = false;
    }

    public override void ModifyMaxStats(out StatModifier health, out StatModifier mana)
    {
        health = StatModifier.Default;
        mana = StatModifier.Default;

        if (BloodySpiked && !Player.GetModPlayer<GrossSpike>().GrossSpiked)
        {
            var loboPlayer = Player.GetModPlayer<LobotometerPlayer>();

            float healthBonus = loboPlayer.BaseDecayRate * loboPlayer.DecayRateMultiplier / 2;
            health.Flat += (int)healthBonus;
        }
    }
}