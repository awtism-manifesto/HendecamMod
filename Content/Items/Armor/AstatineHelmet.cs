using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Head)]
public class AstatineHelmet : ModItem
{
    public static readonly int AdditiveDamageBonus = 19;
    public static readonly int CritBonus = 10;
   
    public static readonly int MaxManaIncrease = 110;
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
        Item.value = Item.sellPrice(gold: 90); // How many coins the item is worth
        Item.rare = ItemRarityID.Red; // The rarity of the item
        Item.defense = 18; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "19% increased damage and 10% increased crit chance");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+110 max mana and lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<AstatineBreastplate>() && legs.type == ModContent.ItemType<AstatineGreaves>();
    }

    public override void UpdateEquip(Player player)
    {
       
        player.statManaMax2 += MaxManaIncrease;
       

        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;
        player.GetCritChance(DamageClass.Generic) += CritBonus;

        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
        loboPlayer.MaxBonus += 110f;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AstatineBar>(32);
        recipe.AddTile<CultistCyclotronPlaced>();

        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
    }
}