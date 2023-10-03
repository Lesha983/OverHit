namespace Chillplay.Tools.SerializedProperty.Editor
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UnityEditor;
	using UnityEngine;

	public static class SerializedPropertyExtension
	{
		//? https://forum.unity.com/threads/get-a-general-object-value-from-serializedproperty.327098/#post-4098484
		public static object GetValue(this SerializedProperty property)
		{
			object obj = property.serializedObject.targetObject;

			FieldInfo field = null;
			foreach (string path in property.propertyPath.Split('.'))
			{
				System.Type type = obj?.GetType();
				field = type?.GetField(path);
				obj = field?.GetValue(obj);
			}
			return obj;
		}

		private static string CollapseStringArray(string[] array)
		{
			string res = "";
			foreach (string elem in array)
			{
				res += elem + ", ";
			}
			return res;
		}

		//? https://forum.unity.com/threads/get-a-general-object-value-from-serializedproperty.327098/#post-6600706
		public static T GetSerializedValue<T>(this SerializedProperty property)
		{
			object @object = property.serializedObject.targetObject;
			Debug.Log(property.name);
			Debug.Log(property.propertyPath);
			string[] propertyNames = property.propertyPath.Split('.');
	
			Debug.Log(CollapseStringArray(propertyNames));
			//! Adding different conditions than the original code
			// Clear the property path from "Array" and "data[i]".
			// if (propertyNames.Length >= 3 && propertyNames[propertyNames.Length - 2] == "Array")
			// 	propertyNames = propertyNames.Take(propertyNames.Length - 2).ToArray();
			bool isArray = propertyNames.Length >= 3;
			isArray &= propertyNames[propertyNames.Length - 2] == "Array" || propertyNames[propertyNames.Length - 3] == "Array";
			if (isArray)
				propertyNames = propertyNames.Take(propertyNames.Length - 2).ToArray();
			Debug.Log(CollapseStringArray(propertyNames));
	
			// Get the last object of the property path.
			foreach (string path in propertyNames)
			{
				if (@object == null) return default(T); //review
				@object = @object.GetType()
					.GetField(path, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
					.GetValue(@object);
			}
	
			if (@object == null) return default(T); //review
			if (@object.GetType().GetInterfaces().Contains(typeof(IList<T>)))
			{
				int propertyIndex = int.Parse(property.propertyPath[property.propertyPath.Length - 2].ToString());
	
				return ((IList<T>) @object)[propertyIndex];
			}
			else return (T) @object;
		}

		//? https://github.com/lordofduct/spacepuppy-unity-framework/blob/master/SpacepuppyBaseEditor/EditorHelper.cs
		public static object GetTargetObjectOfProperty(this SerializedProperty prop)
        {
            if (prop == null) return null;

            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }
            return obj;
        }
		
		private static object GetValue_Imp(object source, string name)
		{
			if (source == null)
				return null;
			var type = source.GetType();

			while (type != null)
			{
				var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
				if (f != null)
					return f.GetValue(source);

				var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
				if (p != null)
					return p.GetValue(source, null);

				type = type.BaseType;
			}
			return null;
		}

		private static object GetValue_Imp(object source, string name, int index)
		{
			var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
			if (enumerable == null) return null;
			var enm = enumerable.GetEnumerator();
			//while (index-- >= 0)
			//    enm.MoveNext();
			//return enm.Current;

			for (int i = 0; i <= index; i++)
			{
				if (!enm.MoveNext()) return null;
			}
			return enm.Current;
		}
	}
}