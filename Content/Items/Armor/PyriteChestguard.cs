using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Body)]
public class PyriteChestguard : ModItem
{
    public override void SetDefaults()
    {
        Item.defense = 6;
        Item.rare = ItemRarityID.Blue;
        Item.value = 67000;
    }
    public static readonly int AdditiveDamageBonus = 4;
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

        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 104f;
        player.endurance = 1f - 0.96f * (1f - player.endurance);
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "4% increased damage and damage reduction");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);




    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PyriteBar>(20);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}