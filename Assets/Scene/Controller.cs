using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pik.Scene { 
    public class Controller : MonoBehaviour
    {
        public GameObject RedPikPrefab;
        public GameObject BlackPikPrefab;
        public GameObject BluePikPrefab;
        public GameObject CaptainPrefab;
        public Transform PikParent;
        public Transform PikNavTarget;
        public int RedPikCount;
        public int BlackPikCount;
        public int BluePikCount;
        
        private const int maxPik = 49;
        private readonly List<Pik.Pik> pikList = new();
        private Captain.Captain captain;


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

            CreateCaptainInstance();

        }

        private void CreatePikInstance(GameObject pikType)
        {
            if (pikList.Count > maxPik) return;
            var o = new Vector3(UnityEngine.Random.Range(0.0f, 2.0f), 0, UnityEngine.Random.Range(0.0f, 2.0f));
            var p = Instantiate(pikType, PikNavTarget.position, Quaternion.identity, PikParent);

            var s = p.GetComponent<Pik.Pik>();
            s.Target = PikNavTarget;
            s.Offset = o;
            s.PikThrown += Pik_PikThrown;
            pikList.Add(s);
        }

        private void CreateCaptainInstance()
        {
            captain = CaptainPrefab.GetComponent<Captain.Captain>();
            captain.PikThrown += Captain_PikThrown;
            captain.PikCalled += Captain_PikCalled;
        }

        private void Captain_PikCalled(object sender, EventArgs e)
        {
            pikList.ForEach(x => x.Call());
        }

        private void Captain_PikThrown(object sender, System.EventArgs e)
        {
            var direction = captain.transform.forward * ((Captain.Captain)sender).Strength + Vector3.up * ((Captain.Captain)sender).Strength * 3f;
            var pik = pikList.FirstOrDefault(x => x.IsAttended);
            if (pik != null)
            {
                pik.Throw(captain.transform.position, direction);
            }
        }
        
        private void Pik_PikThrown(object sender, System.EventArgs e)
        {
           
        }


    }
}
