using UnityEngine.AI;

public abstract class BaseState {

    protected MonsterAI MonsterAI;
    protected NavMeshAgent NavMeshAgent;
    
    protected BaseState(MonsterAI monsterAI) {
        MonsterAI = monsterAI;
        NavMeshAgent = monsterAI.GetComponent<NavMeshAgent>();
    }

    public abstract BaseState GetNextState();
    public abstract void Execute();
    public abstract void Enter();
    public abstract void Exit();
}