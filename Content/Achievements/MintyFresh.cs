using HendecamMod.Content.Tiles.Blocks;
using Terraria.Achievements;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace HendecamMod.Content.Achievements;

public class MintyFresh : ModAchievement
{
    public TileDestroyedCondition Condition { get; private set; }

    public override void SetStaticDefaults()
    {
        Achievement.SetCategory(AchievementCategory.Explorer);
        Condition = AddTileDestroyedCondition([ModContent.TileType<MintalOrePlaced>()]);
    }

}