using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Head)]
public class HallowedFedora : ModItem
{
    public static readonly int AdditiveStupidDamageBonus = 15;
   
   
    public static LocalizedText SetBonusText { get; private set; }

    public override void SetStaticDefaults()
    {
        // If your head equipment should draw hair while drawn, use one of the following:
        // ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
        // ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
        // ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
        // ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true;
        ArmorIDs.Head.Sets.IsTallHat[Item.headSlot] = true;
        ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs(AdditiveStupidDamageBonus);
    }

    public override void SetDefaults()
    {
        Item.width = 22; // Width of the item
        Item.height = 18; // Height of the item
        Item.value = Item.sellPrice(gold: 5); // How many coins the item is worth
        Item.rare = ItemRarityID.Pink; // The rarity of the item
        Item.defense = 8; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "15% increased stupid damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "+100 max Lobotometer and +25% Lobotometer Decay Rate")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);


    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ItemID.HallowedPlateMail && legs.type == ItemID.HallowedGreaves;
    }
  
    public override void UpdateEquip(Player player)
    {
       
        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 100f;
       

        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
        loboPlayer.MaxBonus += 100f; // This is safe - it resets every frame in ResetEffects


        var loboDecay = player.GetModPlayer<LobotometerPlayer>();
        loboDecay.DecayRateMultiplier *= 1.25f;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.HallowedBar, 12);

        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = SetBonusText.Value;
        player.shadowDodge = true;
        player.shadowDodgeTimer = 1800;
    }
}