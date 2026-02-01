using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Head)]
public class PlutoniumFacemask : ModItem
{
    public static readonly int AdditiveDamageBonus = 13;
    public static readonly int CritBonus = 13;
    public static readonly int MaxManaIncrease = 90;
    public static readonly int RangedCritBonus = 10;
    public static LocalizedText SetBonusText { get; private set; }

    public override void SetStaticDefaults()
    {
        // If your head equipment should draw hair while drawn, use one of the following:
        // ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
        // ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
        // ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
        // ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true;

        ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs();
    }

    public override void SetDefaults()
    {
        Item.width = 32; // Width of the item
        Item.height = 28; // Height of the item
        Item.value = Item.sellPrice(gold: 20); // How many coins the item is worth
        Item.rare = ItemRarityID.LightPurple; // The rarity of the item
        Item.defense = 12; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "13% increased damage and crit chance");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+95 max mana and +10% ranged crit chance")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<PlutoniumChestplate>() && legs.type == ModContent.ItemType<PlutoniumPants>();
    }

    public override void UpdateEquip(Player player)
    {
        
        player.statManaMax2 += MaxManaIncrease;
        player.GetCritChance(DamageClass.Ranged) += RangedCritBonus;
        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;
        player.GetCritChance(DamageClass.Generic) += CritBonus;
        player.lifeRegen += -1;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PlutoniumBar>(25);
        recipe.AddTile(TileID.MythrilAnvil);

        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.armorEffectDrawShadow = true;
        player.armorEffectDrawOutlines = true;
        player.armorEffectDrawOutlinesForbidden = true;
    }
}