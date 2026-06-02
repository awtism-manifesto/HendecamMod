using HendecamMod.Content.Items.Weapons.Ranger;
using HendecamMod.Content.Projectiles;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class M1Garand : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 0.7f;
        Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
        Item.value = Item.buyPrice(silver: 4250);
        // Use Properties
        Item.useTime = 19; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 19; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        // The sound that this item plays when used.
       
        // Weapon Properties
        Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
        Item.damage = 81; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 4f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.
        Item.crit = 9;
        Item.ArmorPenetration = 12;
        // Gun Properties
        Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 11f; // The speed of the projectile (measured in pixels per frame.)
        Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
    }
    private int shotCounter;
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (shotCounter <= 7)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(4.4f));


            SoundEngine.PlaySound(new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/GarandShot")
            {
                Volume = 2f,
                PitchVariance = 0f,
                MaxInstances = 10,
            });
            Item.useTime = 19; 
            Item.useAnimation = 19; 

            Projectile.NewProjectileDirect(source, position, newVelocity, type, (int)(damage * 1f), knockback, player.whoAmI);
            shotCounter ++;
        }
        else if (shotCounter == 8)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(4.4f));
            SoundEngine.PlaySound(new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/GarandShot")
            {
                Volume = 2f,
                PitchVariance = 0f,
                MaxInstances = 10,
            });

            Item.useTime = 38;
            Item.useAnimation = 38;

            Projectile.NewProjectileDirect(source, position, newVelocity, type, (int)(damage * 1f), knockback, player.whoAmI);
            shotCounter = 9;
        }
        else if (shotCounter == 9)
        {
            Vector2 new2Velocity = velocity.RotatedBy(MathHelper.ToRadians(180f));

            type = ProjectileType<GarandMag>();
           
            SoundEngine.PlaySound(new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/GarandPing")
            {
                Volume = 2.67f,
                PitchVariance = 0f,
                MaxInstances = 2,
            });
            Item.useTime = 19;
            Item.useAnimation = 19;

            Projectile.NewProjectileDirect(source, position, new2Velocity * 0.5f, type, (int)(damage * 0.25f), knockback, player.whoAmI);
            shotCounter = 0;
        }

        return false;
    }


    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        if (ModLoader.TryGetMod("Macrocosm", out Mod MacroMerica) && MacroMerica.TryFind("SteelRifle", out ModItem SteelRifle))
        {
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CobaltBar, 5);
            recipe.AddIngredient(SteelRifle.Type);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PalladiumBar, 5);
            recipe.AddIngredient(SteelRifle.Type);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        else
        {
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CobaltBar, 5);
            recipe.AddIngredient<AntiqueRifle>();
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PalladiumBar, 5);
            recipe.AddIngredient<AntiqueRifle>();
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-32.25f, -1f);
    }
}