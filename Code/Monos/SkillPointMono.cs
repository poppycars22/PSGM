using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using PoppyScyyeGameModes.Gamemodes;

namespace PoppyScyyeGameModes.Monos
{
    public class SkillPointMono : MonoBehaviour
    {
        public void Start()
        {
            SkillPointShop.SkillPoints.Add("SkillPoints", 10);
        }
    }
}
