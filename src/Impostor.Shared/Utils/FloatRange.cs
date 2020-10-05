using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Impostor.Shared.Utils
{
    public class FloatRange
    {
        public float Min;
        public float Max;
        public FloatRange(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float Lerp(float v)
        {
            return Mathf.Lerp(Min, Max, v);
        }
    }
}
