using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_New : MonoBehaviour
{
    #region Variables
    protected StateMachine<EnemyController_New> stateMachine;

    #endregion Variables

    #region Unity Methods


    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine<EnemyController_New>(this, new IdleState());
        stateMachine.AddState(new MoveState());
        stateMachine.AddState(new AttackState());
    }

    private void Update()
    {
        float elapsedTime = Time.deltaTime * 0.1f;
        stateMachine.Update(Time.deltaTime);
    }
    #endregion Unity Methods
}
