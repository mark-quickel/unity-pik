using Pik.Shared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pik.Pik
{
    public class RedPik : Pik
    {
        public RedPik()
        {
            Color = PikColor.Red;
            MaxThrowDistance = 3.0f;
        }
    }
}
