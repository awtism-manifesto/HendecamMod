using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

public class TacticalLaserSight : ModItem
{
    // By declaring these here, changing the values will alter the effect, and the tooltip

    public static readonly int AdditiveRangedDamageBonus = 8;

    public static readonly int RangedCritBonus = 12;

    // Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.LightPurple;
        Item.value = 315000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "8% increased ranged damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "12% increased ranged crit chance")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Lens, 2);
        recipe.AddIngredient<PlutoniumBar>(9);
        recipe.AddIngredient<ImprovisedLaserSight>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        
        player.GetDamage(DamageClass.Ranged) += AdditiveRangedDamageBonus / 100f;

        player.GetCritChance(DamageClass.Ranged) += RangedCritBonus;
    }
}