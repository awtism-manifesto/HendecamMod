using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Legs)]
public class GraniteLeggings : ModItem
{
    public static readonly int MagicCritBonus = 7;
    public static readonly int MoveSpeedBonus = 7;
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
        Item.defense = 4; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "7% increased magic crit chance & movement speed");
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
        return head.type == ModContent.ItemType<GraniteHeadgear>() && body.type == ModContent.ItemType<GraniteChestguard>();
    }

    public override void UpdateEquip(Player player)
    {
       

        player.GetCritChance(DamageClass.Magic) += MagicCritBonus;
        player.moveSpeed += MoveSpeedBonus / 100f; // Increase the movement speed of the player
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Granite, 75);
        recipe.AddIngredient(ItemID.SilverBar, 25);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Granite, 75);
        recipe.AddIngredient(ItemID.TungstenBar, 25);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        if (ModLoader.TryGetMod("VitalityMod", out Mod VitalMerica) && VitalMerica.TryFind("GlowingGranitePowder", out ModItem GlowingGranitePowder))
        {
            recipe.AddIngredient(GlowingGranitePowder.Type, 25);
        }
    }

    public override void UpdateArmorSet(Player player)
    {
    }
}