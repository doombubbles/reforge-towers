using System;
using System.Linq;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Profile;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.Utils;
using Il2CppAssets.Scripts.Utils;
using MelonLoader;
using ReforgeTowers;
[assembly: MelonInfo(typeof(ReforgeTowersMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace ReforgeTowers;

public class ReforgeTowersMod : BloonsTD6Mod
{
    public static readonly ModSettingDouble BadReforgeWeight = new(1)
    {
        slider = true,
        min = 0,
        max = 1,
        stepSize = .01f,
        icon = VanillaSprites.PanicMonkeyEmoteIcon,
        description =
            "Randomized weight that bad reforges (ones with only negative effects) should be given. 0 will disable."
    };

    public static readonly ModSettingDouble MixedReforgeWeight = new(1)
    {
        slider = true,
        min = 0,
        max = 1,
        stepSize = .01f,
        icon = VanillaSprites.ThinkingMonkeyIcon,
        description =
            "Randomized weight that bad reforges (ones with positive and negative effects) should be given. 0 will disable."
    };

    public static readonly ModSettingDouble GoodReforgeWeight = new(1)
    {
        slider = true,
        min = 0,
        max = 1,
        stepSize = .01f,
        icon = VanillaSprites.FistpumpMonkeyEmoteIcon,
        description =
            "Randomized weight that good reforges (ones with only positive benefits) should be given. 0 will disable."
    };

    public static readonly ModSettingDouble ReforgeCost = new(2)
    {
        slider = true,
        min = 0,
        max = 10,
        stepSize = .1f,
        sliderSuffix = "%",
        icon = VanillaSprites.MoneyBag,
        description = "Cost for reforging, as a percentage of the Tower's overall worth."
    };

    public static int GetReforgeCost(Tower tower) => 5 * (int) Math.Round(ReforgeCost * tower.worth / 500f);

    public static void SetTowerReforge(Tower tower, ModReforge reforge)
    {
        var icon = ModContent.GetInstance<HammerBuffIcon>();

        tower.RemoveMutatorsById(nameof(ReforgeTowersMod));
        tower.AddMutator(new RateSupportModel.RateSupportMutator(true, nameof(ReforgeTowersMod),
            reforge.ReforgeId, 0, new BuffIndicatorModel("BuffIndicatorModel", icon.BuffLocsName, icon.BuffIconName)));
    }

    public static void RandomlyReforge(Tower tower)
    {
        var currentReforge = tower.GetCurrentReforge();
        var weightedGroup = new WeightedGroup<string>(); // can't just use ModReforge b/c il2cpp
        foreach (var modReforge in ModContent.GetContent<ModReforge>()
                     .Where(modReforge => modReforge != currentReforge && modReforge.CanApplyTo(tower)))
        {
            weightedGroup.Add(modReforge.Name, modReforge.Weight);
        }

        var reforge = ModReforge.ReforgesByName[weightedGroup.GetRandom()];

        SetTowerReforge(tower, reforge);

        InGame.Bridge.Simulation.CreateTextEffect(tower.Position, new PrefabReference
        {
            guidRef = "Assets/Monkeys/General/Graphics/Effects/CashUp.prefab"
        }, 1, reforge.Name, true);

        ModContent.GetAudioClip<ReforgeTowersMod>("ReforgeSound").Play();
    }

    public override void OnTowerSaved(Tower tower, TowerSaveDataModel saveData)
    {
        if (tower.GetCurrentReforge() is not ModReforge reforge) return;

        saveData.metaData[nameof(ReforgeTowersMod)] = reforge.Name;
    }

    public override void OnTowerLoaded(Tower tower, TowerSaveDataModel saveData)
    {
        if (!saveData.metaData.ContainsKey(nameof(ReforgeTowersMod))) return;

        var reforgeName = saveData.metaData[nameof(ReforgeTowersMod)];

        if (!ModReforge.ReforgesByName.TryGetValue(reforgeName, out var reforge)) return;

        SetTowerReforge(tower, reforge);
    }

    public class HammerBuffIcon : ModBuffIcon
    {
        public override string Icon => "Hammer";
    }
}