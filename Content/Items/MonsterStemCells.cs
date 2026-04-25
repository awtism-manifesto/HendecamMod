using HendecamMod.Common.Systems;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.Localization;

namespace HendecamMod.Content.Items
{
    public class MonsterStemCells : ModItem
    {
        public const int BonusPerUse = 25; 
        public const int MaxTotalBonus = 200;

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
            string message = $"Your Lobotometer capacity has permanently increased by {BonusPerUse}!";
            if (loboPlayer.PermanentBonus >= MaxTotalBonus)
                message = $"Your Lobotometer has reached maximum capacity!";

            CombatText.NewText(player.getRect(), Color.LimeGreen, message, true);

            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Get the player who is viewing the tooltip
            Player currentPlayer = Main.LocalPlayer;
            var loboPlayer = currentPlayer.GetModPlayer<LobotometerPlayer>();

            // Main tooltip
            var line = new TooltipLine(Mod, "LobotometerBonus", $"Permanently increases max Lobotometer by {BonusPerUse} when consumed")
            {
                OverrideColor = new Color(100, 255, 100)
            };
            tooltips.Add(line);

            // Show PERMANENT progress (this is character-specific and not affected by equipment)
            float currentPermanent = loboPlayer.PermanentBonus;
            int remainingBonus = MaxTotalBonus - (int)currentPermanent;

            line = new TooltipLine(Mod, "Progress", $"Permanent bonus: {currentPermanent} / {MaxTotalBonus}")
            {
                OverrideColor = new Color(150, 150, 255)
            };
            tooltips.Add(line);

            if (remainingBonus > 0)
            {
                int usesLeft = remainingBonus / BonusPerUse;
                line = new TooltipLine(Mod, "Remaining", $"Can be used {usesLeft} more time{(usesLeft != 1 ? "s" : "")}")
                {
                    OverrideColor = new Color(255, 200, 100)
                };
                tooltips.Add(line);
            }
            else
            {
                line = new TooltipLine(Mod, "MaxReached", $"Maximum Lobotometer capacity reached!")
                {
                    OverrideColor = new Color(255, 100, 100)
                };
                tooltips.Add(line);
            }

            // Optional: Show current TOTAL max (including equipment)
            float totalMax = loboPlayer.Max;
            line = new TooltipLine(Mod, "CurrentTotal", $"Current total capacity: {totalMax} (including equipment)")
            {
                OverrideColor = new Color(200, 200, 200)
            };
            tooltips.Add(line);
        }

    }
}