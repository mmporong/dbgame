using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateController : MonoBehaviour
{
    public delegate void OnEnterAttackState();
    public delegate void OnExitAttackState();

    public OnEnterAttackState enterAttackStateHandler;
    public OnExitAttackState exitAttackStateHandler;

    public bool IsInAttackState
    {
        get;
        private set;
    }

    void Start()
    {
        enterAttackStateHandler = new OnEnterAttackState(EnterAttackState);
        exitAttackStateHandler = new OnExitAttackState(ExitAttackState);
    }
    
    #region Helper Methods

    public void OnStateOfAttackState()
    {
        IsInAttackState = true;
        enterAttackHandler();
    }
    public void OnEndOfAttackState()
    {
        IsInAttackState = false;
        exitAttackHandler();
    }
    private void EnterAttackState()
    {
     
    }

    private void ExitAttackState()
    {

    }
   
    #endregion Helper Methods
}

