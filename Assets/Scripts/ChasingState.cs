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
    public override void Exit() {
        // Je me balade, lalala
    }
}