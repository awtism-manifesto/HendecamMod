using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Consumables;

public class CandyHeart : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;

        ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
            new Color(92, 42, 9),
            new Color(51, 24, 6),
            new Color(122, 51, 4)
        };
    }

    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 32;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.useAnimation = 10;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.UseSound = SoundID.Item3;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.rare = ItemRarityID.LightPurple;
        Item.value = Item.buyPrice(silver: 3);
        Item.buffType = BuffID.WellFed3;
        Item.buffTime = 300;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "A small candy given by santa to his most loyal and skilled elf soldiers");
        tooltips.Add(line);
        var line2 = new TooltipLine(Mod, "Face", "Also just so happens to be DELICIOUS");
        tooltips.Add(line2);
        foreach (var l in tooltips)
        {
            if (l.Name.EndsWith(":RemoveMe"))
            {
                l.Hide();
            }
        }
    }
}


