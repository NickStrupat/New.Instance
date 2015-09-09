using System;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace New.Instance
{
	public static class New<T> {
		public static readonly Func<T> Instance = GetInstanceFactory();

		private static Func<T> GetInstanceFactory() {
			var type = typeof(T);
			if (type.IsInterface)
				throw new InvalidOperationException($"`{type.FullName}` is an interface and therefore cannot be instantiated.");
			if (type.IsAbstract)
				throw new InvalidOperationException($"`{type.FullName}` is an abstract class and therefore cannot be instantiated.");

			if (type.IsPrimitive) {
				return Expression.Lambda<Func<T>>(Expression.Constant(default(T))).Compile();
			}

			if (type.IsEnum) {
				//return () => default(T);
				return Expression.Lambda<Func<T>>(Expression.Constant(default(T))).Compile(); // fastest
				//return Expression.Lambda<Func<T>>(Expression.New(type)).Compile();
				//return GetDynamicMethod(type, type, g => g.Emit(OpCodes.Ldc_I4_0));
			}

			var typeIsNullable = Nullable.GetUnderlyingType(type) != null;
			if (typeIsNullable) {
				return Expression.Lambda<Func<T>>(Expression.Constant(default(T), type)).Compile();
			}

			if (type.IsValueType) {
				//return () => default(T);
				return Expression.Lambda<Func<T>>(Expression.Constant(default(T), type)).Compile(); // fastest
				//return Expression.Lambda<Func<T>>(Expression.New(type)).Compile();
				//return GetDynamicMethod(type, type,
				//	g => {
				//		g.DeclareLocal(type);
				//		g.Emit(OpCodes.Ldloca_S, 0);
				//		g.Emit(OpCodes.Initobj, type);
				//		g.Emit(OpCodes.Ldloc_0);
				//	});
			}

			if (type == typeof(String)) {
				//return () => (T) (Object) String.Empty;
				return Expression.Lambda<Func<T>>(Expression.Constant(String.Empty)).Compile(); // fastest
				//return GetDynamicMethod(type, type, g => g.Emit(OpCodes.Ldsfld, type.GetField(nameof(String.Empty))));
			}

			if (type == typeof(Object)) {
				//return () => (T) new Object();
				return Expression.Lambda<Func<T>>(Expression.New(type)).Compile(); // fastest
				//return GetDynamicMethod(type, type, g => g.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes)));
			}

			if (type.IsArray) {
				var elementType = type.GetElementType();
				return Expression.Lambda<Func<T>>(Expression.NewArrayInit(elementType)).Compile(); // fastest
				//return GetDynamicMethod(type, elementType, g => { g.Emit(OpCodes.Ldc_I4_0); g.Emit(OpCodes.Newarr, elementType); });
			}

			if (type.IsClass) {
				var defaultConstructor = type.GetConstructor(Type.EmptyTypes);
				if (defaultConstructor != null) {
					//return Expression.Lambda<Func<T>>(Expression.New(type)).Compile();
					return GetDynamicMethod(type, type, g => g.Emit(OpCodes.Newobj, defaultConstructor)); // fastest
				}
				throw new InvalidOperationException($"`{type.FullName}` has no default constructor.");
			}

			throw new InvalidProgramException($"`{type.FullName}` instantiation is not supported.");
		}

		private static Func<T> GetDynamicMethod(Type type, Type owner, Action<ILGenerator> emitNewInstanceAction) {
			var dynamicMethod = new DynamicMethod(type.FullName + "Factory", type, Type.EmptyTypes, owner, true);
			var ilGen = dynamicMethod.GetILGenerator();
			emitNewInstanceAction(ilGen);
			ilGen.Emit(OpCodes.Ret);
			return (Func<T>)dynamicMethod.CreateDelegate(typeof(Func<T>));
		}
	}
}
