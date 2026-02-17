using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Legs)]
public class UraniumLeggings : ModItem
{
    public static readonly int MoveSpeedBonus = 8;
    public static readonly int AdditiveDamageBonus = 8;
    public static LocalizedText SetBonusText { get; private set; }

    public override void SetStaticDefaults()
    {
       
        SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs();
    }

    public override void SetDefaults()
    {
        Item.width = 32; // Width of the item
        Item.height = 28; // Height of the item
        Item.value = 467000;
        Item.rare = ItemRarityID.Green; // The rarity of the item
        Item.defense = 8; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "8% increased damage and movement speed");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "-20 max life")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<UraniumHelmet>() && body.type == ModContent.ItemType<UraniumChestplate>();
    }

    public override void UpdateEquip(Player player)
    {
        

        player.statLifeMax2 += -20;
        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;
        player.moveSpeed += MoveSpeedBonus / 100f;
        player.runAcceleration *= 1.08f;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<UraniumBar>(24);

        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = SetBonusText.Value;
    }
}