using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pik.Scene { 
    public class Controller : MonoBehaviour
    {
        public GameObject Pik;
        public GameObject Captain;
        public GameObject PikParent;
        private const int maxPik = 99;

        // Start is called before the first frame update
        void Start()
        {
            for (var i = 0; i < maxPik; i++)
            {
                GameObject.Instantiate(Pik, new Vector3(Random.Range(0,2.0f), 0, Random.Range(0, 2.0f)), Quaternion.identity , PikParent.transform);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
