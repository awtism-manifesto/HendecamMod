using HendecamMod.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Weapons
{

    public class AncientCobaltSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 20;
            Item.knockBack = 6;
            Item.ArmorPenetration = 6;
            Item.ChangePlayerDirectionOnShoot = true;
            Item.scale = 1.45f;

            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;


        }



        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "Face", "It feels historic");
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
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AncientCobaltBar>(14);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}
