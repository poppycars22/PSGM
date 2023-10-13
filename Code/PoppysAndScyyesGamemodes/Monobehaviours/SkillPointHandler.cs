using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnboundLib.Cards;
using UnboundLib;
using PSGM.Monos;
using PSGM.Extensions;

namespace PSGM.Monos
{
    public class SkillPointHandler : MonoBehaviour
    {
        
       
        // Start is called before the first frame update
        public void Start()
        {
            SkillPointShop.SkillPoints.Add("SkillPoints", 10);
            
        }

        // Update is called once per frame
        void Update()
        {
            //UnityEngine.Debug.Log($"[{SkillPointShop.SkillPoints}");
        }
        void GameStart()
        {


        }
       void PickEnd()
        {

        
        
        
        }
        /*void OnPointStart()
        {
            if (characterStats.GetAdditionalData().damageSkillPoints > 0)
            {
                gun.damage *= (characterStats.GetAdditionalData().damageSkillPoints * 1.5f);
            }
        }*/
        public Player player;
        public Gun gun;
        public CharacterStatModifiers characterStats;
    }

}
