using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

public class TheAutismManifesto : ModItem
{
    // By declaring these here, changing the values will alter the effect, and the tooltip
    public static readonly int AdditiveStupidDamageBonus = 10;

    public static readonly int MagicCritBonus = 10;
    public static readonly int StupidCritBonus = 10;

    public static readonly int MaxManaIncrease = 75;

    // Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Pink;
        Item.value = 9999000;
        Item.defense = 10;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<SpiritProtectionCharm>();
        recipe.AddIngredient<AutismDiagnosis>();
        recipe.AddIngredient<AutismOrb>(2);
        recipe.AddIngredient<PlutoniumBar>(6);
        recipe.AddIngredient<AstatineBar>(7);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "+75 mana, 10% increased magic and stupid crit chance and +15% stupid damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "-Developer Item-")
        {
            OverrideColor = new Color(255, 15, 85)
        };
        tooltips.Add(line);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        
        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 100f;

        player.GetCritChance<StupidDamage>() += StupidCritBonus;
        player.GetCritChance(DamageClass.Magic) += MagicCritBonus;
        player.statManaMax2 += MaxManaIncrease;
        player.endurance = 1f - 0.95f * (1f - player.endurance);
    }
}