#pragma once

#include "stdafx.h"

// If Oddysee's pointers are unknown just write em here lel.
char AO_Dump[1024];
#define AO_UNKNOWN (int)&AO_Dump

// Abe Functions
ALIVE_FUNC_NOT_IMPL(0x0, 0x00402CA2, void *__fastcall (void * a2), j_Abe_CreatePauseMenu);
//ALIVE_FUNC_NOT_IMPL(0x0, 0x00404390, void *__fastcall (void * thisPtr, int a1, int a2, int a3), Abe_ObjectInitFont);
//ALIVE_FUNC_NOT_IMPL(0x0, 0x004014C4, void *__fastcall (void *thisPtr, int a2, char *a3, int a4, short a5, int a6, int a7, int a8, int a9, char a10, char a11, char a12, int a13, int a14, int a15, int a16), Abe_RenderText);

// Abe Variables
// DDCheat
ALIVE_VAR(0x00508BF8, 0x005CA4B5, bool, gDDCheatEnabled);
ALIVE_VAR(AO_UNKNOWN, 0x005BC000, bool, gDDCheatAlwaysRender);
ALIVE_VAR(AO_UNKNOWN, 0x005C1BD8, bool, gDDCheatShowAI);
ALIVE_VAR(AO_UNKNOWN, 0x005C2C08, bool, gDDCheatIsFlying);

// Input
ALIVE_VAR(AO_UNKNOWN, 0x5C9F70, bool, gInputJoystickEnabled);
ALIVE_VAR(AO_UNKNOWN, 0x005BD4E0, InputPadObject, gInputObj);
ALIVE_VAR(AO_UNKNOWN, 0x0055E85C, char, gInputGamepadString);
// Input Buttons
ALIVE_VAR(AO_UNKNOWN, 0x5C9908, char *, gInputJoyStrB1);
ALIVE_VAR(AO_UNKNOWN, 0x5C990C, char *, gInputJoyStrB2);
ALIVE_VAR(AO_UNKNOWN, 0x5C9910, char *, gInputJoyStrB3);
ALIVE_VAR(AO_UNKNOWN, 0x5C9914, char *, gInputJoyStrB4);
ALIVE_VAR(AO_UNKNOWN, 0x5C9918, char *, gInputJoyStrB5);
ALIVE_VAR(AO_UNKNOWN, 0x5C991C, char *, gInputJoyStrB6);
ALIVE_VAR(AO_UNKNOWN, 0x5C9920, char *, gInputJoyStrB7);
ALIVE_VAR(AO_UNKNOWN, 0x5C9924, char *, gInputJoyStrB8);
ALIVE_VAR(AO_UNKNOWN, 0x5C9928, char *, gInputJoyStrB9);
ALIVE_VAR(AO_UNKNOWN, 0x5C992C, char *, gInputJoyStrB0);

// Globals
ALIVE_VAR(0x0, 0x005C9300, void *, gPtrPauseMenu);

//Level Info
ALIVE_VAR(0x0, 0x005C3030, unsigned short, gCurrentLevel);
ALIVE_VAR(0x0, 0x005C3032, unsigned short, gCurrentPath);
ALIVE_VAR(0x0, 0x005C3034, unsigned short, gCurrentCam);


signed __int16 __fastcall Abe_LoadCam(AE_WorldInfo *thisPtr, void *ecx, __int16 level, __int16 path, __int16 cam, __int16 transition, __int16 a6, __int16 a7);
ALIVE_FUNC_IMPLEX(0x0, 0x00480D30, Abe_LoadCam, true);
signed __int16 __fastcall Abe_LoadCam(AE_WorldInfo *thisPtr, void *ecx, __int16 level, __int16 path, __int16 cam, __int16 transition, __int16 a6, __int16 a7)
{
	if (gAppEnableLog)
		printf("Abe_LoadCam: [%x] Level: %i Path: %i Cam: %i Transition: %i a6: %i a7: %i\n", thisPtr, level, path, cam, transition, a6, a7);

	return Abe_LoadCam_.Ptr()(thisPtr, ecx, level, path, cam, transition, a6, a7);
}

