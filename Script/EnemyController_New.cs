using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_New : MonoBehaviour
{
    #region Variables
    protected StateMachine<EnemyController_New> stateMachine;

    public LayerMask targetMask;
    public Transform target;
    public float viewRadius;
    public float attackRange;

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

    #region Other Methods
    public bool IsAvailableAttack
    {
        get
        {
            if (!target)
            {
                return false;
            }

            float distance = Vector3.Distance(transform.position, target.position);
            return (distance <= attackRange);
        }
    }

    public Transform SearchEnemy()
    {
        target = null;

        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        if (targetInViewRadius.Length > 0)
        {
            target = targetInViewRadius[0].transform;
        }

        return target;
    }

    private void OndrawGizmos()
    {
        OndrawGizmos.color = Color.green;
        OndrawGizmos.DrawWireSphere(transform.position, viewRadius);

        OndrawGizmos.color = Color.red;
        OndrawGizmos.DrawWireSphere(transform.position, attackrRange);
    }

    #endregion
}
