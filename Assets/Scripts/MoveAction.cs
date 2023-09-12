using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{

    private Vector3 targetPos;
    private float stopDistance = .1f;
    private float turnSpeed = 10f;
    private bool isMoving;
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private int maxMoveDistance = 4;
    private Unit unit;

    private void Awake()
    {
        targetPos = transform.position;
        unit = GetComponent<Unit>();
    }

    private void Update()
    {
        float moveSpeed = 4;
        if (Vector3.Distance(transform.position, targetPos) > stopDistance)
        {
            unitAnimator.SetBool("isMoving", true);
            Vector3 moveDirection = (targetPos - transform.position).normalized;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * turnSpeed);
            GridSystemVisual.Instance.UpdateGridVisual();
        }
        else
        {
            unitAnimator.SetBool("isMoving", false);
        }
    }

    public void Move(GridPosition gridPosition)
    {
        targetPos = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);

    }

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        for(int x = -maxMoveDistance; x<=maxMoveDistance; x++) {
            for(int z = -maxMoveDistance; z<=maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                //if it's a valid position and not the unit's current position, add it to valid moves
                if (LevelGrid.Instance.IsValidGridPosition(testGridPosition) && 
                    !(unitGridPosition == testGridPosition) &&
                    !(LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)))
                {
                    validGridPositionList.Add(testGridPosition);
                }
            }
        }

        return validGridPositionList;
    }

    

}
