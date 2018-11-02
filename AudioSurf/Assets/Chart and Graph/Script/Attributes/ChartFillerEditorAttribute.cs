using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChartAndGraph
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ChartFillerEditorAttribute : Attribute
    {
        public GraphDataFiller.DataType ShowForType;
        public ChartFillerEditorAttribute(GraphDataFiller.DataType type)
        {
            ShowForType = type;
        }
    }
}
