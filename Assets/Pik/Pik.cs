using Pik.Shared;
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
        public PikColor Color;
        public float MaxThrowDistance;

        public EventHandler PikMoved;
        public EventHandler PikThrown;
        public EventHandler PikCalled;

        private NavMeshAgent NavMeshAgentInstance;
        private Rigidbody RigidbodyInstance;
        private Projectile ProjectileInstance;
        private bool IsGrounded = true;
        
        void Start()
        {
            NavMeshAgentInstance = GetComponent<NavMeshAgent>();
            NavMeshAgentInstance.acceleration = Acceleration;
            NavMeshAgentInstance.speed = Speed;

            RigidbodyInstance = GetComponent<Rigidbody>();

            ProjectileInstance = GetComponent<Projectile>();
            ProjectileInstance.ProjectileLanded += ProjectileInstance_ProjectileLanded;
        }

        void Update()
        {
            Move();
        }

       public void Project(Vector3 source, Vector3 target)
        {
            IsAttended = false;
            NavMeshAgentInstance.enabled = false;
            IsGrounded = false;
            ProjectileInstance.Initialize(source, target);
            PikThrown?.Invoke(this, EventArgs.Empty);
        }
        
        private void ProjectileInstance_ProjectileLanded(object sender, EventArgs e)
        {
            IsGrounded = true;
        }

        public void Call()
        {
            if (!IsGrounded) return; 
            RigidbodyInstance.isKinematic = false;
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
