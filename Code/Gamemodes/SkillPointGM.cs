using PoppyScyyeGameModes.Monos;
using RWF.GameModes;
using System.Collections;
using UnboundLib;

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
            description: $"Get 10 Skill Points at the start of the game to spend on different stats, get additional skill points every 5 kills or from the Skill point card.")
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
            description: $"Get 10 Skill Points at the start of the game to spend on different stats, get additional skill points every 5 kills or from the Skill point card.")
        {

        }
    }

    /*
    public class SkillPointShop
    {
        public static Shop SkillPoint_Shop;
        public static string ShopID = "PSGM_SkillPoint_Shop";
        public static Dictionary<String, int> SkillPoints = new Dictionary<String, int>();
        public static CharacterStatModifiers characterStats;

        internal static IEnumerator SkillUp()
        {
            SkillPoints = new Dictionary<string, int>();

            if (SkillPoint_Shop != null)
                ShopManager.instance.RemoveShop(SkillPoint_Shop);
            SkillPoint_Shop = ShopManager.instance.CreateShop(ShopID);
            SkillPoint_Shop.UpdateMoneyColumnName("Skill Points");
            SkillPoint_Shop.UpdateTitle("something something grow powerful");
            //GameModeManager.AddOnceHook(GameModeHooks.HookPickStart, (gm) => SetUpShopStart());
            // ChaosPoppycarsGamemodes.Instance.StartCoroutine(SetUpShop());
            yield break;
        }

        internal static IEnumerator SetUpShop()
          {
              List<UnboundLib.Utils.Card> allCards = UnboundLib.Utils.CardManager.cards.Values.ToList();
              List<CardItem> items = new List<CardItem>();
              SkillPoint_Shop.AddItem(new Purchasable);
              SkillPoint_Shop.AddItems(items.Select(c => c.Card.cardInfo.cardName + c.Card.cardInfo.name).ToArray(), items.ToArray());
              yield break;
          }

          internal static IEnumerator WaitTillShopDone()
          {
              bool done = true;
              GameObject gameObject = null;
              GameObject timer = null;
              float time = 120;
              PlayerManager.instance.players.ForEach(p =>
              {
                  if (p.GetAdditionalData().bankAccount.HasFunds(SkillPoints)) { SkillPoint_Shop.Show(p); done = false; }
              });

              if (!done)
              {
                  gameObject = new GameObject();
                  gameObject.AddComponent<Canvas>().sortingLayerName = "MostFront";
                  gameObject.AddComponent<TextMeshProUGUI>().text = "Waiting For Players To Spend Skill Points";
                  Color c = Color.yellow;
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
                  PlayerManager.instance.players.ForEach(p =>
                  {
                      if (ShopManager.instance.PlayerIsInShop(p))
                          done = false;
                  });
                  if (time <= 0)
                      ShopManager.instance.HideAllShops();

              }
              GameObject.Destroy(gameObject);
              GameObject.Destroy(timer);
          }


      }
      internal class SkillItem : Purchasable
      {
          private Dictionary<string, int> cost = new Dictionary<string, int>();
          public override Dictionary<string, int> Cost {  get { return cost; } }

          public override bool CanPurchase(Player player)
          {
              return true;
          }
          public override void OnPurchase(Player player, Purchasable item)
          {
              if (player.data.view.IsMine && !player.GetAdditionalData().bankAccount.HasFunds(SkillPointShop.SkillPoints))
                  SkillPointShop.SkillPoint_Shop.Hide();

              return;
          }



      }

      internal class CardItem : Purchasable
      {

          private Dictionary<string, int> cost = new Dictionary<string, int>();



          public override Dictionary<string, int> Cost { get { return cost; } }



          public override bool CanPurchase(Player player)
          {
              return true;
          }







          public override void OnPurchase(Player player, Purchasable item)
          {

              if (player.data.view.IsMine && !player.GetAdditionalData().bankAccount.HasFunds(SkillPointShop.SkillPoints))
                  SkillPointShop.SkillPoint_Shop.Hide();

              return;

          }
          public static IEnumerator ShowCard(Player player, CardInfo card)
          {
              yield return ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, card, 2f);

              yield break;
          }

          private GameObject GetCardVisuals(CardInfo card, GameObject parent)
          {
              GameObject cardObj = GameObject.Instantiate<GameObject>(card.gameObject, parent.gameObject.transform);
              cardObj.SetActive(true);
              cardObj.GetComponentInChildren<CardVisuals>().firstValueToSet = true;
              RectTransform rect = cardObj.GetOrAddComponent<RectTransform>();
              rect.localScale = 100f * Vector3.one;
              rect.anchorMin = Vector2.zero;
              rect.anchorMax = Vector2.one;
              rect.offsetMin = Vector2.zero;
              rect.offsetMax = Vector2.zero;
              rect.pivot = new Vector2(0.5f, 0.5f);

              GameObject back = FindObjectInChildren(cardObj, "Back");
              try
              {
                  GameObject.Destroy(back);
              }
              catch { }
              FindObjectInChildren(cardObj, "BlockFront")?.SetActive(false);

              var canvasGroups = cardObj.GetComponentsInChildren<CanvasGroup>();
              foreach (var canvasGroup in canvasGroups)
              {
                  canvasGroup.alpha = 1;
              }

              ItemShops.ItemShops.instance.ExecuteAfterSeconds(0.2f, () =>
              {
                  //var particles = cardObj.GetComponentsInChildren<GeneralParticleSystem>().Select(system => system.gameObject);
                  //foreach (var particle in particles)
                  //{
                  //    UnityEngine.GameObject.Destroy(particle);
                  //}

                  var rarities = cardObj.GetComponentsInChildren<CardRarityColor>();

                  foreach (var rarity in rarities)
                  {
                      try
                      {
                          rarity.Toggle(true);
                      }
                      catch
                      {

                      }
                  }

                  var titleText = FindObjectInChildren(cardObj, "Text_Name").GetComponent<TextMeshProUGUI>();

                  if ((titleText.color.r < 0.18f) && (titleText.color.g < 0.18f) && (titleText.color.b < 0.18f))
                  {
                      titleText.color = new Color32(200, 200, 200, 255);
                  }
              });

              return cardObj;
          }
          private static GameObject FindObjectInChildren(GameObject gameObject, string gameObjectName)
          {
              Transform[] children = gameObject.GetComponentsInChildren<Transform>(true);
              return (from item in children where item.name == gameObjectName select item.gameObject).FirstOrDefault();
          }
      }
    }
    */

}
