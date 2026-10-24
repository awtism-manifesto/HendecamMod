using HendecamMod.Common.Systems.Players;
using HendecamMod.Common.Utils;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Ranger;

public class PulsePistols : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
        FrontGunLayer.RegisterData(Item.type);
        BackGunLayer.RegisterData(Item.type);
    }
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 44; // Hitbox width of the item.
        Item.height = 18; // Hitbox height of the item.
        Item.scale = 0.69f;
        Item.rare = ItemRarityID.Cyan; // The color that the item's name will be in-game.
        Item.value = 250000;

        // Use Properties
        Item.useTime = 4; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 40; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.reuseDelay = 40;
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        Item.ArmorPenetration = 10;
        // Weapon Properties
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 47; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 0.25f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.
        Item.noUseGraphic = true;
        // Gun Properties
        Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 14.45f; // The speed of the projectile (measured in pixels per frame.)
        Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ProjectileType<PulseShot>();
    }

    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        return Main.rand.NextFloat() >= 0.5f;
    }
    public int attackCycle = 0;
    public static void Hold(Player player)
    {
        HendecamPlayer modPlayer = player.GetModPlayer<HendecamPlayer>();
        Vector2 mouse = modPlayer.MousePosition;

        if (mouse.X < player.Center.X)
            player.direction = -1;
        else
            player.direction = 1;

        float rotation = (Vector2.Normalize(mouse - player.MountedCenter) * player.direction).ToRotation();
        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, rotation * player.gravDir - modPlayer.recoilFront * player.direction - MathHelper.PiOver2 * player.direction);
        player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, rotation * player.gravDir - modPlayer.recoilBack * player.direction - MathHelper.PiOver2 * player.direction);
    }

    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        Hold(player);
    }
    public override void HoldStyle(Player player, Rectangle heldItemFrame)
    {
        Hold(player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        HendecamPlayer modPlayer = player.GetModPlayer<HendecamPlayer>();
        attackCycle = ++attackCycle % 2;

        modPlayer.recoilFront = 0.2f;

        
        modPlayer.recoilBack = 0.2f;
        
        const int NumProjectiles = 2; // The number of projectiles that this gun will shoot.
        
        Vector2 muzzleOffset = Vector2.Normalize(velocity) * 40f;
        position += new Vector2(10f * player.direction - (6 * attackCycle % 2 == 0 ? 0 : 1), -16f);
        position += muzzleOffset;
        for (int j = 0; j <= 5; j++)
        {
            Dust dust = Dust.NewDustDirect(position, 0, 0, DustID.UltraBrightTorch, 0, 0, 0, Color.White, Main.rand.NextFloat(0.5f, 1.2f));
            dust.noGravity = true;
            dust.velocity = Vector2.Normalize(velocity.RotatedByRandom(MathHelper.ToRadians(30))) * Main.rand.NextFloat(8f, 10f);
        }
        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(7.25f));

            newVelocity *= 1f - Main.rand.NextFloat(0.3f);
            SoundEngine.PlaySound(SoundID.Item91, player.position);
            // Create a projectile.
            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(4f, -18f);
    }
}