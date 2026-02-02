using System.Collections.Generic;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons;

public class AzuriteDualStaff : ModItem
{
    private int shotCounter;

    public override void SetStaticDefaults()
    {
        Item.staff[Type] = true; // This makes the useStyle animate as a staff instead of as a gun.
    }

    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;

        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 33;
        Item.useAnimation = 33;
        Item.autoReuse = true;

        Item.mana = 16;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 77;
        Item.knockBack = 6.7f;
        Item.noMelee = true;

        Item.value = 215000;
        Item.rare = ItemRarityID.Orange;
        Item.shoot = ModContent.ProjectileType<AzuriteBeam>();
        Item.shootSpeed = 12.5f;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (shotCounter <= 0)
        {
            Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(0f));
            SoundEngine.PlaySound(SoundID.Item91, player.position);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, (int)(damage * 1.05f), knockback, player.whoAmI);
            shotCounter = 2;
        }
        else if (shotCounter == 2)
        {
            Vector2 new2Velocity = velocity.RotatedBy(MathHelper.ToRadians(180f));
            SoundEngine.PlaySound(SoundID.Item91, player.position);

            Projectile.NewProjectileDirect(source, position, new2Velocity, type, (int)(damage * 2f), knockback, player.whoAmI);
            shotCounter = 0;
        }

        return false;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Shoots bolts of azurite energy forwards and backwards");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "The backwards bolt deals double damage")
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
        recipe.AddIngredient<AzuriteBar>(12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}