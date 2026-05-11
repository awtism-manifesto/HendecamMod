using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

public class VeteransBadge : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Yellow;
        Item.value = 1025000;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Increases crit damage by 15% and armor penetration by 10");
        tooltips.Add(line);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<VetBoost>().isVet = true;
    }
}

public class VetBoost : ModPlayer
{
    public bool isVet;
   

    public override void ResetEffects()
    {
        isVet = false;
    }

    public override void PostUpdateEquips()
    {
        if (isVet)
        {
            Player.GetArmorPenetration(DamageClass.Generic) += 10;
        }
    }

   
    public override void ModifyHitNPC( NPC target, ref NPC.HitModifiers modifiers)
    {
        if (!isVet)
        {
            return;
        }
        modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) =>
        {
            if (hitInfo.Crit)
            {
                hitInfo.Damage = (int)(hitInfo.Damage * 1.15f);
            }
        };
    }

    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
    {
        if (!isVet)
        {
            return;
        }
        modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) =>
        {
            if (hitInfo.Crit)
            {
                hitInfo.Damage = (int)(hitInfo.Damage * 1.15f);
            }
        };
    }

   
}