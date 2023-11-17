using UnityEngine;

public class MonsterAI : MonoBehaviour {
    
    public GameObject nearestTarget;
    public bool targetInRange;

    private BaseState _currentState;

    private void Start() {
        _currentState = new WanderingState(this);
    }

    private void Update() {
        BaseState nextState = _currentState.GetNextState();
        if (nextState != null)
        {
            _currentState = nextState;
            
        }
        else _currentState.Execute();
        // Get nearest target and insert into nearestTarget
    }
    
}