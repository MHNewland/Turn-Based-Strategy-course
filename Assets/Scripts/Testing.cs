using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private int width;
    private int height;
    private Unit unit;

    private void Start()
    {
        width = LevelGrid.Instance.GetWidth();
        height = LevelGrid.Instance.GetHeight();
        unit = UnitActionSystem.Instance.getSelectedUnit();
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
    }

    private void Update()
    {
        GridSystemVisual.Instance.ShowGridPositionList(
            unit.GetMoveAction().GetValidActionGridPositionList());
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        GridSystemVisual.Instance.HideAllGridPositions();
        unit = UnitActionSystem.Instance.getSelectedUnit();
    }
}
