using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

namespace ChartAndGraph
{
    [CustomEditor(typeof(GraphDataFiller), true)]
    class GraphDataFillerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
          //  var cats = serializedObject.FindProperty("Categories");
           // EditorGUILayout.PropertyField(cats);
//            CategoryDataEditor
  //          Categories
        }
    }
}