void sub_4DD050();
ALIVE_FUNC_IMPLEX(0x0, 0x004DD050, sub_4DD050, true);
void  sub_4DD050()
{
	reinterpret_cast<char *(__fastcall*)(int thisPtr, int ecx,int a1, int a2, int a3, int a4)>(0x0040EC10)(0x005BB5F4, 0x005BB5F4, 0, 0, 640, 240); // Forces screen refresh.
	sub_4DD050_.Ptr()();
}

// Disable Music
int __fastcall AbeMusicManagerUpdate(void *thisPtr);
ALIVE_FUNC_IMPLEX(0x0, 0x47F730, AbeMusicManagerUpdate, true);
int __fastcall AbeMusicManagerUpdate(void *thisPtr)
{
	if (IsMusicEnabled())
		return AbeMusicManagerUpdate_.Ptr()(thisPtr);
	else
		return 0;
}

unsigned char blueLcdPal[32] =
{
	0x00, 0x00, 0x01, 0x80, 0x01, 0x84, 0x20, 0x84, 0x21, 0x80, 0x20, 0x84, 0x21, 0x84, 0x65, 0xCE,
	0x65, 0x8C, 0x8C, /**/0xB1, 0x03/**/, 0xE0, 0x64, 0xCE, 0x65, 0xCE, 0xD7, 0x98, 0x14, 0xA1, 0x18, 0xD8,
};

_DWORD *__fastcall Abe_ObjectInitFont(AE_Font *thisPtr, void*ecx, int a2, int pallete, int a4);
ALIVE_FUNC_IMPLEX(0x0, 0x00433590, Abe_ObjectInitFont, true);
_DWORD *__fastcall Abe_ObjectInitFont(AE_Font *thisPtr,void*ecx, int a2, int pallete, int a4)
{
	//return Abe_ObjectInitFont_.Ptr()(thisPtr, ecx, 240, (int)0x560F80, 0x5BC5C8);
	//return Abe_ObjectInitFont_.Ptr()(thisPtr, ecx, a2, (int)blueLcdPal, a4);

	if (gAppEnableLog)
		printf("Abe_ObjectInitFont: [%X] a2: %i pallete: %x data: %x\n", thisPtr, a2, pallete, a4);

	return Abe_ObjectInitFont_.Ptr()(thisPtr, ecx, a2, pallete, a4);
}

// Fixes the infamous Stereo swap bug that the PC port of Abe's Exoddus has.
int __cdecl SND_PlayMidiNote(int a1, int program, int note, int leftVolume, int rightVolume, int volume);
ALIVE_FUNC_IMPLEX(0x0, 0x004FCB30, SND_PlayMidiNote, true);
int __cdecl SND_PlayMidiNote(int a1, int program, int note, int leftVolume, int rightVolume, int volume)
{
	int noteConverted = (note >> 8) & 0x7F;

	if (gAppEnableLog)
		printf("SND_PlayMidiNote: A1: %X A2: %i A3: %i A4: %i A5: %i Vol:%i\n", a1, program, noteConverted, leftVolume, rightVolume, volume);

	int swap1 = rightVolume;
	rightVolume = leftVolume;
	leftVolume = swap1;

	//program = 17;
	//note = 36 << 8;

	return SND_PlayMidiNote_.Ptr()(a1, program, note, rightVolume, rightVolume, volume);
}

signed int __cdecl Abe_MusicParseMidiMessage(int a1);
ALIVE_FUNC_IMPLEX(0x0, 0x004FD100, Abe_MusicParseMidiMessage, true);
signed int __cdecl Abe_MusicParseMidiMessage(int a1)
{
	//printf("Abe_MusicParseMidiMessage: %X %i\n", a1, a1);
	return Abe_MusicParseMidiMessage_.Ptr()(a1);
}

