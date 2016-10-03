#pragma once
#include "stdafx.h"
#include <Windows.h>

class Injector
{
public:
	Injector(void);
	~Injector(void);

	bool Inject(char* procName,char* dllName);
	bool Inject(DWORD pID,char* dllName);
	DWORD GetTargetThreadIDFromProcName(const char * ProcName);
};

