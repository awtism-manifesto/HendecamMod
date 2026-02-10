using HendecamMod.Common.Systems;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Global
{
    /// <summary>
    /// ModPlayer class coupled with <seealso cref="ExampleInfoDisplay"/> and <seealso cref="ExampleInfoAccessory"/> to show off how to properly add a
    /// new info accessory (such as a Radar, Lifeform Analyzer, etc.)
    /// </summary>
    public class IQTestPlayer : ModPlayer
    {
        // Flag checking when information display should be activated
        public bool showLobotometer;

        // Make sure to use the right Reset hook. This one is unique, as it will still be
        // called when the game is paused; this allows for info accessories to keep updating properly.
        public override void ResetInfoAccessories()
        {
            showLobotometer = false;
        }

        // If we have another nearby player on our team, we want to get their info accessories working on us,
        // just like in vanilla. This is what this hook is for.
        public override void RefreshInfoAccessoriesFromTeamPlayers(Player otherPlayer)
        {
            if (otherPlayer.GetModPlayer<IQTestPlayer>().showLobotometer)
            {
                showLobotometer = true;
            }
        }
    }


    public class IQDisplay : InfoDisplay
    {
        public static Color RedInfoTextColor => new(255, 19, 19, Main.mouseTextColor);

        public override string HoverTexture => Texture + "_Hover";

        public override bool Active()
        {
            return Main.LocalPlayer
                .GetModPlayer<IQTestPlayer>()
                .showLobotometer;
        }

        public override string DisplayValue(
            ref Color displayColor,
            ref Color displayShadowColor)
        {
            Player player = Main.LocalPlayer;
            var loboPlayer = player.GetModPlayer<LobotometerPlayer>();

            float current = loboPlayer.Current;
            float max = loboPlayer.Max;

          
            if (max <= 0f)
            {
                displayColor = InactiveInfoTextColor;
                return "Not Lobotomized";
            }

            bool empty = current <= 0f;

            if (empty)
            {
                displayColor = InactiveInfoTextColor;
                return "Not Lobotomized";
            }

           
            if (current == max)
            {
                displayColor = GoldInfoTextColor;
            }

            // Round for cleaner display
            int currentInt = (int)current;
            int maxInt = (int)max;

            return $"Lobotometer: {currentInt} / {maxInt}";
        }
    }
}