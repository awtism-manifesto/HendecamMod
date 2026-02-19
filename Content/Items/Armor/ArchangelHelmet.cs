using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.Localization;

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

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "+20% crit chance and +20 armor penetration");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Significantly boosts max life")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<ArchangelChestguard>() && legs.type == ModContent.ItemType<ArchangelGreaves>();
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AngelShard>(7);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "";
    }
}