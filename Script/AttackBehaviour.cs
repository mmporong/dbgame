using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{

    #region Variables

#if UNITY_EDITOR
    [Multiline]
    public string developmentDescription = "";
#endif // UNITY_EDITOR

    public int animationIndex;

    public int priority;

    public int damage;
    public float range = 3f;

    [SerializeField]
    protected float coolTime;

    protected float calcCoolTime = 0.0f;

    public GameObject effectPrefab;

    [HideInInspector]
    public LayerMask targetMask;

    public bool IsAvailableAttack { get; internal set; }

    #endregion Variables
    // Start is called before the first frame update
    void Start()
    {
        calcCoolTime = coolTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (calcCoolTime < coolTime)
        {
            calcCoolTime += Time.deltaTime;
        }
    }

    public abstract void ExecuteAttack(GameObject target = null, Transform startPoint = null);

        
}

