using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items.Weapons.Multiclass;

/// <summary>
///     Star Wrath/Starfury style weapon. Spawn projectiles from sky that aim towards mouse.
///     See Source code for Star Wrath projectile to see how it passes through tiles.
///     For a detailed sword guide see <see cref="ExampleSword" />
/// </summary>
public class WaterflameSword : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 32;
        Item.useAnimation = 32;
        Item.autoReuse = true;
        Item.DamageType = GetInstance<MeleeMagicDamage>();
        Item.damage = 36;
        Item.knockBack = 3f;
        Item.scale = 1.15f;
        Item.mana = 6;
        Item.value = Item.buyPrice(gold: 4);
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item1;
        Item.useTurn = true;
        Item.shoot = ProjectileType<Waterflame>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 11.5f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }
}