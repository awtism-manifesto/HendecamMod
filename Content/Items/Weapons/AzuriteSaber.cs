using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons
{

    public class AzuriteSaber : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 21;
            Item.useTime = 21;
            Item.damage = 31;
            Item.knockBack = 7.25f;
            Item.width = 40;
            Item.height = 40;
            Item.shootSpeed = 6f;
            Item.scale = 1.15f;
           
            Item.UseSound = SoundID.Item60;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 1); // Sell price is 5 times less than the buy price.
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<AzuriteSwing>();
            Item.noMelee = true; // This is set the sword itself doesn't deal damage (only the projectile does).
            Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
            Item.autoReuse = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.
            type = ModContent.ProjectileType<AzuriteWave>();                                             // Create a projectile.
            Projectile.NewProjectileDirect(source, position, velocity * 1.5f, type, (int)(damage * 0.8f), knockback, player.whoAmI);

            return true;
        }




        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "Releases a wave of Azurite energy");
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
            Recipe recipe = CreateRecipe();

           

           
            recipe.AddIngredient<AzuriteBar>(12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();






        }
    }
}
