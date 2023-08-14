using Pik.Scene;
using System;
using System.Collections;
using System.Collections.Generic;
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
        
        public EventHandler PikThrown;
        public EventHandler PikCalled;
        public EventHandler CaptainMoved;
        
        private Vector3 Direction;
        private Vector3 LookDirection;
           

        // Update is called once per frame
        void Update()
        {
            CheckMove();
            CheckThrow();
            CheckCall(); 
            Move();
        }

        private void CheckMove()
        {
            Direction = new Vector3(Speed * Input.GetAxis("Horizontal"), 0, Speed * Input.GetAxis("Vertical"));
            LookDirection = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1)) - transform.position;
        }

        void CheckThrow()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                PikThrown?.Invoke(this, EventArgs.Empty);
            }
        }

        void CheckCall()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                PikCalled?.Invoke(this, EventArgs.Empty);
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

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, LookDirection);

            var label = NameLabel;
            var style = new GUIStyle();
            style.normal.textColor = Color.magenta; 
            Handles.Label(label.position, Name, style);
        }

    }
}
