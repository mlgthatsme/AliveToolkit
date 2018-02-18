/*
* Copyright (C) 2016, Michael Grima
* All rights reserved.
*/

#include "stdafx.h"
#include "Main.h"

#include "AeTypes.h"
#include "AliveApi.h"

#include "HookAPI.h"

AliveAPI::Game * mGame = new AliveAPI::Game();

#include "ClrFunctions.h"
#include "Hooks.h"

//#include "CustomObject.h"
#include "Reimplementation.h"
#include "XInputSupport.h"

int __cdecl Abe_LoadImage16(AE_Rect *a1, char *a2);
ALIVE_FUNC_IMPLEX(0x0, 0x004F5E20, Abe_LoadImage16, true);
int __cdecl Abe_LoadImage16(AE_Rect *a1, char *a2)
{
	SaveImage16(a1, a2);
	return Abe_LoadImage16_.Ptr()(a1, a2);
}

void Loop()
{
	Script_Init();

	static bool loadedSave = false;

	//if (GetAsyncKeyState('R'))
	//{
	//	printf("Particle\n");

	//	void * particle = malloc(248); 
	//	//BanLoadResource("MUDIDLE.BAN", 0);
	//	char * resource = BanGetLoadedResource('minA', 512, 0, 0);
	//	reinterpret_cast<void *(__thiscall*)(void *thisPtr, int xpos, int ypos, int a4, int a5, int a6, int a2)>(0x00403BE3)(particle, mGame->GetAbe()->mPosition.X, mGame->GetAbe()->mPosition.Y, 9640, 0, 0, (int)resource);
	//}

	//static bool objCreated = false;
	//if (!objCreated && GetAsyncKeyState('Q'))
	//{
	//	AE_abe * newObj = (AE_abe *)CreateCustomObject();
	//	objCreated = true;
	//}

	//printf("%i\n", gInputObj.mPressed);

	if (!loadedSave)
	{
		if (GetGameType() == 2) // Exoddus
		{
			std::ifstream stream = std::ifstream("debug.sav");
			if (stream.good())
			{
				char saveData[8192];
				stream.read(saveData, 8192);
				mGame->mWorld->LoadQuikSave(saveData);
				stream.close();
			}
			loadedSave = true;

			gPtrPauseMenu = j_Abe_CreatePauseMenu(malloc(616));
		}
	}

	CLROnTick();
}

SCRIPT_FUNCTION bool Raycast(int x1, int y1, int x2, int y2, int layer)
{
	_DWORD lineOut = 0;
	_DWORD xOut = 0;
	_DWORD yOut = 0;
	return reinterpret_cast<bool(__fastcall*)(int thisPtr, int _EDX, signed int a2, signed int a3, signed int a4, signed int a5, _DWORD *a6, _DWORD *a7, _DWORD *a8, int mode)>(0x00401258)
		(*(int*)0x005C1128, 0x04740000, x1, y1, x2, y2, &lineOut, &xOut, &yOut, layer);
}

int __fastcall LoopHook(void *thisPtr);
ALIVE_FUNC_IMPLEX(0x40DD20, 0x45F040, LoopHook, true);
int __fastcall LoopHook(void *thisPtr)
{
	int v = LoopHook_.Ptr()(thisPtr);

	Loop();
	
	
	return v;
}

void MLG_InitHook()
{
	printf("Alive Toolkit by mlgthatsme\n");

	int gameType = GetGameType();
	printf("Game Found: ");
	if (gameType == 1)
		printf("Oddworld: Abe's Oddysee\n");
	else if (gameType == 2)
		printf("Oddworld: Abe's Exoddus\n");
	else
		printf("Unknown Game !\n");

	printf("Installing hooks...\n");
	BaseFunction::HookAll();
	printf("Done!\n");

	gInputJoystickEnabled = true;
	gDDCheatEnabled = true;
	gDDCheatAlwaysRender = false;
	gDDCheatShowAI = false;
}