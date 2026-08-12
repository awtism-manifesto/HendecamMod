using System.Collections.Generic;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Consumables;

public class TerminalLucidityPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[3]
        {
            new Color(85, 32, 102),
            new Color(157, 55, 191),
            new Color(191, 81, 224)
        };
    }

    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.useAnimation = 10;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.UseSound = SoundID.Item3;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.buyPrice(silver: 3);
        Item.buffType = BuffType<TerminalLucidity>();
        Item.buffTime = 64800; // Ticks
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Drastically reduces the Lobotometer's impact on your vision when on low HP");
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
        recipe.AddIngredient<LucidityPotion>();
        recipe.AddIngredient<PurifiedSalt>();
        recipe.AddIngredient(ItemID.Bone);
        recipe.AddTile(TileID.Bottles);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient<LucidityPotion>();
        recipe.AddIngredient<PurifiedSalt>();
        recipe.AddIngredient(ItemID.Bone);
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();

       
    }
}