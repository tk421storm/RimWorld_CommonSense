﻿using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;
namespace CommonSense
{
    //[HarmonyPatch(typeof(JobDriver_Meditate), "<MakeNewToils>b__15_3")]
    [HarmonyPatch(typeof(JobDriver_Meditate), "MeditationTick")]
    public static class JobDriver_Meditate_MakeNewToils_b__15_3_CommonSensePatch
    {
        internal static void Postfix(JobDriver_Meditate __instance)
        {
            if (!Settings.meditation_economy)
                return;
            //
            bool meditating = __instance.pawn.GetTimeAssignment() == TimeAssignmentDefOf.Meditate;
            //
            var entropy = __instance.pawn.psychicEntropy;
            var joy = __instance.pawn.needs?.joy;
            var joyKind = __instance.pawn.CurJob.def.joyKind;
            if (!meditating
                && (joy?.CurLevel >= 0.98f || joyKind != null && joy?.tolerances?.BoredOf(joyKind) == true)
                && (!entropy.NeedsPsyfocus || entropy.CurrentPsyfocus == 1f))
            {
                __instance.EndJobWith(JobCondition.InterruptForced);
            }
        }
    }
}
