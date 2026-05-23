using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Multiclass;

public class RustyDustySawThrower : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;

        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 28;
        Item.useAnimation = 28;
        Item.autoReuse = true;
        Item.DamageType = GetInstance<OmniDamage>();
        Item.damage = 25;
        Item.knockBack = 5.25f;

        Item.noMelee = true;
        Item.value = 195000;
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item99;
        Item.scale = 1f;

        Item.shoot = ProjectileType<DustySawblade>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 9.75f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }
    public float LobotometerCost = 4f;
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
        var line = new TooltipLine(Mod, "Face", "Shoots piercing sawblades that leave a trail of sawdust in their wake");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Uses 4 Braincells")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-2f, -3f);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<Sawdust>(250);
        recipe.AddIngredient(ItemID.Sawmill);
       
        recipe.AddIngredient(ItemID.IllegalGunParts);
        recipe.AddTile(TileID.Sawmill);
        recipe.Register();
    }
}