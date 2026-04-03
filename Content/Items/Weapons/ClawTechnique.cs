using System.Collections.Generic;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items.UnarmedProjectiles;
using Terraria.DataStructures;


namespace HendecamMod.Content.Items.Weapons;

public class ClawTechnique : ModItem 
{
    public override void SetDefaults()
    {
        Item.damage = 6;
        Item.knockBack = 2.25f;
        Item.useStyle = ItemUseStyleID.Rapier; // Makes the player do the proper arm motion
        Item.useAnimation = 24;
        Item.useTime = 24;
        Item.width = 32;
        Item.height = 32;
        Item.UseSound = SoundID.Item1;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.autoReuse = false;
        Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
        Item.noMelee = true; // The projectile will do the damage and not the item
        Item.ArmorPenetration = 5;
        Item.rare = ItemRarityID.White;
        Item.value = 67;

        Item.shoot = ModContent.ProjectileType<ClawProj>(); // The projectile is what makes a shortsword work
        Item.shootSpeed = 0.76f; // This value bleeds into the behavior of the projectile as velocity, keep that in mind when tweaking values
    }

    
   
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Able to rapidly slash at nearby enemies");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Ignores 5 enemy armor")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    
}

















// six seven