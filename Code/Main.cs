using BepInEx;
using HarmonyLib;
using PoppyScyyeGameModes.Gamemodes;
using PoppyScyyeGameModes.Monos;
using UnboundLib;
using UnboundLib.Cards;
using System.Collections;
using UnboundLib.GameModes;
using PoppyScyyeGameModes.Cards;
using UnboundLib.Utils;
using TMPro;
using UnboundLib.Utils.UI;
using UnityEngine;
using Photon.Pun;
using UnboundLib.Networking;
using BepInEx.Configuration;
using System;

namespace PoppyScyyeGameModes
{
    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("io.olavim.rounds.rwf", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.itemshops", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.pickncards")]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]

    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class Main : BaseUnityPlugin
    {
        private const string ModId = "dev.scyyepoppy.rounds.gamemodes";
        private const string ModName = "Poppys And Scyyes Gamemodes";
        private const string Version = "1.0.0"; // What version are we on (major.minor.patch)?

        /*
         * TODO: Make the gamemode work properly :"D
         */
        public static ConfigEntry<int> KillsConfig;
        public static ConfigEntry<int> StartingPointsConfig;
        public static Main instance { get; private set; }

       

        void Awake()
        {
            instance = this;
            Unbound.RegisterCredits("Poppys And Scyyes Gamemodes", new string[] { "Poppycars", "Scyye" }, new string[] { "GitHub", "Poppycars", "Scyye" },
                new string[] {"https://github.com/poppycars22/PSGM", "https://github.com/poppycars22", "https://github.com/Scyye"});


            KillsConfig = base.Config.Bind<int>(ModId, "KillsConfig", 3, "The amount of kills needed to get a skill point");
            StartingPointsConfig = base.Config.Bind<int>(ModId, "StartingPointsConfig", 10, "The amount of skill points each player starts with");
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

       // internal CardInfo GiveSkillPointCard;

        void Start()
        {
            GameModeManager.AddHandler<SkillPointGM>(SkillPointHandler.GameModeID, new SkillPointHandler());
            GameModeManager.AddHandler<SkillPointGM>(SkillPointTeamHandler.GameModeID, new SkillPointTeamHandler());

            GameModeManager.AddHandler<RandomCardPickGM>(RandomCardPickHandler.GameModeID, new RandomCardPickHandler());
            GameModeManager.AddHandler<RandomCardPickGM>(RandomCardPickTeamHandler.GameModeID, new RandomCardPickTeamHandler());

            CustomCard.BuildCard<AmmoSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<BlockCooldownSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<DamageSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<HealthSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<BlockSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<RegenerationSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<BounceSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<JumpSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<MoveSpeedSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<ProjectileSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<ProjectileSpeedSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<ProjectileSimSpeedSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<SpreadSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<KnockbackSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<ReloadSpeedSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<AttackSpeedSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<DoTSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<RespawnsSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<SizeSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<SlowSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<PercentDamageSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<BulletGravSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<BulletSizeSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<BurstSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<LifestealSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<BlockForceSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<UpwardsBlockForceSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<HealOnBlockSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<DmgOnBounceSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<SpeedOnBounceSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<JumpHeightSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<PlayerGravitySkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));

            // CustomCard.BuildCard<GiveSkillPointCard>(c => GiveSkillPointCard = c);
            Unbound.RegisterMenu(ModName, () => { }, this.NewGUI, null, false);
            //Unbound.RegisterMenu(ModName, delegate () { }, new Action<GameObject>(this.NewGUI), null, false);
            // handshake to sync settings
            Unbound.RegisterHandshake(Main.ModId, this.OnHandShakeCompleted);

            GameModeManager.AddHook(GameModeHooks.HookPickEnd, _ => SkillPointShop.WaitUntillShopDone());
            GameModeManager.AddHook(GameModeHooks.HookGameStart, GameStart);

           // GameModeManager.AddHook(GameModeHooks.HookGameStart, RemoveSkillPointCard);

        }
        private void OnHandShakeCompleted()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                NetworkingManager.RPC_Others(typeof(Main), nameof(SyncSettings), new object[] { Main.KillsConfig.Value, Main.StartingPointsConfig.Value });
            }
        }
        [UnboundRPC]
        private static void SyncSettings(int host_kills, int host_StartingPoints)
        {
            Main.KillsConfig.Value = host_kills;
            Main.StartingPointsConfig.Value = host_StartingPoints;
        }
        private void NewGUI(GameObject menu)
        {
            MenuHandler.CreateText("Skill Point Options", menu, out _, 60);
            MenuHandler.CreateText(" ", menu, out _, 30);

            MenuHandler.CreateSlider("Number of kills for a skill point", menu, 30, 1f, 100f, KillsConfig.Value, value => KillsConfig.Value = (int)value, out UnityEngine.UI.Slider _, true);
            MenuHandler.CreateText(" ", menu, out _, 30);
            
            MenuHandler.CreateSlider("Number of Skill Points to start with", menu, 30, 1f, 50f, StartingPointsConfig.Value, value => StartingPointsConfig.Value = (int)value, out UnityEngine.UI.Slider _, true);
            MenuHandler.CreateText(" ", menu, out _, 30);
        }
        
        
        internal IEnumerator GameStart(IGameModeHandler gm)
        {
            yield return SkillPointShop.SkillUp();
            yield break;
        }
        
      /*  internal IEnumerator RemoveSkillPointCard(IGameModeHandler gm)
        {
            if (!gm.Name.Contains("Skill_Point"))
            {
                CardManager.DisableCard(GiveSkillPointCard);
            } else
            {
                CardManager.EnableCard(GiveSkillPointCard);
            }
            yield break;
        }*/
    }
}
