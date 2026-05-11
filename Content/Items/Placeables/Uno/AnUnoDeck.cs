using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using Terraria.GameContent.ItemDropRules;

namespace HendecamMod.Content.Items.Placeables.Uno;

public class AnUnoDeck : ModItem
{
    int draw = Main.rand.Next();

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

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<AnUnoDeck>()));
        itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
            ItemType<RedZero>(),
            ItemType<RedOne>(),
            ItemType<RedOne>(),
            ItemType<RedTwo>(),
            ItemType<RedTwo>(),
            ItemType<RedThree>(),
            ItemType<RedThree>(),
            ItemType<RedFour>(),
            ItemType<RedFour>(),
            ItemType<RedFive>(),
            ItemType<RedFive>(),
            ItemType<RedSix>(),
            ItemType<RedSix>(),
            ItemType<RedSeven>(),
            ItemType<RedSeven>(),
            ItemType<RedEight>(),
            ItemType<RedEight>(),
            ItemType<RedNine>(),
            ItemType<RedNine>(),
            ItemType<RedSkip>(),
            ItemType<RedSkip>(),
            ItemType<RedReverse>(),
            ItemType<RedReverse>(),
            ItemType<RedDrawTwo>(),
            ItemType<RedDrawTwo>(),
            ItemType<RedWild>(),
            ItemType<RedDrawFour>(),
            ItemType<YellowZero>(),
            ItemType<YellowOne>(),
            ItemType<YellowOne>(),
            ItemType<YellowTwo>(),
            ItemType<YellowTwo>(),
            ItemType<YellowThree>(),
            ItemType<YellowThree>(),
            ItemType<YellowFour>(),
            ItemType<YellowFour>(),
            ItemType<YellowFive>(),
            ItemType<YellowFive>(),
            ItemType<YellowSix>(),
            ItemType<YellowSix>(),
            ItemType<YellowSeven>(),
            ItemType<YellowSeven>(),
            ItemType<YellowEight>(),
            ItemType<YellowEight>(),
            ItemType<YellowNine>(),
            ItemType<YellowNine>(),
            ItemType<YellowSkip>(),
            ItemType<YellowSkip>(),
            ItemType<YellowReverse>(),
            ItemType<YellowReverse>(),
            ItemType<YellowDrawTwo>(),
            ItemType<YellowDrawTwo>(),
            ItemType<YellowWild>(),
            ItemType<YellowDrawFour>(),
            ItemType<GreenZero>(),
            ItemType<GreenOne>(),
            ItemType<GreenOne>(),
            ItemType<GreenTwo>(),
            ItemType<GreenTwo>(),
            ItemType<GreenThree>(),
            ItemType<GreenThree>(),
            ItemType<GreenFour>(),
            ItemType<GreenFour>(),
            ItemType<GreenFive>(),
            ItemType<GreenFive>(),
            ItemType<GreenSix>(),
            ItemType<GreenSix>(),
            ItemType<GreenSeven>(),
            ItemType<GreenSeven>(),
            ItemType<GreenEight>(),
            ItemType<GreenEight>(),
            ItemType<GreenNine>(),
            ItemType<GreenNine>(),
            ItemType<GreenSkip>(),
            ItemType<GreenSkip>(),
            ItemType<GreenReverse>(),
            ItemType<GreenReverse>(),
            ItemType<GreenDrawTwo>(),
            ItemType<GreenDrawTwo>(),
            ItemType<GreenWild>(),
            ItemType<GreenDrawFour>(),
            ItemType<BlueZero>(),
            ItemType<BlueOne>(),
            ItemType<BlueOne>(),
            ItemType<BlueTwo>(),
            ItemType<BlueTwo>(),
            ItemType<BlueThree>(),
            ItemType<BlueThree>(),
            ItemType<BlueFour>(),
            ItemType<BlueFour>(),
            ItemType<BlueFive>(),
            ItemType<BlueFive>(),
            ItemType<BlueSix>(),
            ItemType<BlueSix>(),
            ItemType<BlueSeven>(),
            ItemType<BlueSeven>(),
            ItemType<BlueEight>(),
            ItemType<BlueEight>(),
            ItemType<BlueNine>(),
            ItemType<BlueNine>(),
            ItemType<BlueSkip>(),
            ItemType<BlueSkip>(),
            ItemType<BlueReverse>(),
            ItemType<BlueReverse>(),
            ItemType<BlueDrawTwo>(),
            ItemType<BlueDrawTwo>(),
            ItemType<BlueWild>(),
            ItemType<BlueDrawFour>()));
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