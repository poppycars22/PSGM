using BepInEx;
using UnityEngine;
using UnboundLib;
using UnboundLib.Cards;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ModdingUtils;
using ModdingUtils.Extensions;
using System.Collections;
using UnboundLib.GameModes;
using Jotunn.Utils;
using System.Linq;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using PSGM.Gamemodes;
using PSGM.GamemodeHandlers;

namespace PSGM
{

    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("io.olavim.rounds.rwf", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.itemshops", BepInDependency.DependencyFlags.HardDependency)]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]

    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class PoppysAndScyyesGameModes : BaseUnityPlugin
    {
        private const string ModId = "com.Poppycars.CPGM.Id";
        private const string ModName = "Poppys And Scyyes GameModes";
        public const string Version = "0.0.1"; // What version are we on (major.minor.patch)?
        public const string ModInitials = "PSGM";
        public static PoppysAndScyyesGameModes Instance { get; private set; }
        public static object CPGM_Assets { get; internal set; }

       // public static AssetBundle Bundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("cpcart", typeof(ChaosPoppycarsCards).Assembly);

        void Awake()
        {
            // Use this to call any harmony patch files your mod may have
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
            Instance = this;
            // Bundle.LoadAsset<GameObject>("ModCards").GetComponent<CardHolder>().RegisterCards();
            //  Bundle.LoadAllAssets();
        }
        //REGISTER CURSES
        /*  private void RegisterCards() {

              var assests = Bundle.LoadAllAssets<GameObject>();
              List<Type> types = typeof(ChaosPoppycarsCards).Assembly.GetTypes().Where(type => type.IsClass&&!type.IsAbstract&&type.IsSubclassOf(typeof(CustomCard))).ToList();
              foreach(var type in types) {
                  try {
                      var card = assests.Where(a => a is GameObject&&a.GetComponent<CustomCard>()!=null&&a.GetComponent<CustomCard>().GetType()==type).First();
                      try {
                          type.GetField("Card", System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Static).GetValue(null);
                          card.GetComponent<CustomCard>().BuildUnityCard(cardInfo => type.GetField("Card", System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Static).SetValue(null, cardInfo));
                      } catch {
                          card.GetComponent<CustomCard>().BuildUnityCard(null);
                      }
                  } catch {
                  }
              }
          }
        */
        private void Start()

        {
            Instance = this;
            GameModeManager.AddHandler<GM_SkillPoint>(SkillPoint_Handler.GameModeID, new SkillPoint_Handler());
            GameModeManager.AddHandler<GM_SkillPoint>(Team_SkillPoint_Handler.GameModeID, new Team_SkillPoint_Handler());

            //ChaosPoppycarsCards.ArtAssets=AssetUtils.LoadAssetBundleFromResources("cpccart", typeof(ChaosPoppycarsCards).Assembly);
            //RegisterCards();
            /*
                        CustomCard.BuildCard<Sugared>();
                        CustomCard.BuildCard<Getaway>();
                        CustomCard.BuildCard<WoodenHoe>((card) => WoodenHoe.Card = card);
                        CustomCard.BuildCard<WoodenSword>((card) => WoodenSword.Card = card);
                        CustomCard.BuildCard<DoubleDuplicator>();
                        CustomCard.BuildCard<Duplicator>();
                        CustomCard.BuildCard<StoneSword>((card) => StoneSword.Card = card);
                        CustomCard.BuildCard<GoldSword>((card) => GoldSword.Card = card);
                        CustomCard.BuildCard<IronSword>((card) => IronSword.Card = card);
                        CustomCard.BuildCard<DiamondSword>((card) => DiamondSword.Card = card);
                        CustomCard.BuildCard<NetheriteSword>((card) => NetheriteSword.Card = card);
                        CustomCard.BuildCard<MCBow>((card) => MCBow.Card = card);
                        CustomCard.BuildCard<AmmoChest>();
                        CustomCard.BuildCard<StrengthPotion>((card) => StrengthPotion.Card = card);
                        CustomCard.BuildCard<SpeedPotion>((card) => SpeedPotion.Card = card);
                        CustomCard.BuildCard<LetherArmor>((card) => LetherArmor.Card = card);
                        CustomCard.BuildCard<ChainArmor>((card) => ChainArmor.Card = card);
                        CustomCard.BuildCard<IronArmor>((card) => IronArmor.Card = card);
                        CustomCard.BuildCard<GoldArmor>((card) => GoldArmor.Card = card);
                        CustomCard.BuildCard<DiamondArmor>((card) => DiamondArmor.Card = card);
                        CustomCard.BuildCard<NetheriteArmor>((card) => NetheriteArmor.Card = card);
                        CustomCard.BuildCard<JumpPotion>((card) => JumpPotion.Card = card);
                        CustomCard.BuildCard<RegenPotion>((card) => RegenPotion.Card = card);
                        CustomCard.BuildCard<ActivatedDuplicator>();
                        CustomCard.BuildCard<UltimatePotion>((card) => UltimatePotion.Card = card);
                        CustomCard.BuildCard<WoodenAxe>((card) => WoodenAxe.Card = card);
                        CustomCard.BuildCard<BouncyGel>();
                        CustomCard.BuildCard<StoneAxe>((card) => StoneAxe.Card = card);
                        CustomCard.BuildCard<IronAxe>((card) => IronAxe.Card = card);
                        CustomCard.BuildCard<GoldAxe>((card) => GoldAxe.Card = card);
                        CustomCard.BuildCard<DiamondAxe>((card) => DiamondAxe.Card = card);
                        CustomCard.BuildCard<NetheriteAxe>((card) => NetheriteAxe.Card = card);
                        CustomCard.BuildCard<TotemOfUndying>((card) => TotemOfUndying.Card = card);
                        CustomCard.BuildCard<InvisablityPotion>((card) => InvisablityPotion.Card = card);
                        CustomCard.BuildCard<Negafy>();
                        CustomCard.BuildCard<StoneHoe>((card) => StoneHoe.Card = card);
                        CustomCard.BuildCard<GoldHoe>((card) => GoldHoe.Card = card);
                        CustomCard.BuildCard<IronHoe>((card) => IronHoe.Card = card);
                        CustomCard.BuildCard<DiamondHoe>((card) => DiamondHoe.Card = card);
                        CustomCard.BuildCard<NetheriteHoe>((card) => NetheriteHoe.Card = card);
                        CustomCard.BuildCard<WormHoleClip>();
                        CustomCard.BuildCard<MCShield>((card) => MCShield.Card = card);
                        CustomCard.BuildCard<CocaCola>();
                        CustomCard.BuildCard<Pepsi>();
                        CustomCard.BuildCard<DrPepper>();
                        CustomCard.BuildCard<Cards.Sprite>();
                        CustomCard.BuildCard<BouncyBombs>();
                        CustomCard.BuildCard<MountainDew>();
                        CustomCard.BuildCard<LightSaber>();
                        CustomCard.BuildCard<CraftingTable>((card) => CraftingTable.Card = card);
                        CustomCard.BuildCard<BrewingStand>((card) => BrewingStand.Card = card);
                        CustomCard.BuildCard<FlamingArrows>((card) => FlamingArrows.Card = card);
                        CustomCard.BuildCard<PoisonArrows>((card) => PoisonArrows.Card = card);
                        CustomCard.BuildCard<ToxicArrows>((card) => ToxicArrows.Card = card);
                        CustomCard.BuildCard<ExplosiveArrows>((card) => ExplosiveArrows.Card = card);
                        CustomCard.BuildCard<BouncyArrows>((card) => BouncyArrows.Card = card);
                        CustomCard.BuildCard<Arrows>((card) => Arrows.Card = card);
                        CustomCard.BuildCard<PunchII>((card) => PunchII.Card = card);
                        CustomCard.BuildCard<SpeedCurse>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
                        CustomCard.BuildCard<BlockConfusion>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
                        CustomCard.BuildCard<NerfGun>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
                        CustomCard.BuildCard<SpeedDemon>((card) => SpeedDemon.Card = card);
                        CustomCard.BuildCard<MomentumShots>((card) => MomentumShots.Card = card);
                        CustomCard.BuildCard<SpeedyHands>((card) => SpeedyHands.Card = card);
                        CustomCard.BuildCard<Tricky>((card) => Tricky.Card = card);
                        CustomCard.BuildCard<TriggerFinger>((card) => TriggerFinger.Card = card);
                        CustomCard.BuildCard<Swifter>((card) => Swifter.Card = card);
                        CustomCard.BuildCard<Stretches>((card) => Stretches.Card = card);
                        CustomCard.BuildCard<LegDay>((card) => LegDay.Card = card);
                        CustomCard.BuildCard<SpeedstersGun>((card) => SpeedstersGun.Card = card);
                        CustomCard.BuildCard<AirHops>((card) => AirHops.Card = card);
                        // CustomCard.BuildCard<WoodenPickaxe>();
                        // CustomCard.BuildCard<StonePickaxe>();
                        // CustomCard.BuildCard<GoldPickaxe>();
                        CustomCard.BuildCard<PoppysChaos>((card) => PoppysChaos.Card = card);
                        CustomCard.BuildCard<PercentageBullets>();
                        CustomCard.BuildCard<BalloonBullets>();
                        CustomCard.BuildCard<IcySprings>();
                        CustomCard.BuildCard<GeeseSwarm>();
                        CustomCard.BuildCard<Goose>((card) => Goose.Card = card);
                        CustomCard.BuildCard<Whynack>();
                        CustomCard.BuildCard<Anarkey>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
                        CustomCard.BuildCard<FWPhantom>();
                        CustomCard.BuildCard<Peptide>();
                        CustomCard.BuildCard<ScareJackpot>();
                        CustomCard.BuildCard<HealingBlock>();
                        CustomCard.BuildCard<AbsorbingBullets>();
                        CustomCard.BuildCard<RoyalGifting>();
                        CustomCard.BuildCard<BrittleBullets>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
                        CustomCard.BuildCard<FearfulCurse>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
                        CustomCard.BuildCard<LegendaryJackpot>();
                        CustomCard.BuildCard<SuperSprings>();
                        CustomCard.BuildCard<JumpPower>();
                        CustomCard.BuildCard<JumpBursts>();
                        CustomCard.BuildCard<JumpSpeed>();
                        CustomCard.BuildCard<JumpShrink>();
                        CustomCard.BuildCard<Nullgendary>();
                        CustomCard.BuildCard<FriendNulls>();
                        CustomCard.BuildCard<KnifeGoose>((card) => KnifeGoose.Card = card);
                        CustomCard.BuildCard<GoldGoose>((card) => GoldGoose.Card = card);
                        CustomCard.BuildCard<HealthBounces>();
                        //CustomCard.BuildCard<WoodenShovel>((card) => WoodenShovel.Card = card);*/
            //GameModeManager.AddHook(GameModeHooks.HookRoundEnd, UpgradeAction);

            //  GameModeManager.AddHook(GameModeHooks.HookBattleStart, LightSaberRangeReset);
        }


        
       /* private IEnumerator UpgradeAction(IGameModeHandler gm)
        {

            yield return WoodenSword.UpgradeSword(gm);
            yield return WoodenHoe.UpgradeHoe(gm);
            yield return WoodenAxe.UpgradeAxe(gm);
            yield return LetherArmor.UpgradeArmor(gm);
           

        }
        */
        
        /* private IEnumerator LightSaberRangeReset(IGameModeHandler gm)
         {
             yield return LightSaber.RangeResetTruth(gm);
         } */
        IEnumerator GameStart(IGameModeHandler gm)
        {
            // Runs at start of match
            foreach (var player in PlayerManager.instance.players)
            {
                //ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Remove(CPCCardCategories.PotionCategory);
              
            }
            yield break;

            }
        
        internal static AssetBundle ArtAssets;
    }
    static class PSGMCardCategories
    {
        //public static CardCategory PotionCategory = CustomCardCategories.instance.CardCategory("UltimatePotion");
     
    }
}
