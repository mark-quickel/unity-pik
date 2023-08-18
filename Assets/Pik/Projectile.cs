﻿using UnityEngine;
using System.Collections;
using System;
using Unity.VisualScripting;

namespace Pik.Pik
{
    public class Projectile : MonoBehaviour
    {
        // based on and modified from - https://forum.unity.com/threads/throw-an-object-along-a-parabola.158855/
        public float firingAngle = 45.0f;
        public float gravity = 12.0f;

        public Transform ProjectileInstance;
        public Rigidbody RigidbodyInstance;
        public SphereCollider SphereColliderInstance;

        public EventHandler ProjectileLanded;

        public void Start()
        {
            ProjectileInstance = this.transform;
            RigidbodyInstance = GetComponent<Rigidbody>();
            SphereColliderInstance = GetComponent<SphereCollider>();
        }

        public void Initialize(Vector3 source, Vector3 target) 
        {
            transform.position = source;
            target = new Vector3(target.x, target.y + SphereColliderInstance.bounds.extents.y, target.z);
            RigidbodyInstance.isKinematic = true;
            StartCoroutine(LaunchProjectile(target));
        }
        
        IEnumerator LaunchProjectile(Vector3 target)
        {
            yield return StartCoroutine(SimulateProjectile(target));
            ProjectileLanded?.Invoke(this, EventArgs.Empty);
        }
       
        IEnumerator SimulateProjectile(Vector3 target)
        {            
            // Calculate distance to target
            float target_Distance = Vector3.Distance(ProjectileInstance.position, target);

            // Calculate the velocity needed to throw the object to the target at specified angle.
            float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

            // Extract the X  Y componenent of the velocity
            float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
            float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

            // Calculate flight time.
            float flightDuration = target_Distance / Vx;

            // Rotate projectile to face the target.
            ProjectileInstance.rotation = Quaternion.LookRotation(target - ProjectileInstance.position);

            float elapse_time = 0;

            while (elapse_time < flightDuration)
            {
                ProjectileInstance.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

                elapse_time += Time.deltaTime;

                yield return null;
            }
        }
       
    }
}