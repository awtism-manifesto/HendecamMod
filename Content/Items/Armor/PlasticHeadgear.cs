using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Head)]
public class PlasticHeadgear : ModItem
{
    public static readonly int AdditiveStupidDamageBonus = 2;
    public static readonly int StupidArmorPenetration = 5;
    public static readonly int StupidCritBonus = 7;
    public static LocalizedText SetBonusText { get; private set; }

    public override void SetStaticDefaults()
    {
        // If your head equipment should draw hair while drawn, use one of the following:
        // ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
        // ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
        // ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
        // ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true;
        SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs(AdditiveStupidDamageBonus);
    }

    public override void SetDefaults()
    {
        Item.width = 22; // Width of the item
        Item.height = 18; // Height of the item
        Item.value = 17750;
        Item.rare = ItemRarityID.Blue; // The rarity of the item
        Item.defense = 2; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "2% increased stupid damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "7% increased stupid crit chance")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<PlasticChestplate>() && legs.type == ModContent.ItemType<PlasticPants>();
    }

    public override void UpdateEquip(Player player)
    {
        
        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 100f;

        player.GetCritChance<StupidDamage>() += StupidCritBonus;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PlasticScrap>(20);
        recipe.AddIngredient<Polymer>(5);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
    }
}