using System.Collections.Generic;

namespace HendecamMod.Content.Items.Tools;

public class GoldMultiaxe : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
    }

    public override void SetDefaults()
    {
        Item.damage = 19;
        Item.DamageType = DamageClass.Melee;
        Item.width = 50;
        Item.height = 50;
        Item.useTime = 10;
        Item.useAnimation = 25;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 5;
        Item.scale = 1.2f;
        Item.value = 138000;
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost = 1;
        Item.pick = 60;
        Item.useTurn = true;

        Item.axe = 18;
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
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
            Item.hammer = 75;
        }
        else
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 25;
            Item.pick = 60;
            Item.hammer = 0;
            Item.axe = 18;
        }

        return base.CanUseItem(player);
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
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

        recipe.AddIngredient(ItemID.GoldBar, 11);
        recipe.AddIngredient(ItemID.GoldAxe);
        recipe.AddIngredient(ItemID.GoldHammer);
        recipe.AddIngredient(ItemID.GoldPickaxe);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}