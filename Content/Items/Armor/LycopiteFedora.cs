using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.Localization;
using static HendecamMod.Content.Items.Armor.PurifiedSaltChestplate;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Head)]
public class LycopiteFedora : ModItem
{
    public static readonly int StupidCritBonus = 9;

    public static readonly int StupidAttackSpeedBonus = 9;
    public static LocalizedText SetBonusText { get; private set; }

    public override void SetStaticDefaults()
    {
        // If your head equipment should draw hair while drawn, use one of the following:
        // ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
        // ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
        // ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
        // ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true;
        ArmorIDs.Head.Sets.IsTallHat[Item.headSlot] = true;
        ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs();
    }

    public override void SetDefaults()
    {
        Item.width = 22; // Width of the item
        Item.height = 18; // Height of the item
        Item.value = Item.sellPrice(gold: 7); // How many coins the item is worth
        Item.rare = ItemRarityID.Orange; // The rarity of the item
        Item.defense = 4; // The amount of defense the item will give when equipped
        Item.lifeRegen = 1;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "9% increased stupid crit chance and attack speed");
        tooltips.Add(line);

        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))
        {
            line = new TooltipLine(Mod, "Face", "Hendecam Cross-Mod (Thorium) - 9% increased throwing crit chance and attack speed")
            {
                OverrideColor = new Color(34, 221, 240)
            };
            tooltips.Add(line);
        }

        line = new TooltipLine(Mod, "Face", "+0.5 hp/s life regen")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<LycopiteChestplate>() && legs.type == ModContent.ItemType<LycopiteLeggings>();
    }

    public override void UpdateEquip(Player player)
    {
        
        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))
        {
            player.GetCritChance(DamageClass.Throwing) += StupidCritBonus;

            player.GetAttackSpeed(DamageClass.Throwing) += StupidAttackSpeedBonus / 100f;
        }

        player.GetCritChance<StupidDamage>() += StupidCritBonus;

        player.GetAttackSpeed<StupidDamage>() += StupidAttackSpeedBonus / 100f;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<LycopiteBar>(19);

        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.GetModPlayer<SporeGrow>().Sporeing = true;
        player.setBonus = "Rapidly grows explosive mushrooms around the player while at critically low HP";
    }
}
public class SporeGrow : ModPlayer
{

    private const int SporeUseTimeMax = 13;

    public bool Sporeing;
    private int SporeUseTime;

    public override void ResetEffects()
    {
        Sporeing = false;
    }

    public override void PostUpdate()
    {
        // Cooldown ticking down
        if (SporeUseTime > 0)
            SporeUseTime--;
        

        // Only trigger if set bonus is active
        if (!Sporeing)
            return;

        // Cooldown check
        if (SporeUseTime > 0)
            return;

        int baseDamage = 33;

        if (Player.statLife <= Player.statLifeMax2 * 0.33)
        {
            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
               Player.Center - new Vector2(Main.rand.Next(-115, 115), Main.rand.Next(-115, 115)),
                Vector2.Zero,
                ModContent.ProjectileType<BoomShroom>(),
                baseDamage,
                8f,
                Player.whoAmI
            );
        }
        // Start cooldown
        SporeUseTime = SporeUseTimeMax;

    }


}

