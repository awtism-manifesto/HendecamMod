using System.Collections.Generic;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons.Ammo;

public class AnthraxRocket : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 55;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 16;
        Item.height = 16;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 3f;
        Item.value = 139;
        Item.rare = ItemRarityID.Lime;
        Item.shoot = ModContent.ProjectileType<CiaRocket>();
        Item.shootSpeed = 13f; 
        Item.ammo = AmmoID.Rocket;
    }

    public override void SetStaticDefaults()
    {
        AmmoID.Sets.IsSpecialist[Type] = true; 
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.RocketLauncher].Add(Type, ModContent.ProjectileType<CiaRocket>());
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.GrenadeLauncher].Add(Type, ModContent.ProjectileType<CiaRocket>());
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.ProximityMineLauncher].Add(Type, ModContent.ProjectileType<CiaRocket>());
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.SnowmanCannon].Add(Type, ModContent.ProjectileType<CiaRocket>());
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.Celeb2].Add(Type, ProjectileID.Celeb2Rocket);
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Huge blast radius");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'Courtesy of the CIA!'")
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
        Recipe recipe = CreateRecipe(50);
        recipe.AddIngredient(ItemID.VialofVenom);
        recipe.AddIngredient(ItemID.RocketI, 50);
        recipe.Register();
    }
}