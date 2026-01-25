using Terraria;
using Terraria.Achievements;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;
using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Placeables;

namespace HendecamMod.Content.Achievements;

public class FlintAndSteel : ModAchievement
{
    public ItemCraftCondition Condition { get; private set; }

    public override void SetStaticDefaults()
    {
        Achievement.SetCategory(AchievementCategory.Collector);
        Condition = AddItemCraftCondition(ModContent.ItemType<SteelBar>());
    }

}