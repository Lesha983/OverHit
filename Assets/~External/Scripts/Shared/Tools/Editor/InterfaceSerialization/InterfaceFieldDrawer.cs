namespace Chillplay.Tools.Editor
{
    using SerializedProperty.Editor;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(InterfaceField), true)]
    public class InterfaceFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            object interfaceAny = property.GetTargetObjectOfProperty();
            // System.Type genericType = interfaceAny.GetType().BaseType.GenericTypeArguments[0];

            EditorGUI.BeginProperty(position, label, property);

            Object before = property.FindPropertyRelative("value").objectReferenceValue;

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            EditorGUI.BeginChangeCheck();

            Object after = EditorGUI.ObjectField(position, before, typeof(Object), true);

            if (EditorGUI.EndChangeCheck())
            {
                object[] parameters = { after };
				
                interfaceAny.GetType().GetMethod("TrySetFromObject").Invoke(interfaceAny, parameters);
				
                //! This is slow. Drop the .SaveAssets(), if it becomes unbearable.
                EditorUtility.SetDirty(property.serializedObject.targetObject);
                AssetDatabase.SaveAssets();
            }


            //? Display object field
            //? Get an object with the same interface as the Interface generic argument
            //? Set / no set

            EditorGUI.EndProperty();
        }
    }
}