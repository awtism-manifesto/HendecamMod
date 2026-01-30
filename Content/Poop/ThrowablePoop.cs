using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Poop;

public class ThrowablePoop : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 12; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = DamageClass.Throwing;
        Item.useTime = 21;
        Item.useAnimation = 21;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.noUseGraphic = true;
        Item.width = 13;
        Item.height = 13;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 1.75f;
        Item.value = 3;
        Item.rare = ItemRarityID.White;
        Item.shoot = ModContent.ProjectileType<ThrownPoop>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 16.65f; // The speed of the projectile.

    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {

        player.AddBuff(BuffID.Stinky, 61);


        return true;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Makes both you and enemies stinky");
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
        Recipe recipe = CreateRecipe(33);

        recipe.AddIngredient(ItemID.PoopBlock);

        recipe.Register();

    }
}
