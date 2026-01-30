using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Poop;

public class Shitballs : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 9; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = ModContent.GetInstance<RangedStupidDamage>();

        Item.width = 13;
        Item.height = 13;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 1.5f;
        Item.value = 10;
        Item.rare = ItemRarityID.White;
        Item.shoot = ModContent.ProjectileType<ShitBallProjectile>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 5.1f; // The speed of the projectile.
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

        line = new TooltipLine(Mod, "Face", "Poisons your enemies, and even worse, makes them stinky.")
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
        Recipe recipe = CreateRecipe(150);
        recipe.AddIngredient(ItemID.PoopBlock);
        recipe.AddIngredient(ItemID.MusketBall, 150);
        recipe.Register();
    }
}




