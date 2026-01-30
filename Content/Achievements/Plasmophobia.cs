using HendecamMod.Content.NPCs;
using Terraria.Achievements;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace HendecamMod.Content.Achievements;

// ManyExampleWormsKilled is a more complicated example than MinionBossKilled.
// It is unlocked when ExampleWormHead has been defeated 5 times rather than just once.
public class Plasmophobia : ModAchievement
{
    public NPCKilledCondition Condition { get; private set; }

    public override void SetStaticDefaults()
    {
        Achievement.SetCategory(AchievementCategory.Slayer);

        AddNPCKilledCondition(ModContent.NPCType<UnstablePlasmoid>());
    }

    public override Position GetDefaultPosition() => new After("DEFEAT_EMPRESS_OF_LIGHT");
}