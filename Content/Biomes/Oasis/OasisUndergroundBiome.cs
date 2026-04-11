using HendecamMod.Content.Items.Placeables;
using Terraria.Graphics.Capture;

namespace HendecamMod.Content.Biomes.Oasis;

public class OasisUndergroundBiome : ModBiome
{
    public override ModWaterStyle WaterStyle => GetInstance<OasisWaterStyle>();
    public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => GetInstance<OasisUndergroundBackgroundStyle>();
    public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Jungle;
    public override int Music => MusicID.OtherworldlyJungle;
    public override int BiomeTorchItemType => ItemType<AstatineTorch>();
    public override int BiomeCampfireItemType => ItemType<AstatineTorch>();
    public override string BestiaryIcon => base.BestiaryIcon;
    public override string BackgroundPath => base.BackgroundPath;
    public override Color? BackgroundColor => base.BackgroundColor;
    public override string MapBackground => BackgroundPath;
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeLow;

    public override bool IsBiomeActive(Player player)
    {
        bool b1 = GetInstance<OasisTileCount>().oasisBlockCount >= 40;
        bool b2 = !player.ZoneSkyHeight && !player.ZoneOverworldHeight;
        return b1 && b2;
    }
}