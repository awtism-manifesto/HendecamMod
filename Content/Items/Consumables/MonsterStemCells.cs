using HendecamMod.Common.Systems;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Consumables
{
    public class MonsterStemCells : ModItem
    {
        public const int BonusPerUse = 25; 
        public const int MaxTotalBonus = 250;

        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(9, 4));
            ItemID.Sets.AnimatesAsSoul[Type] = true;

            // This makes the item show up in the "Consumables" sort category
            ItemID.Sets.SortingPriorityMaterials[Type] = ItemID.LifeCrystal;
            ItemID.Sets.DrinkParticleColors[Type] =
             [
                 new Color(255, 185, 75),
                 new Color(255, 75,75),
                new Color(200, 170,135)
             ];
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Green;
            Item.value = 25000;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.UseSound = SoundID.Item3; 
        }

        public override bool CanUseItem(Player player)
        {
            var loboPlayer = player.GetModPlayer<LobotometerPlayer>();

            // Check against PERMANENT bonus only (not including equipment)
            if (loboPlayer.PermanentBonus + BonusPerUse > MaxTotalBonus)
            {
                CombatText.NewText(player.getRect(), Color.Red, "Maximum Lobotometer capacity reached!");
                return false;
            }

            return true;
        }

        public override bool? UseItem(Player player)
        {
            var loboPlayer = player.GetModPlayer<LobotometerPlayer>();

            // Double-check (safety)
            if (loboPlayer.PermanentBonus + BonusPerUse > MaxTotalBonus)
                return false;

            // Apply permanent increase
            loboPlayer.PermanentBonus += BonusPerUse;

            // Heal current (optional, but matches Life Crystal behavior)
            loboPlayer.Current += BonusPerUse;
            if (loboPlayer.Current > loboPlayer.Max)
                loboPlayer.Current = loboPlayer.Max;

            // Show message
            string message = $"+25 max Braincells!";
            if (loboPlayer.PermanentBonus >= MaxTotalBonus)
                message = $"Maxed Out!";

            CombatText.NewText(player.getRect(), Color.LimeGreen, message, true);

            return true;
        }

    }
}