using ItemShops.Extensions;
using ItemShops.Utils;
using PoppyScyyeGameModes.Cards;
using PoppyScyyeGameModes.Monos;
using RWF.GameModes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnboundLib;
using UnityEngine;

namespace PoppyScyyeGameModes.Gamemodes
{
    public class SkillPointGM : RWFGameMode
    {
        public override IEnumerator DoStartGame()
        {
            foreach (Player player in PlayerManager.instance.players)
            {
                player.gameObject.GetOrAddComponent<SkillPointMono>();
            }
            yield return base.DoStartGame();
        }
    }

    public class SkillPointHandler : RWFGameModeHandler<SkillPointGM>
    {
        internal const string GameModeName = "Skill Points";
        internal const string GameModeID = "Skill_Points";

        // Null is default values
        public SkillPointHandler() : base(
            name: GameModeName,
            gameModeId: GameModeID,
            allowTeams: false,
            pointsToWinRound: 2,
            roundsToWinGame: 3,
            playersRequiredToStartGame: null,
            maxPlayers: null,
            maxTeams: null,
            maxClients: null,
            description: "Get 10 Skill Points at the start of the game to spend on different stats, get additional skill points every 5 kills or from the Skill point card.")
        {

        }
    }

    public class SkillPointTeamHandler : RWFGameModeHandler<SkillPointGM>
    {
        internal const string GameModeName = "Team Skill Points";
        internal const string GameModeID = "Team_Skill_Points";

        // Null is default values
        public SkillPointTeamHandler() : base(
            name: GameModeName,
            gameModeId: GameModeID,
            allowTeams: true,
            pointsToWinRound: 2,
            roundsToWinGame: 5,
            playersRequiredToStartGame: null,
            maxPlayers: null,
            maxTeams: null,
            maxClients: null,
            description: "Get 10 Skill Points at the start of the game to spend on different stats, get additional skill points every 5 kills or from the Skill point card.")
        {

        }
    }


    
}
