using UnityEngine;

public class WanderingState : BaseState
{
    public WanderingState(MonsterAI monsterAI) : base(monsterAI)
    {
    }
    
    public override BaseState GetNextState()
    {
        if (MonsterAI.playersInSightRange.Count > 0) {
            return new ChasingState(MonsterAI);
        }
        return null;
    }

    public override void Enter()
    {
        SetNextWaypoint();
    }

    public override void Execute()
    {
        MoveToWaypoint();
    }

    public override void Exit()
    {
    }

    private void SetNextWaypoint()
    {
        Waypoints.CurrentIndex = (Waypoints.CurrentIndex + 1) % Waypoints.List.Count; 
    }

    private void MoveToWaypoint()
    {
        if (Vector3.Distance(MonsterAI.transform.position, Waypoints.List[Waypoints.CurrentIndex].position) <= 1f)
        {
            SetNextWaypoint(); 
        }
        NavMeshAgent.SetDestination(Waypoints.List[Waypoints.CurrentIndex].position);
    }
}