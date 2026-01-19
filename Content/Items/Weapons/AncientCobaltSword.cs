using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles;
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
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 18;
            Item.knockBack = 6;
            
            Item.ChangePlayerDirectionOnShoot = true;
            Item.scale = 1.45f;

            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<CobaltBolt>();
            Item.shootSpeed = 10.25f;

        }



        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "Face", "Shoots a beam of ancient cobalt energy");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "It feels historic")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);



            

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
