using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Materials;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.Localization;
using static HendecamMod.Content.Items.Armor.YelmutLeggings;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Body)]
public class UraniumChestplate : ModItem
{
    public static readonly int AdditiveDamageBonus = 10;
    public static readonly int AttackSpeedBonus = 12;

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
        Item.value = 545000;
        Item.rare = ItemRarityID.Green; // The rarity of the item
        Item.defense = 9; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "11% increased damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "-25 max life")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ItemType<UraniumHelmet>() && legs.type == ItemType<UraniumLeggings>();
    }

    public override void UpdateEquip(Player player)
    {
       

        player.statLifeMax2 += -25;
        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<UraniumBar>(30);

        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.GetModPlayer<RadSpeedSys>().RadArmor = true;
        player.setBonus = "Has a chance to give a stacking attack speed buff upon hitting an enemy";
    }
}
public class RadSpeedSys : ModPlayer
{
    public bool RadArmor;

    // Track time since last hit
    private int _framesSinceLastHit;
    private const int MaxFramesForMaxChance = 180; // 3 seconds at 60 FPS

    // Track current buff stage
    private int _currentStage;
    private const int MaxStage = 5;

    // Base chance per stage (will be modified by time since last hit)
    private float[] _baseChances = { 0.20f, 0.15f, 0.12f, 0.10f, 0.08f };

    // Buff durations in frames (60 frames = 1 second)
    private int[] _buffDurations = { 240, 300, 360, 420, 480 }; // 4, 5, 6, 7, 8 seconds

    public override void ResetEffects()
    {
        RadArmor = false;

        // Only reset the timer if we're not in combat
        // This prevents the timer from resetting mid-combat
        if (!RadArmor)
        {
            _framesSinceLastHit = 0;
            _currentStage = 0;
        }
    }

    public override void PostUpdate()
    {
        // Increment the timer if we haven't hit anything
        if (RadArmor)
        {
            _framesSinceLastHit++;

            // Cap the timer to prevent overflow
            if (_framesSinceLastHit > MaxFramesForMaxChance * 2)
                _framesSinceLastHit = MaxFramesForMaxChance * 2;
        }
    }

    public override void OnHitAnything(float x, float y, Entity victim)
    {
        if (!RadArmor)
            return;

        // Reset the timer on hit
        _framesSinceLastHit = 0;

        // Check if we can apply a buff
        TryApplyBuff();
    }

    private void TryApplyBuff()
    {
        // Determine which stage to try applying
        int targetStage = GetCurrentStage();

        // If we're already at max stage, try to refresh it
        if (targetStage >= MaxStage)
        {
            if (ShouldApplyBuff(MaxStage))
            {
                ApplyBuff(MaxStage);
            }
            return;
        }

        // Try to apply the next stage
        int nextStage = targetStage + 1;
        if (ShouldApplyBuff(nextStage))
        {
            ApplyBuff(nextStage);
        }
    }

    private int GetCurrentStage()
    {
        // Check which buff is currently active
        if (Player.HasBuff(BuffType<RadSpeed5>())) return 5;
        if (Player.HasBuff(BuffType<RadSpeed4>())) return 4;
        if (Player.HasBuff(BuffType<RadSpeed3>())) return 3;
        if (Player.HasBuff(BuffType<RadSpeed2>())) return 2;
        if (Player.HasBuff(BuffType<RadSpeed>())) return 1;
        return 0;
    }

    private bool ShouldApplyBuff(int stage)
    {
        // Calculate chance multiplier based on time since last hit
        float timeMultiplier = CalculateTimeMultiplier();

        // Get base chance for this stage (0-indexed)
        float baseChance = _baseChances[stage - 1];

        // Calculate final chance
        float finalChance = Math.Min(baseChance * timeMultiplier, 0.95f);

        // Roll for the buff
        return Main.rand.NextFloat() < finalChance;
    }

    private float CalculateTimeMultiplier()
    {
        // Calculate how long it's been since last hit as a percentage of max
        float timeRatio = (float)_framesSinceLastHit / MaxFramesForMaxChance;

        // Clamp between 0 and 1
        timeRatio = Math.Min(timeRatio, 1f);

        
        return 1f + (timeRatio * 2f);
    }

    private void ApplyBuff(int stage)
    {
        // Clear lower stage buffs when applying a higher stage
        if (stage > 1)
        {
            Player.ClearBuff(BuffType<RadSpeed>());
        }
        if (stage > 2)
        {
            Player.ClearBuff(BuffType<RadSpeed2>());
        }
        if (stage > 3)
        {
            Player.ClearBuff(BuffType<RadSpeed3>());
        }
        if (stage > 4)
        {
            Player.ClearBuff(BuffType<RadSpeed4>());
        }

        // Apply the appropriate buff with its duration
        int duration = _buffDurations[stage - 1];
        switch (stage)
        {
            case 1:
                Player.AddBuff(BuffType<RadSpeed>(), duration);
                break;
            case 2:
                Player.AddBuff(BuffType<RadSpeed2>(), duration);
                break;
            case 3:
                Player.AddBuff(BuffType<RadSpeed3>(), duration);
                break;
            case 4:
                Player.AddBuff(BuffType<RadSpeed4>(), duration);
                break;
            case 5:
                Player.AddBuff(BuffType<RadSpeed5>(), duration);
                break;
        }
    }

   
}