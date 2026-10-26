using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Head)]
public class MantiusHat : ModItem
{

    public static readonly int DamageBonus = 10;
    public static readonly int CritBonus = 9;
    public static readonly int AdditiveDamageBonus = 10;
    public static readonly int CritBonus2 = 8;
    public static readonly int ArmorPenetration = 5;
    public static readonly int AttackSpeedBonus = 5;
    public static readonly int MoveSpeedBonus = 12;

    public static LocalizedText SetBonusText { get; private set; }

   

    public override void SetDefaults()
    {
        Item.width = 32; // Width of the item
        Item.height = 28; // Height of the item
        Item.value = Item.sellPrice(gold: 9); // How many coins the item is worth
        Item.rare = ItemRarityID.LightRed; // The rarity of the item
        Item.defense = 7; // The amount of defense the item will give when equipped
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ItemType<MantiusChestplate>() && legs.type == ItemType<MantiusLeggings>();
    }

    public override void UpdateEquip(Player player)
    {
        // GetDamage returns a reference to the specified damage class' damage StatModifier.
        // Since it doesn't return a value, but a reference to it, you can freely modify it with mathematics operators (+, -, *, /, etc.).
        // StatModifier is a structure that separately holds float additive and multiplicative modifiers, as well as base damage and flat damage.
        // When StatModifier is applied to a value, its additive modifiers are applied before multiplicative ones.
        // Base damage is added directly to the weapon's base damage and is affected by damage bonuses, while flat damage is applied after all other calculations.
        // In this case, we're doing a number of things:
        // - Adding 25% damage, additively. This is the typical "X% damage increase" that accessories use, use this one.
        // - Adding 12% damage, multiplicatively. This effect is almost never used in Terraria, typically you want to use the additive multiplier above. It is extremely hard to correctly balance the game with multiplicative bonuses.
        // - Adding 4 base damage.
        // - Adding 5 flat damage.
        // Since we're using DamageClass.Generic, these bonuses apply to ALL damage the player deals.

        player.GetAttackSpeed(DamageClass.Ranged) += DamageBonus / 100f;
        player.GetCritChance(DamageClass.Ranged) += CritBonus;

    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<MantiusBar>(12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        if (Main.hardMode)
        {
            player.statLifeMax2 = (int)(player.statLifeMax2 * 1.08f);
            player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;
            player.moveSpeed += MoveSpeedBonus / 100f;
            player.runAcceleration *= 1.15f;
            player.GetArmorPenetration(DamageClass.Generic) += ArmorPenetration;
            player.lifeRegen = (int)(player.lifeRegen + 3f);
            player.GetCritChance(DamageClass.Generic) += CritBonus2;
            player.GetAttackSpeed(DamageClass.Generic) += AttackSpeedBonus / 100f;
          
            player.statManaMax2 += 60;
        }

        player.setBonus = "Slightly increases all stats in Hardmode";
    }
}