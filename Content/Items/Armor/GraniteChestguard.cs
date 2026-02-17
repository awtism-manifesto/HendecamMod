using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Body)]
public class GraniteChestguard : ModItem
{
    public static readonly int MagicDamageBonus = 5;
    public static readonly int MagicCritBonus = 6;
    public static readonly int MaxManaIncrease = 50;

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
        Item.value = Item.sellPrice(silver: 95); // How many coins the item is worth
        Item.rare = ItemRarityID.White; // The rarity of the item
        Item.defense = 6; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "6% increased magic damage & crit chance");
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
        return head.type == ModContent.ItemType<GraniteHeadgear>() && legs.type == ModContent.ItemType<GraniteLeggings>();
    }

    public override void UpdateEquip(Player player)
    {
       
        player.GetDamage(DamageClass.Magic) += MagicDamageBonus / 100f;
        player.GetCritChance(DamageClass.Magic) += MagicCritBonus;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Granite, 90);
        recipe.AddIngredient(ItemID.SilverBar, 30);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Granite, 90);
        recipe.AddIngredient(ItemID.TungstenBar, 30);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        if (ModLoader.TryGetMod("VitalityMod", out Mod VitalMerica) && VitalMerica.TryFind("GlowingGranitePowder", out ModItem GlowingGranitePowder))
        {
            recipe.AddIngredient(GlowingGranitePowder.Type, 35);
        }
    }

    public override void UpdateArmorSet(Player player)
    {
        player.statManaMax2 += MaxManaIncrease;

        player.setBonus = "+50 max mana";
    }
}