using HendecamMod.Common.Systems;
using HendecamMod.Content.Items.Accessories;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Global
{
    
    public class BrainScanPlayer : ModPlayer
    {
        // Flag checking when information display should be activated
        public bool showLobotometerDecay;

        // Make sure to use the right Reset hook. This one is unique, as it will still be
        // called when the game is paused; this allows for info accessories to keep updating properly.
        public override void ResetInfoAccessories()
        {
            showLobotometerDecay = false;
        }

        // If we have another nearby player on our team, we want to get their info accessories working on us,
        // just like in vanilla. This is what this hook is for.
        public override void RefreshInfoAccessoriesFromTeamPlayers(Player otherPlayer)
        {
            if (otherPlayer.GetModPlayer<BrainScanPlayer>().showLobotometerDecay)
            {
                showLobotometerDecay = true;
            }
        }
    }


    public class ScanDisplay : InfoDisplay
    {
        public static Color RedInfoTextColor => new(255, 19, 19, Main.mouseTextColor);

        public override string HoverTexture => Texture + "_Hover";

        public override bool Active()
        {
            return Main.LocalPlayer
                .GetModPlayer<BrainScanPlayer>()
                .showLobotometerDecay;
        }

        public override string DisplayValue(
            ref Color displayColor,
            ref Color displayShadowColor)
        {
            Player player = Main.LocalPlayer;
            var loboPlayer = player.GetModPlayer<LobotometerPlayer>();

            float current = loboPlayer.decayPerTick;

            displayColor = InactiveInfoTextColor;


            if (player.GetModPlayer<BaseSpike>().Spiked)
            {
                displayColor = RedInfoTextColor;
                return $"Lobotometer decay rate: 0/sec (pinned)";
            }

          

            return $"Lobotometer decay rate: {current * 60}/sec";
        }
    }
}