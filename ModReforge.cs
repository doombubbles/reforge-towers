using System;
using System.Collections.Generic;
using System.Linq;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace ReforgeTowers;

public abstract class ModReforge : ModContent
{
    private static int nextID;

    public static readonly Dictionary<int, ModReforge> ReforgesById = new();
    public static readonly Dictionary<string, ModReforge> ReforgesByName = new();

    public abstract TowerSet TowerSet { get; }

    public virtual int Damage => 0;

    public virtual int Pierce => 0;

    public virtual float AttackSpeed => 0f;

    public virtual float Range => 0f;

    public virtual float Cash => 0f;

    private IEnumerable<float> Effects => new[] { Damage, Pierce, AttackSpeed, Range, Cash };

    public float Weight =>
        Effects.All(effect => effect >= 0) ? ReforgeTowersMod.GoodReforgeWeight
        : Effects.All(effect => effect <= 0) ? ReforgeTowersMod.BadReforgeWeight
        : ReforgeTowersMod.MixedReforgeWeight;

    public int ReforgeId { get; private set; }

    public float Cost(TowerModel towerModel)
    {
        var total = 0f;
        if (DamageValid(towerModel)) total += Damage * .1f;
        if (PierceValid(towerModel)) total += Pierce * .05f;
        if (RangeValid(towerModel)) total += Range / 3f;
        if (AttackSpeedValid(towerModel)) total += AttackSpeed / 2f;
        if (CashValid(towerModel)) total += Cash;
        return total;
    }

    private static string Sign(float f) => f > 0 ? "+" : "";

    public string Description(Tower tower)
    {
        var towerModel = tower.towerModel;
        var effects = new List<string>();

        if (DamageValid(towerModel) && Damage != 0)
        {
            effects.Add($"{Sign(Damage)}{Damage} Damage");
        }

        if (PierceValid(towerModel) && Pierce != 0)
        {
            effects.Add($"{Sign(Pierce)}{Pierce} Pierce");
        }

        if (AttackSpeedValid(towerModel) && AttackSpeed != 0)
        {
            effects.Add($"{Sign(AttackSpeed)}{AttackSpeed:P0} Speed");
        }

        if (RangeValid(towerModel) && Range != 0)
        {
            effects.Add($"{Sign(Range)}{Range:P0} Range");
        }

        if (CashValid(towerModel) && Cash != 0)
        {
            effects.Add($"{Sign(Cash)}{Cash:P0} Cash");
        }

        var cost = Cost(towerModel);
        effects.Add(towerModel.IsHero() ? $"{Sign(-cost)}{-cost:P0} XP" : $"{Sign(cost)}{cost:P0} Cost");

        return effects.Join();
    }

    public override void Register()
    {
        ReforgeId = nextID++;
        ReforgesById[ReforgeId] = this;
        ReforgesByName[Name] = this;
    }

    public virtual bool CanApplyTo(Tower? tower) =>
        tower?.towerModel is TowerModel towerModel &&
        (towerModel.towerSet == TowerSet || TowerSet == TowerSet.None) &&
        ((Damage == 0 || DamageValid(towerModel)) &&
         (Pierce == 0 || PierceValid(towerModel)) &&
         (Range == 0 || RangeValid(towerModel)) &&
         (AttackSpeed == 0 || AttackSpeedValid(towerModel)) ||
         Cash > 0 && CashValid(towerModel));

    private static bool DamageValid(TowerModel towerModel) => towerModel.GetDescendants<DamageModel>().Any();

    private static bool PierceValid(TowerModel towerModel) =>
        towerModel.GetDescendants<ProjectileModel>().Any(p => p.pierce > 0 && p.pierce < 1e6);

    private static bool RangeValid(TowerModel towerModel) => true;

    private static bool AttackSpeedValid(TowerModel towerModel) => towerModel.GetDescendants<WeaponModel>()
        .Any(w => !w.HasBehavior<EmissionsPerRoundFilterModel>());

    private static bool CashValid(TowerModel towerModel) =>
        towerModel.GetDescendants<CashModel>().Any() || towerModel.GetDescendants<PerRoundCashBonusTowerModel>().Any();

    public void Apply(TowerModel tower)
    {
        if (Damage > 0)
        {
            foreach (var damageModel in tower.GetDescendants<DamageModel>().ToList().Where(d => d.damage > 0))
            {
                damageModel.damage = Math.Max(1, damageModel.damage + Damage);
            }
        }

        if (Pierce > 0)
        {
            foreach (var proj in tower.GetDescendants<ProjectileModel>().ToList().Where(p => p.pierce > 0))
            {
                proj.pierce = Math.Max(1, proj.pierce + Pierce);
            }
        }

        if (Range > 0)
        {
            tower.range *= 1 + Range;
            foreach (var attackModel in tower.GetAttackModels())
            {
                attackModel.range *= 1 + Range;
            }
        }

        if (AttackSpeed > 0)
        {
            foreach (var weaponModel in tower.GetAttackModels().SelectMany(attackModel =>
                         attackModel.weapons ?? new Il2CppReferenceArray<WeaponModel>(0)))
            {
                weaponModel.Rate /= 1 + AttackSpeed;
            }
        }

        if (Cash > 0)
        {
            tower.GetDescendants<CashModel>().ForEach(model =>
            {
                model.minimum *= 1 + Cash;
                model.maximum *= 1 + Cash;
            });

            tower.GetDescendants<PerRoundCashBonusTowerModel>().ForEach(model => model.cashPerRound *= 1 + Cash);
        }

        if (tower.HasBehavior(out HeroModel heroModel))
        {
            heroModel.xpScale /= 1 + Cost(tower);
        }
    }
}