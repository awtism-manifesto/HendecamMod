using HendecamMod.Common.Systems;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

public class TintedLenses : ModItem
{
    // By declaring these here, changing the values will alter the effect, and the tooltip
    public static readonly int ArmorPenetration = 10;

    public static readonly int CritBonus = 5;

    // Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Pink;
        Item.value = 236000;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Sunglasses);
        recipe.AddIngredient(ItemID.SoulofLight, 5);
        recipe.AddIngredient(ItemID.SoulofSight, 5);
        recipe.AddTile<CobaltWorkBenchPlaced>();
        recipe.Register();
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Increases armor penetration by 10");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+5% crit chance")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "+50% increased Lobotometer decay rate")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
       

        player.GetCritChance(DamageClass.Generic) += CritBonus;
        player.GetArmorPenetration(DamageClass.Generic) += ArmorPenetration;

        var loboDecay = player.GetModPlayer<LobotometerPlayer>();
        loboDecay.DecayRateMultiplier *= 1.5f;

    }
}