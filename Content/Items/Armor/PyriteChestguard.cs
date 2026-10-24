using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Body)]
public class PyriteChestguard : ModItem
{
    public static readonly int AdditiveDamageBonus = 4;

    public override void SetDefaults()
    {
        Item.defense = 6;
        Item.rare = ItemRarityID.Blue;
        Item.value = 67000;
    }

    public override void UpdateEquip(Player player)
    {
       

        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;
        player.endurance = 1f - 0.96f * (1f - player.endurance);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PyriteBar>(20);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}