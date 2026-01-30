using HendecamMod.Content.Items;
using Terraria.Achievements;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace HendecamMod.Content.Achievements;

public class TheSniper : ModAchievement
{
    public ItemCraftCondition Condition { get; private set; }

    public override void SetStaticDefaults()
    {
        Achievement.SetCategory(AchievementCategory.Collector);
        Condition = AddItemCraftCondition(ModContent.ItemType<TrueJfkExperience>());
    }

}