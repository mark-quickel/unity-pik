using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Pik.Pik
{
    public abstract class Pik : MonoBehaviour
    {
        public float Speed = 2.0f;
        public float Acceleration = 2.0f;
        public Transform Target;
        public Vector3 Offset;
        public bool IsAttended;

        public EventHandler PikMoved;
        public EventHandler PikThrown;
        public EventHandler PikCalled;

        private NavMeshAgent NavMeshAgentInstance;
        private Rigidbody RigidbodyInstance;
        

        void Start()
        {
            NavMeshAgentInstance = GetComponent<NavMeshAgent>();
            NavMeshAgentInstance.acceleration = Acceleration;
            NavMeshAgentInstance.speed = Speed;

            RigidbodyInstance = GetComponent<Rigidbody>();

            IsAttended = true;
        }
        
        void Update()
        {
            Move();
        }

        public void Throw(Vector3 thrower, Vector3 direction)
        {
            IsAttended = false;
            transform.position = thrower;
            NavMeshAgentInstance.enabled = false;
            RigidbodyInstance.AddForce(direction, ForceMode.Impulse);
            PikThrown?.Invoke(this, EventArgs.Empty);
        }

        public void Call()
        {
            // TODO: check to see if grounded
            NavMeshAgentInstance.enabled = true;
            IsAttended = true;
        }
        
        void Move()
        {
            if (!IsAttended) return;
            NavMeshAgentInstance.destination = Target.position + Offset;
            PikMoved?.Invoke(this, EventArgs.Empty);
        }


    }
}
