using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class WanderingState : BaseState
{
    public WanderingState(MonsterAI monsterAI) : base(monsterAI)
    {
    }
    
    private Transform _turretTransform;

    public override BaseState GetNextState()
    {
        if (MonsterAI.nearestTarget != null)
        {
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

    public override void StateOnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MonsterAI.nearestTarget = other.transform;
        }
    }
    
    public override void StateOnTriggerExit(Collider other)
    {
        return;
    }

    private void SetNextWaypoint()
    {
        if (MonsterAI.rallyNearestWaypoint)
        {
            foreach (Transform waypoints in Waypoints.List)
            {
                
            }
        }
        else
        {
            Waypoints.CurrentIndex = (Waypoints.CurrentIndex + 1) % Waypoints.List.Count; 
        }
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