using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Placeables.Uno;

public class AnUnoDeck : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.maxStack = 1;
        Item.width = 24;
        Item.height = 24;
        Item.rare = ItemRarityID.Expert;
        Item.value = 300000;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Right click in the inventory to draw a card");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'Everyone has uno dipshit, it came free with your fucking xbox'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);




    }
    public override bool CanRightClick()
    {
        return true;
    }
    int draw = Main.rand.Next();
    public override void ModifyItemLoot(ItemLoot itemLoot)
    {

        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AnUnoDeck>(), 1));
        itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
ModContent.ItemType<RedZero>(),
ModContent.ItemType<RedOne>(),
ModContent.ItemType<RedOne>(),
ModContent.ItemType<RedTwo>(),
ModContent.ItemType<RedTwo>(),
ModContent.ItemType<RedThree>(),
ModContent.ItemType<RedThree>(),
ModContent.ItemType<RedFour>(),
ModContent.ItemType<RedFour>(),
ModContent.ItemType<RedFive>(),
ModContent.ItemType<RedFive>(),
ModContent.ItemType<RedSix>(),
ModContent.ItemType<RedSix>(),
ModContent.ItemType<RedSeven>(),
ModContent.ItemType<RedSeven>(),
ModContent.ItemType<RedEight>(),
ModContent.ItemType<RedEight>(),
ModContent.ItemType<RedNine>(),
ModContent.ItemType<RedNine>(),
ModContent.ItemType<RedSkip>(),
ModContent.ItemType<RedSkip>(),
ModContent.ItemType<RedReverse>(),
ModContent.ItemType<RedReverse>(),
ModContent.ItemType<RedDrawTwo>(),
ModContent.ItemType<RedDrawTwo>(),
ModContent.ItemType<RedWild>(),
ModContent.ItemType<RedDrawFour>(),
ModContent.ItemType<YellowZero>(),
ModContent.ItemType<YellowOne>(),
ModContent.ItemType<YellowOne>(),
ModContent.ItemType<YellowTwo>(),
ModContent.ItemType<YellowTwo>(),
ModContent.ItemType<YellowThree>(),
ModContent.ItemType<YellowThree>(),
ModContent.ItemType<YellowFour>(),
ModContent.ItemType<YellowFour>(),
ModContent.ItemType<YellowFive>(),
ModContent.ItemType<YellowFive>(),
ModContent.ItemType<YellowSix>(),
ModContent.ItemType<YellowSix>(),
ModContent.ItemType<YellowSeven>(),
ModContent.ItemType<YellowSeven>(),
ModContent.ItemType<YellowEight>(),
ModContent.ItemType<YellowEight>(),
ModContent.ItemType<YellowNine>(),
ModContent.ItemType<YellowNine>(),
ModContent.ItemType<YellowSkip>(),
ModContent.ItemType<YellowSkip>(),
ModContent.ItemType<YellowReverse>(),
ModContent.ItemType<YellowReverse>(),
ModContent.ItemType<YellowDrawTwo>(),
ModContent.ItemType<YellowDrawTwo>(),
ModContent.ItemType<YellowWild>(),
ModContent.ItemType<YellowDrawFour>(),
ModContent.ItemType<GreenZero>(),
ModContent.ItemType<GreenOne>(),
ModContent.ItemType<GreenOne>(),
ModContent.ItemType<GreenTwo>(),
ModContent.ItemType<GreenTwo>(),
ModContent.ItemType<GreenThree>(),
ModContent.ItemType<GreenThree>(),
ModContent.ItemType<GreenFour>(),
ModContent.ItemType<GreenFour>(),
ModContent.ItemType<GreenFive>(),
ModContent.ItemType<GreenFive>(),
ModContent.ItemType<GreenSix>(),
ModContent.ItemType<GreenSix>(),
ModContent.ItemType<GreenSeven>(),
ModContent.ItemType<GreenSeven>(),
ModContent.ItemType<GreenEight>(),
ModContent.ItemType<GreenEight>(),
ModContent.ItemType<GreenNine>(),
ModContent.ItemType<GreenNine>(),
ModContent.ItemType<GreenSkip>(),
ModContent.ItemType<GreenSkip>(),
ModContent.ItemType<GreenReverse>(),
ModContent.ItemType<GreenReverse>(),
ModContent.ItemType<GreenDrawTwo>(),
ModContent.ItemType<GreenDrawTwo>(),
ModContent.ItemType<GreenWild>(),
ModContent.ItemType<GreenDrawFour>(),
ModContent.ItemType<BlueZero>(),
ModContent.ItemType<BlueOne>(),
ModContent.ItemType<BlueOne>(),
ModContent.ItemType<BlueTwo>(),
ModContent.ItemType<BlueTwo>(),
ModContent.ItemType<BlueThree>(),
ModContent.ItemType<BlueThree>(),
ModContent.ItemType<BlueFour>(),
ModContent.ItemType<BlueFour>(),
ModContent.ItemType<BlueFive>(),
ModContent.ItemType<BlueFive>(),
ModContent.ItemType<BlueSix>(),
ModContent.ItemType<BlueSix>(),
ModContent.ItemType<BlueSeven>(),
ModContent.ItemType<BlueSeven>(),
ModContent.ItemType<BlueEight>(),
ModContent.ItemType<BlueEight>(),
ModContent.ItemType<BlueNine>(),
ModContent.ItemType<BlueNine>(),
ModContent.ItemType<BlueSkip>(),
ModContent.ItemType<BlueSkip>(),
ModContent.ItemType<BlueReverse>(),
ModContent.ItemType<BlueReverse>(),
ModContent.ItemType<BlueDrawTwo>(),
ModContent.ItemType<BlueDrawTwo>(),
ModContent.ItemType<BlueWild>(),
ModContent.ItemType<BlueDrawFour>()));
    }
    public override void UpdateInventory(Player player)
    {
        draw = Main.rand.Next();
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<Paper>(110);
        recipe.AddIngredient(ItemID.RedDye, 25);
        recipe.AddIngredient(ItemID.YellowDye, 25);
        recipe.AddIngredient(ItemID.GreenDye, 25);
        recipe.AddIngredient(ItemID.BlueDye, 25);
        recipe.AddIngredient(ItemID.BlackDye, 10);
        recipe.Register();
    }
}

