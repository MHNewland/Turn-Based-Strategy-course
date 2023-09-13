using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{

    private float spinSpeed = 360;
    private float totalSpinAmount = 360;

    //public delegate void SpinCompleteDelegate();


    private void Update()
    {
        if (!isActive) return;

        float spinAddAmount = spinSpeed * Time.deltaTime;
        transform.eulerAngles += new Vector3(0,spinAddAmount,0);
        totalSpinAmount += spinAddAmount;

        if (totalSpinAmount >= 360f)
        {
            isActive = false;
            base.EndAction();
        }
        
    }

    public void  Spin(Action onActionComplete)
    {
        base.StartAction(onActionComplete);
        totalSpinAmount = 0f;

    }
}
