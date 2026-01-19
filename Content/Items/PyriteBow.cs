using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace HendecamMod.Content.Items
{
    public class PyriteBow : ModItem
    {
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 62; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.scale = 1.2f;
            Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
            Item.value = 55000;


            // Use Properties
            Item.useTime = 23; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 23; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.


            // The sound that this item plays when used.
            Item.UseSound = Terraria.ID.SoundID.Item102;


            // Weapon Properties
            Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            Item.damage = 19; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 2.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.
            


            // Gun Properties
            // For some reason, all the guns in the vanilla source have this.
            

            Item.shootSpeed = 8.9f; // The speed of the projectile (measured in pixels per frame.)
            Item.useAmmo = ItemID.WoodenArrow;
            Item.shoot = ModContent.ProjectileType<PyriteArrowProj>();


        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
           type = ModContent.ProjectileType<PyriteArrowProj>();
        }


        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "Converts all arrows into Pyrite Arrows that leave a trail of sparks")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);



           
        }



        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<PyriteBar>(12);
            

            recipe.AddTile(TileID.Anvils);
            recipe.Register();







        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1f, -1f);
        }
    }
}