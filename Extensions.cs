using System;
using System.Collections.Generic;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers;

namespace ReforgeTowers;

public static class Extensions
{
    public static ModReforge? GetCurrentReforge(this Tower? tower)
    {
        var mutator = tower?.GetMutatorById(nameof(ReforgeTowersMod));
        var supportMutator = mutator?.mutator.Cast<RateSupportModel.RateSupportMutator>();

        return supportMutator?.GetReforge();
    }

    public static bool HasReforge(this Tower tower) => tower.GetCurrentReforge() != null;

    public static bool HasReforge(this Tower tower, out ModReforge modReforge) =>
        (modReforge = tower.GetCurrentReforge()!) != null;

    public static ModReforge? GetReforge(this RateSupportModel.RateSupportMutator mutator)
    {
        var id = Convert.ToInt32(mutator.multiplier);

        return ModReforge.ReforgesById.GetValueOrDefault(id);
    }
}