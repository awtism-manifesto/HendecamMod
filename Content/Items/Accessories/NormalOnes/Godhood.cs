
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using HendecamMod.Content.Items.Accessories.NastyPatty;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories.NormalOnes
{
    //[AutoloadEquip(EquipType.Beard)]
    public class Godhood : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(silver: 128000);
            Item.rare = ItemRarityID.Master;
            Item.accessory = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Nothing can touch you, everything you touch crumbles to dust"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Works best with Hallowed Armor"));
        }
        public override void UpdateEquip(Player player)
        {
            player.AddBuff(BuffID.ShadowDodge, 300);
            player.AddBuff(BuffID.ParryDamageBuff, 300);
        }
    }
}