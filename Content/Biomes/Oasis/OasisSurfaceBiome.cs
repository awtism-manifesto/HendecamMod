using HendecamMod.Content.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Biomes.Oasis;

public class OasisSurfaceBiome : ModBiome
    {
    public override ModWaterStyle WaterStyle => ModContent.GetInstance<OasisWaterStyle>();
    public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.GetInstance<OasisSurfaceBackgroundStyle>();
    public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Jungle;
    public override int Music => MusicID.OtherworldlyRain;
    public override int BiomeTorchItemType => ModContent.ItemType<AstatineTorch>();
    public override int BiomeCampfireItemType => ModContent.ItemType<AstatineTorch>();
    public override string BestiaryIcon => base.BestiaryIcon;
    public override string BackgroundPath => base.BackgroundPath;
    public override Color? BackgroundColor => base.BackgroundColor;
    public override string MapBackground => BackgroundPath;
    public override bool IsBiomeActive(Player player)
        {
        bool b1 = ModContent.GetInstance<OasisTileCount>().oasisBlockCount >= 40;
        bool b2 = player.ZoneSkyHeight || player.ZoneOverworldHeight;
        return b1 && b2;
        }
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeLow;
    }

