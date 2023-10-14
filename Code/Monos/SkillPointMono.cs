using PoppyScyyeGameModes.Gamemodes;
using UnityEngine;
using Photon.Pun.Simple;
using Photon.Realtime;
using System.Collections.Generic;
using ItemShops.Extensions;
using ItemShops.Utils;
using RWF.GameModes;
using PoppyScyyeGameModes.Cards;
using System.Collections;
using TMPro;
using ModdingUtils.GameModes;
using System;

namespace PoppyScyyeGameModes.Monos
{
    public class SkillPointMono : MonoBehaviour
    {

        public void Awake()
        {
            UnityEngine.Debug.Log("1");
            Player player = this.GetComponentInParent<Player>();
            UnityEngine.Debug.Log("2");
            player.GetAdditionalData().bankAccount.Deposit(new Dictionary<string, int> { { "Skill Points", 10 } });
            UnityEngine.Debug.Log("3");
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

        public static Dictionary<Player, int> PlayerSkillPoints = new Dictionary<Player, int>();

        internal static IEnumerator SkillUp()
        {
            UnityEngine.Debug.Log("1.1");
            Skill_Points = new Dictionary<string, int>();
            UnityEngine.Debug.Log("2.1");
            //Skill_Points.Add("Skill Points", 1);
            if (SkillPointItemShop != null)
            {
                UnityEngine.Debug.Log("3.1");
                ShopManager.instance.RemoveShop(SkillPointItemShop);
                UnityEngine.Debug.Log("4.1");
            }
            UnityEngine.Debug.Log("5.1");
            SkillPointItemShop = ShopManager.instance.CreateShop(ShopID);
            UnityEngine.Debug.Log("6.1");
            SkillPointItemShop.UpdateMoneyColumnName("Skill Points");
            UnityEngine.Debug.Log("7.1");
            SkillPointItemShop.UpdateTitle("Test Title For Now");
            UnityEngine.Debug.Log("8.1");
            Main.instance.StartCoroutine(SetUpShop());
            UnityEngine.Debug.Log("9.1");
            yield break;
        }

        internal static IEnumerator SetUpShop()
        {
            UnityEngine.Debug.Log("1.2");
            Dictionary<string, int> map = new Dictionary<string, int>();
            UnityEngine.Debug.Log("2.2");
            List<PurchasableCard> cards = new List<PurchasableCard>();
            UnityEngine.Debug.Log("3.2");
            foreach (var c in SkillPointCard.Cards)
            {
                UnityEngine.Debug.Log("4.2");
                map.Add("Skill Points", c.GetCost());
                UnityEngine.Debug.Log("5.2");
                cards.Add(new PurchasableCard(c.cardInfo, map, new[] { new Tag("Skill Stat") }));
                UnityEngine.Debug.Log("6.2");
                map.Remove("Skill Points");
                UnityEngine.Debug.Log("7.2");
            }
            UnityEngine.Debug.Log("8.2");
            SkillPointItemShop.AddItems(cards.ToArray());
            UnityEngine.Debug.Log("9.2");
            yield break;
        }

        internal static IEnumerator WaitTillShopDone()
        {
            UnityEngine.Debug.Log("1.3");
            bool done = true;
            UnityEngine.Debug.Log("2.3");
            GameObject gameObject = new GameObject();
            UnityEngine.Debug.Log("3.3");
            GameObject timer = new GameObject();
            UnityEngine.Debug.Log("4.3");
            float time = 120;
            UnityEngine.Debug.Log("5.3");
            PlayerManager.instance.players.ForEach(p => {
                UnityEngine.Debug.Log("6.3");
                if (p.GetAdditionalData().bankAccount.HasFunds(new Dictionary<string, int> { { SkillPoints, 1 } })) { SkillPointItemShop.Show(p); done = false; }
            });
            UnityEngine.Debug.Log("7.3");
            if (!done)
            {
                UnityEngine.Debug.Log("8.3");
                gameObject = new GameObject();
                gameObject.AddComponent<Canvas>().sortingLayerName = "MostFront";
                gameObject.AddComponent<TextMeshProUGUI>().text = "Waiting For Players to skill up";
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
                UnityEngine.Debug.Log("9.3");
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
