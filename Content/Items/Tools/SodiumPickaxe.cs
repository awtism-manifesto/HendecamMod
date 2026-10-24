using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Tools;

public class SodiumPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Melee;
        Item.damage = 16;

        Item.width = 35;
        Item.height = 35;
        Item.useTime = 11;
        Item.useAnimation = 20;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 3;
        Item.useTurn = true;

        Item.value = Item.buyPrice(gold: 1); // Buy this item for one gold - change gold to any coin and change the value to any number <= 100
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;

        Item.pick = 63; // How strong the pickaxe is, see https://terraria.wiki.gg/wiki/Pickaxe_power for a list of common values
        Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<RockSalt>(40);
        recipe.AddTile(TileID.Anvils);

        recipe.Register();
    }
}