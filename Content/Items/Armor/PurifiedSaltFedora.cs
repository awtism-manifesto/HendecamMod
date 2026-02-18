using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Head)]
public class PurifiedSaltFedora : ModItem
{
    public static readonly int StupidCritBonus = 19;
   
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
        SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs();
    }

    public override void SetDefaults()
    {
        Item.width = 22; // Width of the item
        Item.height = 18; // Height of the item
        Item.value = Item.sellPrice(gold: 27); // How many coins the item is worth
        Item.rare = ItemRarityID.Yellow; // The rarity of the item
        Item.defense = 10; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "19% increased stupid critical strike chance");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+25% Lobotometer decay rate and +2.5% max life")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<PurifiedSaltChestplate>() && legs.type == ModContent.ItemType<PurifiedSaltLeggings>();
    }

    public override void UpdateEquip(Player player)
    {
        

       
        player.GetCritChance<StupidDamage>() += StupidCritBonus;
        player.statLifeMax2 = (int)(player.statLifeMax2 * 1.025f);

        var loboDecay = player.GetModPlayer<LobotometerPlayer>();
        loboDecay.DecayRateMultiplier *= 1.25f;

    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        if (ModLoader.TryGetMod("TheConfectionRebirth", out Mod SweetMerica) && SweetMerica.TryFind("NeapoliniteBar", out ModItem NeapoliniteBar))
        {
            recipe.AddIngredient<RockSaltFedora>();
            recipe.AddIngredient<PurifiedSalt>(54);
            recipe.AddIngredient(NeapoliniteBar.Type, 8);
            recipe.AddIngredient(ItemID.SpectreBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        else
        {
            recipe.AddIngredient<RockSaltFedora>();
            recipe.AddIngredient<PurifiedSalt>(54);
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddIngredient(ItemID.SpectreBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

    public override void UpdateArmorSet(Player player)
    {
    }
}