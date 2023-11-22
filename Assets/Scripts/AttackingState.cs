using System;
using System.Linq;
using UnityEngine;


public class AttackingState : BaseState
{
    private Transform _targetTransform;

    public AttackingState(MonsterAI monsterAI) : base(monsterAI)
    { 
        GameObject firstOrDefault = MonsterAI.playersInAttackRange.FirstOrDefault();
        if (!firstOrDefault) throw new Exception("Cannot find any player to attack");
        _targetTransform = firstOrDefault.transform;
    }
    
    public override BaseState GetNextState()
    {
        if (MonsterAI.playersInSightRange.Count > 0 && MonsterAI.playersInAttackRange.Count == 0 && !MonsterAI.injured)
        {
            return new ChasingState(MonsterAI);
        }
        
        if (MonsterAI.playersInSightRange.Count == 0 && MonsterAI.playersInAttackRange.Count == 0 && !MonsterAI.injured)
        {
            return new WanderingState(MonsterAI);
        }
        
        if (MonsterAI.injured)
        {
            return new FleeingState(MonsterAI);
        }
        return null;
    }

    public override void Execute()
    {
        var position = _targetTransform.position;
        MonsterAI.Fire();
        NavMeshAgent.SetDestination(position); 
        MonsterAI.RotateToTarget(position);
    }
    public override void Enter() {
        
    }
    public override void Exit() {
        
    }
    
    
}