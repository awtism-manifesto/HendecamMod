using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class TheMoon : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 42;
        Item.useAnimation = 42;
        Item.autoReuse = true;
        Item.DamageType = ModContent.GetInstance<StupidDamage>();
        Item.damage = 1230;
        Item.knockBack = 35;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.ArmorPenetration = 35;
        Item.value = 4250000;
        Item.rare = ItemRarityID.Red;
        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<TheFuckingMoon>();
        Item.shootSpeed = 16f;
    }
    public float LobotometerCost = 18f;
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 1; 

        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(2));

            newVelocity *= 1f - Main.rand.NextFloat(0.33f);

            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return false; 
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Literally throws the fucking moon at your enemies");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Inflicts hit enemies with Moon Burn for a long time")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Uses 18 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<LunarGem>(100);
        recipe.AddIngredient<FragmentFlatEarth>(10);
        recipe.AddIngredient(ItemID.LunarBar, 8);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}