namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class PoorMahoganyHelmet : ModItem
{
    public override void SetDefaults()
    {
        Item.defense = 1;
        Item.rare = ItemRarityID.White;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PoorMahogany>(25);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Immune to the Poisoned debuff";
        player.buffImmune[BuffID.Poisoned] = true;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<PoorMahoganyChestplate>() && legs.type == ModContent.ItemType<PoorMahoganyLeggings>();
    }
}