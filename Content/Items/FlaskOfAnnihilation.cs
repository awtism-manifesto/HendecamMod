using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria;

namespace HendecamMod.Content.Items;

public class FlaskOfAnnihilation: ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;

        ItemID.Sets.DrinkParticleColors[Type] =
        [
            new Color(55, 185, 255),
            new Color(15, 135,255),
            new Color(100, 222,255)
        ];
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Melee and Whip attacks inflict enemies with level 4 Rad Poisoning");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'They will suffer, as you have.'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void SetDefaults()
    {
        Item.UseSound = SoundID.Item3;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.useTurn = true;
        Item.useAnimation = 14;
        Item.useTime = 14;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.width = 14;
        Item.height = 24;
        Item.buffType = ModContent.BuffType<WeaponImbueAnnihilation>();
        Item.buffTime = Item.flaskTime;
        Item.value = Item.sellPrice(0, 0, 11, 33);
        Item.rare = ItemRarityID.Purple;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<FlaskOfFusion>()
            .AddIngredient<PromethiumOre>()
            .AddTile<CultistCyclotronPlaced>()
            .Register();
    }
}