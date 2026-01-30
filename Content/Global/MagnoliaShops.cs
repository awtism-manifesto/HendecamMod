using HendecamMod.Content.Items.Placeables;
using HendecamMod.Content.Items.Weapons;

namespace HendecamMod.Content.Global;

public class MagnoliaShops : GlobalNPC
{
    public override void ModifyShop(NPCShop shop)
    {
        if (shop.NpcType == NPCID.Merchant)
        {
            // Adding an item to a vanilla NPC is easy:
            // This item sells for the normal price.
            shop.Add<MoltenShuriken>(condition: Terraria.Condition.Hardmode);
        }

        if (shop.NpcType == NPCID.GoblinTinkerer)
        {
            // Adding an item to a vanilla NPC is easy:
            // This item sells for the normal price.
            shop.Add<SteelBar>(condition: Terraria.Condition.Hardmode);
        }

        if (shop.NpcType == NPCID.Cyborg)
        {
            // Adding an item to a vanilla NPC is easy:
            // This item sells for the normal price.
            shop.Add<MorbiumBar>(condition: Terraria.Condition.DownedCultist);
        }
    }
}