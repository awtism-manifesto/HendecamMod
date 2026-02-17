using System.Collections.Generic;
using HendecamMod.Content.Items.Consumables;

namespace HendecamMod.Content.Items;

public class BalkanRagePotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;

        ItemID.Sets.DrinkParticleColors[Type] = new Color[3]
        {
            new Color(195, 32, 102),
            new Color(17, 155, 11),
            new Color(91, 21, 224)
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
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.buyPrice(silver: 20);
        Item.buffType = ModContent.BuffType<Buffs.BalkanRage>(); 
        Item.buffTime = 21600;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "those who know...")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<LeadskinPotion>();
        recipe.AddIngredient<WeedLeaves>();
        recipe.AddIngredient<PurifiedSalt>();
        recipe.AddIngredient<UraniumOre>();
        recipe.AddTile(TileID.Bottles);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient<LeadskinPotion>();
        recipe.AddIngredient<WeedLeaves>();
        recipe.AddIngredient<PurifiedSalt>();
        recipe.AddIngredient<UraniumOre>();
        recipe.AddTile(TileID.AlchemyTable);
        recipe.Register();
    }
}