using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Head)]
public class ArchangelHelmet : ModItem
{
    public override void SetStaticDefaults()
    {
        

        ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
      
    }


    public override void UpdateEquip(Player player)
    {
        player.statLifeMax2 = (int)(player.statLifeMax2 * 1.05) + 35;
        player.GetCritChance(DamageClass.Generic) += 20;
        player.GetArmorPenetration(DamageClass.Generic) += 20;
    }

    public override void SetDefaults()
    {
        Item.width = 32; // Width of the item
        Item.height = 28; // Height of the item
        Item.value = Item.sellPrice(gold: 777); // How many coins the item is worth
        Item.rare = ItemRarityID.Purple; // The rarity of the item
        Item.defense = 32; // The amount of defense the item will give when equipped
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ItemType<ArchangelChestguard>() && legs.type == ItemType<ArchangelGreaves>();
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
   
    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "";
    }
}