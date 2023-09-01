using Pik.Events;
using System;
using UnityEngine;

namespace Pik
{
    public class PikTarget : MonoBehaviour
    {
        public float InitialDiameter;
        public float MaxDiameter;
        public float InitialHeight;
        public float MaxHeight;
        public float GrowthSpeed;
        public GameObject Source;

        public EventHandler<TargetCallingEventArgs> PikTargetCalling;

        private float Diameter;
        private float Height;
        private Collider ColliderInstance;
        private LineRenderer LineRendererInstance;

        void Start()
        {
            DisableCalling();
            ColliderInstance = GetComponent<Collider>();
            LineRendererInstance = GetComponent<LineRenderer>();
        }

        void Update()
        {
            transform.localScale = new Vector3(Diameter, Height, Diameter);
            LineRendererInstance.SetPosition(0, transform.position);
            LineRendererInstance.SetPosition(1, Source.transform.position);
        }

        public void Move(Vector3 position)
        {
            transform.position = new Vector3(position.x, position.y, position.z);
        }

        public void EnableCalling()
        {
            Diameter = Math.Min(Diameter + GrowthSpeed, MaxDiameter);
            Height = Math.Min(Height + GrowthSpeed, MaxHeight);
            PikTargetCalling?.Invoke(this, new TargetCallingEventArgs() { ColliderInstance = this.ColliderInstance });
        }

        public void DisableCalling()
        {
            Diameter = InitialDiameter;
            Height = InitialHeight;
        }


    }
}
