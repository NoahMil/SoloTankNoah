using System;
using System.Linq;
using UnityEngine;

public class ChasingState : BaseState {

    private Transform _targetTransform;
    
    public ChasingState(MonsterAI monsterAI) : base(monsterAI) {
        GameObject firstOrDefault = MonsterAI.playersInSightRange.FirstOrDefault();
        if (!firstOrDefault) throw new Exception("Cannot find any player to chase");
        _targetTransform = firstOrDefault.transform;
    }
    
    public override BaseState GetNextState() {
        if (MonsterAI.playersInAttackRange.Count > 0) {
            return new AttackingState(MonsterAI);
        }
        
        if (MonsterAI.playersInSightRange.Count == 0)
        {
            return new WanderingState(MonsterAI);
        }
        return null;
    }

    public override void Execute()
    {
        var position = _targetTransform.position;
        NavMeshAgent.SetDestination(position);
        MonsterAI.RotateToTarget(position);
    }

    public override void Enter() {
        // Je me balade, lalala
    }
    public override void Exit()
    {
    }
    
}