signed __int16 j_Abe_LoadSave_(void);
ALIVE_FUNC_IMPLEX(0x0, 0x004022A2, j_Abe_LoadSave_, true);
signed __int16 j_Abe_LoadSave_(void)
{
	if (gAppEnableLog)
		printf("Abe_LoadSave CALLED\n");

	return j_Abe_LoadSave__.Ptr()();
}

void *__cdecl j_Abe_GenerateSave(int a1);
ALIVE_FUNC_IMPLEX(0x0, 0x004029EB, j_Abe_GenerateSave, true);
void *__cdecl j_Abe_GenerateSave(int a1)
{
	if (gAppEnableLog)
		printf("Abe_GenerateSave CALLED\n");

	return j_Abe_GenerateSave_.Ptr()(a1);
}

// Use these to find out where strings being rendered are located.
char __cdecl Abe_LoadString(char *src, char *dst, int a3, char a4);
ALIVE_FUNC_IMPLEX(0x0, 0x004969D0, Abe_LoadString, true);
char __cdecl Abe_LoadString(char *src, char *dst, int a3, char a4)
{
	if (gInputJoystickEnabled)
	{
		if (gCurrentLevel == 0)
		{
			if (!strcmp(src, "esc"))
				src = "";
			else if (!strcmp(src, "x"))
				src = "";
		}
	}
	
	char r = Abe_LoadString_.Ptr()(src, dst, a3, a4);

	//if (gAppEnableLog)
	//	printf("Abe_LoadString: Src: %x Dst: %x Str: %s StrOut: %s a4: %i\n", src, dst, src, dst, a4);

	return r;
}

int __fastcall Abe_RenderText(void *font,void* ecx, int a2, char *text, int x, int y, int renderInGameLayer, int a7, int a8, int a9, char a10, char a11, char a12, int a13, int a14, int a15, int a16);
ALIVE_FUNC_IMPLEX(0x0, 0x004337D0, Abe_RenderText, true);
int __fastcall Abe_RenderText(void *font, void* ecx, int a2, char *text, int x, int y, int renderInGameLayer, int a7, int a8, int a9, char a10, char a11, char a12, int a13, int a14, int a15, int a16)
{
	//if (gAppEnableLog)
	//	printf("Abe_RenderText: Addr: %x %s\n", text, text);

	return Abe_RenderText_.Ptr()(font, 0, a2, text, x, y, renderInGameLayer, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16);
}

int __cdecl Abe_InputGetPressed(int playerIndex);
ALIVE_FUNC_IMPLEX(0x0, 0x004FA9C0, Abe_InputGetPressed, true);
int __cdecl Abe_InputGetPressed(int playerIndex)
{
	return CLROnInput(Abe_InputGetPressed_.Ptr()(playerIndex), playerIndex);
}

signed int __cdecl AbeBMPNew(_DWORD *a1, int a2, int a3, int a4, int a5);
ALIVE_FUNC_IMPLEX(0x0, 0x004F1990, AbeBMPNew, true);
signed int __cdecl AbeBMPNew(_DWORD *a1, int a2, int a3, int a4, int a5)
{
	//printf("BMPNew: %X %i %i %i %i\n", a1, a2, a3, a4, a5);
	return AbeBMPNew_.Ptr()(a1, a2, a3, a4, a5);
}

signed int __cdecl GPU_LoadImage(AE_Rect *a1, char * a2);
ALIVE_FUNC_IMPLEX(0x0, 0x004F5E90, GPU_LoadImage, true);
signed int __cdecl GPU_LoadImage(AE_Rect *a1, char * a2)
{
	//printf("GPU Load Image: Rect: %i %i %i %i\n", a1->X, a1->Y, a1->Width, a1->Height);
	return GPU_LoadImage_.Ptr()(a1, a2);
}

