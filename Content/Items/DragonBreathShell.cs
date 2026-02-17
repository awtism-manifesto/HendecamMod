using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items;

public class DragonBreathShell : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 17; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = DamageClass.Ranged;

        Item.width = 13;
        Item.height = 13;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 0f;
        Item.value = 64;
        Item.rare = ItemRarityID.LightRed;
        Item.shoot = ModContent.ProjectileType<DragonSpawn>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 7.25f; // The speed of the projectile.
        Item.ammo = AmmoID.Bullet; // The ammo class this ammo belongs to.
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Shoots a ton of flaming sparks out of your gun")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica) && ThorMerica.TryFind("SoulofPlight", out ModItem SoulofPlight))
        {
            recipe = CreateRecipe(400);
            recipe.AddIngredient<RefinedOil>();
            recipe.AddIngredient(SoulofPlight.Type);
            recipe.AddIngredient<FireDiamond>();
            recipe.AddIngredient<PurifiedSalt>();
            recipe.AddIngredient(ItemID.EmptyBullet, 400);
            recipe.AddTile(TileID.Furnaces);

            recipe.Register();
        }

        else
        {
            recipe = CreateRecipe(250);
            recipe.AddIngredient<RefinedOil>();
            recipe.AddIngredient<FireDiamond>();
            recipe.AddIngredient<PurifiedSalt>();
            recipe.AddIngredient(ItemID.EmptyBullet, 250);
            recipe.Register();
        }
    }
}