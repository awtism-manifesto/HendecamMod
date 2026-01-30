using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Rarities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Tools;

public class ThePurifier : ModItem
{
    public override void SetStaticDefaults()
    {

        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
    }
    public override void SetDefaults()
    {
        Item.damage = 33;
        Item.DamageType = DamageClass.Melee;
        Item.width = 50;
        Item.height = 50;
        Item.useTime = 6;
        Item.useAnimation = 20;
        Item.useTurn = true;

        Item.scale = 1.45f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 7;

        Item.value = Item.buyPrice(gold: 9); // Buy this item for one gold - change gold to any coin and change the value to any number <= 100
        Item.rare = ItemRarityID.Pink;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost = 2;
        Item.pick = 155;

        Item.axe = 26;
        Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
    }
    public override bool AltFunctionUse(Player player)
    {


        return true;


    }
    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.pick = 0;

            Item.axe = 0;
            Item.hammer = 105;

        }
        else
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 7;
            Item.useAnimation = 20;
            Item.pick = 155;
            Item.hammer = 0;
            Item.axe = 26;
        }

        return base.CanUseItem(player);
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Left click for pickaxe and axe functionality");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Right click for hammer functionality")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind("Gelpick", out ModItem Gelpick))
        {
            recipe = CreateRecipe();
            recipe.AddIngredient<PurifiedSalt>(90);
            recipe.AddIngredient(Gelpick.Type);
            recipe.AddIngredient<KingslayerMultiaxe>();
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
        else
        {
            recipe = CreateRecipe();
            recipe.AddIngredient<PurifiedSalt>(108);
            recipe.AddIngredient<KingslayerMultiaxe>();

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();


        }

    }
}