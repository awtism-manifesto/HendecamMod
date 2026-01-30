namespace HendecamMod.Content.Items.Consumables;

public class LeadskinPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;

        ItemID.Sets.DrinkParticleColors[Type] = new Color[3]
        {
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
        Item.buffType = ModContent.BuffType<Buffs.LeadPoisoningDebuff>();
        Item.buffTime = 25200;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BottledWater);
        recipe.AddIngredient(ItemID.LeadBar);
        recipe.AddIngredient(ItemID.Daybloom);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BottledWater);
        recipe.AddIngredient(ItemID.IronBar);
        recipe.AddIngredient(ItemID.Daybloom);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}