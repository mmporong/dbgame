﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Variables
    protected StateMachine<EnemyController> stateMachine;
    public StateMachine<EnemyController> StateMachine => stateMachine;

    private FieldOfView fov;

    // FieldOfView 에서 처리
    // public LayerMask targetMask;
    // public Transform target;
    // public float viewRadius;
    public float attackRange;
    public Transform Target => fov?.NearestTarget;

    #endregion Variables

    #region Unity Methods


    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine<EnemyController>(this, new IdleState());
        stateMachine.AddState(new MoveState());
        stateMachine.AddState(new AttackState());

        fov = GetComponent<FieldOfView>();
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
            if (!Target)
            {
                return false;
            }

            float distance = Vector3.Distance(transform.position, Target.position);
            return (distance <= attackRange);
        }
    }

    public Transform SearchEnemy()
    {

        return Target;
        
        // FieldOfView 에서 처리
        //target = null;

        //Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        //if (targetInViewRadius.Length > 0)
        //{
        //    target = targetInViewRadius[0].transform;
        //}

        //return target;
    }

    //private void OndrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, viewRadius);

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}

    #endregion
}
