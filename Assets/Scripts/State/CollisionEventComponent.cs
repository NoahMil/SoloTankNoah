using System;
using UnityEngine;
using UnityEngine.Events;

namespace State
{
    [Serializable]
    public class ColliderEvent : UnityEvent<Collider> {}

    public class CollisionEventComponent : MonoBehaviour {
    
        [SerializeField] private ColliderEvent onTriggerEnter;
        [SerializeField] private ColliderEvent onTriggerStay;
        [SerializeField] private ColliderEvent onTriggerExit;
    
        private void OnTriggerEnter(Collider other) {
            onTriggerEnter.Invoke(other);
        }

        private void OnTriggerStay(Collider other) {
            onTriggerStay.Invoke(other);
        }

        private void OnTriggerExit(Collider other) {
            onTriggerExit.Invoke(other);
        }
    
    }
}