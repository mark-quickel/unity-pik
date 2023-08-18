using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pik.UI
{
    public class PikTarget : MonoBehaviour
    {
        public void Move(Vector3 position)
        {
            transform.position = new Vector3(position.x, position.y, position.z);
        }
    }
}
