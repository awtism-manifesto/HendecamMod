using Terraria.ModLoader;

namespace HendecamMod.Content.Biomes.Oasis
    {
    public class OasisUndergroundBackgroundStyle : ModUndergroundBackgroundStyle
        {
        public override void FillTextureArray(int[] textureSlots)
            {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Assets/Textures/Backgrounds/OasisBiomeUnderground0");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Assets/Textures/Backgrounds/OasisBiomeUnderground1");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Assets/Textures/Backgrounds/OasisBiomeUnderground2");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Assets/Textures/Backgrounds/OasisBiomeUnderground3");
            }
        }
    }