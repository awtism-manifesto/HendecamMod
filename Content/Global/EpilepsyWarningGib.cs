using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Items.Armor;
using HendecamMod.Content.Items.Consumables;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Weapons.Magic;
using HendecamMod.Content.Items.Weapons.Melee;
using HendecamMod.Content.Items.Weapons.Multiclass;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Default;

namespace HendecamMod.Content.Global;

public class EpilepsyWarningGive : ModPlayer
{
   
    private bool NameContains(string target)
    {
        return Player.name.Contains(target, System.StringComparison.OrdinalIgnoreCase);
    }

    public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
    {
        
        if (NameContains("Echo Overwatch") || NameContains("Mina Liao"))
        {
            var obj2 = new Item();
            obj2.SetDefaults(ItemType<EchoKit>());
            yield return obj2;
        }
        if (NameContains("Susie Gaster"))
        {
            var obj2 = new Item();
            obj2.SetDefaults(ItemType<ManeAx>());
            yield return obj2;
        }

        if (NameContains("Yelmut"))
        {
            var obj2 = new Item();
            obj2.SetDefaults(ItemType<YelmutsHelmet>());
            yield return obj2;
        }
        if (NameContains("Jones"))
        {
            var obj2 = new Item();
            obj2.SetDefaults(ItemID.Hook);
            yield return obj2;
        }

        if (NameContains("Autism Manifesto") || NameContains("Manifesto"))
        {
            var obj2 = new Item();
            obj2.SetDefaults(ItemType<PhatBlunt>());
            yield return obj2;
        }


        if (NameContains("Rivvie") || NameContains("HateMate101") || NameContains("River"))
        {
            var obj2 = new Item();
            obj2.SetDefaults(ItemType<Bullshit1>());
            yield return obj2;
        }

        
        if (NameContains("Mag"))
        {
            var obj2 = new Item();
            obj2.SetDefaults(ItemType<SteelLongsword>());
            yield return obj2;
        }

       
        if (NameContains("lemonearth") || NameContains("LemonEarth") || NameContains("Lemon Earth") ||
            NameContains("lemn test"))
        {
            var obj2 = new Item();
            obj2.SetDefaults(ItemID.Lemon);
            var obj3 = new Item();
            obj3.SetDefaults(ItemType<FragmentFlatEarth>());

            yield return obj2;
            yield return obj3;
        }

        
        if (NameContains("Zygarde"))
        {
            var obj2 = new Item();
            obj2.SetDefaults(ItemType<CoreEnforcer>());
            yield return obj2;
        }

        
        if (NameContains("Akira"))
        {
            var obj2 = new Item();
            obj2.SetDefaults(ItemType<AncientCobaltBar>());
            obj2.stack = 100;
            yield return obj2;
        }

        // Always give EpilepsyWarning
        var obj = new Item();
        obj.SetDefaults(ModContent.ItemType<EpilepsyWarning>());
        yield return obj;
    }
}