using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

public class MarksmanLaserSight : ModItem
{

    public static readonly int AdditiveRangedDamageBonus = 16;

    public static readonly int RangedCritBonus = 10;

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Red;
        Item.value = 1010000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "16% increased ranged damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "10% increased ranged crit chance")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        foreach (var l in tooltips)
        {
            if (l.Name.EndsWith(":RemoveMe"))
            {
                l.Hide();
            }
        }
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BlackLens, 2);
        recipe.AddIngredient<AstatineBar>(12);
        recipe.AddIngredient<TacticalLaserSight>();
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Ranged) += AdditiveRangedDamageBonus / 100f;
        player.GetCritChance(DamageClass.Ranged) += RangedCritBonus;
    }
}