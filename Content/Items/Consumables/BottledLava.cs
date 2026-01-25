using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Consumables;

public class BottledLava : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;

        // Dust that will appear in these colors when the item with ItemUseStyleID.DrinkLiquid is used
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
        Item.value = Item.buyPrice(copper: 42);
        Item.buffType = BuffID.OnFire; 
        Item.buffTime = 6000; 
        Item.buffType = BuffID.Burning;
        Item.buffTime = 6000; 
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Bottle, 1);
        recipe.AddCondition(Condition.NearLava);
        recipe.Register();
    }
}