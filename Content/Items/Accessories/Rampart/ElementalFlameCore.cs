
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.Rampart;

//[AutoloadEquip(EquipType.Beard)]
public class ElementalFlameCore : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 500);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Grants immunity to Frostburn, Shadowflame, Hellfire, and Cursed Inferno");
        tooltips.Add(line);
    }
    public override void UpdateEquip(Player player)
    {
        player.buffImmune[BuffID.Frostburn] = true;
        player.buffImmune[BuffID.ShadowFlame] = true;
        player.buffImmune[BuffID.OnFire3] = true;
        player.buffImmune[BuffID.CursedInferno] = true;
    }
}