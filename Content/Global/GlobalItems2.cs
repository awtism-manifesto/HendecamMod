using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Items.Accessories.Cubes;

namespace HendecamMod.Content.Global;


public class CobaltPickaxeBuff : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.CobaltPickaxe;
    }

    public override void SetDefaults(Item item)
    {
        item.pick = 125;
    }
}
public class ShimmerLocks : GlobalItem
{
   
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ShimmerTransformToItem[ItemID.Hammush] = ItemID.MushroomSpear;
        ItemID.Sets.ShimmerTransformToItem[ItemID.SlimeHook] = ItemType<GelCube>();
    }
  
}
