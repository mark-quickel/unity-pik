using Pik.Shared;
using Pik.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pik.UI
{
    public class PikSelector : MonoBehaviour
    {
        public PikSelection PikSelectionLeft;
        public PikSelection PikSelectionCenter;
        public PikSelection PikSelectionRight;
        public TextMeshProUGUI PikCountText;

        public List<PikColorUIMapping> PikColorImageMappings;
        
        public PikColor SelectedPikColor
        {
            get { return PikSelectionCenter.SelectedColor; }
        }

        public void SetPikSelectionSprites(PikColor[] pikColors)
        {
            PikSelectionLeft.LoadImage(PikColorImageMappings.First(x => x.Color == pikColors[0]).SpriteInstance);
            PikSelectionCenter.LoadImage(PikColorImageMappings.First(x => x.Color == pikColors[1]).SpriteInstance);
            PikSelectionRight.LoadImage(PikColorImageMappings.First(x => x.Color == pikColors[2]).SpriteInstance);
        }

        public void SetPikSelectionLabel(string text)
        {
            PikCountText.text = text;
        }


    }
}
