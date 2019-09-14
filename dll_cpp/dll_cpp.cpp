// dll_cpp.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "dll_cpp.h"

int __stdcall testAdd(int a, int b) {
	return a + b;
}

int __stdcall testMulti(int a, int b) {
	return a * b;
}


