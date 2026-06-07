using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Items.Armor;
using HendecamMod.Content.Items.Consumables;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Weapons.Magic;
using HendecamMod.Content.Items.Weapons.Melee;
using HendecamMod.Content.Items.Weapons.Multiclass;
using HendecamMod.Content.Items.Weapons.VapeItems;
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
            obj2.SetDefaults(ItemID.GrapplingHook);
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
           
            var obj3 = new Item();
            obj3.SetDefaults(ItemID.PoopBlock);
            obj3.stack = 6767;
            yield return obj2;
            yield return obj3;
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
        if (NameContains("Brandon Herrera"))
        {
            var obj2 = new Item();
            obj2.SetDefaults(ItemType<AmmoRecycler>());
            yield return obj2;
        }

        if (NameContains("Akira"))
        {
            var obj2 = new Item();
            obj2.SetDefaults(ItemType<AncientCobaltBar>());
            obj2.stack = 100;
            yield return obj2;
        }
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica))
        {
            if (Main.rand.NextBool(2))
            {
                var obj2 = new Item();
                obj2.SetDefaults(ModContent.ItemType<CopperVape>());
                yield return obj2;
            }
            else
            {
                var obj2 = new Item();
                obj2.SetDefaults(ModContent.ItemType<TinVape>());
                yield return obj2;

            }
           
        }

        var obj = new Item();
        obj.SetDefaults(ModContent.ItemType<EpilepsyWarning>());
        yield return obj;
    }
}