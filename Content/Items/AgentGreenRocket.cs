using System.Collections.Generic;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items;

public class AgentGreenRocket : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 36; 
        Item.DamageType = DamageClass.Ranged;
        Item.width = 16;
        Item.height = 16;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 3.5f;
        Item.value = 101;
        Item.rare = ItemRarityID.LightRed;
        Item.shoot = ModContent.ProjectileType<AgentGreenProj>();
        Item.shootSpeed = 12.85f;
        Item.ammo = AmmoID.Rocket;
    }

    public override void SetStaticDefaults()
    {
        AmmoID.Sets.IsSpecialist[Type] = true;
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.RocketLauncher].Add(Type, ModContent.ProjectileType<AgentGreenProj>());
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.GrenadeLauncher].Add(Type, ModContent.ProjectileType<AgentGreenProj>());
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.ProximityMineLauncher].Add(Type, ModContent.ProjectileType<AgentGreenProj>());
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.SnowmanCannon].Add(Type, ModContent.ProjectileType<AgentGreenProj>());
        AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.Celeb2].Add(Type, ProjectileID.Celeb2Rocket);
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'Didn't we use these back in 'nam?'")
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
        Recipe recipe = CreateRecipe(75);
        recipe.AddIngredient(ItemID.CursedFlame);
        recipe.AddIngredient<RocketNeg1>(75);
        recipe.Register();
    }
}