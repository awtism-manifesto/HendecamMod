using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Consumables;

public class AdderallPill : ModItem
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
        Item.useAnimation = 10;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.UseSound = SoundID.Item3;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.buyPrice(copper: 4);
        Item.buffType = BuffID.Swiftness;
        Item.buffTime = 18000;
    }
}