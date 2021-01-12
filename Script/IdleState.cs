using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State<EnemyController>
{
    public bool isPatrol = true;
    private float minIdleTime = 0.0f;
    private float maxIdleTime = 3.0f;
    private float idleTime = 0.0f;

    private Animator animator;
    private CharacterController controller;

    private int hashMove = Animator.StringToHash("Move");
    private int hashMoveSpeed = Animator.StringToHash("MoveSpeed");

    public override void OnInitialized()
    {
        animator = context.GetComponent<Animator>();
        controller = context.GetComponent<CharacterController>();
    }

    public override void OnEnter()
    {
        animator?.SetBool(hashMove, false);
        animator.SetFloat(hashMoveSpeed, 0);
        controller?.Move(Vector3.zero);

        if (isPatrol)
        {
            idleTime = Random.Range(minIdleTime, maxIdleTime);

        }
    }

    public override void Update(float deltaTime)
    {
        // 계속해서 적을 검색
        Transform enemy = context.SearchEnemy();
        if (enemy)
        {
            if (context.IsAvailableAttack)
            {
                stateMachine.ChangeState<AttackState>();
            } else
            {
                stateMachine.ChangeState<MoveState>();
            }
        }
        else if (isPatrol && stateMachine.ElapsedTimeInstate > idleTime)
        {
            stateMachine.ChangeState<MoveToWaypoint>();
        }
    }

    public override void OnExit()
    {
        
    }

}
