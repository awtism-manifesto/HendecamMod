namespace HendecamMod.Content.Biomes.Oasis;

public class OasisWaterfallStyle : ModWaterfallStyle
{
    public override void AddLight(int i, int j) =>
        Lighting.AddLight(new Vector2(i, j).ToWorldCoordinates(), Color.White.ToVector3() * 0.5f);
}