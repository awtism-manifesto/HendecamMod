using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Tools;

public class PlatinumMultiaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 20;
        Item.DamageType = DamageClass.Melee;
        Item.width = 50;
        Item.height = 50;
        Item.useTime = 10;
        Item.useAnimation = 24;
        Item.useTurn = true;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 5;
        Item.scale = 1.25f;
        Item.value = 162000;
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost = 1;
        Item.pick = 61;

        Item.axe = 19;
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
            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.pick = 0;

            Item.axe = 0;
            Item.hammer = 77;
        }
        else
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 24;
            Item.pick = 61;
            Item.hammer = 0;
            Item.axe = 19;
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

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient(ItemID.PlatinumBar, 11);
        recipe.AddIngredient(ItemID.PlatinumAxe);
        recipe.AddIngredient(ItemID.PlatinumHammer);
        recipe.AddIngredient(ItemID.PlatinumPickaxe);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}