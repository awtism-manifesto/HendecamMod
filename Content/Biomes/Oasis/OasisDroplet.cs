using Terraria.GameContent;

namespace HendecamMod.Content.Biomes.Oasis;

public class OasisDroplet : ModGore
{
    public override void SetStaticDefaults()
    {
        ChildSafety.SafeGore[Type] = true;
        GoreID.Sets.LiquidDroplet[Type] = true;
        UpdateType = GoreID.WaterDrip;
    }
}