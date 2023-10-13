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

            yield break;
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
            pointsToWinRound: 1,
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
            pointsToWinRound: 1,
            roundsToWinGame: 5,
            playersRequiredToStartGame: null,
            maxPlayers: null,
            maxTeams: null,
            maxClients: null,
            description: "Get 10 Skill Points at the start of the game to spend on different stats, get additional skill points every 5 kills or from the Skill point card.")
        {

        }
    }


    public class SkillPointShop
    {
        #pragma warning disable CS8618
        public static Shop SkillPointItemShop;
        public static string ShopID = "psgm.skillpoint";
        public static CharacterStatModifiers? CharacterStats;

        public static Dictionary<Player, int> PlayerSkillPoints = new Dictionary<Player, int>();

        internal static IEnumerator SkillUp()
        {
            if (SkillPointItemShop != null)
                ShopManager.instance.RemoveShop(SkillPointItemShop);
            SkillPointItemShop = ShopManager.instance.CreateShop(ShopID);
            SkillPointItemShop.UpdateMoneyColumnName("Skill Points");
            SkillPointItemShop.UpdateTitle("Test Title For Now");
            Main.instance.StartCoroutine(SetUpShop());
            yield break;
        }

        internal static IEnumerator SetUpShop()
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            List<PurchasableCard> cards = new List<PurchasableCard>();
            foreach (var c in SkillPointCard.Cards)
            {
                map.Add("Skill Points", c.GetCost());
                cards.Add(new PurchasableCard(c.cardInfo, map, new[] { new Tag("Skill Stat") }));
                map.Remove("Skill Points");
            }
            SkillPointItemShop.AddItems(cards.ToArray());
            yield break;
        }

        internal static IEnumerator WaitTillShopDone()
        {
            bool done = true;
            GameObject gameObject = null;
            GameObject timer = null;
            float time = 120;
            PlayerManager.instance.players.ForEach(p => {
                // if (p.GetAdditionalData().bankAccount.HasFunds()) { SkillPointItemShop.Show(p); done = false; }
            });

            if (!done)
            {
                gameObject = new GameObject();
                gameObject.AddComponent<Canvas>().sortingLayerName = "MostFront";
                gameObject.AddComponent<TextMeshProUGUI>().text = "Waiting For Players In Wish Menu";
                Color c = Color.magenta;
                c.a = .85f;
                gameObject.GetComponent<TextMeshProUGUI>().color = c;
                gameObject.transform.localScale = new Vector3(.2f, .2f);
                gameObject.transform.localPosition = new Vector3(0, 5);
                timer = new GameObject();
                timer.AddComponent<Canvas>().sortingLayerName = "MostFront";
                timer.transform.localScale = new Vector3(.2f, .2f);
                timer.transform.localPosition = new Vector3(0, 16);
                timer.AddComponent<TextMeshProUGUI>().color = c;
                for (int i = 0; i < 5; i++)
                {
                    timer.GetComponent<TextMeshProUGUI>().text = ((int)time).ToString();
                    yield return new WaitForSecondsRealtime(1f);
                    time -= 1;
                }
            }
            while (!done)
            {
                timer.GetComponent<TextMeshProUGUI>().text = ((int)time).ToString();
                done = true;
                yield return new WaitForSecondsRealtime(0.2f);
                time -= 0.2f;
                PlayerManager.instance.players.ForEach(p => {
                    if (ShopManager.instance.PlayerIsInShop(p))
                        done = false;
                });
                if (time <= 0)
                {
                    ShopManager.instance.HideAllShops();
                    done = true;
                }

            }
            GameObject.Destroy(gameObject);
            GameObject.Destroy(timer);
        }
    }
}
