/*
* Copyright (C) 2016, Michael Grima
* All rights reserved.
*/

// DLLInjector.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "Injector.h"
#include <iostream>

#define PROC_NAME "Exoddus.exe" 

Injector * injector = new Injector();

void StartProcess()
{
	DWORD procID = injector->GetTargetThreadIDFromProcName("exoddus.exe");
	if (procID == 0)
	{
		STARTUPINFO si = { sizeof(STARTUPINFO) };
		si.cb = sizeof(si);
		PROCESS_INFORMATION pi;


		CreateProcess("exoddus.exe", NULL, NULL, NULL, FALSE, CREATE_NO_WINDOW, NULL, NULL, &si, &pi);
		Sleep(500);
	}
}

void InjectDll(const char * file)
{
	TCHAR currentDir[MAX_PATH];
	TCHAR dllDir[MAX_PATH];
	GetCurrentDirectory(MAX_PATH, currentDir);

	strcpy(dllDir, currentDir);
	strcat(dllDir, file);

	if (injector->Inject(PROC_NAME, dllDir)) {
		printf("DLL injected!\n");
	}
	else {
		printf("Failed to inject dll...\n");
	}
}

int _tmain(int argc, _TCHAR* argv[])
{
	StartProcess();

	InjectDll("\\TestHook.dll");

	return 0;
}