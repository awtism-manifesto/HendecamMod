using HendecamMod.Content.DamageClasses;
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
    public class TheWither : ModItem
    {
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 62; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.LightPurple; // The color that the item's name will be in-game.
            Item.value = 330000;


            // Use Properties
            // Use Properties
            Item.useTime = 12; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 36; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
            
          
            // The sound that this item plays when used.
            Item.UseSound = Terraria.ID.SoundID.Item45;


            // Weapon Properties
            Item.DamageType = ModContent.GetInstance<OmniDamage>();
            Item.damage = 48; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 7.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.
            Item.ArmorPenetration = 5;
            Item.mana = 6;

            if (ModLoader.TryGetMod("Consolaria", out Mod Cons2Merica))


            {
                Item.damage = 66;
                Item.useTime = 11; // The item's use time in ticks (60 ticks == 1 second.)
                Item.useAnimation = 33; // The length of the item's use animation in ticks (60 ticks == 1 second.)
                Item.rare = ItemRarityID.Yellow;
            }

                // Gun Properties
                // For some reason, all the guns in the vanilla source have this.
                Item.shoot = ModContent.ProjectileType<WitherSkullBlack>();
           
            Item.shootSpeed = 14.25f; // The speed of the projectile (measured in pixels per frame.)

        }
       
        private int tickCounter = 0;
        private int nextSpawnTick = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            const int NumProjectiles = 1; // The number of projectiles that this gun will shoot.
           
            for (int i = 0; i < NumProjectiles; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(5.5f));
                
               
               


                if (nextSpawnTick == 0)
                {
                    nextSpawnTick = 2;
                }
                tickCounter++;

                if (tickCounter >= nextSpawnTick && tickCounter < 3)
                {
                    if (Main.rand.NextBool(4))
                    {
                        Vector2 new2Velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5.5f));
                        type = ModContent.ProjectileType<WitherSkullBlack>();
                        Projectile.NewProjectileDirect(source, position, new2Velocity, type, damage, knockback, player.whoAmI);
                    }
                    
                    if (Main.rand.NextBool(5))
                    {
                        Vector2 new3Velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5.5f));
                        type = ModContent.ProjectileType<WitherSkullBlack>();
                        Projectile.NewProjectileDirect(source, position, new3Velocity, type, damage, knockback, player.whoAmI);
                    }

                    type = ModContent.ProjectileType<WitherSkullBlack>();
                    Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                    

                   


                    tickCounter = 5;
                    nextSpawnTick = 5;
                }
                else if (tickCounter >= 5)
                {
                    if (Main.rand.NextBool(3))
                    {
                        Vector2 new2Velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5.5f));
                        type = ModContent.ProjectileType<WitherSkullBlack>();
                        Projectile.NewProjectileDirect(source, position, new2Velocity, type, damage, knockback, player.whoAmI);
                    }

                    if (Main.rand.NextBool(3))
                    {
                        Vector2 new3Velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5.5f));
                        type = ModContent.ProjectileType<WitherSkullBlack>();
                        Projectile.NewProjectileDirect(source, position, new3Velocity, type, damage, knockback, player.whoAmI);
                    }

                    type = ModContent.ProjectileType<WitherSkullBlack>();
                    Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);

                    tickCounter = 0;
                    nextSpawnTick = 2;

                }
                else
                {
                    type = ModContent.ProjectileType<WitherSkullBlue>();
                    Projectile.NewProjectileDirect(source, position, velocity * 0.81f, type, damage, knockback, player.whoAmI);

                }

            }

            return false; // Return false because we don't want tModLoader to shoot projectile
        }

       


        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "This should not exist in this realm.")
            {
                OverrideColor = new Color(25, 25, 25)
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
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<PocketSand>();
            recipe.AddIngredient<PocketEbonsand>();
            recipe.AddIngredient<PocketCrimsand>();
            recipe.AddIngredient<PocketPearlsand>();
            recipe.AddIngredient(ItemID.ObsidianSkull, 3);
            recipe.AddIngredient(ItemID.SoulofNight, 18);
           
            recipe.AddIngredient(ItemID.SoulofMight, 18);
            recipe.AddIngredient(ItemID.SoulofFright, 18);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            if (ModLoader.TryGetMod("Consolaria", out Mod ConsMerica) && ConsMerica.TryFind("SoulofBlight", out ModItem SoulofBlight))


            {
                recipe.AddIngredient(SoulofBlight.Type, 18);



            }
           
            if (!ModLoader.TryGetMod("Consolaria", out Mod Cons2Merica))


            {

                recipe.AddIngredient(ItemID.SoulofFlight, 18);


            }

            if (ModLoader.TryGetMod("ContinentOfJourney", out Mod HomeMerica) && HomeMerica.TryFind<ModItem>("NetherStar", out ModItem NetherStar))



            {

                recipe.AddIngredient(NetherStar.Type);


            }

        }
        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7f, -5f);
        }
    }
}