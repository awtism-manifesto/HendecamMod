using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items;

/// <summary>
///     Star Wrath/Starfury style weapon. Spawn projectiles from sky that aim towards mouse.
///     See Source code for Star Wrath projectile to see how it passes through tiles.
///     For a detailed sword guide see <see cref="ExampleSword" />
/// </summary>
public class ComicallyLargeTurkeyLeg : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 144;
        Item.height = 122;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 42;
        Item.useAnimation = 42;
        Item.autoReuse = true;
        Item.scale = 2f;
        Item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
        Item.damage = 67;
        Item.knockBack = 7;
        Item.value = Item.buyPrice(gold: 5);
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item1;

        Item.shoot = ModContent.ProjectileType<GreaseBomb>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 23.25f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }
    public float LobotometerCost = 11f;
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
        var line = new TooltipLine(Mod, "Face", "Flings grease bombs at your enemies");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Uses 11 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.ChickenNugget, 2);
        recipe.AddIngredient<GiantBone>();

        recipe.AddTile(TileID.Hellforge);
        recipe.Register();
    }
}