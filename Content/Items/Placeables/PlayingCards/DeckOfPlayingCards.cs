using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using Terraria.GameContent.ItemDropRules;

namespace HendecamMod.Content.Items.Placeables.PlayingCards;

public class DeckOfPlayingCards : ModItem
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

    public override bool CanRightClick()
    {
        return true;
    }

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<DeckOfPlayingCards>()));
        itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
            ItemType<AceClubs>(),
            ItemType<AceDiamonds>(),
            ItemType<AceHearts>(),
            ItemType<AceSpades>(),
            ItemType<ColoredJoker>(),
            ItemType<DarkJoker>(),
            ItemType<EightClubs>(),
            ItemType<EightDiamonds>(),
            ItemType<EightHearts>(),
            ItemType<EightSpades>(),
            ItemType<FiveClubs>(),
            ItemType<FiveDiamonds>(),
            ItemType<FiveHearts>(),
            ItemType<FiveSpades>(),
            ItemType<FourClubs>(),
            ItemType<FourDiamonds>(),
            ItemType<FourHearts>(),
            ItemType<FourSpades>(),
            ItemType<JackClubs>(),
            ItemType<JackDiamonds>(),
            ItemType<JackHearts>(),
            ItemType<JackSpades>(),
            ItemType<KingClubs>(),
            ItemType<KingDiamonds>(),
            ItemType<KingHearts>(),
            ItemType<KingSpades>(),
            ItemType<NineClubs>(),
            ItemType<NineDiamonds>(),
            ItemType<NineHearts>(),
            ItemType<NineSpades>(),
            ItemType<QueenClubs>(),
            ItemType<QueenDiamonds>(),
            ItemType<QueenHearts>(),
            ItemType<QueenSpades>(),
            ItemType<SevenClubs>(),
            ItemType<SevenDiamonds>(),
            ItemType<SevenHearts>(),
            ItemType<SevenSpades>(),
            ItemType<SixClubs>(),
            ItemType<SixDiamonds>(),
            ItemType<SixHearts>(),
            ItemType<SixSpades>(),
            ItemType<TenClubs>(),
            ItemType<TenDiamonds>(),
            ItemType<TenHearts>(),
            ItemType<TenSpades>(),
            ItemType<ThreeClubs>(),
            ItemType<ThreeDiamonds>(),
            ItemType<ThreeHearts>(),
            ItemType<ThreeSpades>(),
            ItemType<TwoClubs>(),
            ItemType<TwoDiamonds>(),
            ItemType<TwoHearts>(),
            ItemType<TwoSpades>()));
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