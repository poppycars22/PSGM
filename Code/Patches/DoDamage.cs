using HarmonyLib;
using ModdingUtils.Extensions;
using PoppyScyyeGameModes.Extentions;
using PoppyScyyeGameModes.Gamemodes;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib;
using UnityEngine;

namespace PoppyScyyeGameModes.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(HealthHandler), "DoDamage")]
    internal class HealthHandlerPatchDoDamage
    {
        private static bool Prefix(HealthHandler __instance, ref Vector2 damage, ref Player damagingPlayer)
        {

            if (damagingPlayer != null && SkillPointGM.instance != null)
            {
                if (ModdingUtils.AIMinion.Extensions.CharacterDataExtension.GetAdditionalData(damagingPlayer.data).isAIMinion)
                    SkillPointGM.instance.lastPlayerDamage[((CharacterData)__instance.GetFieldValue("data")).player.playerID] = ModdingUtils.AIMinion.Extensions.CharacterDataExtension.GetAdditionalData(damagingPlayer.data).spawner.playerID;
                else
                    SkillPointGM.instance.lastPlayerDamage[((CharacterData)__instance.GetFieldValue("data")).player.playerID] = damagingPlayer.playerID;
            }
            return true;
        }
    }
}