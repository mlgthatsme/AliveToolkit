/*
* Copyright (C) 2016, Michael Grima
* All rights reserved.
*/

#include "stdafx.h"
#include "TestHook.h"

AliveAPI::Game * mGame = new AliveAPI::Game();

#include <Xinput.h>

#include "game_functions.hpp"
#include "Hooks.h"
#include "ClrFunctions.h"
#include "CustomObject.h"
#include "Reimplementation.h"
#include "XInputSupport.h"

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
			SetupCustomObjectVTable();

			std::ifstream stream = std::ifstream("debug.sav");
			if (stream.good())
			{
				char saveData[8192];
				stream.read(saveData, 8192);
				mGame->mWorld->LoadQuikSave(saveData);
				stream.close();
			}
			loadedSave = true;

			void * mainMenuPtr = (void*)0x5C9F70;
			
			if (mainMenuPtr == 0)
			{
				mainMenuPtr = j_Abe_CreatePauseMenu(malloc(268));
			}
		}
	}

	CLROnTick();
}

int __fastcall LoopHook(void *thisPtr);
ALIVE_FUNC_IMPLEX(0x40DD20, 0x45F040, LoopHook, true);
int __fastcall LoopHook(void *thisPtr)
{
	Loop();
	return LoopHook_.Ptr()(thisPtr);
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

	printf("Installing hooks...");
	BaseFunction::HookAll();
	printf("Done!\n");

	gInputJoystickEnabled = true;
	gDDCheatEnabled = true;
	gDDCheatAlwaysRender = true;
	gDDCheatShowAI = false;
}