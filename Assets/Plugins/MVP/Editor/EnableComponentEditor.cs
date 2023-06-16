using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

namespace MVP.Editor
{
    [CustomEditor(typeof(MvpComponent))]
    public class EnableComponentEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawPropertiesExcluding(serializedObject, "m_Script");
            
            var objectType = typeof(MvpComponent);
            var fieldInfo = objectType.GetField("_components", BindingFlags.NonPublic | BindingFlags.Instance);
            
            var dictionary = fieldInfo.GetValue((MvpComponent)target) as IDictionary<Component, (object, Presenter)>;
            foreach (var (_, (model, _)) in dictionary)
            {
                var modelType = model.GetType();
                var fields = modelType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            
                foreach (var field in fields)
                {
                    if (typeof(System.MulticastDelegate).IsAssignableFrom(field.FieldType)) continue;
                    var fieldValue = field.GetValue(model);
                    EditorGUILayout.LabelField(field.Name, fieldValue != null ? fieldValue.ToString() : "null");
                }
            }
        }
    }
}
