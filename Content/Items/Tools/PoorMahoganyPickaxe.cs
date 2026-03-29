using System.Collections.Generic;

namespace HendecamMod.Content.Items.Tools;

public class PoorMahoganyPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 4;
        Item.DamageType = DamageClass.Melee;
        Item.width = 35;
        Item.height = 35;
        Item.useTime = 19;
        Item.useAnimation = 23;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 1;
        Item.useTurn = true;

        Item.value = Item.buyPrice(gold: 0);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;

        Item.pick = 25;
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Why did you make ts");
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
        recipe.AddIngredient<PoorMahogany>(14);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}