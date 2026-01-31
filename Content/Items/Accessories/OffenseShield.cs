using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Armor;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Shield)] // Load the spritesheet you create as a shield for the player when it is equipped.
public class OffenseShield : ModItem
{
    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.rare = ItemRarityID.LightPurple; // The color that the item's name will be in-game.
        Item.value = 975000;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.defense = 5;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
       
        player.GetModPlayer<OffenseProc>().Offended = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Massively increases damage output for a short time after being hit");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Also still grants Panic upon being hit")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.PanicNecklace);
       
        recipe.AddIngredient<DefenseShield>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
       
    }
    public class OffenseProc : ModPlayer
    {
      
        private const int ExplosionCooldownMax = 60 * 15;

        public bool Offended;
        private int explosionCooldown;

        public override void ResetEffects()
        {
            Offended = false;
        }

        public override void PostUpdate()
        {
            // Cooldown ticking down
            if (explosionCooldown > 0)
                explosionCooldown--;

          
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            // Only trigger if set bonus is active
            if (!Offended)
                return;

            Player.AddBuff(BuffID.Panic, 480);

            if (explosionCooldown > 0)
                return;
            Player.AddBuff(ModContent.BuffType<SheildOffense>(), 90);
           

            // Start cooldown
            explosionCooldown = ExplosionCooldownMax;
        }
    }
}