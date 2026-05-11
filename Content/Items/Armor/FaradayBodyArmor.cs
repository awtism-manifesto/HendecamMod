using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using System.Linq;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Body)]
public class FaradayBodyArmor : ModItem
{
    public static readonly int StupidCritBonus = 9;
    public static readonly int StupidArmorPenetration = 15;
    public static readonly int AdditiveStupidDamageBonus = 9;

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
        Item.value = Item.sellPrice(gold: 130); // How many coins the item is worth
        Item.rare = ItemRarityID.Red; // The rarity of the item
        Item.defense = 23; // The amount of defense the item will give when equipped
        Item.lifeRegen = 6;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "+15 stupid armor penetration and +40% Lobotometer decay rate");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+9% stupid damage and crit chance")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "+3 HP/Sec life regen")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ItemType<FaradayFedora>() && legs.type == ItemType<FaradayPants>();
    }

    public override void UpdateEquip(Player player)
    {
        
        player.GetCritChance<StupidDamage>() += StupidCritBonus;
        player.GetArmorPenetration<StupidDamage>() += StupidArmorPenetration;
        player.GetDamage<StupidDamage>() += AdditiveStupidDamageBonus / 100f;

        var loboDecay = player.GetModPlayer<LobotometerPlayer>();
        loboDecay.DecayRateMultiplier *= 1.40f;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.LunarBar, 16);

        recipe.AddIngredient<FragmentFlatEarth>(20);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Summons an orbiting sun and moon around the player that shoot deadly lasers at foes, and extra deadly Eclipse Lasers when aligned";
        player.GetModPlayer<FlatEarthSunMoon>().Bodying = true;

    }
}
public class FlatEarthSunMoon : ModPlayer
{
    private const int BodyUseTimeMax = 1000000;

    public bool Bodying;
    private int BodyUseTime;

    // Track the spawned sentries
    public List<int> ActiveMoonSentryIndices = new List<int>();
    public List<int> ActiveSunSentryIndices = new List<int>();

    public override void ResetEffects()
    {
        bool wasBodying = Bodying;
        Bodying = false;

        // If set bonus was just removed and we're respawning sentries, reset cooldown
        if (wasBodying && !Bodying)
        {
            BodyUseTime = 0; // Reset cooldown so sentries spawn immediately when re-equipped
        }
    }

    public override void PostUpdate()
    {
        // Cooldown ticking down
        if (BodyUseTime > 0)
            BodyUseTime--;

        // Clean up any dead projectiles from our tracking lists
        CleanupDeadProjectiles();

        // Check if set bonus is active
        if (Bodying)
        {
            // Cooldown check - spawn new sentries if needed
            if (BodyUseTime <= 0 && !HasActiveSentries())
            {
                int baseDamage = 67;

                // Spawn moon sentry
                int moonSentry = Projectile.NewProjectile(
                    Player.GetSource_FromThis(),
                    Player.Center,
                    Vector2.Zero,
                    ProjectileType<FaradayMoonSentry>(),
                    baseDamage,
                    6.7f,
                    Player.whoAmI
                );
                ActiveMoonSentryIndices.Add(moonSentry);

                // Spawn sun sentry
                int sunSentry = Projectile.NewProjectile(
                    Player.GetSource_FromThis(),
                    Player.Center,
                    Vector2.Zero,
                    ProjectileType<FaradaySunSentry>(),
                    baseDamage,
                    6.7f,
                    Player.whoAmI
                );
                ActiveSunSentryIndices.Add(sunSentry);

                // Start cooldown
                BodyUseTime = BodyUseTimeMax;
            }
        }
        else
        {
            // Set bonus is NOT active - despawn all sentries
            DespawnAllSentries();
        }
    }

    private bool HasActiveSentries()
    {
        return ActiveMoonSentryIndices.Any(index => Main.projectile[index].active) ||
               ActiveSunSentryIndices.Any(index => Main.projectile[index].active);
    }

    private void CleanupDeadProjectiles()
    {
        // Remove dead moon sentries from tracking list
        ActiveMoonSentryIndices.RemoveAll(index =>
            index < 0 ||
            index >= Main.maxProjectiles ||
            !Main.projectile[index].active ||
            Main.projectile[index].type != ProjectileType<FaradayMoonSentry>()
        );

        // Remove dead sun sentries from tracking list
        ActiveSunSentryIndices.RemoveAll(index =>
            index < 0 ||
            index >= Main.maxProjectiles ||
            !Main.projectile[index].active ||
            Main.projectile[index].type != ProjectileType<FaradaySunSentry>()
        );
    }

    private void DespawnAllSentries()
    {
        // Despawn all moon sentries
        foreach (int index in ActiveMoonSentryIndices.ToList())
        {
            if (index >= 0 && index < Main.maxProjectiles && Main.projectile[index].active)
            {
                Main.projectile[index].Kill();
            }
        }
        ActiveMoonSentryIndices.Clear();

        // Despawn all sun sentries
        foreach (int index in ActiveSunSentryIndices.ToList())
        {
            if (index >= 0 && index < Main.maxProjectiles && Main.projectile[index].active)
            {
                Main.projectile[index].Kill();
            }
        }
        ActiveSunSentryIndices.Clear();
    }

    public override void OnEnterWorld()
    {
        // Clear tracking lists when entering a world to prevent stale references
        ActiveMoonSentryIndices.Clear();
        ActiveSunSentryIndices.Clear();
    }
}

