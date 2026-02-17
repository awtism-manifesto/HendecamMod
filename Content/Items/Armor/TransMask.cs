using System.Collections.Generic;
using HendecamMod.Content.Items.Placeables;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class TransMask : ModItem
{
    public static readonly int AdditiveDamageBonus = 6;
    public static readonly int MeleeCritBonus = 2;
    public static readonly int MeleeCritBonusB = 8;


    public static LocalizedText SetBonusText { get; private set; }

    public override void SetStaticDefaults()
    {
        SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs();
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Melee) += AdditiveDamageBonus / 100f;
        player.GetCritChance(DamageClass.Melee) += MeleeCritBonus;
    }

    public override void SetDefaults()
    {
        Item.width = 32; // Width of the item
        Item.height = 28; // Height of the item
        Item.value = Item.sellPrice(gold: 2); // How many coins the item is worth
        Item.rare = ItemRarityID.Orange; // The rarity of the item
        Item.defense = 8; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "+6% melee damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+2% crit chance")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<TransBodyplate>() && legs.type == ModContent.ItemType<TransGreaves>();
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<TransBar>(10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    // Set bonus code
    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "+8% crit chance";
        player.GetCritChance(DamageClass.Melee) += MeleeCritBonusB;
    }
}