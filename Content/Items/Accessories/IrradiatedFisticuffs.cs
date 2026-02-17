using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class IrradiatedFisticuffs : ModItem
{
    public static readonly int MeleeAttackSpeedBonus = 14;

    public static readonly int AdditiveMeleeDamageBonus = 14;

    // Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs();

    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Red;
        Item.value = 1425000;
        Item.defense = 11;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "14% increased melee speed and damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Enemies are much more likely to target you")
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

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BerserkerGlove);
        recipe.AddIngredient(ItemID.WarriorEmblem);
        recipe.AddIngredient<AstatineBar>(15);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Melee) += AdditiveMeleeDamageBonus / 100f;
        player.GetAttackSpeed(DamageClass.Melee) += MeleeAttackSpeedBonus / 100f;
        player.aggro += 660;
        player.lifeRegen += -4;
    }
}