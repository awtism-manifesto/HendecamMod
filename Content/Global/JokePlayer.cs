using HendecamMod.Common.Systems;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Global
{
    
    public class JokePlayer : ModPlayer
    {
        // Flag checking when information display should be activated
        public bool showJokes;

        // Make sure to use the right Reset hook. This one is unique, as it will still be
        // called when the game is paused; this allows for info accessories to keep updating properly.
        public override void ResetInfoAccessories()
        {
           
                showJokes = false;
           
                
        }

        // If we have another nearby player on our team, we want to get their info accessories working on us,
        // just like in vanilla. This is what this hook is for.
        public override void RefreshInfoAccessoriesFromTeamPlayers(Player otherPlayer)
        {
            if (otherPlayer.GetModPlayer<JokePlayer>().showJokes)
            {
                showJokes = true;
            }
        }
    }
    

    public class ZeJokeDisplay : InfoDisplay
    {
        public static Color InfoTextColor => new(245,175, 25, Main.mouseTextColor);
        public static Color GreenInfoTextColor => new(50, 255, 50, Main.mouseTextColor);

        public static Color RedInfoTextColor => new(250, 55, 50, Main.mouseTextColor);

        public int JokeCycler;
        public int choice;
        public override string HoverTexture => Texture + "_Hover";

        public string JokeDisplayed = "Welcome to the Joke Jumbotron!!!";

        public override bool Active()
        {
            return Main.LocalPlayer
                .GetModPlayer<JokePlayer>()
                .showJokes;
        }

        public override string DisplayValue(
            ref Color displayColor,
            ref Color displayShadowColor)
        {

            JokeCycler++;


            

           
            if (JokeCycler >= 240)
            {
               
                choice = Main.rand.Next(25);
                if (choice == 17)
                {
                    displayColor = GreenInfoTextColor;
                }
                else
                {
                    displayColor = InfoTextColor;
                }
                    switch (choice)
                    {
                        case 0:
                            JokeDisplayed = "River? I hardly know her";
                            break;
                        case 1:
                            JokeDisplayed = "How does Alpine make nachos? using the Elf Melter!";
                            break;
                        case 2:
                            JokeDisplayed = "Deltarune Tomorrow";
                            break;
                        case 3:
                            JokeDisplayed = "Well at least you don't have to fight... Freddy Fazbear....";
                            break;

                        case 4:
                            JokeDisplayed = "How much revenue has Azafure LLC made? 67 dollars!";
                            break;

                        case 5:
                            JokeDisplayed = "Hendecam Mod: At least we aren't Calamity!";
                            break;
                        case 6:
                            JokeDisplayed = "IS THAT THE ROARING KNIGHT???";
                            break;
                        case 7:
                            JokeDisplayed = "I wonder what the Brain of Cthulhu's lobotometer decay rate is...";
                            break;
                        case 8:
                            JokeDisplayed = "Chlorophyte Shitballs! Now 50% shittier!!!";
                            break;
                        case 9:
                            JokeDisplayed = "Hendecam Balance Changes: Nerfed Omni again";
                            break;
                        case 10:
                            JokeDisplayed = "HALF LIFE 3 CONFIRMED???";
                            break;
                        case 11:
                            JokeDisplayed = "Terraria 1.5: The Final Update (fr this time trust us)";
                            break;
                        case 12:
                            JokeDisplayed = "I use Arch Linux btw";
                            break;
                        case 13:
                            JokeDisplayed = "Hendecam Mod: More optimized than Pokemon Scarlet & Violet!";
                            break;
                        case 14:
                            JokeDisplayed = "Why did the chicken cross the road? To find the TerMerica 1.4.5 Port!";
                            break;
                        case 15:
                            JokeDisplayed = "Number 15. Burger king foot lettuce.";
                            break;
                        case 16:
                            JokeDisplayed = "The JFK Experience: Ending hardcore runs since 2024!";
                            break;
                        case 17:
                            JokeDisplayed = "Say you'll be green!";

                            break;
                        case 18:
                            JokeDisplayed = "Why did spazmatism cross the road? To get to the brother side!";
                            break;
                        case 19:
                            JokeDisplayed = "According to all known laws of aviation, there is no way a bee should be able to fly";
                            break;
                    case 20:
                        JokeDisplayed = "There is no joke. this is the EVIL joke jumbotron!!!!";
                        break;
                    case 21:
                        JokeDisplayed = "How do you steal a coat? You just jacket.";
                        break;
                    case 22:
                        JokeDisplayed = "How long does it take a Terrarian to graduate? Forever, they keep getting more finals.";
                        break;
                    case 23:
                        JokeDisplayed = "How do you get an art major off of your porch? You pay for the pizza.";
                        break;
                    case 24:
                        JokeDisplayed = "Why did the Moon Lord lose to dryads? because they were outstanding.";
                        break;
                }
                
                    JokeCycler = 0;
            }
            else
            {
                if (choice == 17)
                {
                    displayColor = GreenInfoTextColor;
                }
                if (choice == 20)
                {
                    displayColor = RedInfoTextColor;
                }
                else
                {
                    displayColor = InfoTextColor;
                }
            }
                return $" {JokeDisplayed}";
        }
    }
}