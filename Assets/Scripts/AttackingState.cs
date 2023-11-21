using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

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
        if (MonsterAI.playersInSightRange.Count > 0 && MonsterAI.playersInAttackRange.Count == 0)
        {
            return new ChasingState(MonsterAI);
        }
        
        if (MonsterAI.playersInSightRange.Count == 0 && MonsterAI.playersInAttackRange.Count == 0)
        {
            return new WanderingState(MonsterAI);
        }

        /*if (MonsterAI.NearestTarget != null && !MonsterAI.TargetInRange)
        {
            return new ChasingState(MonsterAI);
        }
        if (MonsterAI.NearestTarget == null) {
            return new WanderingState(MonsterAI);
        }*/

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
        // Je me balade, lalala
    }
    public override void Exit() {
        // Je me balade, lalala
    }

    /*public override void StateOnTriggerExit(Collider other)
    {
        float distanceThreshold = 5f; 

        if (other.CompareTag("Player"))
        {
            MonsterAI.NearestTarget = other.transform;
            float distanceToPlayer = Vector3.Distance(MonsterAI.transform.position, other.transform.position);
        
            if (distanceToPlayer > distanceThreshold)
            {
                MonsterAI.TargetInRange = false;
            }
        };
    }*/
    
}