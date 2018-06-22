/*
* Copyright (C) 2016, Michael Grima
* All rights reserved.
*/

// DLLInjector.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "Injector.h"
#include <iostream>
#include <fstream>

Injector * injector = new Injector();
std::string argString;

HANDLE StartProcess(const char * target)
{
	DWORD procID = injector->GetTargetThreadIDFromProcName(target);
	if (procID == 0)
	{
		STARTUPINFO si = { sizeof(STARTUPINFO) };
		si.cb = sizeof(si);
		PROCESS_INFORMATION pi;

		CreateProcess(target, const_cast<char *>(argString.c_str()), NULL, NULL, FALSE, CREATE_SUSPENDED, NULL, NULL, &si, &pi);
        return pi.hThread;
	}
}

void InjectDll(const char * file, char * target)
{
	TCHAR currentDir[MAX_PATH];
	TCHAR dllDir[MAX_PATH];
	GetCurrentDirectory(MAX_PATH, currentDir);

	strcpy(dllDir, currentDir);
	strcat(dllDir, file);

	if (injector->Inject(target, dllDir)) {
		printf("DLL injected!\n");
	}
	else {
		printf("Failed to inject dll...\n");

		std::cin.get();
	}
}

int _tmain(int argc, _TCHAR* argv[])
{
    argString = "";

    for (int i = 1; i < argc; i++)
    {
        argString += std::string(argv[i]) + " ";
    }

    HANDLE gameThread = nullptr;

	if (std::ifstream("Exoddus.exe"))
	{
        gameThread = StartProcess("Exoddus.exe");
		InjectDll("\\TestHook.dll", "Exoddus.exe");
	}
	else if (std::ifstream("AbeWin.exe"))
	{
        gameThread = StartProcess("AbeWin.exe");
		InjectDll("\\TestHook.dll", "AbeWin.exe");
	}
	else
	{
		printf("Could not find Exoddus.exe or AbeWin.exe");
		std::cin.get();
	}

    Sleep(100);

    if (gameThread != nullptr)
        ResumeThread(gameThread);

	return 0;
}