/*
* Copyright (C) 2016, Michael Grima
* All rights reserved.
*/

#include "stdafx.h"
#include "Main.h"

static bool hookInitDone = false;

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		if (!hookInitDone)
		{
			hookInitDone = true;

			AllocConsole();
			freopen("CONOUT$", "w", stdout);
			SetConsoleTitle(L"ALIVE hook debug console");
			SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), FOREGROUND_GREEN | FOREGROUND_BLUE | FOREGROUND_RED);

			MLG_InitHook();
		}
		break;
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}

