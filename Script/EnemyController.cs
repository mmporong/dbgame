using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IAttackable, IDamageable
{
    #region Variables
    protected StateMachine<EnemyController> stateMachine;
    public StateMachine<EnemyController> StateMachine => stateMachine;
    private FieldOfView fieldOfView;
    private Animator animator;


    public Transform projectileTransform;
    public Transform hitTransform;

    public int maxHealth = 100;



    [SerializeField]
    private List<AttackBehaviour> attackBehaviours = new List<AttackBehaviour>();


    // FieldOfView 에서 처리
    public LayerMask TargetMask => fieldOfView.targetMask;
    public Transform target => fieldOfView.NearestTarget;

    // public float viewRadius;
    public float attackRange;
    public Transform Target => fieldOfView?.NearestTarget;

    public Transform[] waypoints;
    [HideInInspector]
    public Transform targetWaypoint = null;
    private int waypointIndex = 0;



    #endregion Variables

    #region Properties
    public int health
    {
        get;
        private set;
    }
    #endregion Properties


    #region Unity Methods


    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine<EnemyController>(this, new MoveToWaypoints());
        IdleState idleState = new IdleState();
        idleState.isPatrol = true;
        stateMachine.AddState(idleState);
        stateMachine.AddState(new MoveState());
        stateMachine.AddState(new AttackState());
        stateMachine.AddState(new DeadState());
        InitAttackBehaviour();

        health = maxHealth;

        fieldOfView = GetComponent<FieldOfView>();
        

    }



    private void Update()
    {
        CheckAttackBehaviour();
        float elapsedtime = Time.deltaTime * 0.1f;
        stateMachine.Update(Time.deltaTime);
    }
    #endregion Unity Methods

    #region Other Methods

    public virtual bool IsAvailableAttack => false;

    //public bool IsAvailableAttack
    //{
    //    get
    //    {
    //        if (!Target)
    //        {
    //            return false;
    //        }

    //        float distance = Vector3.Distance(transform.position, Target.position);
    //        return (distance <= attackRange);
    //    }
    //}

    public Transform SearchEnemy()
    {

        return Target;

        //
        // FieldOfView에서 처리

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

    //    Gizmos.color   = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}
    public Transform FindNextWaypoint()
    {
        targetWaypoint = null;
        if (waypoints.Length > 0)
        {
            targetWaypoint = waypoints[waypointIndex];
        }

        waypointIndex = (waypointIndex + 1) % waypoints.Length;
        return targetWaypoint;
        #endregion
    }

    private void InitAttackBehaviour()
    {
        foreach (AttackBehaviour behaviour in attackBehaviours)
        {
            if (CurrentAttackBehaviour == null)
            {
                CurrentAttackBehaviour = behaviour;
            }

            behaviour.targetMask = TargetMask;

        }
    }

    private void CheckAttackBehaviour()
    {
        if (CurrentAttackBehaviour == null || !CurrentAttackBehaviour.IsAlive)
        {
            CurrentAttackBehaviour = null;

            foreach (AttackBehaviour behaviour in attackBehaviours)
            {
                if (behaviour.IsAlive)
                {
                    if ((CurrentAttackBehaviour == null) || (CurrentAttackBehaviour.priority < behaviour.priority))
                    {
                        CurrentAttackBehaviour = behaviour;

                    }
                }
            }
        }
    }

    #region IAttackable interfaces
    public AttackBehaviour CurrentAttackBehaviour
    {
        get;
        private set;
    }

    public void OnExecuteAttack(int attackIndex)
    {
        if (CurrentAttackBehaviour != null && Target != null)
        {
            CurrentAttackBehaviour.ExecuteAttack(Target.gameObject, projectilePoint);
        }
    }
    #endregion IAttackable interfaces

    #region IDamagable interfaces     
    bool IsAlive => health > 0;
    

    void TakeDamage(int damage, GameObject hitEffectPrefab) {
        if (!IsAlive)
        {
            return;
        }

        health -= damage;

        if (hitEffectPrefab)
        {
            Instantiate(hitEffectPrefab, hitTransform);
        }

        if (IsAlive)
        {
            animator?.SetTrigger(hitTriggerHash);
        }
        else
        {
            stateMachine.ChangeState<DeadState>;
        }
    }
    
    #endregion IDamagable interfaces

}