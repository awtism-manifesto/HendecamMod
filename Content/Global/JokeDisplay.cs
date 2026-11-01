using HendecamMod.Common.Systems;
using HendecamMod.Common.Utilities;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Global
{
    public class ZeJokeDisplay : InfoDisplay
    {
        public int JokeCycler;
        public int choice = -1;
        public int jokeCount = 41;
        public override string HoverTexture => Texture + "_Hover";
        public static Color InfoTextColor => new(255,255,255, Main.mouseTextColor);
        public string JokeDisplayed = Language.GetTextValue("Mods.HendecamMod.Items.JokeJumbotron.Jokes.Start");

        public override bool Active()
        {
            return Main.LocalPlayer
                .GetHCPlayer()
                .showJokes;
        }

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            JokeCycler++;
            if (JokeCycler >= 240)
            {
                choice = Main.rand.Next(jokeCount + 1);
                while (choice == 41)//dodge the line
                {
                    choice = Main.rand.Next(jokeCount + 1);
                }
                JokeDisplayed = Language.GetTextValue($"Mods.HendecamMod.Items.JokeJumbotron.Jokes.{choice}");
                JokeCycler = 0;
            }
            if (choice == 20)
            {
                displayColor = new Color(15, 15, 105, Main.mouseTextColor);
            }
            else
            {
                displayColor = InfoTextColor;
            }
            return JokeDisplayed;
        }
    }
}