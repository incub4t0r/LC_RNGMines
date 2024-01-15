using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using GameNetcodeStuff;
using UnityEngine;

namespace RNGMines.Patches
{
    internal class MinePatch
    {
        private static bool HasLineOfSight(Vector3 source, Vector3 target, LayerMask obstructionLayer, int distanceRange = 30)
        {
            Vector3 direction = target - source;
            float distance = direction.magnitude;

            if (distance > distanceRange)
            {
                return false;
            }

            Ray ray = new Ray(source, direction.normalized);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distance, obstructionLayer))
            {
                return false;
            }

            return true;
        }

        [HarmonyPatch(typeof(Landmine), "Update")]
        [HarmonyPostfix]
        private static void UpdatePatch(Landmine __instance)
        {
            PlayerControllerB player = GameNetworkManager.Instance.localPlayerController.GetComponentInChildren<PlayerControllerB>();

            if (player == null)
            {
                return;
            }

            if (__instance.hasExploded)
            { 
                return;
            }

            bool playerSeesMine = HasLineOfSight(player.transform.position, __instance.transform.position, StartOfRound.Instance.collidersAndRoomMaskAndDefault, 15);

            if (playerSeesMine)
            {
                int rnjesus = UnityEngine.Random.Range(0, 10000);
                if (rnjesus == 666)
                {
                    RNGMineBase.GetLogger().LogInfo("RNJesus has forsaken you, triggering mine");
                    __instance.GetType().GetMethod("TriggerMineOnLocalClientByExiting", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(__instance, null);
                }
            }
        }
    }
}
