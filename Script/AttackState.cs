using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State_New<EnemyController_New>
{
    public class AttackState_New : AttackState_New<EnemyController_New>
    {
        private Animator animator;

        private int hashAttack = Animator.StringToHash("Attack");

        public override void OnInitialized()
        {
            animator = context.GetComponent<animator>();
        }

        public override void OnEnter()
        {
            if (context.IsAvailableAttack)
            {
                animator?.SetTrigger(hashAttack);
            } 
            else
            {
                statMachine.ChangeState<IdleState_New>();

            }
        }

        public override void Update(float deltaTime)
        {

        }
    }
}
