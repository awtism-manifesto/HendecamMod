using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Armor;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;

public class CanofLasers : ModItem
{
   

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Orange;
        Item.value = 35500;
        Item.defense = 1;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.CanOfWorms);
        recipe.AddIngredient<LaserCharge>(1000);
       
        recipe.AddTile(TileID.Anvils);
        recipe.AddOnCraftCallback(GiveWorms);
        void GiveWorms(Recipe recipe, Item item, List<Item> consumedItems, Item destinationStack)
        {
            Main.LocalPlayer.GetItem(Main.myPlayer, new Item(ItemID.Worm, Main.rand.Next(5, 12)), default(GetItemSettings));
        }
        recipe.Register();
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "All projectile attacks shoot an extra laser projectile every 4th shot");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'Why'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<Lasering>().HolUpImBeamin = true;

    }
    public class Lasering : ModPlayer
    {
        public bool HolUpImBeamin;
        private int shotCounter = 0;

        public override void ResetEffects()
        {
            HolUpImBeamin = false;
        }

        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
           
            if (HolUpImBeamin)
            {
                shotCounter++;
                if (shotCounter == 4)
                {
                   
                    Projectile.NewProjectile(
                        Player.GetSource_ItemUse(item),
                        position,
                        velocity,
                        ModContent.ProjectileType<ChargeLaser>(),
                        (int)(damage * 0.85f),
                        knockback,
                        Player.whoAmI
                    );
                    shotCounter = 0;
                }
            }
        }


    }
}