signed int __cdecl sub_4F5BD0(AE_Rect *a1, unsigned __int8 a2, unsigned __int8 a3, unsigned __int8 a4);
ALIVE_FUNC_IMPLEX(0x0, 0x004F5BD0, sub_4F5BD0, true);
signed int __cdecl sub_4F5BD0(AE_Rect *a1, unsigned __int8 a2, unsigned __int8 a3, unsigned __int8 a4)
{
	//printf("sub_4F5BD0: Rect: %i %i %i %i\n", a1->X, a1->Y, a1->Width, a1->Height);
	return sub_4F5BD0_.Ptr()(a1, a2, a3, a4);
}


_DWORD * __cdecl  Abe_BlitScreen(const void *a1, char a2);
ALIVE_FUNC_IMPLEX(0x0, 0x004F5640, Abe_BlitScreen, true);
_DWORD * __cdecl  Abe_BlitScreen(const void *a1, char a2)
{
	CLROnDebugDraw();
	return Abe_BlitScreen_.Ptr()(a1, a2);
}

// Disable original ddcheat rendering
_DWORD *__cdecl Abe_DebugDrawString(int hdc, int x, int y, int color, int a5, LPCSTR lpString);
ALIVE_FUNC_IMPLEX(0x0, 0x004F2230, Abe_DebugDrawString, true);
_DWORD *__cdecl Abe_DebugDrawString(int hdc, int x, int y, int color, int a5, LPCSTR lpString)
{
	return 0;
}

// Steal raycasts for debugging
bool __fastcall Hook_Raycast(void *thisPtr, void * _EDX, signed int a2, signed int a3, signed int a4, signed int a5, _DWORD *a6, _DWORD *a7, _DWORD *a8, int mode);
ALIVE_FUNC_IMPLEX(0x0, 0x00401258, Hook_Raycast, true);
bool __fastcall Hook_Raycast(void *thisPtr, void * _EDX, signed int a2, signed int a3, signed int a4, signed int a5, _DWORD *a6, _DWORD *a7, _DWORD *a8, int mode)
{
	bool r = Hook_Raycast_.Ptr()(thisPtr, _EDX, a2, a3, a4, a5, a6, a7, a8, mode);// Abe_Raycast((AE_PathList *)thisPtr, a2, a3, a4, a5, (AE_pathLine **)a6, a7, a8, mode);
	AddRaycastEntry(r, a2, a3, a4, a5, *a6, *a7, *a8, mode);
	return r;
}

signed __int16 __cdecl Abe_LoadResource(char *resourceFilename, int a2);
ALIVE_FUNC_IMPLEX(0x0, 0x0049C170, Abe_LoadResource, true);
signed __int16 __cdecl Abe_LoadResource(char *resourceFilename, int a2)
{
	if (gAppEnableLog)
		printf("Loading Resource File: %s\n", resourceFilename);

	return Abe_LoadResource_.Ptr()(resourceFilename, a2);
}

signed int __cdecl Abe_LoadImage(AE_Rect *a1, char *a2);
ALIVE_FUNC_IMPLEX(0x0, 0x004F5FB0, Abe_LoadImage, true);
signed int __cdecl Abe_LoadImage(AE_Rect *a1, char *a2)
{
	//printf("Load Image: Rect: %i %i %i %i\n", a1->X, a1->Y, a1->Width, a1->Height);
	return Abe_LoadImage_.Ptr()(a1, a2);
}

//////////////////////
// MEMORY FUNCTIONS
//////////////////////
void *__cdecl abe_malloc(size_t a1);
ALIVE_FUNC_IMPLEX(0x0, 0x005212C0, abe_malloc, true)
void *__cdecl abe_malloc(size_t a1)
{
	void * r = malloc(a1 + 4);
	((int *)r)[0] = a1;
	return (void*)(((int)r) + 4);
}
//////////////////////