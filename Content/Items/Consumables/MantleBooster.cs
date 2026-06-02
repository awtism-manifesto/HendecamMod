using HendecamMod.Common.Systems;
using HendecamMod.Content.Tiles.Blocks;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Consumables;

public class MantleBooster : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 5;

       
    }

    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 22;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 22;
        Item.useTime = 22;
        Item.useTurn = true;
        Item.UseSound = SoundID.Item3;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.buyPrice(gold: 50);
       
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Gives a powerful but extremely short-lived boost to the spread rate of Mantius Ore");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "WARNING: May cause some lag upon use")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            line = new TooltipLine(Mod, "Face", "If you're gonna use this item in multiplayer, warn the other people you're playing with. Don't be an asshole.")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }

    }
    public static void TurboStartMantiusSpreading()
    {
       

        // Find all Mantius ore tiles
        List<Point> mantiusTiles = new List<Point>();

        for (int x = 0; x < Main.maxTilesX; x++)
        {
            for (int y = 0; y < Main.maxTilesY; y++)
            {
                Tile tile = Main.tile[x, y];
                if (tile.HasTile && tile.TileType == TileType<MantiusOrePlaced>())
                {
                    mantiusTiles.Add(new Point(x, y));
                }
            }
        }

        int tileCount = mantiusTiles.Count;



       
        int spreadAttemptsPerTile = Math.Max(11, Math.Min(85, 25000 / (tileCount + 50)));






        // Create an instance of the tile
        MantiusOrePlaced oreTile = new MantiusOrePlaced();

        for (int attempt = 0; attempt < spreadAttemptsPerTile; attempt++)
        {
            foreach (Point tilePos in mantiusTiles)
            {
                // Call TrySpread directly (now public)
                oreTile.TrySpread(tilePos.X, tilePos.Y);
            }
        }


    }
    public override bool? UseItem(Player player)
    {
        // Only run the effect on the server in multiplayer
        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            // Tell the server to run the boost
            var netMessage = Mod.GetPacket();
            netMessage.Write((byte)1); // Message type
            netMessage.Write(player.whoAmI);
            netMessage.Send();

            // Consume the item locally
            player.ConsumeItem(Item.type);
            return true;
        }

        // Single player or server - run directly
        TurboStartMantiusSpreading();
        player.ConsumeItem(Item.type);
        return true;
    }
}
