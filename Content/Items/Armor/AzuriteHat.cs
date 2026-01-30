using HendecamMod.Content.Items.Placeables;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class AzuriteHat : ModItem
{
    public static readonly int MaxMinionIncrease = 2;

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
    public static readonly int AdditiveDamageBonus = 15;
    public static readonly int MeleeAttackSpeedBonus = 10;
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Melee) += AdditiveDamageBonus / 115f;
        player.GetAttackSpeed(DamageClass.Melee) += MeleeAttackSpeedBonus / 110f;
    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 28;
        Item.value = Item.sellPrice(gold: 2);
        Item.rare = ItemRarityID.Orange;
        Item.defense = 5;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "15% increased melee damage and 10% increased melee speed");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        foreach (var l in tooltips)
        {
            if (l.Name.EndsWith(":RemoveMe"))
            {
                l.Hide();
            }
        }
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<AzuritePlatemail>() && legs.type == ModContent.ItemType<AzuriteGreaves>();
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AzuriteBar>(10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "+2 max summon slots";
        player.maxMinions += MaxMinionIncrease;
    }
}