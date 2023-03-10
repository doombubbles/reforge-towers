using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.TowerSelectionMenu;

namespace ReforgeTowers;

[HarmonyPatch(typeof(RateSupport.RateSupportMutator), nameof(RateSupport.RateSupportMutator.Mutate))]
internal static class RateMutator_Mutate
{
    [HarmonyPrefix]
    private static bool Prefix(RateSupport.RateSupportMutator __instance, Model model, ref bool __result)
    {
        if (__instance.id != nameof(ReforgeTowersMod)) return true;

        var tower = model.Cast<TowerModel>();

        var reforge = __instance.GetReforge()!;

        reforge.Apply(tower);

        __result = true;
        return false;
    }
}

[HarmonyPatch]
internal static class TowerSelectionMenu_Show
{
    private static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(TowerSelectionMenu), nameof(TowerSelectionMenu.Show));
        yield return AccessTools.Method(typeof(TowerSelectionMenu), nameof(TowerSelectionMenu.UpdateTower));
    }

    [HarmonyPostfix]
    private static void Postfix(TowerSelectionMenu __instance)
    {
        var reforgePanel = __instance.GetComponentInChildren<ReforgePanel>();
        if (reforgePanel == null)
        {
            reforgePanel = ReforgePanel.Create(__instance);
        }

        reforgePanel.UpdateVisuals();
    }
}

[HarmonyPatch(typeof(TowerSelectionMenu), nameof(TowerSelectionMenu.CashChanged))]
internal static class TowerSelectionMenu_CashChanged
{
    [HarmonyPostfix]
    private static void Postfix(TowerSelectionMenu __instance)
    {
        var reforgePanel = __instance.GetComponentInChildren<ReforgePanel>();
        if (reforgePanel == null) return;

        reforgePanel.UpdateCost();
    }
}

[HarmonyPatch(typeof(Simulation), nameof(Simulation.GetSimulationBehaviorDiscount))]
internal static class Simulation_GetSimulationBehaviorDiscount
{
    [HarmonyPostfix]
    private static void Postfix(Tower tower, ref float __result)
    {
        if (!tower.HasReforge(out var reforge)) return;

        __result -= reforge.Cost(tower.towerModel);
    }
}