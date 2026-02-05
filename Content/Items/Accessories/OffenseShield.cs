using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Armor;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Shield)] 
public class OffenseShield : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32; 
        Item.height = 32; 
        Item.rare = ItemRarityID.LightPurple;
        Item.value = 750000;
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
        recipe.AddIngredient(ItemID.AvengerEmblem);
        recipe.AddIngredient<PanicShield>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
       
    }
    public class OffenseProc : ModPlayer
    {
      
        private const int buffCooldownMax = 60 * 8;

        public bool Offended;
        private int buffCooldown;

        public override void ResetEffects()
        {
            Offended = false;
        }

        public override void PostUpdate()
        {
            if (buffCooldown > 0)
                buffCooldown--;
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (!Offended)
                return;
            Player.AddBuff(BuffID.Panic, 480);

            if (buffCooldown > 0)
                return;
            Player.AddBuff(ModContent.BuffType<SheildOffense>(), 240);
           
            buffCooldown = buffCooldownMax;
        }
    }
}