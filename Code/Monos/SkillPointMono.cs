using PoppyScyyeGameModes.Gamemodes;
using UnityEngine;
using Photon.Pun.Simple;
using Photon.Realtime;
using System.Collections.Generic;
using ItemShops.Extensions;
using ItemShops.Utils;
using RWF.GameModes;

namespace PoppyScyyeGameModes.Monos
{
    public class SkillPointMono : MonoBehaviour
    {
        Player player;

        public void Start()
        {
            this.player = this.GetComponentInParent<Player>();
           player.GetAdditionalData().bankAccount.Deposit(new Dictionary<string, int> { { "Skill Points", 10 } });
        }
    }
}
