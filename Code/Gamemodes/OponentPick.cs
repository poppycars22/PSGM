using RWF.GameModes;
using System;
using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using UnboundLib.GameModes;
using System.Collections;
using Photon.Realtime;

namespace PoppyScyyeGameModes.Gamemodes {
    internal class OponentPick : RWFGameMode {
        Harmony harmony = new Harmony("dev.scyyepoppy.rounds.gamemodes");
        static Dictionary<Player, List<CardInfo>> addQueue = new Dictionary<Player, List<CardInfo>>();
        static Dictionary<Player, List<CardInfo>> removeQueue = new Dictionary<Player, List<CardInfo>>();
        public override void StartGame()
        {
            base.StartGame();
            UnityEngine.Debug.Log("Game Started");

            var original = typeof(CardChoice).GetMethod("Pick");
            var postfix = typeof(OponentPick).GetMethod("Postfix");

            harmony.Patch(original, postfix: new HarmonyMethod(postfix));

            GameModeManager.AddHook(GameModeHooks.HookPickEnd, PickEnd);
        }

        IEnumerator PickEnd(IGameModeHandler handler)
        {
            UnityEngine.Debug.Log("Pick End");
            foreach (var entry in addQueue)
            {
                foreach (var card in entry.Value)
                {
                    UnityEngine.Debug.Log("Adding Card: " + card.cardName + " to " + entry.Key.playerID);
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(entry.Key, card, false, card.cardName.Substring(0, 1), 0, 0);
                }
            }
            foreach (var entry in removeQueue)
            {
                foreach (var card in entry.Value)
                {
                    UnityEngine.Debug.Log("Removing card: " + card.cardName + " from " + entry.Key.playerID);
                    ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(entry.Key, card, editCardBar: true);
                }
            }

            addQueue.Clear();
            removeQueue.Clear();

            yield break;
        }

        protected override void GameOver(int winningTeamID)
        {
            base.GameOver(winningTeamID);
            harmony.UnpatchSelf();
            UnityEngine.Debug.Log("Game Over");
        }

        protected override void GameOver(int[] winningTeamIDs)
        {
            base.GameOver(winningTeamIDs);
            harmony.UnpatchSelf();
            UnityEngine.Debug.Log("Game Over Team");
        }

        [HarmonyPatch(typeof(CardChoice), nameof(CardChoice.Pick))]
        [HarmonyPostfix]
        public static void Postfix(CardChoice __instance, GameObject pickedCard)
        {
            Player? player = PlayerManager.instance.players.Find(p => p.playerID == __instance.pickrID);
            CardInfo? card = pickedCard.GetComponent<CardInfo>();
            UnityEngine.Debug.Log(player == null);
            UnityEngine.Debug.Log(card == null);
            UnityEngine.Debug.Log(player.playerID + " picked card: " + card.cardName);

            if (removeQueue.ContainsKey(player))
            {
                removeQueue[player].Add(card);
            }
            else
            {
                removeQueue.Add(player, new List<CardInfo> { card });
            }

            if (addQueue.ContainsKey(PlayerManager.instance.players[GetIndexNextPlayer(player)]))
            {
                addQueue[PlayerManager.instance.players[GetIndexNextPlayer(player)]].Add(card);
            }
            else
            {
                addQueue.Add(PlayerManager.instance.players[GetIndexNextPlayer(player)], new List<CardInfo> { card });
            }
            /*
            ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer
                (player, card, editCardBar: true);
            

            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(PlayerManager.instance.players[GetIndexNextPlayer(player)],
                card, false, card.cardName.Substring(0, 1), 0, 0);
            */
        }



        private static int GetIndexOfCard(Player player, CardInfo card)
        {
            return Array.IndexOf(player.data.currentCards.ToArray(), card);
        }
        private static int GetIndexNextPlayer(Player player)
        {
            if (PlayerManager.instance.players.IndexOf(player) + 1 == PlayerManager.instance.players.Count)
            {
                UnityEngine.Debug.Log("Last Player");
                return 0;
            }

            UnityEngine.Debug.Log(PlayerManager.instance.players.IndexOf(player) + 1);
            return PlayerManager.instance.players.IndexOf(player) + 1;

        }
    }

    internal class OponentPickHandler : RWFGameModeHandler<OponentPick> {
        internal const string GameModeName = "Opponent Pick";
        internal const string GameModeID = "Opponent_Pick";

        // Null is default values
        public OponentPickHandler() : base(
            name: GameModeName,
            gameModeId: GameModeID,
            allowTeams: false,
            pointsToWinRound: 2,
            roundsToWinGame: 5,
            playersRequiredToStartGame: null,
            maxPlayers: null,
            maxTeams: null,
            maxClients: null,
            description: "Pick your opponents card for them")
        {

        }
    }

    internal class OponentPickTeamHandler : RWFGameModeHandler<OponentPick> {
        internal const string GameModeName = "Team Opponent Pick";
        internal const string GameModeID = "Team_Opponent_Pick";

        // Null is default values
        public OponentPickTeamHandler() : base(
            name: GameModeName,
            gameModeId: GameModeID,
            allowTeams: true,
            pointsToWinRound: 2,
            roundsToWinGame: 5,
            playersRequiredToStartGame: null,
            maxPlayers: null,
            maxTeams: null,
            maxClients: null,
            description: "Pick your opponents card for them")
        {

        }
    }
}
