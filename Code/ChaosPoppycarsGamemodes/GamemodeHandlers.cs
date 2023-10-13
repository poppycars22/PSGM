using RWF.GameModes;
using System;
using System.Collections.Generic;
using System.Text;
using ChaosPoppycarsGamemodes.Gamemodes;

namespace ChaosPoppycarsGamemodes.GamemodeHandlers
{

    public class SkillPoint_Handler : RWFGameModeHandler<GM_SkillPoint>
    {
        internal const string GameModeName = "Skill Points";
        internal const string GameModeID = "Skill_Points";
        public SkillPoint_Handler() : base(
            name: GameModeName,
            gameModeId: GameModeID,
            allowTeams: false,
            pointsToWinRound: 1,
            roundsToWinGame: 3,
            // null values mean RWF's instance values
            playersRequiredToStartGame: null,
            maxPlayers: null,
            maxTeams: null,
            maxClients: null,
            description: $"Get 10 Skill Points at the start of the game to spend on different stats, get additional skill points every 5 kills or from the Skill point card.")
            //videoURL: "https://github.com/Tess-y/Simple_Gamemodes/raw/master/Videos/Timed_Deathmatch.MP4")
    {

        }
    }

    public class Team_SkillPoint_Handler : RWFGameModeHandler<GM_SkillPoint>
    {
        internal const string GameModeName = "Team Skill Points";
        internal const string GameModeID = "Team_Skill_Points";
        public Team_SkillPoint_Handler() : base(
            name: GameModeName,
            gameModeId: GameModeID,
            allowTeams: true,
            pointsToWinRound: 1,
            roundsToWinGame: 5,
            // null values mean RWF's instance values
            playersRequiredToStartGame: null,
            maxPlayers: null,
            maxTeams: null,
            maxClients: null,
            description: $"Get 10 Skill Points at the start of the game to spend on different stats, get additional skill points every 5 kills or from the Skill point card.")
            //videoURL: "https://github.com/Tess-y/Simple_Gamemodes/raw/master/Videos/Timed_Deathmatch.MP4")
        {

        }
    }
}