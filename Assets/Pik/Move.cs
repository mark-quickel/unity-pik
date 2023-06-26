using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Pik.Pik
{
    public class Move : MonoBehaviour
    {
        public float Speed = 1.9f;
        public Transform Target;
        public Vector3 Offset;

        private NavMeshAgent NMAgent;

        void Start()
        {
            NMAgent = GetComponent<NavMeshAgent>();
            NMAgent.speed = Speed;
        }

        // Update is called once per frame
        void Update()
        {
            NMAgent.destination = Target.position + Offset;
        }

    }
}
