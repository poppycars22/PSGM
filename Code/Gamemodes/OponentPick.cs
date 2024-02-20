using RWF.GameModes;
using System;
using HarmonyLib;
using UnityEngine;

namespace PoppyScyyeGameModes.Gamemodes {
    internal class OponentPick : RWFGameMode {
        Harmony harmony = new Harmony("dev.scyyepoppy.rounds.gamemodes");
        public override void StartGame()
        {
            base.StartGame();
            UnityEngine.Debug.Log("Game Started");

            harmony.PatchAll();
            UnityEngine.Debug.Log(harmony.GetPatchedMethods().GetEnumerator().Current + " methods patched");
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
        static void Postfix(CardChoice __instance, GameObject pickedCard)
        {
            Player player = PlayerManager.instance.players.Find(p => p.playerID == __instance.pickrID);
            CardInfo card = pickedCard.GetComponent<CardInfo>();
            UnityEngine.Debug.Log(player.playerID + " picked card: " + card.cardName);
            
            ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer
                (player, GetIndexOfCard(player, card));
            UnityEngine.Debug.Log("Removing card: " + card.cardName + " from " + player.playerID);

            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(PlayerManager.instance.players[GetIndexNextPlayer(player)],
                card, false, card.cardName.Substring(0, 1), 0, 0);
            UnityEngine.Debug.Log("Adding Card: " + card.cardName + " to " + PlayerManager.instance.players[GetIndexNextPlayer(player)].playerID);
        }



        private static int GetIndexOfCard(Player player, CardInfo card)
        {
            return Array.IndexOf(player.data.currentCards.ToArray(), card);
        }
        private static int GetIndexNextPlayer(Player player)
        {
            if (PlayerManager.instance.players.IndexOf(player) + 1 == PlayerManager.instance.players.Count)
                return 0;
            else
                return PlayerManager.instance.players.IndexOf(player) + 1;
        }
    }

    internal class OponentPickHandler : RWFGameModeHandler<OponentPick> {
        internal const string GameModeName = "Oponent Pick";
        internal const string GameModeID = "Oponent_Pick";

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
            description: "Pick your oponents card for them")
        {

        }
    }

    internal class OponentPickTeamHandler : RWFGameModeHandler<OponentPick> {
        internal const string GameModeName = "Team Oponent Pick";
        internal const string GameModeID = "Team_Oponent_Pick";

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
            description: "Pick your oponents card for them")
        {

        }
    }
}
