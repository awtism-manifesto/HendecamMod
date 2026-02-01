using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

public class OverclockedWrench : ModItem
{
    // By declaring these here, changing the values will alter the effect, and the tooltip

    // Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Lime;
        Item.value = 1499000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "12% increased movement speed and whip speed");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+2 max sentries")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        // Here we will hide all tooltips whose title end with ':RemoveMe'
        // One like that is added at the start of this method
        foreach (var l in tooltips)
        {
            if (l.Name.EndsWith(":RemoveMe"))
            {
                l.Hide();
            }
        }

        // Another method of hiding can be done if you want to hide just one line.
        // tooltips.FirstOrDefault(x => x.Mod == "ExampleMod" && x.Name == "Verbose:RemoveMe")?.Hide();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<Overclockified>().Overclocked = true; // Put all boosts in the ModPlayer below to toggle dynamically
    }
}

public class Overclockified : ModPlayer
{
    public static readonly int MeleeAttackSpeedBonus = 13;
    public static readonly int MoveSpeedBonus = 13;
    public bool Overclocked;

    public override void ResetEffects()
    {
        Overclocked = false;
    }

    public override void PostUpdateRunSpeeds()
    {
        if (Player.GetModPlayer<Ultraboostified>().Ultraboosted || !Overclocked) //Disable when Ultraboosted
        {
            return;
        }

        if (Main.rand.NextBool(8)) // 1-in-3 chance every tick
        {
            int dust = Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric,
                Player.velocity.X * Main.rand.NextFloat(-1f, 1.8f), Player.velocity.Y * Main.rand.NextFloat(-1f, 1.8f), 70, default, 0.65f);
            Main.dust[dust].noGravity = true;
        }
    }

    public override void PostUpdateEquips()
    {
        if (Player.GetModPlayer<Ultraboostified>().Ultraboosted || !Overclocked) //Disable when Ultraboosted
        {
            return;
        }

        Player.maxTurrets += 2;
        Player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += MeleeAttackSpeedBonus / 100f;
        Player.runAcceleration *= 1.12f;
        Player.moveSpeed += MoveSpeedBonus / 112f;
    }
}