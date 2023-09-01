using UnityEngine;
using UnityEngine.UI;

namespace Pik.UI
{
    public class PikSelection : MonoBehaviour
    {
        public PikColor SelectedColor;
        
        private Image ImageInstance;
        
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