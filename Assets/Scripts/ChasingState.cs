using UnityEngine;

public class ChasingState : BaseState {
    
    public ChasingState(MonsterAI monsterAI) : base(monsterAI) { }

    public override BaseState GetNextState() {
        if (MonsterAI.nearestTarget == null) {
            return new WanderingState(MonsterAI);
        }
        return null;
    }

    public override void Execute() {
        NavMeshAgent.SetDestination(MonsterAI.nearestTarget.transform.position);
    }

    public override void Enter() {
        // Je me balade, lalala
    }
    public override void Exit()
    {
        MonsterAI.rallyNearestWaypoint = true;
    }

    public override void StateOnTriggerEnter(Collider other)
    {
        return;
    }
    
    public override void StateOnTriggerExit(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
            MonsterAI.nearestTarget = null;
        }
    }
}