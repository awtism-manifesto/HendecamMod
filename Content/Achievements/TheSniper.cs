using HendecamMod.Content.Items.Weapons.Ranger;
using Terraria.Achievements;
using Terraria.GameContent.Achievements;

namespace HendecamMod.Content.Achievements;

public class TheSniper : ModAchievement
{
    public ItemCraftCondition Condition { get; private set; }

    public override void SetStaticDefaults()
    {
        Achievement.SetCategory(AchievementCategory.Collector);
        Condition = AddItemCraftCondition(ItemType<TrueJfkExperience>());
    }
}