public class AttackingState : BaseState
{
    public AttackingState(MonsterAI monsterAI) : base(monsterAI) { }

    public override BaseState GetNextState()
    {
        if (MonsterAI.nearestTarget != null && !MonsterAI.targetInRange)
        {
            return new ChasingState(MonsterAI);
        }
        if (MonsterAI.nearestTarget == null) {
            return new WanderingState(MonsterAI);
        }

        return null;
    }

    public override void Execute()
    {
        
    }
    public override void Enter() {
        // Je me balade, lalala
    }
    public override void Exit() {
        // Je me balade, lalala
    }
}