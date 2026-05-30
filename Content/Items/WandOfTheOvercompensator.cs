using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class WandOfTheOvercompensator : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.25f;
        Item.rare = ItemRarityID.Orange; // The color that the item's name will be in-game.
        Item.value = 215000;
        // Use Properties
        // Use Properties
        Item.useTime = 48; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 48; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
        // The sound that this item plays when used.
        Item.UseSound = SoundID.Item20;
        // Weapon Properties
        Item.DamageType = GetInstance<AutismDamage>();
        Item.damage = 42; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 7.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.
        Item.mana = 12;
        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shoot = ProjectileType<FireBlast>();

        Item.shootSpeed = 11.5f; // The speed of the projectile (measured in pixels per frame.)
    }
    public float LobotometerCost = 12f;
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        type = ProjectileType<FireBlast>();
    }

    

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Uses 12 Braincells");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Lets loose a huge blast of fire that splits into many flame jets")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "The perfect wand for wizards who are insecure about their.. y'know.")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FlowerofFire);
        recipe.AddIngredient(ItemID.WandofSparking);
        recipe.AddIngredient(ItemID.Wood, 100);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-14.5f, -3f);
    }
}