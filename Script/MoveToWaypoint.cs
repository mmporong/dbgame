using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToWaypoints : State<EnemyController>
{
    private Animator animator;
    private CharacterController controller;
    private NavMeshAgent agent;

    private int hashMove = Animator.StringToHash("Move");
    private int hashMoveSpeed = Animator.StringToHash("MoveSpeed");

    public override void OnInitialized()
    {
        animator = context.GetComponent<Animator>();
        controller = context.GetComponent<CharacterController>();
        agent = context.GetComponent<NavMeshAgent>();
    }

    public override void OnEnter()
    {
        animator?.SetBool(hashMove, false);
        animator.SetFloat(hashMoveSpeed, 0);
        controller?.Move(Vector3.zero);
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
    }

    public override void OnExit()
    {
        
    }

}
