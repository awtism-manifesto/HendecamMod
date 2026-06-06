using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using System.Collections.Generic;

namespace HendecamMod.Content.Global;


public class WoodSplinters : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.WoodenSword;
        }
        else return false;
       
    }

    public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffType<Splinters>(), 240);
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now inflicts Splinters on hit") { OverrideColor = Color.DarkViolet });
    }
}
public class EbonwoodSplinters : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.EbonwoodSword;
        }
        else return false;
    }

    public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffType<Splinters>(), 270);
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now inflicts Splinters on hit") { OverrideColor = Color.DarkViolet });
    }

}
public class ShadewoodSplinters : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.ShadewoodSword;
        }
        else return false;
    }

    public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffType<Splinters>(), 270);
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now inflicts Splinters on hit") { OverrideColor = Color.DarkViolet });
    }

}
public class PalmSplinters : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.PalmWoodSword;
        }
        else return false;
    }

    public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffType<Splinters>(), 240);
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now inflicts Splinters on hit") { OverrideColor = Color.DarkViolet });
    }
}

public class BorSplinters : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.BorealWoodSword;
        }
        else return false;
    }

    public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffType<Splinters>(), 240);
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now inflicts Splinters on hit") { OverrideColor = Color.DarkViolet });
    }

}
public class AshSplinters : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.AshWoodSword;
        }
        else return false;
    }

    public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffType<Splinters>(), 240);
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now inflicts Splinters on hit") { OverrideColor = Color.DarkViolet });
    }

}
public class PearlSplinters : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        if (GetInstance<HendecamConfig>().MiscVanillaWeaponChanges == true)
        {
            return item.type == ItemID.PearlwoodSword;
        }
        else return false;
    }

    public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffType<Splinters>(), 240);
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Now inflicts Splinters on hit") { OverrideColor = Color.DarkViolet });
    }

}




