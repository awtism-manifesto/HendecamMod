using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Multiclass;

public class LimestoneThrowingRock : ModItem
{
    public override void SetDefaults()
    {
        // Alter any of these values as you see fit, but you should probably keep useStyle on 1, as well as the noUseGraphic and noMelee bools

        // Common Properties
        Item.rare = ItemRarityID.White;
        Item.value = Item.sellPrice(copper: 5);
        Item.maxStack = 9999;

        // Use Properties
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 23;
        Item.useTime = 23;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.consumable = true;

        // Weapon Properties			
        Item.damage = 15;
        Item.knockBack = 3.67f;
        Item.noUseGraphic = true; // The item should not be visible when used
        Item.noMelee = true; // The projectile will do the damage and not the item
        Item.DamageType = GetInstance<RangedStupidDamage>();

        // Projectile Properties
        Item.shootSpeed = 9.15f;
        Item.shoot = ProjectileType<LimestoneRockProj>(); // The projectile that will be thrown
    }
    public float LobotometerCost = 1f;
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
        var line = new TooltipLine(Mod, "Face", "Bursts into a lingering cloud of toxic limestone dust");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);


    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(25);

        recipe.AddIngredient<Limestone>(10);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
        
    }
}