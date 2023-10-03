namespace Chillplay.Tools.TimeSpan
{

    using System;
    using UnityEditor;
    using UnityEngine;
    using static System.TimeSpan;

    [CustomPropertyDrawer(typeof(TimeSpanBoxAttribute))]
    public class TimeSpanBoxDrawer : PropertyDrawer
    {
        const float XPadding = 30f;
        const float YPadding = 5f;
        const float Height = 20f;

        public override void OnGUI(Rect position,
            SerializedProperty property,
            GUIContent label)
        {

            var attr = attribute as TimeSpanBoxAttribute;
            var value = FromHours(property.floatValue);
            var text = FormatText(attr.Format, value);

            EditorGUI.PropertyField(position, property, label, true);

            position = new Rect(
                XPadding,
                position.y + EditorGUI.GetPropertyHeight(property, label, true) + YPadding,
                position.width - XPadding,
                Height);

            EditorGUI.HelpBox(position, text, MessageType.None);
        }

        private string FormatText(string format, TimeSpan value)
        {
            format = format
                .Replace(":", @"\:")
                .Replace(".", @"\")
                .Replace(@"0\:", "0:");

            return string.Format(format, value);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true) + Height;
        }
    }
}