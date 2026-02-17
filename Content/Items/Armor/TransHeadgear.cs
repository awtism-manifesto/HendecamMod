using System.Collections.Generic;
using HendecamMod.Content.Items.Placeables;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class TransHeadgear : ModItem
{
    public static readonly int AdditiveDamageBonus = 11;

    public static LocalizedText SetBonusText { get; private set; }

    public override void SetStaticDefaults()
    {
        SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs();
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Summon) += AdditiveDamageBonus / 100f;
    }

    public override void SetDefaults()
    {
        Item.width = 32; // Width of the item
        Item.height = 28; // Height of the item
        Item.value = Item.sellPrice(gold: 2); // How many coins the item is worth
        Item.rare = ItemRarityID.Orange; // The rarity of the item
        Item.defense = 2; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "+11% magic damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
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

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Increases max minions and max sentries by 1";
        player.maxMinions += 1;
        player.maxTurrets += 1;
    }
}