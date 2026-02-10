using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class MediocreGrades : ModItem
{
    public static readonly int AdditiveStupidDamageBonus = 11;
    public static readonly int StupidCritBonus = 11;

    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs();

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.LightPurple;
        Item.value = 15000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Increases Stupid damage as Lobotometer increases, up to 12% at max");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Increases Stupid attack speed by 12%, bonus decays as Lobotometer rises")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'C's get degrees bro'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<GoodGrades>();
        recipe.AddIngredient<BadGrades>();
        recipe.AddIngredient(ItemID.SoulofFright, 5);
        recipe.AddIngredient(ItemID.SoulofSight, 5);
        recipe.AddIngredient(ItemID.SoulofMight, 5);

        if (ModLoader.TryGetMod("SOTS", out Mod SOTSMerica) && SOTSMerica.TryFind("SoulOfPlight", out ModItem SoulOfPlight))
        {
            recipe.AddIngredient(SoulOfPlight.Type, 5);
        }

        recipe.AddTile<CobaltWorkBenchPlaced>();
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();



        float lobotometerPercent = loboPlayer.Current / loboPlayer.Max;
         float damageBonus = lobotometerPercent * 0.12f;
        float speedBonus = (1f - lobotometerPercent) * 0.12f;
        player.GetDamage(ModContent.GetInstance<StupidDamage>()) += damageBonus;
        player.GetAttackSpeed(ModContent.GetInstance<StupidDamage>()) += speedBonus;

    }
}