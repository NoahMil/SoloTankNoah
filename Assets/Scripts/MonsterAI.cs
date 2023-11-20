using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MonsterAI : MonoBehaviour {
    
    [HideInInspector] public Transform nearestTarget;
    public bool targetInRange;
    private BaseState _currentState;
    public bool rallyNearestWaypoint;
    public float dist;
    

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
        
        Debug.Log(_currentState);
        foreach (Transform waypoints in Waypoints.List)
        {
            dist = Vector3.Distance(transform.position,waypoints.position);
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Via Direct Cast
        // WanderingState state = (WanderingState)_currentState;
        // if (state != null) state.StateOnTriggerEnter();
        
        // Via Safe Cast
        // WanderingState state2 = _currentState as WanderingState;
        // if (state2 != null) state2.StateOnTriggerEnter();
        
        // Type comparison
        // if (_currentState.GetType() == typeof(WanderingState))
        //    _currentState.StateOnTriggerEnter();
        
        // Type comparison with internal cast
        // if (_currentState is WanderingState state3)
        // {
        //   state.StateOnTriggerEnter();
        // }
        
        _currentState.StateOnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _currentState.StateOnTriggerExit(other);
    }
}