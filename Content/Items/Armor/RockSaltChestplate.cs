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
[AutoloadEquip(EquipType.Body)]
public class RockSaltChestplate : ModItem
{
    public static readonly int StupidAttackSpeedBonus = 4;
    public static readonly int StupidCritBonus = 4;

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
        Item.value = Item.sellPrice(gold: 3); // How many coins the item is worth
        Item.rare = ItemRarityID.Green; // The rarity of the item
        Item.defense = 5; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "4% increased stupid attack speed and crit strike");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+40 max Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<RockSaltFedora>() && legs.type == ModContent.ItemType<RockSaltLeggings>();
    }

    public override void UpdateEquip(Player player)
    {
       
        player.GetAttackSpeed<StupidDamage>() += StupidAttackSpeedBonus / 100f;
       
        player.GetCritChance<StupidDamage>() += StupidCritBonus;
        var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
        loboPlayer.MaxBonus += 40f;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<RockSalt>(55);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.GetModPlayer<LobotoSalt>().Salting = true;
        player.noFallDmg = true;
        player.setBonus = "Negates fall damage and causes salt to rapidly fall from the sky at max lobotometer";

       
    }
    public class LobotoSalt : ModPlayer
    {

        private const int SaltUseTimeMax = 7;

        public bool Salting;
        private int SaltUseTime;

        public override void ResetEffects()
        {
            Salting = false;
        }

        public override void PostUpdate()
        {
            // Cooldown ticking down
            if (SaltUseTime > 0)
                SaltUseTime--;
            var loboPlayer = Player.GetModPlayer<LobotometerPlayer>();

            // Only trigger if set bonus is active
            if (!Salting)
                return;

            // Cooldown check
            if (SaltUseTime > 0)
                return;

            int baseDamage = 10;

            if (loboPlayer.Current == loboPlayer.Max)
            {
                Projectile.NewProjectile(
                    Player.GetSource_FromThis(),
                   Player.Center - new Vector2(Main.rand.Next(-110, 110), 790),
                    new Vector2(Main.rand.Next(-3, 3), 15f),
                    ModContent.ProjectileType<SaltOmni>(),
                    baseDamage,
                    1.5f,
                    Player.whoAmI
                );
            }
            // Start cooldown
            SaltUseTime = SaltUseTimeMax;

        }


    }
}