using System.Collections.Generic;
using HendecamMod.Content.Items.Placeables;

namespace HendecamMod.Content.Items.Weapons.Ranger;

public class AzuriteBow : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 62;
        Item.height = 32;
        Item.scale = 1.1f;
        Item.rare = ItemRarityID.Orange;
        Item.value = 215000;
        Item.useTime = 24;
        Item.useAnimation = 24; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item5;
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 34; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 1.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true;
        Item.shoot = ProjectileID.WoodenArrowFriendly; // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 11f; // The speed of the projectile (measured in pixels per frame.)
        Item.useAmmo = AmmoID.Arrow; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Converts arrows into Azurite arrows");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Azurite arrows have powerful homing")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AzuriteBar>(10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-1f, -1f);
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ProjectileType<Projectiles.AzuriteArrowProjectile>();
    }
}