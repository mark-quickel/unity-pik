using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pik.Scene { 
    public class Controller : MonoBehaviour
    {
        public GameObject RedPikPrefab;
        public GameObject BlackPikPrefab;
        public GameObject BluePikPrefab;
        public GameObject Captain;
        public Transform PikParent;
        public Transform PikNavTarget;
        public int RedPikCount;
        public int BlackPikCount;
        public int BluePikCount;
        
        private const int maxPik = 49;
        private readonly List<Pik.Pik> pikList = new();
        

        // Start is called before the first frame update
        void Start()
        {
            // Create Pik with some randomized attribtues (for now)
            for (var i = 0; i < RedPikCount; i++)
            {
                CreatePikInstance(RedPikPrefab);
            }

            for (var i = 0; i < BlackPikCount; i++)
            {
                CreatePikInstance(BlackPikPrefab);
            }

            for (var i = 0; i < BluePikCount; i++)
            {
                CreatePikInstance(BluePikPrefab);
            }

        }

        private void CreatePikInstance(GameObject pikType)
        {
            if (pikList.Count > maxPik) return;
            var o = new Vector3(Random.Range(0.0f, 2.0f), 0, Random.Range(0.0f, 2.0f));
            var p = Instantiate(pikType, PikNavTarget.position, Quaternion.identity, PikParent);

            var s = p.GetComponent<Pik.Pik>();
            s.Target = PikNavTarget;
            s.Offset = o;
            pikList.Add(s);
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
