using HendecamMod.Common.Systems;
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
       
        recipe.AddIngredient<FissionDrive>();
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "+100 mana and Lobotometer, +10% damage reduction");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Increases all damage by 5% of your max Lobotometer ")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Increases all attack speed as Lobotometer increases, up to 25% at max")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "-Developer Item-")
        {
            OverrideColor = new Color(255, 15, 85)
        };
        tooltips.Add(line);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {

        player.statManaMax2 += 100;
        player.endurance = 1f - 0.9f * (1f - player.endurance);

        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
        loboPlayer.MaxBonus += 100f;

        float damageBonus = loboPlayer.Max / 2000;
        float lobotometerPercent = loboPlayer.Current / loboPlayer.Max;
        float speedBonus = lobotometerPercent * 0.25f;
        //float speedBonus = (1f - lobotometerPercent) * 0.25f; // inverse

        player.GetAttackSpeed(DamageClass.Generic) += speedBonus;
        player.GetDamage(DamageClass.Generic) += damageBonus;

    }
}