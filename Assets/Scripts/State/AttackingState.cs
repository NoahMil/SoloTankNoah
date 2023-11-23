using System;
using System.Linq;
using UnityEngine;

namespace State
{
    public class AttackingState : BaseState
    {
        private Transform _targetTransform;
        private float _safetyDistance = 0.8f; 


        public AttackingState(MonsterAI monsterAI) : base(monsterAI)
        { 
            GameObject firstOrDefault = MonsterAI.playersInAttackRange.FirstOrDefault();
            if (!firstOrDefault) throw new Exception("Cannot find any player to attack");
            _targetTransform = firstOrDefault.transform;
        }
    
        public override BaseState GetNextState()
        {
            if (MonsterAI.playersInSightRange.Count > 0 && MonsterAI.playersInAttackRange.Count == 0 && !MonsterAI.injured) // 
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
            Vector3 targetPosition = _targetTransform.position;
            Vector3 directionToTarget = targetPosition - MonsterAI.transform.position;

            float distanceToTarget = directionToTarget.magnitude;

            if (distanceToTarget > _safetyDistance) {
                NavMeshAgent.SetDestination(targetPosition - directionToTarget.normalized * _safetyDistance);
                MonsterAI.RotateToTarget(targetPosition);
                MonsterAI.Fire();
            }
        }
        public override void Enter() {
        
        }
        public override void Exit() {
        
        }
    
    
    }
}