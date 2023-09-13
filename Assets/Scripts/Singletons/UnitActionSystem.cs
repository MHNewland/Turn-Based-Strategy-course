using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    
    public static UnitActionSystem Instance { get; private set; }

    [SerializeField] private LayerMask unitLayer;
    [SerializeField] private Unit selectedUnit;

    private bool isBusy;

    public event EventHandler OnSelectedUnitChanged;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one UnitActionSystem" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (isBusy) return;

        if (Input.GetMouseButtonDown(0))
        {
            SetBusy();
            if (!TryUnitSelection())
            {
                MoveAction unitMoveAction = selectedUnit.GetMoveAction();
                GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

                if (unitMoveAction.IsValidActionGridPosition(gridPosition))
                {
                    selectedUnit.GetMoveAction().Move(gridPosition, ClearBusy);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            SetBusy();
            selectedUnit.GetSpinAction().Spin(ClearBusy);
        }
    }

    private bool TryUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayer)){
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit)){
                SetSelectedUnit(unit);
                return true;

            }
        }
        return false;
        
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        ClearBusy();
    }

    public void SetBusy()
    {
        isBusy = true;
    }

    public void ClearBusy()
    {
        isBusy = false;
    }

    public Unit getSelectedUnit() => selectedUnit;
   
         
}
