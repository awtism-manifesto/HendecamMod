using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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

        Item.value = Item.buyPrice(gold: 0); // Buy this item for one gold - change gold to any coin and change the value to any number <= 100
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;

        Item.pick = 25; // How strong the pickaxe is, see https://terraria.wiki.gg/wiki/Pickaxe_power for a list of common values
        Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Why did you make ts");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<PoorMahogany>(14);
        recipe.AddTile(TileID.WorkBenches);

        recipe.Register();

    }
}