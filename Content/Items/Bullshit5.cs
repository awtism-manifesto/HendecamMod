using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Rarities;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class Bullshit5 : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.staff[Type] = true; // This makes the useStyle animate as a staff instead of as a gun.
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
    }

    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1f;
        Item.rare = ModContent.RarityType<Seizure2>();
        Item.value = Item.buyPrice(gold: 199888);

        // Use Properties
        Item.useTime = 16; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 16; // The length of the item's use animation in ticks (60 ticks == 1 second.)

        Item.useStyle = ItemUseStyleID.Swing; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        Item.mana = 25;
        // Weapon Properties
        Item.DamageType = ModContent.GetInstance<OmniDamage>();
        Item.damage = 6969; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 99f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.

        // Gun Properties
        Item.shoot = ModContent.ProjectileType<BullshitEnergyAxe>(); // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 9f; // The speed of the projectile (measured in pixels per frame.)
    }

    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public float LobotometerCost = 10f;
    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.reuseDelay = 0;
            Item.ArmorPenetration = 225;
            Item.autoReuse = true;
            Item.mana = 10;
            LobotometerCost = 10f;
        }
        else
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.ArmorPenetration = 75;
            Item.shoot = ModContent.ProjectileType<BullshitEnergyAxe>();
            Item.mana = 25;
            LobotometerCost = 25f;
        }

        return base.CanUseItem(player);
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (player.altFunctionUse == 2)
        {
            for (int i = 0; i < 1; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(20f));

                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.25f);

                Projectile.NewProjectile(source, position, newVelocity * 2.1f, type, damage, knockback, player.whoAmI);
            }

            SoundEngine.PlaySound(SoundID.NPCDeath51, player.position);
            SoundEngine.PlaySound(SoundID.NPCDeath44, player.position);
            SoundEngine.PlaySound(SoundID.NPCDeath55, player.position);
            SoundEngine.PlaySound(SoundID.Item32, player.position);
            SoundEngine.PlaySound(SoundID.Item103, player.position);

            return false;
        }

        {
            SoundEngine.PlaySound(SoundID.Item71, player.position);
            SoundEngine.PlaySound(SoundID.NPCDeath51, player.position);
            SoundEngine.PlaySound(SoundID.NPCDeath44, player.position);
            SoundEngine.PlaySound(SoundID.NPCDeath55, player.position);
            SoundEngine.PlaySound(SoundID.Item32, player.position);
            SoundEngine.PlaySound(SoundID.Item103, player.position);

            damage = (int)(damage * Main.rand.NextFloat(0.99f, 0.995f));

            float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), ModContent.ProjectileType<BullshitSwing>(), (int)(damage * 1.35f), knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.

            Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(22.5f));
            Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            Vector2 new2Velocity = velocity.RotatedBy(MathHelper.ToRadians(-22.5f));
            Projectile.NewProjectile(source, position, new2Velocity, type, damage, knockback, player.whoAmI);

            return true; // Return false because we don't want tModLoader to shoot projectile}
        }
    }
   
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Left click to swing and release homing axes that split into all the bullshit from before");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Right click to cast these axes quicker, without the added melee damage")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Melee hits inflict every applicable debuff in the game")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Uses 25 Lobotometer on left click, 10 on right click")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "-Developer Item-")
        {
            OverrideColor = new Color(252, 141, 204)
        };
        tooltips.Add(line);
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-0.5f, -0.5f);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<Bullshit4>();
        recipe.AddIngredient<EternalCrab>();
        recipe.AddIngredient<RiverBanner>();
        recipe.AddIngredient<BlackHole>();

        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }
}