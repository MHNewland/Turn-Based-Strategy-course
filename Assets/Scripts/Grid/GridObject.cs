using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridObject 
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    public List<Unit> unitList = new List<Unit>();

    public GridObject(GridSystem gs, GridPosition gp)
    {
        this.gridSystem = gs;
        this.gridPosition = gp;
       
    }

    public override string ToString()
    {
        string unitString = "";
        foreach (Unit unit in unitList)
        {
            unitString += "\n" + unit.ToString();
        }
        return gridPosition.ToString() + unitString;
    }

    public List<Unit> GetUnits()
    {
        return unitList;
    }

    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    public void RemoveUnit(int index)
    {
        unitList.RemoveAt(index);
    }
}
