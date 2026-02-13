using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Body)]
public class FossilFuelChestplate : ModItem
{
    public static readonly int AdditiveSummonDamageBonus = 16;
    public static readonly int CritBonus = 16;

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
        Item.value = Item.sellPrice(gold: 19); // How many coins the item is worth
        Item.rare = ItemRarityID.LightRed; // The rarity of the item
        Item.defense = 16; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "16% increased summon damage and ranged crit chance");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "25% chance not to consume ammo")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<FossilFuelHelmet>() && legs.type == ModContent.ItemType<FossilFuelPants>();
    }

    public override void UpdateEquip(Player player)
    {
       
        player.GetDamage(DamageClass.Summon) += AdditiveSummonDamageBonus / 100f;
        player.ammoCost75 = true;
        player.GetCritChance(DamageClass.Ranged) += CritBonus;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<CrudeOil>(80);
        recipe.AddIngredient<RefinedOil>(35);
        recipe.AddIngredient(ItemID.FossilShirt);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {

        player.wingTimeMax += 115;
        player.jumpSpeedBoost += 1.15f;
        player.maxFallSpeed = player.maxFallSpeed * 1.05f;
        player.wingRunAccelerationMult += 1.2f;
        player.wingAccRunSpeed += 1.2f;
        player.setBonus = "Increased flight time and aerial mobility";
    }
}