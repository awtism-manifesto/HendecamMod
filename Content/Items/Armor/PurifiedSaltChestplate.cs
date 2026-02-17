using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Body)]
public class PurifiedSaltChestplate : ModItem
{
    public static readonly int StupidAttackSpeedBonus = 10;

    public static readonly int AdditiveStupidDamageBonus = 10;
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
        Item.value = Item.sellPrice(gold: 26); // How many coins the item is worth
        Item.rare = ItemRarityID.Yellow; // The rarity of the item
        Item.defense = 19; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "10% increased stupid damage and attack speed");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+25 max life")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<PurifiedSaltFedora>() && legs.type == ModContent.ItemType<PurifiedSaltLeggings>();
    }

    public override void UpdateEquip(Player player)
    {
       

        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 100f;
        player.GetAttackSpeed<StupidDamage>() += StupidAttackSpeedBonus / 100f;
        player.statLifeMax2 += 25;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        if (ModLoader.TryGetMod("TheConfectionRebirth", out Mod SweetMerica) && SweetMerica.TryFind("NeapoliniteBar", out ModItem NeapoliniteBar))
        {
            recipe.AddIngredient<RockSaltChestplate>();
            recipe.AddIngredient<PurifiedSalt>(81);
            recipe.AddIngredient(NeapoliniteBar.Type, 12);
            recipe.AddIngredient(ItemID.SpectreBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        else
        {
            recipe.AddIngredient<RockSaltChestplate>();
            recipe.AddIngredient<PurifiedSalt>(81);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(ItemID.SpectreBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

    public override void UpdateArmorSet(Player player)
    {

        player.GetModPlayer<LobotoBoulder>().Bouldering = true;
        player.setBonus = "Friendly boulders of purified salt cascade down upon the player's position at max Lobotometer";
    }
    public class LobotoBoulder : ModPlayer
    {
       
        private const int BoulderUseTimeMax = 9;

        public bool Bouldering;
        private int BoulderUseTime;

        public override void ResetEffects()
        {
            Bouldering = false;
        }

        public override void PostUpdate()
        {
            // Cooldown ticking down
            if (BoulderUseTime > 0)
                BoulderUseTime--;
            var loboPlayer = Player.GetModPlayer<LobotometerPlayer>();

            // Only trigger if set bonus is active
            if (!Bouldering)
                return;

            // Cooldown check
            if (BoulderUseTime > 0)
                return;

            int baseDamage = 105;

            if (loboPlayer.Current == loboPlayer.Max)
            {
                Projectile.NewProjectile(
                    Player.GetSource_FromThis(),
                   Player.Center - new Vector2(Main.rand.Next(-150, 150), 940),
                    new Vector2(Main.rand.Next(-3, 3), 20f),
                    ModContent.ProjectileType<PureSaltBoulder>(),
                    baseDamage,
                    10.5f,
                    Player.whoAmI
                );
            }
            // Start cooldown
            BoulderUseTime = BoulderUseTimeMax;

        }

       
    }

}