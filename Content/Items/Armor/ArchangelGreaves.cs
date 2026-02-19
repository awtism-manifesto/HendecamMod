using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Legs)]
public class ArchangelGreaves : ModItem
{
    public static readonly int MoveSpeedBonus = 40;
  
    public static readonly int AttackSpeedBonus = 15;
  
   
    public override void SetDefaults()
    {
        Item.width = 32; // Width of the item
        Item.height = 28; // Height of the item
        Item.value = Item.sellPrice(gold: 777); // How many coins the item is worth
        Item.rare = ItemRarityID.Purple; // The rarity of the item
        Item.defense = 36; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "+40% increased movement speed and acceleration and +15% attack speed");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Significantly boosts max life")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<ArchangelHelmet>() && body.type == ModContent.ItemType<ArchangelChestguard>();
    }

    public override void UpdateEquip(Player player)
    {
        player.statLifeMax2 = (int)(player.statLifeMax2 * 1.05) + 35;

        player.GetAttackSpeed(DamageClass.Generic) += AttackSpeedBonus / 100f;
      
        player.moveSpeed += MoveSpeedBonus / 100f;
        player.runAcceleration *= 1.5f;
        player.GetModPlayer<AngelSpeed>().Angel = true;

    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AngelShard>(10);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
    }

    public class AngelSpeed : ModPlayer
    {
        public bool Angel;

        public override void ResetEffects()
        {
            Angel = false;
        }

        public override void PostUpdateRunSpeeds()
        {
            
            if (!Angel)
            {
                return;
            }

            Player.runAcceleration *= 1.35f; // Modifies player run acceleration
            Player.maxRunSpeed *= 1.35f;
            Player.accRunSpeed *= 1.35f;
            Player.runSlowdown *= 1.35f;
        }
    }
}