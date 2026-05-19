using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles.Items.UnarmedProjectiles;


namespace HendecamMod.Content.Items.Weapons.Other;

public class RingTechnique : ModItem
    {
    public override void SetDefaults()
        {
        Item.damage = 6;
        Item.knockBack = 2.25f;
        Item.useStyle = ItemUseStyleID.Swing; // Makes the player do the proper arm motion
        Item.useAnimation = 24;
        Item.useTime = 24;
        Item.width = 32;
        Item.height = 32;
        Item.channel = true;
        Item.UseSound = SoundID.Item1;
        Item.scale = 0.1f;
        Item.DamageType = ModContent.GetInstance<UnarmedDamage>();
        Item.autoReuse = false;
        Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
        Item.noMelee = false; // The projectile will do the damage and not the item
        Item.ArmorPenetration = 5;
        Item.rare = ItemRarityID.LightRed;
        Item.value = 67;
        Item.shoot = ModContent.ProjectileType<RingProj>(); // The projectile is what makes a shortsword work
        Item.shootSpeed = 0.01f; // This value bleeds into the behavior of the projectile as velocity, keep that in mind when tweaking values
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