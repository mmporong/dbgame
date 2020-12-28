using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveState : Statae_New<EnemyController_New>
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
        agent?.SetDestination(context.target.position);
        animator?.SetBool(hashMove, true);
    }

    public override void Update(float deltaTime)
    {
        Transform enemy = context.SearchEnemy();
        if (enemy)
        {
            agent.SetDestination(context.target.position);

            if(agent.remainingDistance > agent.stoppingDistance)
            {
                controller.Move(agent.velocity * deltaTime);
                animator.SetFloat(hashMoveSpeed, agent.velocity.magnitude / agent.speed, 1f, deltaTime);
                return;

            }
        }

        if (!enemy && agent.remainingDistance <= agent.stoppingDistance)
        {
            stateMachine.ChangeState<IdleState_New>();
        }
    }

    public override void OnExit()
    {
        animator?.SetBool(hashMove, false);
        animator?.SetBool(hashMoveSpeed, 0f);
        agent.ResetPath();
    }

}
