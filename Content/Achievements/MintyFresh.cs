using HendecamMod.Content.Items;
using Terraria;
using Terraria.Achievements;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using HendecamMod.Content.Tiles;
using HendecamMod.Content.Tiles.Blocks;

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