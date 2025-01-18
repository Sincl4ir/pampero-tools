using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pampero.Tools.Utils
{
    public static class ReflectionExtensions
	{
		// Get the value of a private field using reflection
		public static T GetPrivateFieldValue<T>(this object obj, string fieldName)
		{
			Type type = obj.GetType();
			FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);

			if (fieldInfo == null)
			{
				throw new ArgumentException($"Field '{fieldName}' not found in type '{type.FullName}'.");
			}

			return (T)fieldInfo.GetValue(obj);
		}

		// Set the value of a private field using reflection
		public static void SetPrivateFieldValue<T>(this object obj, string fieldName, T value)
		{
			Type type = obj.GetType();
			FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);

			if (fieldInfo == null)
			{
				throw new ArgumentException($"Field '{fieldName}' not found in type '{type.FullName}'.");
			}

			fieldInfo.SetValue(obj, value);
		}

		// Get the value of a private or public property using reflection
		public static T GetPropertyValue<T>(this object obj, string propertyName)
		{
			Type type = obj.GetType();
			PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			if (propertyInfo == null)
			{
				throw new ArgumentException($"Property '{propertyName}' not found in type '{type.FullName}'.");
			}

			return (T)propertyInfo.GetValue(obj);
		}

		// Set the value of a private or public property using reflection
		public static void SetPropertyValue<T>(this object obj, string propertyName, T value)
		{
			Type type = obj.GetType();
			PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			if (propertyInfo == null)
			{
				throw new ArgumentException($"Property '{propertyName}' not found in type '{type.FullName}'.");
			}

			propertyInfo.SetValue(obj, value);
		}

		// Get all fields of a type, including private fields of base types
		public static IEnumerable<FieldInfo> GetAllFields(this Type type)
		{
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
			return type.GetFields(bindingFlags);
		}

		// Get all properties of a type, including private properties of base types
		public static IEnumerable<PropertyInfo> GetAllProperties(this Type type)
		{
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
			return type.GetProperties(bindingFlags);
		}

		// Get all methods of a type, including private methods of base types
		public static IEnumerable<MethodInfo> GetAllMethods(this Type type)
		{
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
			return type.GetMethods(bindingFlags);
		}


		// Check if a type has a specific attribute
		public static bool HasAttribute<TAttribute>(this Type type) where TAttribute : Attribute
		{
			return type.GetCustomAttributes(typeof(TAttribute), true).Length > 0;
		}

		// Get all types in the same assembly that have a specific attribute
		public static IEnumerable<Type> GetTypesWithAttribute<TAttribute>(this Assembly assembly) where TAttribute : Attribute
		{
			return assembly.GetTypes().Where(type => type.HasAttribute<TAttribute>());
		}

		// Check if a method has a specific attribute
		public static bool HasAttribute<TAttribute>(this MethodInfo methodInfo) where TAttribute : Attribute
		{
			return methodInfo.GetCustomAttributes(typeof(TAttribute), true).Length > 0;
		}

		// Get all methods in a type that have a specific attribute
		public static IEnumerable<MethodInfo> GetMethodsWithAttribute<TAttribute>(this Type type) where TAttribute : Attribute
		{
			return type.GetMethods().Where(method => method.HasAttribute<TAttribute>());
		}

		// Check if a property has a specific attribute
		public static bool HasAttribute<TAttribute>(this PropertyInfo propertyInfo) where TAttribute : Attribute
		{
			return propertyInfo.GetCustomAttributes(typeof(TAttribute), true).Length > 0;
		}

		// Get all properties in a type that have a specific attribute
		public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<TAttribute>(this Type type) where TAttribute : Attribute
		{
			return type.GetProperties().Where(property => property.HasAttribute<TAttribute>());
		}

		// Check if a field has a specific attribute
		public static bool HasAttribute<TAttribute>(this FieldInfo fieldInfo) where TAttribute : Attribute
		{
			return fieldInfo.GetCustomAttributes(typeof(TAttribute), true).Length > 0;
		}

		// Get all fields in a type that have a specific attribute
		public static IEnumerable<FieldInfo> GetFieldsWithAttribute<TAttribute>(this Type type) where TAttribute : Attribute
		{
			return type.GetFields().Where(field => field.HasAttribute<TAttribute>());
		}

		// Check if a parameter has a specific attribute
		public static bool HasAttribute<TAttribute>(this ParameterInfo parameterInfo) where TAttribute : Attribute
		{
			return parameterInfo.GetCustomAttributes(typeof(TAttribute), true).Length > 0;
		}

		// Get all parameters in a method that have a specific attribute
		public static IEnumerable<ParameterInfo> GetParametersWithAttribute<TAttribute>(this MethodBase methodBase) where TAttribute : Attribute
		{
			return methodBase.GetParameters().Where(parameter => parameter.HasAttribute<TAttribute>());
		}

		// Check if an assembly has a specific attribute
		public static bool HasAttribute<TAttribute>(this Assembly assembly) where TAttribute : Attribute
		{
			return assembly.GetCustomAttributes(typeof(TAttribute), true).Length > 0;
		}

		// Get all types in an assembly that implement a specific interface
		public static IEnumerable<Type> GetTypesImplementingInterface<TInterface>(this Assembly assembly)
		{
			Type interfaceType = typeof(TInterface);
			return assembly.GetTypes().Where(type => interfaceType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract);
		}

		// Invoke a method by name on an object instance
		public static object InvokeMethod(this object obj, string methodName)
		{
			Type type = obj.GetType();
			MethodInfo methodInfo = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			if (methodInfo == null)
			{
				throw new ArgumentException($"Method '{methodName}' not found in type '{type.FullName}'.");
			}

			return methodInfo.Invoke(obj, null);
		}

		// Invoke a method by name on an object instance with parameters
		public static object InvokeMethod(this object obj, string methodName, params object[] parameters)
		{
			Type type = obj.GetType();
			MethodInfo methodInfo = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			if (methodInfo == null)
			{
				throw new ArgumentException($"Method '{methodName}' not found in type '{type.FullName}'.");
			}

			return methodInfo.Invoke(obj, parameters);
		}

		// Invoke a static method by name on a type
		public static object InvokeStaticMethod(this Type type, string methodName)
		{
			MethodInfo methodInfo = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

			if (methodInfo == null)
			{
				throw new ArgumentException($"Static method '{methodName}' not found in type '{type.FullName}'.");
			}

			return methodInfo.Invoke(null, null);
		}

		// Invoke a static method by name on a type with parameters
		public static object InvokeStaticMethod(this Type type, string methodName, params object[] parameters)
		{
			MethodInfo methodInfo = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

			if (methodInfo == null)
			{
				throw new ArgumentException($"Static method '{methodName}' not found in type '{type.FullName}'.");
			}

			return methodInfo.Invoke(null, parameters);
		}
	}
}