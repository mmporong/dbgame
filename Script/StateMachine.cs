using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class State<T>
{
    protected StateMachine<T> stateMachine;
    protected T context;

    public State()
    {

    }

    internal void SetMachineAndContext(StateMachine<T> stateMachine, T context)
    {
        this.stateMachine = stateMachine;
        this.context = context;

        OnInitialized();
    }

    public virtual void OnInitialized()
    {

    }

    public virtual void OnEnter()
    {

    }

    public abstract void Update(float deltaTime);

    public virtual void OnExit()
    {

    }
}
public sealed class StateMachine<T>
{
    private T context;

    // 현재 상태에 대한 정보
    private State<T> currentState;

    // 외부에서 할당 불가 리드온리 
    public State<T> CurrentState => currentState;

    private State<T> previousState;
    public State<T> PreviousState => previousState;

    // 변환된 상태에서 지난 시간 
    private float elapsedTimeInState = 0.0f;
    public float ElapsedTimeInState => elapsedTimeInState;

    // State를 초기화하며 StateMachine에 등록
    // T값은 State의 상태가 되고 Value는 State의 인스턴스가 됨
    private Dictionary<System.Type, State<T>> states = new Dictionary<System.Type, State<T>>();

    // 생성자 구현

    public StateMachine(T context, State<T> initialState)
    {
        this.context = context;

        // Setup our initial state
        AddState(initialState);
        currentState = initialState;
        currentState.OnEnter();
    }

    public void AddState(State<T> state)
    {
        state.SetMachineAndContext(this, context);
        states[state.GetType()] = state;
    }

    public void Update(float deltaTime)
    {
        elapsedTimeInState += deltaTime;

        currentState.Update(deltaTime);
    }

    // State 간 변경
    public R ChangeState<R>() where R : State<T>
    {
        var newType = typeof(R);
        if (currentState.GetType() == newType)
        {
            return currentState as R;
        }

        if (currentState != null)
        {
            currentState.OnExit();
        }

        // 현재 상태와 이전 상태 교체
        previousState = currentState;
        currentState = states[newType];
        currentState.OnEnter();
        // 전환된 상태의 시간 경과
        elapsedTimeInState = 0.0f;

        return currentState as R;
    }
}
