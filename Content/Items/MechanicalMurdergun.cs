using HendecamMod.Content.Buffs;
using HendecamMod.Content.Global;
using HendecamMod.Content.Items.Weapons.Summon;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class MechanicalMurdergun : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 62;
        Item.height = 32;
        Item.scale = 0.85f;
        Item.rare = ItemRarityID.Pink;
        Item.value = 339000;

        Item.useTime = 12; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 12; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        // The sound that this item plays when used.
        Item.UseSound = SoundID.Item67;
        // Weapon Properties
        Item.DamageType = DamageClass.Magic; // Sets the damage type to ranged.
        Item.damage = 60; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 3.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.

        Item.ArmorPenetration = 20;
        Item.mana = 10;
        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shoot = ProjectileID.PurificationPowder;

        Item.shootSpeed = 13.25f; // The speed of the projectile (measured in pixels per frame.)
    }
   
    
    public override bool AltFunctionUse(Player player)
    {
        bool LaserOrCD = true;
       

        for (int i = 0; i < Player.MaxBuffs; i++)
        {
            int buffType = player.buffType[i];

            if (buffType == BuffType<DroneSwarmCooldown>())
            {
                LaserOrCD = false;
            }
        }

        if (!LaserOrCD)
        {

            CombatText.NewText(player.Hitbox, Color.Red, "On Cooldown!");

        }

        return LaserOrCD;
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ProjectileID.LightBeam;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (player.altFunctionUse == 2)
        {
            var laserPlayer = player.GetModPlayer<LaserCD>();

            player.AddBuff(BuffType<DroneSwarmCooldown>(), 2700);

            laserPlayer.LaserTimer = 900;

            SoundEngine.PlaySound(new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/ActivatedTurboCharge")
            {
                Volume = 2.67f,
                PitchVariance = 0.2f,
                MaxInstances = 3,
            });

        }
        else
        {
            const int NumProjectiles = 1; // The number of projectiles that this gun will shoot.

            for (int i = 0; i < NumProjectiles; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(2.33f));

                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.2f);

                int proj = Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                Main.projectile[proj].GetGlobalProjectile<FastLaserSwords>().fromMechGun = true;
            }
        }
        return false;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Shoots laser swords that pierce enemy armor and are, quite frankly, pretty badass");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Right Click to activate Drone Swarm, which causes all five probes to shoot along with the main gun for 15 seconds")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.LaserRifle);
        recipe.AddIngredient(ItemID.HallowedBar, 12);
        recipe.AddIngredient(ItemID.SoulofMight, 15);

        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        if (ModLoader.TryGetMod("Split", out Mod Sp) && Sp.TryFind("ElementOfDeath", out ModItem ElementOfDeath))
        {
            recipe.AddIngredient(ElementOfDeath.Type);
        }
    }

    // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-8f, 4f);
    }
}
public class LaserCD : ModPlayer
{
   
    public int LaserTimer { get; set; } = 0;

    public override void ResetEffects()
    {
       
        if (LaserTimer > 0)
        {
            LaserTimer--;
        }


    }
}