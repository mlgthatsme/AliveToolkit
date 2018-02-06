#pragma once

// Abe Functions
ALIVE_FUNC_NOT_IMPL(0x0, 0x00402CA2, void *__fastcall (void * a2), j_Abe_CreatePauseMenu);
//ALIVE_FUNC_NOT_IMPL(0x0, 0x00404390, void *__fastcall (void * thisPtr, int a1, int a2, int a3), Abe_ObjectInitFont);
//ALIVE_FUNC_NOT_IMPL(0x0, 0x004014C4, void *__fastcall (void *thisPtr, int a2, char *a3, int a4, short a5, int a6, int a7, int a8, int a9, char a10, char a11, char a12, int a13, int a14, int a15, int a16), Abe_RenderText);

// Abe Variables
// DDCheat
ALIVE_VAR(0x00508BF8, 0x005CA4B5, bool, gDDCheatEnabled);
ALIVE_VAR(0x0, 0x005BC000, bool, gDDCheatAlwaysRender);
ALIVE_VAR(0x0, 0x005C1BD8, bool, gDDCheatShowAI);
ALIVE_VAR(0x0, 0x005C2C08, bool, gDDCheatIsFlying);

// Input
ALIVE_VAR(0x0, 0x5C9F70, bool, gInputJoystickEnabled);
ALIVE_VAR(0x0, 0x005BD4E0, InputPadObject, gInputObj);

// Disable Music
int __fastcall AbeMusicManagerUpdate(void *thisPtr);
ALIVE_FUNC_IMPLEX(0x0, 0x47F730, AbeMusicManagerUpdate, true);
int __fastcall AbeMusicManagerUpdate(void *thisPtr)
{
	return 0;
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
	auto r = Abe_BlitScreen_.Ptr()(a1, a2);
	return r;
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