using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Accessories;


public class PlatinumChip : ModItem
{
   
    public override void SetDefaults()
    {
        Item.width = 22; 
        Item.height = 18; 
        Item.value = Item.sellPrice(gold: 2); 
        Item.rare = ItemRarityID.White; 
        Item.defense = 1; 
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Causes platinum shards to fly out when you get hit");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }
   

  

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<PlatShat>().Shat = true;
    }

    public override void AddRecipes()
    {
        Recipe
            recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.PlatinumBar, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();


    }


}

public class PlatShat : ModPlayer
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
        

        int baseDamage = 12;

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
            ProjectileType<PlatSpawn>(),
            calculatedDamage,
            3f,
            Player.whoAmI
        );

      
    }
}