using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using static HendecamMod.Content.Items.Armor.YelmutLeggings;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Body)]
public class PlutoniumChestplate : ModItem
{
    public static readonly int CritBonus = 7;
    public static readonly int AdditiveDamageBonus = 14;
   
   

    public static readonly int MeleeAttackSpeedBonus = 11;
    public static LocalizedText SetBonusText { get; private set; }

    public override void SetStaticDefaults()
    {
        // If your head equipment should draw hair while drawn, use one of the following:
        // ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
        // ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
        // ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
        // ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true;
        SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs();
    }

    public override void SetDefaults()
    {
        Item.width = 32; // Width of the item
        Item.height = 28; // Height of the item
        Item.value = Item.sellPrice(gold: 27); // How many coins the item is worth
        Item.rare = ItemRarityID.LightPurple; // The rarity of the item
        Item.defense = 21; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "14% increased damage and 7% increased crit chance");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+11% melee speed and +55% lobotometer decay rate")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<PlutoniumFacemask>() && legs.type == ModContent.ItemType<PlutoniumPants>();
    }

    public override void UpdateEquip(Player player)
    {
        

       
        player.GetCritChance(DamageClass.Generic) += CritBonus;
      
        player.GetAttackSpeed(DamageClass.Melee) += MeleeAttackSpeedBonus / 100f;

        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;

        var loboDecay = player.GetModPlayer<LobotometerPlayer>();
        loboDecay.DecayRateMultiplier *= 1.55f;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PlutoniumBar>(36);
        recipe.AddTile(TileID.MythrilAnvil);

        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "All projectile weapons have a 1-in-5 chance to spawn a powerful, homing beam of Plutonium energy";
        player.GetModPlayer<PlutoBeams>().ImBeamin = true;

    }

}
public class PlutoBeams : ModPlayer
{
    public bool ImBeamin;

    public override void ResetEffects()
    {
        ImBeamin = false;
    }

    public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        // Keep the original projectile type
        // Don't override 'type'
        if (ImBeamin)
        {
            if (Main.rand.NextBool(5))
            {
                // Spawn additional projectile while keeping the original
                Projectile.NewProjectile(
                    Player.GetSource_ItemUse(item),
                    position,
                    velocity,
                    ModContent.ProjectileType<PlutoParticleOmni>(),
                    (int)(damage * 1.25f),
                    knockback,
                    Player.whoAmI
                );
            }
        }
    }


}