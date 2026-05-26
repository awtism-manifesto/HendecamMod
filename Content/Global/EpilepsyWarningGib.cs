using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Items.Armor;
using HendecamMod.Content.Items.Consumables;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Weapons;
using HendecamMod.Content.Items.Weapons.Melee;
using HendecamMod.Content.Items.Weapons.Multiclass;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Default;

namespace HendecamMod.Content.Global;

// If you post this code file anywhere other than the Hendecam Github, you are a bitch ass motherfucker and you probably support israel
// (ew, israel)

public class EpilepsyWarningGive : ModPlayer 
{
    public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
    {
        if (Player.name == "Echo Overwatch" || Player.name == "Mina Liao" || Player.name == "echo overwatch" || Player.name == "mina liao")

        {
            var obj2 = new Item();
            obj2.SetDefaults(ModContent.ItemType<EchoKit>());
            yield return obj2;
        }
        if (Player.name == "Yelmut" || Player.name == "yelmut" )

        {
            var obj2 = new Item();
            obj2.SetDefaults(ModContent.ItemType<YelmutsHelmet>());
            yield return obj2;
        }
        if (Player.name == "Rivvie" || Player.name == "HateMate101" || Player.name == "rivvie" || Player.name == "River" || Player.name == "river")

        {
            var obj2 = new Item();
            obj2.SetDefaults(ModContent.ItemType<Bullshit1>());
            yield return obj2;
        }
        if (Player.name == "Mag" || Player.name == "mag")

        {
            var obj2 = new Item();
            obj2.SetDefaults(ModContent.ItemType<SteelLongsword>());
            yield return obj2;
        }
        if (Player.name == "Zygarde" || Player.name == "zygarde")

        {
            var obj2 = new Item();
            obj2.SetDefaults(ModContent.ItemType<CoreEnforcer>());
            yield return obj2;
        }

        if (Player.name == "Akira" || Player.name == "akira")

        {
            var obj2 = new Item();
            obj2.SetDefaults(ModContent.ItemType<AncientCobaltBar>());
            obj2.stack = 100;
            yield return obj2;
        }
        if (Player.name == "Phantasmic" || Player.name == "Sentinel" || Player.name == "Anonymous" || Player.name == "phantasmic" || Player.name == "sentinel" || Player.name == "anonymous")

        {
            for (int i = 0; i < Player.inventory.Length; i++)
            {
                var obj2 = new Item();
                obj2.SetDefaults(ModContent.ItemType<SpiritRum>());
                obj2.stack = 99999;
                yield return obj2;
            }
          
            
        }
       

        var obj = new Item();
            obj.SetDefaults(ModContent.ItemType<EpilepsyWarning>());
            yield return obj;
        
    }
}