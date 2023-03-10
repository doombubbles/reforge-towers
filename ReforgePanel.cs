using System;
using System.Linq;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Components;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2Cpp;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.TowerSelectionMenu;
using Il2CppNinjaKiwi.Common;
using Il2CppTMPro;
using MelonLoader;
using UnityEngine;

namespace ReforgeTowers;

[RegisterTypeInIl2Cpp(false)]
public class ReforgePanel : MonoBehaviour
{
    public TowerSelectionMenu menu = null!;

    public ModHelperButton button = null!;

    public ModHelperText cost = null!;

    public ModHelperText description = null!;

    public ReforgePanel(IntPtr ptr) : base(ptr)
    {
    }

    public void OnReforgeClicked()
    {
        var tower = menu.selectedTower.tower;

        var reforgeCost = ReforgeTowersMod.GetReforgeCost(tower);
        if (!(InGame.instance.GetCash() >= reforgeCost)) return;
        InGame.instance.AddCash(-reforgeCost);

        ReforgeTowersMod.RandomlyReforge(tower);

        UpdateVisuals();
        foreach (var upgradeButton in menu.upgradeButtons.Where(o => o.isActiveAndEnabled))
        {
            upgradeButton.UpdateVisuals(upgradeButton.path, false);
        }
    }

    public void UpdateVisuals()
    {
        var tower = menu.selectedTower?.tower;
        if (tower == null) return;


        if (tower.GetCurrentReforge() is ModReforge reforge)
        {
            description.SetText(reforge.Description(tower));

            var nameText = menu.GetComponentFromChildrenByName<NK_TextMeshProUGUI>("TowerNameText");
            var text = LocalizationManager.Instance.GetText(tower.towerModel.baseId);
            if (menu.GetComponentFromChildrenByName<RectTransform>("TSMNamedMonkeysInput").Exists(out var result) &&
                result.gameObject.active)
            {
                nameText = result.GetComponentInChildren<NK_TextMeshProUGUI>();
                nameText.enableAutoSizing = true;
                text = InGame.Bridge.GetNamedMonkeyName(InGame.Bridge.MyPlayerNumber, tower.namedMonkeyKey);
            }

            if (nameText == null)
            {
                TaskScheduler.ScheduleTask(UpdateVisuals);
            }
            else
            {
                nameText.SetText(reforge.Name + " " + text);
            }

        }
        else
        {
            description.SetText("Not Yet Reforged");
        }

        UpdateCost();
    }

    public void UpdateCost()
    {
        var tower = menu.selectedTower?.tower;
        if (tower == null) return;

        var reforgeCost = ReforgeTowersMod.GetReforgeCost(tower);
        cost.SetText($"${reforgeCost:N0}");
        var canAfford = InGame.instance.GetCash() >= reforgeCost;
        cost.Text.color = canAfford ? Color.white : Color.red;
        button.Button.interactable = canAfford;
    }

    public static ReforgePanel Create(TowerSelectionMenu menu)
    {
        var panel = menu.scalar.gameObject.AddModHelperPanel(new Info("ReforgePanel", 0, -1240, 950, 250),
            VanillaSprites.BrownInsertPanel);
        var reforgePanel = panel.AddComponent<ReforgePanel>();
        reforgePanel.menu = menu;

        var inset = panel.AddPanel(new Info("InnerPanel")
        {
            AnchorMin = new Vector2(0, 0), AnchorMax = new Vector2(1, 1), Size = -50
        }, VanillaSprites.BrownInsertPanelDark, RectTransform.Axis.Horizontal, 25);

        reforgePanel.button = inset.AddButton(new Info("ReforgeButton", 225),
            VanillaSprites.GreenBtn, new Action(() => reforgePanel.OnReforgeClicked()));
        reforgePanel.button.AddImage(
            new Info("Image") { AnchorMin = new Vector2(0, 0), AnchorMax = new Vector2(1, 1), Size = -50 },
            ModContent.GetTextureGUID<ReforgeTowersMod>("Hammer"));

        reforgePanel.cost = inset.AddText(new Info("ReforgeCost", 200, 200),
            "$0", 80, TextAlignmentOptions.Left);
        reforgePanel.cost.Text.enableAutoSizing = true;

        reforgePanel.description = inset.AddText(new Info("ReforgeDescription", 375, 250), "Description", 42f);

        return reforgePanel;
    }
}