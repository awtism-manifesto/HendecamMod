using System.Collections.Generic;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Armor;

// The AutoloadEquip attribute automatically attaches an equip texture to this item.
// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
[AutoloadEquip(EquipType.Legs)]
public class PlutoniumPants : ModItem
{
    public static readonly int MoveSpeedBonus = 17;
    public static readonly int AdditiveDamageBonus = 9;
    public static readonly int AdditiveThrowDamageBonus = 7;
    public static readonly int MaxMinionIncrease = 1;
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
        Item.value = Item.sellPrice(gold: 25); // How many coins the item is worth
        Item.rare = ItemRarityID.LightPurple; // The rarity of the item
        Item.defense = 15; // The amount of defense the item will give when equipped
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "21% increased movement speed and 9% increased damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "+1 minion slot")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return head.type == ModContent.ItemType<PlutoniumFacemask>() && body.type == ModContent.ItemType<PlutoniumChestplate>();
    }

    public override void UpdateEquip(Player player)
    {
        

        player.maxMinions += MaxMinionIncrease;
        player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;
        player.moveSpeed += MoveSpeedBonus / 100f;
        player.runAcceleration *= 1.21f;
        player.lifeRegen += -1;

        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))
        {
            player.GetDamage(DamageClass.Throwing) += AdditiveDamageBonus / 100f;
        }
    }

    // UpdateArmorSet allows you to give set bonuses to the armor.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PlutoniumBar>(31);

        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void UpdateArmorSet(Player player)
    {
    }

    public class Penis : ModPlayer
    {
        public bool BigPenis;

        public override void ResetEffects()
        {
            BigPenis = false;
        }

        public override void PostUpdateRunSpeeds()
        {
            // We only want our additional changes to apply if ExampleStatBonusAccessory is equipped and not on a mount.
            if (Player.mount.Active || !BigPenis)
            {
                return;
            }

            Player.runAcceleration *= 1.21f;
            Player.maxRunSpeed *= 1.21f;
            Player.accRunSpeed *= 1.21f;
            Player.runSlowdown *= 1.21f;
        }
    }
}