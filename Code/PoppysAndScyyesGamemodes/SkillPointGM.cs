using ModdingUtils.Extensions;
using Photon.Pun;
using Photon.Pun.Simple;
using RWF.GameModes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnboundLib;
using UnboundLib.GameModes;
using UnboundLib.Networking;
using PSGM.Monos;

namespace PSGM.Gamemodes
{
    public class GM_SkillPoint : RWFGameMode
    {
        

        public override IEnumerator DoStartGame()
        {
            foreach (Player player in PlayerManager.instance.players)
            {
               player.gameObject.GetOrAddComponent<SkillPointHandler>();
            }

            yield break;
        }
    }
}