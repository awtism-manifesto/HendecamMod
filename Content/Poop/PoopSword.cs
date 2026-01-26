using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
namespace HendecamMod.Content.Poop;

// This is a basic item template.
// Please see tModLoader's ExampleMod for every other example:
// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
public class PoopSword : ModItem
{
    // The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.HendecamMod.hjson' file.
    public override void SetDefaults()
    {
        Item.damage = 11;
        Item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 17;
        Item.useAnimation = 17;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 3;
        Item.useTurn = true;
        Item.value = Item.buyPrice(copper: 1);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<PooShit>();
        Item.shootSpeed = 5.75f;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 1; // The number of projectiles that this gun will shoot.
        player.AddBuff(BuffID.Stinky, 61);
        damage = (int)(damage * Main.rand.NextFloat(0.66f, 0.667f));
        for (int i = 0; i < NumProjectiles; i++)
        {
            // Rotate the velocity randomly by 30 degrees at max.
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(0.15f));



            // Create a projectile.
            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);

        }

        return false; // Return false because we don't want tModLoader to shoot projectile
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Flings a weak clump of poop");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Makes the user stinky")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);




    }
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        // Inflict the OnFire debuff for 1 second onto any NPC/Monster that this hits.
        // 60 frames = 1 second
        target.AddBuff(BuffID.Stinky, 900);
        target.AddBuff(BuffID.Poisoned, 180);
        for (int i = 0; i < 7; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Poop);
            dust.noGravity = true;
            dust.velocity *= 7.5f;
            dust.scale *= 1.25f;
        }

    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddRecipeGroup("Wood", 6);
        recipe.AddIngredient(ItemID.PoopBlock, 6);

        recipe.Register();
    }
}
