using HendecamMod.Content.Items.Armor;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Shield)] 
public class PanicShield : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32; 
        Item.height = 32; 
        Item.rare = ItemRarityID.Orange; 
        Item.value = 216000;
        Item.maxStack = 1;
        Item.accessory = true;
        Item.defense = 3;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
       
        player.GetModPlayer<PanicProc>().Panicked = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
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
    public class PanicProc : ModPlayer
    {
      
        //private const int ExplosionCooldownMax = 60 * 15;

        public bool Panicked;
       // private int explosionCooldown;

        public override void ResetEffects()
        {
            Panicked = false;
        }

        public override void PostUpdate()
        {
            // Cooldown ticking down
           // if (explosionCooldown > 0)
               // explosionCooldown--;

          
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            // Only trigger if set bonus is active
            if (!Panicked)
                return;

            Player.AddBuff(BuffID.Panic, 480);

            // Start cooldown
           // explosionCooldown = ExplosionCooldownMax;
        }
    }
}