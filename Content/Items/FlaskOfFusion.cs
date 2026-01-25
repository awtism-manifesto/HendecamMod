using HendecamMod.Content.Buffs;
using Microsoft.Xna.Framework;
using HendecamMod.Content.Items;
using Newtonsoft.Json.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace HendecamMod.Content.Items;


public class FlaskOfFusion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;

        ItemID.Sets.DrinkParticleColors[Type] = [
            new Color(255, 25, 55),
            new Color(255, 115, 150),
            new Color(255, 85, 160)
        ];
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Melee and Whip attacks inflict enemies with level 2 Rad Poisoning");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);



        // Here we will hide all tooltips whose title end with ':RemoveMe'
        // One like that is added at the start of this method
        foreach (var l in tooltips)
        {
            if (l.Name.EndsWith(":RemoveMe"))
            {
                l.Hide();
            }
        }

        // Another method of hiding can be done if you want to hide just one line.
        // tooltips.FirstOrDefault(x => x.Mod == "ExampleMod" && x.Name == "Verbose:RemoveMe")?.Hide();
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
        Item.buffType = ModContent.BuffType<WeaponImbueFusion>();
        Item.buffTime = Item.flaskTime;
        Item.value = Item.sellPrice(0, 0, 3, 33);
        Item.rare = ItemRarityID.Red;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<FlaskOfFission>()
            .AddIngredient<AstatineOre>()
            .AddTile(TileID.ImbuingStation)
            .Register();
    }
}