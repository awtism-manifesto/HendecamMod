using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using HendecamMod.Content.Items.Placeables;

namespace HendecamMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class AzuriteGreaves : ModItem
    {
        public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults()
        {


            SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs();
        }
        public static readonly int AdditiveDamageBonus = 10;
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += AdditiveDamageBonus / 110f;
            player.statManaMax2 += 50;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 7;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "Face", "Increases magic damage by 10% and max mana by 50");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "")
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
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<AzuriteHat>() && body.type == ModContent.ItemType<AzuritePlatemail>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AzuriteBar>(15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void UpdateArmorSet(Player player)
        {

           
        }
    }
}