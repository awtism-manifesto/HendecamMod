using HendecamMod.Content.Global;
using HendecamMod.Content.Items.Consumables;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items;

public class PyrrhicClaymore : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 59;
        Item.useAnimation = 11;
        Item.useTime = 11;
        Item.knockBack = 9f;
        Item.useStyle = ItemUseStyleID.Rapier; // Makes the player do the proper arm motion

        Item.width = 32;
        Item.height = 32;
        Item.UseSound = SoundID.Item1;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.autoReuse = false;
        Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
        Item.noMelee = true; // The projectile will do the damage and not the item

        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(0, 19, 50, 0);

        Item.shoot = ModContent.ProjectileType<PyrrhicProj>(); // The projectile is what makes a shortsword work
        Item.shootSpeed = 7.65f; // This value bleeds into the behavior of the projectile as velocity, keep that in mind when tweaking values
    }

    public override bool AltFunctionUse(Player player)
    {
        return true;
    }

    // if (ModLoader.TryGetMod("Terbritish", out Mod TerBritish))
    // Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(9f));
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(11.11f));
        if (player.altFunctionUse == 2)
        {
            int proj = Projectile.NewProjectile(source, position, velocity * 2.33f, ModContent.ProjectileType<PyrrhicProjThrown>(), (int)(damage * 0.67f), (int)(knockback * 0.99f), player.whoAmI);
            Main.projectile[proj].GetGlobalProjectile<PyrrhicComboSetup>().fromthePyrrhicClaymore = true;
            player.AddBuff(BuffID.Ichor, 36);

            return false;
        }
        else
        {
            int proj = Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            Main.projectile[proj].GetGlobalProjectile<PyrrhicCombo>().fromPyrrhicClaymore = true;
            Main.projectile[proj].GetGlobalProjectile<PyrrhicComboSetup>().fromthePyrrhicClaymore = false;
            player.AddBuff(BuffID.Ichor, 36);
            return false; // Prevent vanilla projectile spawn
        }
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Deals great damage, but inflicts the user with Ichor");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Right click to throw the claymore")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Set up a combo by throwing the claymore, complete it by stabbing")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Completed combos deal increased damage and grant the player a powerful but short defense buff")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {

        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<VerdantClaymore>();
       
        recipe.AddIngredient(ItemID.SoulofNight, 10);
        recipe.AddIngredient<PyriteBar>(12);
        recipe.AddIngredient(ItemID.Ichor, 15);


        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}