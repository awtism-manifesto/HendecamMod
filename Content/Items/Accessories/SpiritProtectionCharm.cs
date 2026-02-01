using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;

namespace HendecamMod.Content.Items.Accessories;

public class SpiritProtectionCharm : ModItem
{
    // By declaring these here, changing the values will alter the effect, and the tooltip
    public static readonly int MagicCritBonus = 7;

    public static readonly int MaxManaIncrease = 70;

    // Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Pink;
        Item.value = 99000;
        Item.defense = 5;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.ManaRegenerationBand);
        recipe.AddIngredient<EmpoweredManaCrystal>();
        recipe.AddIngredient<SaltPendant>();
        recipe.AddIngredient<PearlceramicSheet>(9);
        recipe.AddIngredient<PurifiedSalt>(99);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "6% reduced damage taken");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+7% magic crit chance and +70 mana")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
       

        player.GetCritChance(DamageClass.Magic) += MagicCritBonus;
        player.statManaMax2 += MaxManaIncrease;
        player.endurance = 1f - (0.94f * (1f - player.endurance));
    }
}