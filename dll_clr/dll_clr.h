// dll_clr.h

#pragma once

#include "dll_cpp.h"

using namespace System;

namespace dll_clr {

	public ref class DllClr
	{
		// TODO:  在此处添加此类的方法。
		public:
			int Add(int a, int b) {
				Cdll_cpp cpp;
				return cpp.Add(a, b);
			}

			long Fibonacci(long num)
			{
				Cdll_cpp cpp;
				return cpp.Fibonacci(num);
			}

			//阶乘
			long Factorial(long num)
			{
				Cdll_cpp cpp;
				return cpp.Factorial(num);
			}
	};
}
