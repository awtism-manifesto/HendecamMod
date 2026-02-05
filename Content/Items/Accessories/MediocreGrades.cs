using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
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
        Item.value = 90000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "11% increased stupid damage, 11% increased stupid critical strike chance");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "C's get degrees bro")
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
        recipe.AddIngredient<GoodGrades>();
        recipe.AddIngredient<BadGrades>();
        recipe.AddIngredient(ItemID.SoulofFright, 5);
        recipe.AddIngredient(ItemID.SoulofSight, 5);
        recipe.AddIngredient(ItemID.SoulofMight, 5);

        if (ModLoader.TryGetMod("SOTS", out Mod SOTSMerica) && SOTSMerica.TryFind("SoulOfPlight", out ModItem SoulOfPlight))
        {
            recipe.AddIngredient(SoulOfPlight.Type, 5);
        }

        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 100f;
        player.GetCritChance<StupidDamage>() += StupidCritBonus;
    }
}