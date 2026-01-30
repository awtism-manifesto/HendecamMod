using HendecamMod.Content.Tiles;
using Terraria.Achievements;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace HendecamMod.Content.Achievements;

// ManyExampleWormsKilled is a more complicated example than MinionBossKilled.
// It is unlocked when ExampleWormHead has been defeated 5 times rather than just once.
public class WoahTrippyBro : ModAchievement
{
    public TileDestroyedCondition Condition { get; private set; }

    public override void SetStaticDefaults()
    {
        Achievement.SetCategory(AchievementCategory.Explorer);
        // There are 4 AchievementCategory options: Slayer, Collector, Explorer, and Challenger.
        // Slayer is the default.
        // If you want to change the achievement's category, you can do this:

        // Unlike MinionBossKilled, which uses AddNPCKilledCondition, this ModAchievement uses AddIntCondition to track the 5 kills. This is necessary because AddNPCKilledCondition only supports tracking a single kill.
        Condition = AddTileDestroyedCondition([ModContent.TileType<LycopiteOreTile>()]);
    }

    public override Position GetDefaultPosition() => new Before("STILL_HUNGRY");
}