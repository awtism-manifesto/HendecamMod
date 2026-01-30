using HendecamMod.Content.NPCs.Bosses;
using Terraria.Achievements;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace HendecamMod.Common.Systems;

// ManyExampleWormsKilled is a more complicated example than MinionBossKilled.
// It is unlocked when ExampleWormHead has been defeated 5 times rather than just once.
public class SkullCracker : ModAchievement
{
    public NPCKilledCondition Condition { get; private set; }

    public override void SetStaticDefaults()
    {
        Achievement.SetCategory(AchievementCategory.Slayer);
        AddNPCKilledCondition(ModContent.NPCType<HeadOfCthulhu>());
    }

    public override Position GetDefaultPosition() => new After("MINER_FOR_FIRE");
}