
using HendecamMod.Common.Utilities;
using HendecamMod.Content.Global;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;


public class JokeJumbotron : ModItem
{
   

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Radar);
    }
    public override void UpdateVanity(Player player)
    {
        player.GetHCPlayer().showJokes = true;
       

    }
    public override void UpdateInfoAccessory(Player player)
    {
        player.GetHCPlayer().showJokes = true;

    }
}