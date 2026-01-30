using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;




namespace HendecamMod.Content.Items;


public class SporeShiv : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 20;
        Item.useTime = 20;
        Item.damage = 27;
        Item.knockBack = 4.15f;
        Item.width = 40;
        Item.height = 40;
        Item.shootSpeed = 11.33f;
        Item.scale = 1f;
        Item.noUseGraphic = true;
        Item.UseSound = SoundID.Item1;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.buyPrice(gold: 9); // Sell price is 5 times less than the buy price.
        Item.DamageType = DamageClass.Ranged;
        Item.shoot = ModContent.ProjectileType<SporeShivProj>();
        Item.noMelee = true; // This is set the sword itself doesn't deal damage (only the projectile does).
        Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
        Item.autoReuse = true;
        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))
        {
            Item.DamageType = DamageClass.Throwing;

        }
    }






    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Spore shivs duplicate themselves upon hitting an enemy");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))
        {

            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod Cross-Mod (Thorium): Now deals Throwing damage") { OverrideColor = Color.LightSeaGreen });
        }


    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<LycopiteBar>(10);


        recipe.AddTile(TileID.Anvils);
        recipe.Register();






    }


}
