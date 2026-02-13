using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Legs)]
public class YelmutLeggings : ModItem
{
    public static readonly int MoveSpeedBonus = 10;
    public static readonly int MeleeCritBonus = 10;

   

    public override void SetDefaults()
    {
        Item.width = 22; // Width of the item
        Item.height = 18; // Height of the item
        Item.value = Item.sellPrice(gold : 15); // How many coins the item is worth
        Item.rare = ItemRarityID.LightRed; // The rarity of the item
        Item.defense = 12; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "10% increased melee crit chance");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "10% increased movement speed")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<WeedHeadgear>() && body.type == ModContent.ItemType<WeedShirt>();
    }

    public override void UpdateEquip(Player player)
    {
       

      
      
        player.GetCritChance(DamageClass.Melee) += MeleeCritBonus;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<WeedLeaves>(42);

        recipe.AddTile(TileID.Loom);
        recipe.Register();
       
    }

    public override void UpdateArmorSet(Player player)
    {
    }
}