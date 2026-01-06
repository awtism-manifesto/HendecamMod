using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace HendecamMod.Content.Tiles
{
	public class PyriteOreTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			AddMapEntry(new Color(191, 128, 51), Language.GetText("Pyrite"));
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			TileID.Sets.Ore[Type] = true;
			Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
			Main.tileOreFinderPriority[Type] = 290;
			HitSound = SoundID.Tink;
		}
	}
}