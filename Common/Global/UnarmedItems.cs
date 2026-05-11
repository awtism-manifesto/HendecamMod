using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;

namespace HendecamMod.Common.Global;

public class BandBuff : GlobalItem
    {
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
        return item.type == ItemID.RodOfHarmony;
        }

    public override void SetDefaults(Item item)
        {
        item.value = Item.sellPrice(silver: 500);
        item.rare = ItemRarityID.LightRed;
        item.accessory = true;
        }
    public static readonly int StupidSpeed = 5;
    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
        player.GetAttackSpeed<UnarmedDamage>() += StupidSpeed / 100f;
        player.GetDamage<UnarmedDamage>() += StupidSpeed / 100f;
        }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod: Grants immunity to Chaos State when equipped") { OverrideColor = Color.DarkViolet });
        }
    }
