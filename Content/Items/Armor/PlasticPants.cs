using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using static HendecamMod.Content.Items.Armor.YelmutLeggings;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Legs)]
public class PlasticPants : ModItem
{
    public static readonly int AdditiveStupidDamageBonus = 3;
    public static readonly int MoveSpeedBonus = 6;
    public static readonly int StupidCritBonus = 3;
    public static readonly int StupidArmorPenetration = 5;
    public static LocalizedText SetBonusText { get; private set; }

    public override void SetStaticDefaults()
    {
        // If your head equipment should draw hair while drawn, use one of the following:
        // ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
        // ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
        // ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
        // ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true;
        SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs(AdditiveStupidDamageBonus);
    }

    public override void SetDefaults()
    {
        Item.width = 22; // Width of the item
        Item.height = 18; // Height of the item
        Item.value = 19500;
        Item.rare = ItemRarityID.Blue; // The rarity of the item
        Item.defense = 3; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "3% increased stupid damage and crit chance");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+10 max Lobotometer and +10% Lobotometer decay rate")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        
    }
    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect

    public override void UpdateEquip(Player player)
    {
       
        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 100f;
      
        player.GetCritChance<StupidDamage>() += StupidCritBonus;

        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
        loboPlayer.MaxBonus += 10f;

        var loboDecay = player.GetModPlayer<LobotometerPlayer>();
        loboDecay.DecayRateMultiplier *= 1.1f;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PlasticScrap>(27);
        recipe.AddIngredient<Polymer>(6);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<PlasticHeadgear>() && body.type == ModContent.ItemType<PlasticChestplate>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.GetModPlayer<PlasticApply>().Plasticy = true;

        player.setBonus = "Stupid-class weapons apply one stack of Microplastic Poisoning with every hit";
    }
}
public class PlasticApply : ModPlayer
{
    public bool Plasticy;

    public override void ResetEffects()
    {
        Plasticy = false;
    }

    public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Plasticy && item.DamageType.CountsAsClass<StupidDamage>())
        {
            target.ApplyMicroplasticPoison(durationInSeconds: 3, stacks: 1);
        }
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Plasticy && proj.DamageType.CountsAsClass<StupidDamage>())
        {
            target.ApplyMicroplasticPoison(durationInSeconds: 3, stacks: 1);
        }
    }


}