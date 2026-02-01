using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Body)]
public class KingslayerBreastplate : ModItem
{
    public static readonly int AdditiveSummonDamageBonus = 6;
    public static readonly int CritBonus = 6;
    public static readonly int MaxMinionIncrease = 1;
    public static readonly int MoveSpeedBonus = 6;
    public static readonly int AdditiveDamageBonus = 5;
    public static readonly int CritBonus2 = 5;
    public static readonly int ArmorPenetration = 3;
    public static readonly int AttackSpeedBonus = 3;
    public static LocalizedText SetBonusText { get; private set; }

    public override void SetStaticDefaults()
    {
        // If your head equipment should draw hair while drawn, use one of the following:
        // ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
        // ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
        // ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
        // ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true;
        SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs();
    }

    public override void SetDefaults()
    {
        Item.width = 32; // Width of the item
        Item.height = 28; // Height of the item
        Item.value = Item.sellPrice(gold: 10); // How many coins the item is worth
        Item.rare = ItemRarityID.Green; // The rarity of the item
        Item.defense = 7; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "6% increased crit chance and summon damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+1 minion slot")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<KingslayerHelmet>() && legs.type == ModContent.ItemType<KingslayerGreaves>();
    }

    public override void UpdateEquip(Player player)
    {
       
        player.GetDamage(DamageClass.Summon) += AdditiveSummonDamageBonus / 100f;
        player.maxMinions += MaxMinionIncrease;
        player.GetCritChance(DamageClass.Generic) += CritBonus;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<KingslayerBar>(22);

        recipe.AddIngredient(ItemID.FlinxFurCoat);

        recipe.AddTile(TileID.Solidifier);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        if (NPC.AnyDanger())
        {
            player.statLifeMax2 = (int)(player.statLifeMax2 * 1.05f);
            player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 105f;
            player.moveSpeed += MoveSpeedBonus / 108f;
            player.runAcceleration *= 1.1f;
            player.GetArmorPenetration(DamageClass.Generic) += ArmorPenetration;
            player.lifeRegen = (int)(player.lifeRegen + 2.5f);
            player.GetCritChance(DamageClass.Generic) += CritBonus2;
            player.GetAttackSpeed(DamageClass.Generic) += AttackSpeedBonus / 103f;
            player.statDefense += 5;
            player.statManaMax2 += 40;
        }

        player.setBonus = "Slightly increases all stats during a boss fight or invasion";
    }
}