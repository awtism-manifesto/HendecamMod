using System.Collections.Generic;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items;

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
        Item.value = 54;
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
        var line = new TooltipLine(Mod, "Face", "Randomly bounces in a new direction whenever it hits an enemy");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
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