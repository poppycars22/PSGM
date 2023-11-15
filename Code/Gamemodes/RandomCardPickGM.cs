using HarmonyLib;
using RWF;
using RWF.GameModes;
using RWF.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnboundLib;
using UnboundLib.GameModes;
using UnityEngine;
using PickNCards;
using Photon.Pun;

namespace PoppyScyyeGameModes.Gamemodes
{
    public class RandomCardPickGM : RWFGameMode
    {
        public int picks;
        private bool anyCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return true;

        }
        public override IEnumerator DoStartGame()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                int picksMaster = PickNCards.PickNCards.PicksConfig.Value;
                picks = picksMaster;
            }
            //UnityEngine.Debug.Log(picks);
            CardBarHandler.instance.Rebuild();
            UIHandler.instance.InvokeMethod("SetNumberOfRounds", (int)GameModeManager.CurrentHandler.Settings["roundsToWinGame"]);
            ArtHandler.instance.NextArt();
            yield return GameModeManager.TriggerHook("GameStart");
            GameManager.instance.battleOngoing = false;
            UIHandler.instance.ShowJoinGameText("LETS GOO!", PlayerSkinBank.GetPlayerSkinColors(1).winText);
            yield return new WaitForSecondsRealtime(0.25f);
            UIHandler.instance.HideJoinGameText();
            PlayerSpotlight.CancelFade(disable_shadow: true);
            PlayerManager.instance.SetPlayersSimulated(simulated: false);
            PlayerManager.instance.InvokeMethod("SetPlayersVisible", false);
            MapManager.instance.LoadNextLevel();
            TimeHandler.instance.DoSpeedUp();
            yield return new WaitForSecondsRealtime(1f);
            yield return GameModeManager.TriggerHook("PickStart");
            List<Player> pickOrder = PlayerManager.instance.GetPickOrder();
            foreach (Player player in pickOrder)
            {
                for (int i = 0; i < picks; i++)
                {
                    yield return WaitForSyncUp();
                    yield return GameModeManager.TriggerHook("PlayerPickStart");
                    Gun gun = player.data.weaponHandler.gun;
                    GunAmmo gunAmmo = GetComponentInParent<GunAmmo>();
                    CharacterData data = player.data;
                    HealthHandler health = player.data.healthHandler;
                    Gravity gravity = GetComponentInParent<Gravity>();
                    Block block = GetComponentInParent<Block>();
                    CharacterStatModifiers characterStats = GetComponentInParent<CharacterStatModifiers>();
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, anyCondition), false, "", 2f, 2f, true);
                    yield return GameModeManager.TriggerHook("PlayerPickEnd");
                    yield return new WaitForSecondsRealtime(0.1f);
                }
            }

            yield return WaitForSyncUp();
            CardChoiceVisuals.instance.Hide();
            yield return GameModeManager.TriggerHook("PickEnd");
            PlayerSpotlight.FadeIn();
            MapManager.instance.CallInNewMapAndMovePlayers(MapManager.instance.currentLevelID);
            TimeHandler.instance.DoSpeedUp();
            TimeHandler.instance.StartGame();
            GameManager.instance.battleOngoing = true;
            UIHandler.instance.ShowRoundCounterSmall(teamPoints, teamRounds);
            PlayerManager.instance.InvokeMethod("SetPlayersVisible", true);
            StartCoroutine(DoRoundStart());
        }

        public override IEnumerator RoundTransition(int[] winningTeamIDs)
        {
            yield return GameModeManager.TriggerHook("PointEnd");
            yield return GameModeManager.TriggerHook("RoundEnd");
            if (GameModeManager.CurrentHandler.GetGameWinners().Any())
            {
                GameOver(winningTeamIDs);
                yield break;
            }

            StartCoroutine(PointVisualizer.instance.DoWinSequence(teamPoints, teamRounds, winningTeamIDs));
            yield return new WaitForSecondsRealtime(1f);
            MapManager.instance.LoadNextLevel();
            yield return new WaitForSecondsRealtime(1.3f);
            PlayerManager.instance.SetPlayersSimulated(simulated: false);
            TimeHandler.instance.DoSpeedUp();
            yield return GameModeManager.TriggerHook("PickStart");
            PlayerManager.instance.InvokeMethod("SetPlayersVisible", false);
            List<Player> pickOrder = PlayerManager.instance.GetPickOrder(winningTeamIDs);
            foreach (Player player in pickOrder)
            {
                if (!Enumerable.Contains(winningTeamIDs, player.teamID))
                {
                    for (int i = 0; i < picks; i++)
                    {
                        yield return WaitForSyncUp();
                        yield return GameModeManager.TriggerHook("PlayerPickStart");
                        Gun gun = player.data.weaponHandler.gun;
                        GunAmmo gunAmmo = GetComponentInParent<GunAmmo>();
                        CharacterData data = player.data;
                        HealthHandler health = player.data.healthHandler;
                        Gravity gravity = GetComponentInParent<Gravity>();
                        Block block = GetComponentInParent<Block>();
                        CharacterStatModifiers characterStats = GetComponentInParent<CharacterStatModifiers>();
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, anyCondition), false, "", 2f, 2f, true);
                        yield return GameModeManager.TriggerHook("PlayerPickEnd");
                        yield return new WaitForSecondsRealtime(0.1f);
                    }
                }
            }

            PlayerManager.instance.InvokeMethod("SetPlayersVisible", true);
            yield return GameModeManager.TriggerHook("PickEnd");
            yield return StartCoroutine(WaitForSyncUp());
            PlayerSpotlight.FadeIn();
            TimeHandler.instance.DoSlowDown();
            MapManager.instance.CallInNewMapAndMovePlayers(MapManager.instance.currentLevelID);
            PlayerManager.instance.RevivePlayers();
            yield return new WaitForSecondsRealtime(0.3f);
            TimeHandler.instance.DoSpeedUp();
            GameManager.instance.battleOngoing = true;
            isTransitioning = false;
            UIHandler.instance.ShowRoundCounterSmall(teamPoints, teamRounds);
            StartCoroutine(DoRoundStart());
        }


    }

    public class RandomCardPickHandler : RWFGameModeHandler<RandomCardPickGM>
    {
        internal const string GameModeName = "Random Card Pick";
        internal const string GameModeID = "Random_Card_Pick";

        // Null is default values
        public RandomCardPickHandler() : base(
            name: GameModeName,
            gameModeId: GameModeID,
            allowTeams: false,
            pointsToWinRound: 2,
            roundsToWinGame: 5,
            playersRequiredToStartGame: null,
            maxPlayers: null,
            maxTeams: null,
            maxClients: null,
            description: "Skillfully picking? Never heard of it! Get a random card each pick phase.")
        {

        }
    }

    public class RandomCardPickTeamHandler : RWFGameModeHandler<RandomCardPickGM>
    {
        internal const string GameModeName = "Team Random Card Pick";
        internal const string GameModeID = "Random_Card_Pick_Team";

        // Null is default values
        public RandomCardPickTeamHandler() : base(
            name: GameModeName,
            gameModeId: GameModeID,
            allowTeams: true,
            pointsToWinRound: 2,
            roundsToWinGame: 5,
            playersRequiredToStartGame: null,
            maxPlayers: null,
            maxTeams: null,
            maxClients: null,
            description: "Skillfully picking? Never heard of it! Get a random card each pick phase.")
        {

        }
    }
}
