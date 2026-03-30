using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Tools;

public class PromethiumPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 90;
        Item.DamageType = DamageClass.Melee;
        Item.width = 38;
        Item.height = 38;
        Item.useTime = 6;
        Item.useAnimation = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 1;
        Item.useTurn = true;
        Item.value = Item.buyPrice(gold: 8);
        Item.rare = ItemRarityID.Purple;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.pick = 260;
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "");
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
        recipe.AddIngredient<PromethiumBar>(18);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }
}