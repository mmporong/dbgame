using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State<EnemyController>
{
    private Animator animator;
    private AttackStateController attackStateController;
    private IAttackable attackable;

    protected int attackTriggerHash = Animator.StringToHash("AttackTrigger");
    protected int attackIndexHash = Animator.StringToHash("AttackIndex");

    public override void OnInitialized()
    {
        animator = context.GetComponent<Animator>();
        attackStateController = context.GetComponent<AttackStateController>();
        attackable = context.GetComponent<IAttackable>();
    }

    public override void OnEnter()
    {
        if(attackable == null || attackable.CurrentAttackBehaviour == null)
        {
            stateMachine.ChangeState<IdleState>();
            return;
        }

        attackStateController.enterAttackStateHandler += OnEnterAttackState();
        attackStateController.exitAttackStateHandler += OnExitAttackState();

        animator?.SetInteger(attackIndexHash, attackable.CurrentAttackBehaviour.animationIndex);
        animator?.SetTrigger(attackTriggerHash);

        if (context.IsAvailableAttack)
        {
            animator?.SetTrigger(attackTriggerHash);
        }
        else
        {
            stateMachine.ChangeState<IdleState>();

        }
    }

    public void OnEnterAttackState()
    {

    }
    public void OnExitAttackState()
    {
        stateMachine.ChangeState<IdleState>();

    }
}

