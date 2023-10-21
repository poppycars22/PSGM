using HarmonyLib;
using RWF.GameModes;
using System.Collections;
using System.Collections.Generic;
using UnboundLib;
using UnboundLib.GameModes;
using UnityEngine;

namespace PoppyScyyeGameModes.Gamemodes
{
    public class RandomCardPickGM : RWFGameMode
    {
        static bool inPickPhase = false;

        public override IEnumerator DoStartGame()
        {
            GameModeManager.AddOnceHook(GameModeHooks.HookPickStart, TogglePhase );
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, TogglePhase);

            Main.instance.StartCoroutine(StartPickTimer(CardChoice.instance));

            yield return base.DoStartGame();
        }

        private IEnumerator TogglePhase(IGameModeHandler handler)
        {
            inPickPhase = !inPickPhase;
            yield break;
        }

        

        internal static IEnumerator StartPickTimer(CardChoice instance)
        {
            yield return new WaitWhile(() => !inPickPhase);
            
            yield return new WaitForSeconds(DrawNCards.DrawNCards.NumDraws * 0.4f);

            var traverse = Traverse.Create(instance);

            var spawnedCards = (List<GameObject>)traverse.Field("spawnedCards").GetValue();
            int selectedCard = (int)instance.GetFieldValue("currentlySelectedCard");
            instance.Pick(spawnedCards[selectedCard]);
            

            traverse.Field("pickrID").SetValue(-1);
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
