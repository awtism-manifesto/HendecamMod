using System.Collections.Generic;
using HendecamMod.Content.Projectiles;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Melee;

public class FlinxsFurblade : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 17;
        Item.useAnimation = 17;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 34;
        Item.knockBack = 2.5f;
        Item.scale = 1.2f;
        Item.value = 120000;
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
        Item.shoot = ProjectileType<FurBall>();
        Item.shootSpeed = 16.66f;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 1; // The number of projectiles that this gun will shoot.
        damage = (int)(damage * 0.8f);
        for (int i = 0; i < NumProjectiles; i++)
        {
            // Rotate the velocity randomly by 30 degrees at max.
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(2));

            // Decrease velocity randomly for nicer visuals.
            newVelocity *= 1f - Main.rand.NextFloat(0.25f);

            // Create a projectile.
            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; // Return false because we don't want tModLoader to shoot projectile
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        if (ModLoader.TryGetMod("Fargowiltas", out Mod FargoMerica2) && FargoMerica2.TryFind("Cyborg", out ModItem Cyborg))
        {
            recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.DeerclopsBossBag);
            recipe.Register();
        }
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Flings bouncy furballs with every swing");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        
    }
}