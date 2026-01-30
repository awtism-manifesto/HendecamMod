using System.Collections.Generic;
using HendecamMod.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

//[AutoloadEquip(EquipType.Beard)]
public class KillShelterPermit : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 500);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Grants 3hp/s Life Regen, at the cost of Pets");
        tooltips.Add(line);
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<NastyRegen>().NastyEffect = true;
        player.ClearBuff(BuffID.PetBunny);
        player.ClearBuff(BuffID.PetDD2Dragon);
        player.ClearBuff(BuffID.PetDD2Gato);
        player.ClearBuff(BuffID.PetLizard);
        player.ClearBuff(BuffID.PetParrot);
        player.ClearBuff(BuffID.PetSapling);
        player.ClearBuff(BuffID.PetSpider);
        player.ClearBuff(BuffID.PetTurtle);
        player.ClearBuff(BuffID.BerniePet);
        player.ClearBuff(BuffID.BlueChickenPet);
        player.ClearBuff(BuffID.BrainOfCthulhuPet);
        player.ClearBuff(BuffID.ChesterPet);
        player.ClearBuff(BuffID.DD2BetsyPet);
        player.ClearBuff(BuffID.DD2OgrePet);
        player.ClearBuff(BuffID.DeerclopsPet);
        player.ClearBuff(BuffID.DestroyerPet);
        player.ClearBuff(BuffID.DualSlimePet);
        player.ClearBuff(BuffID.DukeFishronPet);
        player.ClearBuff(BuffID.EaterOfWorldsPet);
        player.ClearBuff(BuffID.EverscreamPet);
        player.ClearBuff(BuffID.EyeOfCthulhuPet);
        player.ClearBuff(BuffID.GlommerPet);
        player.ClearBuff(BuffID.IceQueenPet);
        player.ClearBuff(BuffID.JunimoPet);
        player.ClearBuff(BuffID.KingSlimePet);
        player.ClearBuff(BuffID.LunaticCultistPet);
        player.ClearBuff(BuffID.MartianPet);
        player.ClearBuff(BuffID.MoonLordPet);
        player.ClearBuff(BuffID.PigPet);
        player.ClearBuff(BuffID.PlanteraPet);
        player.ClearBuff(BuffID.QueenSlimePet);
        player.ClearBuff(BuffID.SkeletronPet);
        player.ClearBuff(BuffID.SkeletronPrimePet);
        player.ClearBuff(BuffID.TwinsPet);
        player.ClearBuff(BuffID.BabyDinosaur);
        player.ClearBuff(BuffID.BabyEater);
        player.ClearBuff(BuffID.BabyGrinch);
        player.ClearBuff(BuffID.BabyHornet);
        player.ClearBuff(BuffID.BabyImp);
        player.ClearBuff(BuffID.BabyPenguin);
        player.ClearBuff(BuffID.BabyRedPanda);
        player.ClearBuff(BuffID.BabySkeletronHead);
        player.ClearBuff(BuffID.BabySnowman);
        player.ClearBuff(BuffID.BabyTruffle);
        player.ClearBuff(BuffID.BabyWerewolf);
        player.ClearBuff(BuffID.BlackCat);
        player.ClearBuff(BuffID.CavelingGardener);
        player.ClearBuff(BuffID.CompanionCube);
        player.ClearBuff(BuffID.CursedSapling);
        player.ClearBuff(BuffID.DirtiestBlock);
        player.ClearBuff(BuffID.DynamiteKitten);
        player.ClearBuff(BuffID.UpbeatStar);
        player.ClearBuff(BuffID.EyeballSpring);
        player.ClearBuff(BuffID.FennecFox);
        player.ClearBuff(BuffID.GlitteryButterfly);
        player.ClearBuff(BuffID.LilHarpy);
        player.ClearBuff(BuffID.MiniMinotaur);
        player.ClearBuff(BuffID.Plantero);
        player.ClearBuff(BuffID.Puppy);
        player.ClearBuff(BuffID.ShadowMimic);
        player.ClearBuff(BuffID.SharkPup);
        player.ClearBuff(BuffID.Spiffo);
        player.ClearBuff(BuffID.Squashling);
        player.ClearBuff(BuffID.TikiSpirit);
        player.ClearBuff(BuffID.VoltBunny);
        player.ClearBuff(BuffID.ZephyrFish);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SoulofNight, 50);
        recipe.AddIngredient<Paper>();
        recipe.AddTile(TileID.Bookcases);
        recipe.Register();
    }
}