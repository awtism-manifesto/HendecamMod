using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items;


public class CraterCreator : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 99;
        Item.height = 99;
        Item.scale = 1.5f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.autoReuse = true;

        Item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
        Item.damage = 208;
        Item.knockBack = 12.5f;
        Item.value = 9650000;
        Item.rare = ItemRarityID.Red;
        Item.UseSound = SoundID.Item14;

        Item.shoot = ModContent.ProjectileType<Crater>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 13.2f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }
    public float LobotometerCost = 9f;
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
        var line = new TooltipLine(Mod, "Face", "Creates a massive explosion with every swing");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Uses 9 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MeteoriteBar, 15);
        recipe.AddIngredient<AstatineBar>(10);
        recipe.AddIngredient<FragmentFlatEarth>(5);
        recipe.AddIngredient(ItemID.FragmentSolar, 5);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind("AstralBar", out ModItem AstralBar))
        {
            recipe.AddIngredient(AstralBar.Type, 5);
        }
    }
}