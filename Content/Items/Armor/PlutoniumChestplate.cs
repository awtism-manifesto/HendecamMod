using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Body)]
public class PlutoniumChestplate : ModItem
{
    public static readonly int CritBonus = 8;
    public static readonly int AdditiveDamageBonus = 15;
    public static readonly int AttackSpeedBonus = 15;
    public static readonly int StupidArmorPenetration = 10;

    public static readonly int MeleeAttackSpeedBonus = 11;
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
        Item.value = Item.sellPrice(gold: 27); // How many coins the item is worth
        Item.rare = ItemRarityID.LightPurple; // The rarity of the item
        Item.defense = 17; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "15% increased damage and 8% increased crit chance");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+10 stupid armor penetration and +11% melee speed")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<PlutoniumFacemask>() && legs.type == ModContent.ItemType<PlutoniumPants>();
    }

    public override void UpdateEquip(Player player)
    {
        

        player.GetArmorPenetration<StupidDamage>() += StupidArmorPenetration;
        player.GetCritChance(DamageClass.Generic) += CritBonus;
        player.lifeRegen += -1;
        player.GetAttackSpeed(DamageClass.Melee) += MeleeAttackSpeedBonus / 100f;

        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PlutoniumBar>(36);
        recipe.AddTile(TileID.MythrilAnvil);

        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Increases attack speed by 15% at the cost of 10% max life";
        player.statLifeMax2 = (int)(player.statLifeMax2 * 0.9f);
        player.GetAttackSpeed(DamageClass.Generic) += AttackSpeedBonus / 115f;
    }
}