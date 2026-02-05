using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

public class ImprovisedLaserSight : ModItem
{

    public static readonly int AdditiveRangedDamageBonus = 4;

    public static readonly int RangedCritBonus = 7;

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Green;
        Item.value = 85000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "4% increased ranged damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "7% increased ranged crit chance")
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

        recipe.AddIngredient<UraniumBar>(6);
        recipe.AddIngredient<Polymer>(10);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Ranged) += AdditiveRangedDamageBonus / 100f;
        player.GetCritChance(DamageClass.Ranged) += RangedCritBonus;
    }
}