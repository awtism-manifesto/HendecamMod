using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

public class OverclockedWrench : ModItem
{
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
        var line = new TooltipLine(Mod, "Face", "12% increased movement speed and whip speed");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+2 max sentries")
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