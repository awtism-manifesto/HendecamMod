using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Legs)]
public class YelmutLeggings : ModItem
{
    public static readonly int MoveSpeedBonus = 10;
    public static readonly int MeleeCritBonus = 10;

   

    public override void SetDefaults()
    {
        Item.width = 22; // Width of the item
        Item.height = 18; // Height of the item
        Item.value = Item.sellPrice(gold : 15); // How many coins the item is worth
        Item.rare = ItemRarityID.LightRed; // The rarity of the item
        Item.defense = 12; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "10% increased melee crit chance and 8% increased ranged attack speed");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+60 max Mana and Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+1 max minion and sentry slot")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Effects of Fairy Boots")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Set Bonus compatible with Lycopite Armor")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "-Developer Item-")
        {
            OverrideColor = new Color(220, 40, 245)
        };
        tooltips.Add(line);

    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        if (head.type == ModContent.ItemType<LycopiteFedora>()|| head.type == ModContent.ItemType<LycopiteHelmet>() || head.type == ModContent.ItemType<LycopiteMask>() || head.type == ModContent.ItemType<YelmutsHelmet>()
            && body.type == ModContent.ItemType<YelmutFaeChestplate>() || body.type == ModContent.ItemType<LycopiteChestplate>())
        { 
            return true; 
        }
         return false;
    }
    public static readonly int AttackSpeedBonus = 8;
    public override void UpdateEquip(Player player)
    {
        if (player.rocketBoots <= 0)
        {
            player.moveSpeed += 1f;
            player.accRunSpeed = 12f;
            player.rocketBoots = 2;
            player.vanityRocketBoots = 2;

        }

        player.flowerBoots = true;

        player.GetCritChance(DamageClass.Melee) += MeleeCritBonus;
        player.GetAttackSpeed(DamageClass.Ranged) += AttackSpeedBonus / 100f;
        player.statManaMax2 += 60;
        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
        loboPlayer.MaxBonus += 60f;
        player.maxMinions += 1;
        player.maxTurrets += 1;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PoorMahogany>(30);
        recipe.AddIngredient<CeramicSheet>(15);
        recipe.AddIngredient<LycopiteBar>(10);
        recipe.AddIngredient(ItemID.ShadowScale, 8);
        recipe.AddIngredient(ItemID.FairyBoots);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient<PoorMahogany>(30);
        recipe.AddIngredient<CeramicSheet>(15);
        recipe.AddIngredient<LycopiteBar>(10);
        recipe.AddIngredient(ItemID.TissueSample, 8);
        recipe.AddIngredient(ItemID.FairyBoots);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();

    }

    public override void UpdateArmorSet(Player player)
    {

        player.GetModPlayer<YelmutBoost>().YelBuff = true;
        player.setBonus = "Tag enemies with Ranged or Magic weapons to deal improved damage with Melee or Stupid weapons";

    }

    public class YelmutBoost : ModPlayer
    {
        public bool YelBuff;

        public override void ResetEffects()
        {
            YelBuff = false;
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (YelBuff && item.DamageType.CountsAsClass<MagicDamageClass>() || YelBuff && item.DamageType.CountsAsClass<RangedDamageClass>())
            {
                target.AddBuff(ModContent.BuffType<YelmutTag>(), 480);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (YelBuff && proj.DamageType.CountsAsClass<MagicDamageClass>() || YelBuff && proj.DamageType.CountsAsClass<RangedDamageClass>())
            {
                target.AddBuff(ModContent.BuffType<YelmutTag>(), 480);
            }
        }

       
    }
}