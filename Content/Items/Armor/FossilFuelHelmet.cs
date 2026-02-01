using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Head)]
public class FossilFuelHelmet : ModItem
{
    public static readonly int AdditiveSummonDamageBonus = 15;
    public static readonly int CritBonus = 8;

    public static readonly int MaxMinionIncrease = 1;

    public static LocalizedText SetBonusText { get; private set; }

    public override void SetStaticDefaults()
    {
        // If your head equipment should draw hair while drawn, use one of the following:
        // ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
        // ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
        // ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
        // ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true;

        ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs();
    }

    public override void SetDefaults()
    {
        Item.width = 32; // Width of the item
        Item.height = 28; // Height of the item
        Item.value = Item.sellPrice(gold: 19); // How many coins the item is worth
        Item.rare = ItemRarityID.LightRed; // The rarity of the item
        Item.defense = 7; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "+15% ranged and summon damage, +8% whip size and ranged crit chance");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+1 max minion")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<KevlarBodysuit>() && legs.type == ModContent.ItemType<KevlarPants>();
    }

    public override void UpdateEquip(Player player)
    {
       

        player.GetDamage(DamageClass.Summon) += AdditiveSummonDamageBonus / 100f;
        player.GetDamage(DamageClass.Ranged) += AdditiveSummonDamageBonus / 100f;
        player.GetCritChance(DamageClass.Ranged) += CritBonus;
        player.maxMinions += MaxMinionIncrease;
        player.whipRangeMultiplier = 1.08f;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<CrudeOil>(50);
        recipe.AddIngredient<RefinedOil>(20);
        recipe.AddIngredient(ItemID.FossilHelm);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
    }
}