
using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;


public class UraniumChip : ModItem
{

    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 18;
        Item.value = Item.sellPrice(gold: 5);
        Item.rare = ItemRarityID.Green;
        Item.defense = 3;
        Item.accessory = true;
    }




    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<UrShat>().Shat = true;
    }

    public override void AddRecipes()
    {
        Recipe
            recipe = CreateRecipe();
        recipe.AddIngredient<PlatinumChip>();
        recipe.AddIngredient<UraniumBar>(5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();


    }


}

public class UrShat : ModPlayer
{


    public bool Shat;


    public override void ResetEffects()
    {
        Shat = false;
    }



    public override void OnHurt(Player.HurtInfo info)
    {
        if (!Shat)
            return;


        int baseDamage = 20;

        float totalMultiplier = 1f; // Base 100%

        // Generic damage (100% effectiveness)
        totalMultiplier += Player.GetDamage(DamageClass.Generic).Additive - 1f;

        // Get all damage classes and check if they should contribute to OmniDamage
        // OmniDamage gets 67% of specialized class bonuses
        DamageClass[] specializedClasses = new DamageClass[] {
        DamageClass.Magic,
        DamageClass.Melee,
        DamageClass.Ranged,
        DamageClass.Summon,
        DamageClass.Throwing,
        GetInstance<StupidDamage>()
        };
        foreach (var damageClass in specializedClasses)
        {
            // Add 67% of the class's bonus
            float classBonus = Player.GetDamage(damageClass).Additive - 1f;
            if (classBonus > 0)
            {
                totalMultiplier += classBonus * 0.67f;
            }
        }

        // Calculate final damage
        int calculatedDamage = (int)(baseDamage * totalMultiplier);


        Projectile.NewProjectile(
            Player.GetSource_FromThis(),
            Player.Center,
            new Vector2(0f, -5f),
            ProjectileType<UraniumSpawn>(),
            calculatedDamage,
            3f,
            Player.whoAmI
        );


    }
}