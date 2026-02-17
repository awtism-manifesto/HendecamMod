using System.Collections.Generic;
using HendecamMod.Content.Projectiles;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class ThePeoplesPitchfork : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 39;
        Item.useAnimation = 39;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Ranged;
        Item.damage = 16;
        Item.knockBack = 6.67f;
        Item.noMelee = true;
        Item.noUseGraphic = true; 
        Item.value = Item.buyPrice(silver: 10);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<PitchforkProj>(); 
        Item.shootSpeed = 10.33f;
        }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 1;

        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(4.5f));

            newVelocity *= 1f - Main.rand.NextFloat(0.175f);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; 
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'Perfect for impaling the rich before eating them!'")
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
        Recipe recipe = CreateRecipe();
        if (ModLoader.TryGetMod("bitsnbobs", out Mod YelMerica) && YelMerica.TryFind("PoorMahoganyBlock", out ModItem PoorMahoganyBlock))
        {
            recipe = CreateRecipe();
            recipe.AddIngredient(PoorMahoganyBlock.Type, 18);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.Register();
        }
        else
        {
            recipe = CreateRecipe();
            recipe.AddRecipeGroup("Wood", 18);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.Register();
        }
    }
}