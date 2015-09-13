using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NewInstance;

namespace Tests {
	static class New2<T> {
		public static readonly Func<T> Instance = GetInstanceFactory();

		private static Func<T> GetInstanceFactory() {
			return Expression.Lambda<Func<T>>(Expression.New(typeof (T))).Compile();
		}
	}

	struct Baz {
		private String name;
		private DateTime when;
		public Decimal money;
		public Decimal money2;
		public Decimal money3;
	}

	class Foo {
		private String name;
		private DateTime when;
		public Decimal money;
		public Decimal money2;
		public Decimal money3;
	}

	class Woo {
		private static Int32 count;
		public Woo() {
			++count;
		}
	}
	enum Bar {}

	[TestClass]
	public class UnitTest1 {
		const Int32 limit = 10000000;

		[TestMethod]
		public void Int32ConstructorPerformance() {
			var instance = new Int32();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = new Int32();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void Int32ActivatorPerformance() {
			var instance = Activator.CreateInstance<Int32>();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = Activator.CreateInstance<Int32>();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void Int32NewInstancePerformance() {
			var instance = New<Int32>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New<Int32>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void Int32ExpressionNewPerformance() {
			var instance = New2<Int32>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New2<Int32>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}



		[TestMethod]
		public void FooActivatorPerformance() {
			var instance = Activator.CreateInstance<Foo>();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = Activator.CreateInstance<Foo>();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void FooNewInstancePerformance() {
			var instance = New<Foo>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New<Foo>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void FooExpressionNewPerformance() {
			var instance = New2<Foo>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New2<Foo>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}





		[TestMethod]
		public void BarConstructorPerformance() {
			var instance = new Bar();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = new Bar();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void BarActivatorPerformance() {
			var instance = Activator.CreateInstance<Bar>();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = Activator.CreateInstance<Bar>();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void BarNewInstancePerformance() {
			var instance = New<Bar>.Instance();
			var instanceFunc = New<Bar>.Instance;
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New<Bar>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void BarExpressionNewPerformance() {
			var instance = New2<Bar>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New2<Bar>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}





		[TestMethod]
		public void StringFuncStringEmptyPerformance() {
			Func<String> se = () => String.Empty;
			var instance = se();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = se();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void StringEmptyPerformance() {
			var instance = String.Empty;
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = String.Empty;
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void StringNewInstancePerformance() {
			var instance = New<String>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New<String>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}




		[TestMethod]
		public void StringArrayConstructorFuncPerformance() {
			Func<String[]> func = () => new String[0];
			var instance = func();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = func();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void StringArrayConstructorPerformance() {
			var instance = new String[0];
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = new String[0];
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void StringArrayNewInstancePerformance() {
			var instance = New<String[]>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New<String[]>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void StringSquareArrayNewInstancePerformance() {
			var instance = New<String[,]>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New<String[,]>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}




		[TestMethod]
		public void BooleanArrayNewInstancePerformance() {
			var instance = New<Boolean[]>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New<Boolean[]>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}





		[TestMethod]
		public void BooleanNewInstancePerformance() {
			var instance = New<Boolean>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New<Boolean>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void SingleNewInstancePerformance() {
			var instance = New<Single>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New<Single>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void DoubleNewInstancePerformance() {
			var instance = New<Double>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New<Double>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void PrimitiveNewInstancePerformance() {
			PrimitiveNewInstance<Boolean>();
			PrimitiveNewInstance<Byte>();
			PrimitiveNewInstance<SByte>();
			PrimitiveNewInstance<Int16>();
			PrimitiveNewInstance<UInt16>();
			PrimitiveNewInstance<Int32>();
			PrimitiveNewInstance<UInt32>();
			PrimitiveNewInstance<Int64>();
			PrimitiveNewInstance<UInt64>();
			PrimitiveNewInstance<IntPtr>();
			PrimitiveNewInstance<UIntPtr>();
			PrimitiveNewInstance<Char>();
			PrimitiveNewInstance<Double>();
			PrimitiveNewInstance<Single>();
        }

		static void PrimitiveNewInstance<T>() {
			var instance = New<T>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New<T>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks + " " + typeof(T).Name);
		}




		[TestMethod]
		public void ObjectNewInstancePerformance() {
			var instance = New<Object>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New<Object>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void WooNewInstancePerformance() {
			var instance = New<Woo>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New<Woo>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}



		[TestMethod]
		public void BazNewInstancePerformance() {
			var instance = New<Baz>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New<Baz>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}

		[TestMethod]
		public void NullableBazNewInstancePerformance() {
			var instance = New<Baz?>.Instance();
			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; i++)
				instance = New<Baz?>.Instance();
			sw.Stop();
			Console.WriteLine(sw.ElapsedTicks);
		}



		[TestMethod]
		public void Performance() {
			//using (var file = File.Create("results.csv"))
			//using (var streamWrites = new StreamWriter(file)){
				foreach (var test in new Action<Int32>[] {Perf<Int32>, Perf<Foo>, Perf<Bar>}) {
					//var list = new List<Int32>();
					foreach (var i in new[] {10, 100, 1000, 10000, 100000, 1000000}) {
						//list.Add(
						test(i);
						//);
					}
					//streamWrites.WriteLine(String.Join(",", list));
				}
			//}
		}

		void Perf<T>(Int32 limit) where T : new() {
			T instance;

			var sw = new Stopwatch();
			sw.Start();
			for (var i = 0; i != limit; ++i)
				instance = Activator.CreateInstance<T>();
			sw.Stop();
			var activator = sw.ElapsedTicks;

			sw.Restart();
			for (var i = 0; i != limit; ++i)
				instance = New<T>.Instance();
			sw.Stop();
			var newInstance = sw.ElapsedTicks;

			sw.Restart();
			for (var i = 0; i != limit; ++i)
				instance = new T();
			sw.Stop();
			var newT = sw.ElapsedTicks;

			sw.Restart();
			for (var i = 0; i != limit; ++i)
				instance = New2<T>.Instance();
			sw.Stop();
			var expressionNew = sw.ElapsedTicks;

			Console.WriteLine(typeof(T).Name);
			Console.WriteLine("Activator      " + activator);
			Console.WriteLine("new T()        " + newT);
			Console.WriteLine("Expression.New " + expressionNew);
			Console.WriteLine("New.Instance   " + newInstance);
			Console.WriteLine();

			//Assert.IsTrue(newInstance < expressionNew);
			//Assert.IsTrue(newInstance < activator);
		}
	}
}
