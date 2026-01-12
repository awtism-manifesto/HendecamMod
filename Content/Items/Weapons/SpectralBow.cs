using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles.Items;
using HendecamMod.Content.Items.Weapons;


namespace HendecamMod.Content.Items.Weapons
{
    public class SpectralBow : ModItem
    {
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 62; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.scale = 1f;
            Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
            Item.value = 110000;


            // Use Properties
            Item.useTime = 14; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 14; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
          

            // The sound that this item plays when used.
            Item.UseSound = SoundID.Item102;


            // Weapon Properties
            Item.DamageType = ModContent.GetInstance<RangedSummonDamage>(); // Sets the damage type to ranged.
            Item.damage = 58; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 4.25f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.



            // Gun Properties
            // For some reason, all the guns in the vanilla source have this.


            Item.shootSpeed = 11.5f; // The speed of the projectile (measured in pixels per frame.)
            Item.useAmmo = ItemID.WoodenArrow;
            Item.shoot = ModContent.ProjectileType<SpectralArrowProj>();


        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ModContent.ProjectileType<SpectralArrowProj>();
        }

       



        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "Shoots Spectral Arrows that light enemies up and cause summons to focus them");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "10 summon tag damage")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);



           
        }



        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<SplashPotionOfShine>(100);
            recipe.AddIngredient(ItemID.PixieDust, 50);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.GoldBow);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = CreateRecipe();

            recipe.AddIngredient<SplashPotionOfShine>(100);
            recipe.AddIngredient(ItemID.PixieDust, 50);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.PlatinumBow);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();






        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1f, -1f);
        }
    }
}