using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Accessories;

public class MorbiumCube : ModItem
{
    public static readonly int AdditiveDamageBonus = 8;

    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 26;
        Item.rare = ItemRarityID.Yellow; 
        Item.value = 303000;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.defense = 25;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 108f;
        player.maxFallSpeed = player.maxFallSpeed * -1.25f;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Makes you fall in reverse");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Who the fuck turned off gravity?")
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
        recipe.AddIngredient<CubicMold>();
        recipe.AddIngredient<Placeables.MorbiumBar>(12);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}