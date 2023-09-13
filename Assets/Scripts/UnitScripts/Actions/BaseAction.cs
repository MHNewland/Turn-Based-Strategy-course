using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction :MonoBehaviour
{
    protected Unit unit;
    protected bool isActive;
    protected Action onActionComplete;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    protected void StartAction(Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;
    }

    protected virtual void EndAction()
    {
        try
        {
            onActionComplete();
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
        finally
        {
            isActive = false;
        }
    }


}
