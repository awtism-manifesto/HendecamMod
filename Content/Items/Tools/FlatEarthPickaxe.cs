using System.Collections.Generic;
using HendecamMod.Content.Rarities;

namespace HendecamMod.Content.Items.Tools;

public class FlatEarthPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 80;
        Item.DamageType = DamageClass.Melee;
        Item.width = 50;
        Item.height = 50;
        Item.useTime = 6;
        Item.useAnimation = 11;
        Item.useTurn = true;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 6;

        Item.value = Item.buyPrice(gold: 69);
        Item.rare = ModContent.RarityType<HotPink>();
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost = 4;
        Item.pick = 225;
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

        recipe.AddIngredient<FragmentFlatEarth>(12);
        recipe.AddIngredient(ItemID.LunarBar, 10);

        recipe.AddTile(TileID.LunarCraftingStation);

        recipe.Register();
    }
}