using System;
using System.Linq;
using UnityEngine;

public class FleeingState : BaseState
{
    private Transform _targetTransform;
    private float _fleeingDistance = 10.0f;
    private Vector3 _oppositeDestination;

    public FleeingState(MonsterAI monsterAI) : base(monsterAI)
    {
        GameObject firstOrDefault = MonsterAI.playersInFleeRange.FirstOrDefault();
        if (!firstOrDefault) throw new Exception("Cannot find any player to flee");
        _targetTransform = firstOrDefault.transform;
    }
    
    public override BaseState GetNextState()
    {
        if (MonsterAI.playersInFleeRange.Count == 0 && MonsterAI.injured)
        {
            return new WanderingState(MonsterAI);
        }
        return null;
    }

    public override void Enter()
    {
        
    }

    public override void Execute()
    {
        Fleeing();
    }

    public override void Exit()
    {
    }

    private void Fleeing()
    { 
        var position = MonsterAI.transform.position; 
        
        Vector3 fleeingDirection = position - _targetTransform.position; 
        
        fleeingDirection.Normalize(); 
        _oppositeDestination = position + fleeingDirection * _fleeingDistance;
        
        NavMeshAgent.SetDestination(_oppositeDestination);
        MonsterAI.RotateToTarget(_oppositeDestination);
    }
}