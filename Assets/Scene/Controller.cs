using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pik.Pik;

namespace Pik.Scene { 
    public class Controller : MonoBehaviour
    {
        public GameObject Pik;
        public GameObject Captain;
        public Transform PikParent;
        public Transform PikNavTarget;
        
        private const int maxPik = 49;
        private readonly List<GameObject> pikList = new();
        

        // Start is called before the first frame update
        void Start()
        {
            // Create Pik with some randomized attribtues (for now)
            for (var i = 0; i < maxPik; i++)
            {
                var o = new Vector3(Random.Range(0.0f, 2.0f), 0, Random.Range(0.0f, 2.0f));
                var p = Instantiate(Pik, PikNavTarget.position, Quaternion.identity, PikParent);
                pikList.Add(p);
                var m = p.GetComponent<Move>();
                m.Target = PikNavTarget;
                m.Offset = o;
                m.Speed = Random.Range(3.0f, 3.5f);

            }
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
