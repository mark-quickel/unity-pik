using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pik.Captain
{
    public class Move : MonoBehaviour
    {
        public float Speed = 2.0f;

        // Update is called once per frame
        void Update()
        {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * Speed * Time.deltaTime));
        }

    }
}
