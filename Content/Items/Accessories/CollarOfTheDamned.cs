using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Beard)]
public class CollarOfTheDamned : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 22; // Width of the item
        Item.height = 18; // Height of the item
        Item.value = Item.sellPrice(silver: 666); // How many coins the item is worth
        Item.rare = ItemRarityID.Red; // The rarity of the item
        Item.accessory = true;
        Item.vanity = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.PinkDye);
        recipe.AddIngredient(ItemID.CrabBanner);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
}