using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ChartAndGraph
{
    public class BaseScrollableCategoryData
    {
        public string Name;
        [HideInInspector]
        public int ViewOrder = -1;
        public double? MaxX, MaxY, MinX, MinY, MaxRadius;
    }
}
