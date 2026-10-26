using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Legs)]
public class MantiusLeggings : ModItem
{
    public static readonly int SpeedBonus = 6;

   
    public static LocalizedText SetBonusText { get; private set; }

    

    public override void SetDefaults()
    {
        Item.width = 22; // Width of the item
        Item.height = 18; // Height of the item
        Item.value = Item.sellPrice(gold: 11); // How many coins the item is worth
        Item.rare = ItemRarityID.LightRed; // The rarity of the item
        Item.defense = 9; // The amount of defense the item will give when equipped
        
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ItemType<MantiusChestplate>() && head.type == ItemType<LycopiteFedora>();
    }

    public override void UpdateEquip(Player player)
    {
        

        player.moveSpeed += SpeedBonus / 100f;
        player.runAcceleration *= 1.06f;
        player.GetDamage(DamageClass.Generic) += SpeedBonus / 100f;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<MantiusBar>(20);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
    }
}