using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class PitVipers : ModItem
{

    public static readonly int AdditiveStupidDamageBonus = 12;
    public static readonly int StupidCritBonus = 12;
    public static readonly int StupidAttackSpeedBonus = 12;
    public static readonly int StupidArmorPenetration = 12;

    // Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AdditiveStupidDamageBonus);

    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Red;
        Item.value = 1225000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "12% incrased damage, crit chance, and attack speed as well as 12 armor penetration for the Stupid class");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+175 Max Lobotometer and 105% increased lobotometer decay rate")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "'The unofficial logo of frat boys, the stupidest demographic in existence'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<TintedLenses>();
        recipe.AddIngredient<TrenboloneAcetate>();
        recipe.AddIngredient<MediocreGrades>();
        recipe.AddIngredient<FragmentFlatEarth>(10);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 100f;
        player.GetAttackSpeed<StupidDamage>() += StupidAttackSpeedBonus / 100f;
        player.GetArmorPenetration<StupidDamage>() += StupidArmorPenetration;
        player.GetCritChance<StupidDamage>() += StupidCritBonus;
        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
        loboPlayer.MaxBonus += 175f; // This is safe - it resets every frame in ResetEffects


        var loboDecay = player.GetModPlayer<LobotometerPlayer>();
        loboDecay.DecayRateMultiplier *= 2.05f;

    }
}