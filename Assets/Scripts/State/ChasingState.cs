using System;
using System.Linq;
using UnityEngine;

public class ChasingState : BaseState {

    private Transform _targetTransform;
    private float _safetyDistance = 1f; 

    public ChasingState(MonsterAI monsterAI) : base(monsterAI) {
        GameObject firstOrDefault = MonsterAI.playersInSightRange.FirstOrDefault();
        if (!firstOrDefault) throw new Exception("Cannot find any player to chase");
        _targetTransform = firstOrDefault.transform;
    }
    
    public override BaseState GetNextState() {
        if (MonsterAI.playersInAttackRange.Count > 0 && !MonsterAI.injured) {
            return new AttackingState(MonsterAI);
        }
        
        if (MonsterAI.playersInSightRange.Count == 0 && !MonsterAI.injured) {
            return new WanderingState(MonsterAI);
        }

        if (MonsterAI.injured) {
            return new FleeingState(MonsterAI);
        }
        return null;
    }

    public override void Execute() {
        Vector3 targetPosition = _targetTransform.position;
        Vector3 directionToTarget = targetPosition - MonsterAI.transform.position;

        float distanceToTarget = directionToTarget.magnitude;

        if (distanceToTarget > _safetyDistance) {
            NavMeshAgent.SetDestination(targetPosition - directionToTarget.normalized * _safetyDistance);
            MonsterAI.RotateToTarget(targetPosition);
        }
    }

    public override void Enter() {
    }

    public override void Exit() {
    }
}