using UnityEngine;
using System.Collections.Generic;
using ItemShops.Utils;
using PoppyScyyeGameModes.Cards;
using System.Collections;
using TMPro;
using System;
using System.Linq;
using PoppyScyyeGameModes.Extentions;
using ItemShops.Extensions;

namespace PoppyScyyeGameModes.Monos
{
    public class SkillPointMono : MonoBehaviour
    {

        public void Awake()
        {
            Player player = this.GetComponentInParent<Player>();
            player.AddSkillPoints(10);
        }
    }
    public class SkillPointShop
    {
        #pragma warning disable CS8618
        public static Shop SkillPointItemShop;
        public const string SkillPoints = "Skill Points";
        public static Dictionary<String, int> Skill_Points = new Dictionary<String, int>();
        public static string ShopID = "psgm.skillpoint";
        public static CharacterStatModifiers? CharacterStats;
        public static readonly Tag SkillPointTag = new Tag("Stats");
        public static Tag cardTag;




        internal static IEnumerator SkillUp()
        {
            Skill_Points = new Dictionary<string, int>();
            Skill_Points.Add("Skill Points", 1);
            if (SkillPointItemShop != null)
            {
                ShopManager.instance.RemoveShop(SkillPointItemShop);
            }
            SkillPointItemShop = ShopManager.instance.CreateShop(ShopID);
            SkillPointItemShop.UpdateMoneyColumnName("Skill Points");
            SkillPointItemShop.UpdateTitle("Skill Up");
            Main.instance.StartCoroutine(SetUpShop());
            yield break;
        }

        internal static IEnumerator SetUpShop()
        {
            List<PurchasableCard> cards = new List<PurchasableCard>();
            foreach (var c in SkillPointCard.Cards)
            {
                if (c.GetLimit() == 0)
                {
                    cardTag = new Tag(c.GetCategory());
                    //SkillPointItemShop.AddItem(new PurchasableCard(c.cardInfo, new Dictionary<string, int> { { SkillPoints, c.GetCost() } }, new Tag[] { SkillPointTag, cardTag }), new PurchaseLimit(0, c.GetLimit()));
                    cards.Add(new PurchasableCard(c.cardInfo, new Dictionary<string, int> { { SkillPoints, c.GetCost() } }, new Tag[] { SkillPointTag, cardTag }));
                }
                else
                {
                        SkillPointItemShop.AddItem(new PurchasableCard(c.cardInfo, new Dictionary<string, int> { { SkillPoints, c.GetCost() } }, new Tag[] { SkillPointTag, cardTag, new Tag("Limited") }), new PurchaseLimit(0, c.GetLimit()));
                }

            }
            SkillPointItemShop.AddItems(cards.Select(c => c.Card.cardName + c.Card.name).ToArray(), cards.ToArray(), new PurchaseLimit(0, 0));
            yield break;
        }

        internal static IEnumerator WaitUntillShopDone()
        {
            bool done = true;
            GameObject gameObject = new GameObject();
            GameObject timer = new GameObject();
            float time = 120;
            PlayerManager.instance.players.ForEach(p =>
            {
                if (p.GetAdditionalData().bankAccount.HasFunds(new Dictionary<string, int> { { SkillPoints, 1 } })) { SkillPointItemShop.Show(p); done = false; }
            });
            if (!done)
            {
                gameObject = new GameObject();
                gameObject.AddComponent<Canvas>().sortingLayerName = "MostFront";
                gameObject.AddComponent<TextMeshProUGUI>().text = "Waiting For Players to skill up";
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
                    if (!p.GetAdditionalData().bankAccount.HasFunds(new Dictionary<string, int> { { SkillPoints, 1 } }) && p.data.view.IsMine)
                    {
                        SkillPointItemShop.Hide();
                    }
                });
                if (time <= 0)
                {
                    ShopManager.instance.HideAllShops();
                    done = true;
                }

            }
            UnityEngine.Object.Destroy(gameObject);
            UnityEngine.Object.Destroy(timer);
        }
    }
}
