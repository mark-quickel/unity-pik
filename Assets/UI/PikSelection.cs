using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pik.Shared;
using UnityEngine.UI;

namespace Pik.UI
{
    public class PikSelection : MonoBehaviour
    {
        public PikColor SelectedColor;
        
        private Image ImageInstance;
        
        // Start is called before the first frame update
        void Start()
        {
            ImageInstance = GetComponent<Image>();
        }

        public void LoadImage(Sprite sprite)
        {
            ImageInstance.sprite = sprite;
        }
    }
}                                       