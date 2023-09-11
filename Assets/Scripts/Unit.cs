
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 targetPos;
    private float stopDistance = .1f;
    private float turnSpeed = 10f;
    [SerializeField] private Animator unitAnimator;
    private GridPosition gridPosition;


    private void Awake()
    {
        targetPos = transform.position;
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);   
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
        }
        else
        {
            unitAnimator.SetBool("isMoving", false);
        }
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }

    }

    public void Move(Vector3 targetPos)
    {
        if (targetPos != null) this.targetPos = targetPos;
    }

}
