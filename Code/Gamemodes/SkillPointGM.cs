using Photon.Pun;
using PoppyScyyeGameModes.Monos;
using RWF.GameModes;
using System.Collections;
using System.Collections.Generic;
using UnboundLib;
using UnboundLib.Networking;

namespace PoppyScyyeGameModes.Gamemodes
{
    public class SkillPointGM : RWFGameMode
    {
        internal static SkillPointGM instance;
        public Dictionary<int, int> Kills = new Dictionary<int, int>() { };
        internal Dictionary<int, int> lastPlayerDamage = new Dictionary<int, int>() { };

        public override IEnumerator DoStartGame()
        {
            foreach (Player player in PlayerManager.instance.players)
            {
                gameObject.GetOrAddComponent<SkillPointMono>();
            }
            yield return base.DoStartGame();
        }
        public override void PlayerJoined(Player player)
        {
            Kills[player.playerID] = 0;
            lastPlayerDamage[player.playerID] = player.playerID;
            base.PlayerJoined(player);
        }
        public override IEnumerator DoPointStart()
        {
            foreach (Player player in PlayerManager.instance.players)
            {
                lastPlayerDamage[player.playerID] = player.playerID;
            }
            yield return base.DoPointStart();
        }
        public override IEnumerator DoRoundStart()
        {
            foreach (Player player in PlayerManager.instance.players)
            {
                lastPlayerDamage[player.playerID] = player.playerID;
            }
            yield return base.DoRoundStart();
        }
        protected override void Awake()
        {
            instance = this;
            base.Awake();
        }
        public void Start()
        {
            StartCoroutine(Init());
        }
        public override void PlayerDied(Player killedPlayer, int teamsAlive)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (lastPlayerDamage[killedPlayer.playerID] == killedPlayer.playerID)
                {
                    NetworkingManager.RPC(typeof(SkillPointGM), nameof(UpdateKills), new object[] { killedPlayer.playerID, -1 });
                }
                else
                {
                    if (GetPlayerWithID(lastPlayerDamage[killedPlayer.playerID]).teamID == killedPlayer.teamID)
                        NetworkingManager.RPC(typeof(SkillPointGM), nameof(UpdateKills), new object[] { killedPlayer.playerID, 1 });
                    else
                        NetworkingManager.RPC(typeof(SkillPointGM), nameof(UpdateKills), new object[] { lastPlayerDamage[killedPlayer.playerID], 1 });
                }
            }
            if (teamsAlive == 1)
            {
                TimeHandler.instance.DoSlowDown();
                if (PhotonNetwork.IsMasterClient)
                {
                    NetworkingManager.RPC(typeof(RWFGameMode), nameof(RPCA_NextRound), new int[1] { PlayerManager.instance.GetLastPlayerAlive().teamID }, teamPoints, teamRounds);
                }
            }
        }
        public static int GetKills(int playerId)
        {
            int kills;
            instance.Kills.TryGetValue(playerId, out kills);
            return kills;
        }
        [UnboundRPC]
        public static void UpdateKills(int playerID, int kills)
        {
            instance.Kills[playerID] += kills;
        }
        [UnboundRPC]
        public static void SetKills(int playerID, int kills)
        {
            instance.Kills[playerID] = kills;
        }
        internal static Player GetPlayerWithID(int playerID)
        {
            for (int i = 0; i < PlayerManager.instance.players.Count; i++)
            {
                if (PlayerManager.instance.players[i].playerID == playerID)
                {
                    return PlayerManager.instance.players[i];
                }
            }
            return null;
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
            description: "Get 10 Skill Points at the start of the game to spend on different stats, get additional skill points every 3 kills or from the Skill point card.")
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
