using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderingState : BaseState {
    
    public WanderingState(MonsterAI monsterAI) : base(monsterAI) { }
    private Transform turretTransform;
    private float wanderingRadius = 10f;
    private float wanderingSpeed = 2f;
    private Vector3 wanderPoint;

    public override BaseState GetNextState() {
        if (MonsterAI.nearestTarget != null) {
            return new ChasingState(MonsterAI);
        }
        return null;
    }
    
    public override void Enter() {
        GetNewWanderPoint();
    }

    public override void Execute() {
        if (Vector3.Distance(turretTransform.position, wanderPoint) <= 1f)
        {
            GetNewWanderPoint(); 
        }
        float step = wanderingSpeed * Time.deltaTime;
        turretTransform.position = Vector3.MoveTowards(turretTransform.position, wanderPoint, step);
        
    }
    
    private void GetNewWanderPoint()
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized * wanderingRadius;
        wanderPoint = new Vector3(randomPoint.x, 0f, randomPoint.y) + turretTransform.position;
    }
    
    public override void Exit() {
    }
}