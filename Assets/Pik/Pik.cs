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

        private NavMeshAgent NMAgent;

        void Start()
        {
            NMAgent = GetComponent<NavMeshAgent>();
            NMAgent.acceleration = Acceleration;
            NMAgent.speed = Speed;
        }

        // Update is called once per frame
        void Update()
        {
            NMAgent.destination = Target.position + Offset;
        }

    }
}
