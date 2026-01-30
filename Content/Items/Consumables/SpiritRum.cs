using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Consumables;

public class SpiritRum : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;

        ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
            new Color(240, 240, 240),
            new Color(200, 200, 200),
            new Color(140, 140, 140)
        };
    }

    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.useTurn = true;
        Item.UseSound = SoundID.Item3;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.rare = ItemRarityID.Blue;
        Item.buffType = BuffID.NoBuilding;
        Item.buffTime = 69420;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Ale, 1);
        recipe.AddIngredient(ItemID.DirtBlock, 1);
        recipe.AddTile(TileID.Bottles);
        recipe.Register();
        if (ModLoader.TryGetMod("ThoriumMod", out Mod MagThorium) && MagThorium.TryFind("SpiritDroplet", out ModItem SpiritDroplet))
        {
            recipe = CreateRecipe(10);
            recipe.AddIngredient(ItemID.Ale, 10);
            recipe.AddIngredient(SpiritDroplet.Type);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}