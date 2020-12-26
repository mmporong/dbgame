using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State<EnemyController_New>
{
    private Animator animator;
    private CharacterController controller;

    protected int hasMove = Animator.StringToHash("Move");
    protected int hasMoveSpeed = Animator.StringToHash("MoveSpeed");

    public override void OnInitialized()
    {
        animator = context.GetComponent<Animator>();
        controller = context.GetComponent<CharacterController>;
    }

    public override void OnEnter()
    {
        animator?.SetBool(hashMove, false);
        animator?.SetFloat(hashMoveSpeed, 0);
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
