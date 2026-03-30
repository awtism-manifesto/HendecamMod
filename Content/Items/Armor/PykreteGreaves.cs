namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Legs)]
public class PykreteGreaves : ModItem
{
    public override void SetDefaults()
    {
        Item.defense = 4;
        Item.rare = ItemRarityID.White;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<Pykrete>(35);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "+5% damage reduction, immune to cold debuffs";
        player.endurance = 1f - 0.95f * (1f - player.endurance);
        player.buffImmune[BuffID.Chilled] = true;
        player.buffImmune[BuffID.Frozen] = true;
        player.buffImmune[BuffID.Frostburn] = true;
        player.buffImmune[BuffID.Frostburn2] = true;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<PykreteChestplate>() && head.type == ModContent.ItemType<PykreteHelmet>();
    }
}