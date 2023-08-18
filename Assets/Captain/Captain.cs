using Pik.Scene;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Pik.Captain
{
    public class Captain : MonoBehaviour
    {
        public float Speed;
        public float Strength;
        public string Name;
        public Transform NameLabel;
        public float MaxThrowDistance;

        public EventHandler PikThrown;
        public EventHandler PikCalled;
        public EventHandler PikSelectionChanged;
        public EventHandler CaptainMoved;
        public EventHandler<TargetMovedEventArgs> PikTargetMoved;
                
        private Vector3 Direction;
        private Vector3 TargetPosition;


        // Update is called once per frame
        void Update()
        {
            CheckMove();
            CheckThrow();
            CheckCall();
            CheckTargetMoved();            
            CheckPikSelectedChanged();
            Move();
        }

        private void CheckMove()
        {
            Direction = new Vector3(Speed * Input.GetAxis("Horizontal"), 0, Speed * Input.GetAxis("Vertical"));
        }

        private void CheckTargetMoved()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, LayerMask.GetMask("Ground")))
            {
                if (Vector3.Distance(hit.point, transform.position) < MaxThrowDistance)
                {
                    TargetPosition = hit.point;
                }
                else
                {
                    // cap the throwing distance
                    var diff = hit.point - transform.position;
                    diff.Normalize();
                    diff *= MaxThrowDistance;
                    TargetPosition = transform.position + diff;
                }
            }

            PikTargetMoved?.Invoke(this, new TargetMovedEventArgs { Position = TargetPosition });

        }

        void CheckThrow()
        {
            if (Input.GetButtonUp("Fire1"))
            {
                PikThrown?.Invoke(this, EventArgs.Empty);
            }
        }

        void CheckCall()
        {
            if (Input.GetButtonUp("Fire2"))
            {
                PikCalled?.Invoke(this, EventArgs.Empty);
            }
        }

        void CheckPikSelectedChanged()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                PikSelectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        void Move()
        {
            if (Direction.magnitude > 0.1f)
            {
                var rotation = Quaternion.LookRotation(Direction, Vector3.up);
                transform.localRotation = rotation;
                transform.Translate(transform.forward * Speed * Time.deltaTime, Space.World);
                CaptainMoved?.Invoke(this, EventArgs.Empty);
            }
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * 5);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, Vector3.forward * 5);

            var label = NameLabel;
            var style = new GUIStyle();
            style.normal.textColor = Color.magenta; 
            Handles.Label(label.position, Name, style);
        }

    }
}
