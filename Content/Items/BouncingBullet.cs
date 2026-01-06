using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items
{
    public class BouncingBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 199;
        }

        public override void SetDefaults()
        {
            Item.damage = 11; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
            Item.DamageType = DamageClass.Ranged;
            Item.rare = ItemRarityID.LightRed;
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
            Item.knockBack = 2.5f;
            Item.value = 33;
          
            Item.shoot = ModContent.ProjectileType<BouncingBulletProj>();
            Item.shootSpeed = 5.5f; // The speed of the projectile.
            Item.ammo = AmmoID.Bullet; // The ammo class this ammo belongs to.
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "Randomly bounces in a new direction whenever it hits an enemy");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "")
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
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(150);
            recipe.AddIngredient(ItemID.GelBalloon);
            recipe.AddIngredient(ItemID.PinkGel);
            recipe.AddIngredient<Rubber>(2);
            recipe.AddIngredient<Kevlar>();
            recipe.AddIngredient(ItemID.EmptyBullet, 150);
            recipe.Register();
        }
    }
}