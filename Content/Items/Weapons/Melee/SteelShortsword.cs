using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Melee;

public class SteelShortsword : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 15;
        Item.knockBack = 2.5f;
        Item.useStyle = ItemUseStyleID.Rapier; // Makes the player do the proper arm motion
        Item.useAnimation = 12;
        Item.useTime = 12;
        Item.width = 32;
        Item.height = 32;
        Item.UseSound = SoundID.Item1;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.autoReuse = false;
        Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
        Item.noMelee = true; // The projectile will do the damage and not the item

        Item.rare = ItemRarityID.White;
        Item.value = 31000;

        Item.shoot = ProjectileType<SteelShortswordProj>(); // The projectile is what makes a shortsword work
        Item.shootSpeed = 3.65f; // This value bleeds into the behavior of the projectile as velocity, keep that in mind when tweaking values
    }

   
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<SteelBar>(6);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Right click to throw the saber");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    
}