using System.Collections.Generic;
using HendecamMod.Content.Items.Placeables;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Body)]
public class MintalBreastplate : ModItem
{
   

   

    public override void SetDefaults()
    {
        Item.width = 32; // Width of the item
        Item.height = 28; // Height of the item
        Item.value = 290000;
        Item.rare = ItemRarityID.LightRed; // The rarity of the item
        Item.defense = 19; // The amount of defense the item will give when equipped
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Melee) += 11 / 100f;
        player.GetDamage(DamageClass.Magic) += 11 / 100f;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "11% increased Melee and Magic damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ItemType<MintalHelmet>() && legs.type == ItemType<MintalLeggings>();
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<MintalBar>(20);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.statManaMax2 += player.statDefense;
        player.setBonus = "Increases max mana by your defense stat, increases melee damage based on your max mana";
        player.GetDamage(DamageClass.Melee) += player.statManaMax2 / 1500f;
    }
}