using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Shield)] 
public class DefenseShield : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32; 
        Item.height = 32; 
        Item.rare = ItemRarityID.Blue; 
        Item.value = 27500;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.defense = 2;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddRecipeGroup("IronBar", 15);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}