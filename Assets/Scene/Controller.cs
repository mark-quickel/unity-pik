using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pik.UI;
using Pik.Events;

namespace Pik.Scene { 
    public class Controller : MonoBehaviour
    {
        public GameObject RedPikPrefab;
        public GameObject BlackPikPrefab;
        public GameObject BluePikPrefab;
        public Transform PikParent;
        public Transform PikNavTarget;
        public int RedPikCount;
        public int BlackPikCount;
        public int BluePikCount;
        public UI.Controller UIControllerInstance;
        public PikTarget PikTargetInstance;
        public Captain CaptainInstance;
        
        private const int MaxPik = 49;
        private readonly List<Pik> PikList = new();
        private PikColor[] PikColors;
        private Vector3 PikTargetPosition;

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

            PikColors = new PikColor[] { PikColor.Red, PikColor.Blue, PikColor.Black };
            UIUpdatePikSelector();
            CreatePikTargetInstance();
            CreateCaptainInstance();
        }

        private void CreatePikInstance(GameObject pikType)
        {
            if (PikList.Count > MaxPik) return;
            var o = new Vector3(UnityEngine.Random.Range(0.0f, 2.0f), 0, UnityEngine.Random.Range(0.0f, 2.0f));
            var p = Instantiate(pikType, PikNavTarget.position, Quaternion.identity, PikParent);

            var s = p.GetComponent<Pik>();
            s.Target = PikNavTarget;
            s.Offset = o;
            s.IsAttended = true;
            PikList.Add(s);
        }

        private void CreateCaptainInstance()
        {
            CaptainInstance.PikThrown += Captain_PikThrown;
            CaptainInstance.PikCalling += Captain_PikCalling;
            CaptainInstance.PikCalled += Captain_PikCalled;
            CaptainInstance.PikSelectionChanged += Captain_PikSelectionChanged;
            CaptainInstance.PikTargetMoved += Captain_PikTargetMoved;
        }

        private void CreatePikTargetInstance()
        {
            PikTargetInstance.PikTargetCalling += PikTarget_PikTargetCalling;
        }

        private void Captain_PikTargetMoved(object sender, TargetMovedEventArgs e)
        {
            PikTargetPosition = e.Position;
            PikTargetInstance.Move(PikTargetPosition);
        }

        private void Captain_PikCalling(object sender, EventArgs e)
        {
            PikTargetInstance.EnableCalling();
        }

        private void PikTarget_PikTargetCalling(object sender, TargetCallingEventArgs e)
        {

            PikList.Where(x => e.ColliderInstance.bounds.Contains(x.transform.position)).ToList()
                .ForEach(x => x.Call());
            
            UIUpdatePikSelector();
        }
        
        private void Captain_PikCalled(object sender, EventArgs e)
        {
            PikTargetInstance.DisableCalling();
        }

        private void Captain_PikThrown(object sender, System.EventArgs e)
        {
            var pik = PikList.FirstOrDefault(x => x.IsAttended && x.Color == PikColors[1]);
            if (pik != null)
            {
                pik.Project(CaptainInstance.transform.position, PikTargetPosition);
                UIUpdatePikSelector();
                return;
            }
            
            if (PikList.Count(x => x.IsAttended) > 0)
            {
                ShiftPikSelection();
            }
        }

        private void Captain_PikSelectionChanged(object sender, EventArgs e)
        {
            ShiftPikSelection();
        }

        private void ShiftPikSelection()
        {
            var p0 = PikColors[0];
            var p1 = PikColors[1];
            var p2 = PikColors[2];

            PikColors = new PikColor[] { p2, p0, p1 };
            
            UpdateCaptainMaxThrowDistance();
            UIUpdatePikSelector();
        }

        private void UIUpdatePikSelector() 
        {
            var selectedCount = PikList.Count(x => x.IsAttended && x.Color == PikColors[1]);
            UIControllerInstance.PikSelectorInstance.SetPikSelectionSprites(PikColors);
            UIControllerInstance.PikSelectorInstance.SetPikSelectionLabel(selectedCount.ToString());
        }

        private void UpdateCaptainMaxThrowDistance()
        {
            var pik = PikList.FirstOrDefault(x => x.Color == PikColors[1]);
            if (pik != null)
            {
                CaptainInstance.MaxThrowDistance = pik.MaxThrowDistance;
            }
        }

    }
}
