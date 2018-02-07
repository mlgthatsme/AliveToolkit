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
// Input Buttons
ALIVE_VAR(0x0, 0x5C9908, char *, gInputJoyStrB1);
ALIVE_VAR(0x0, 0x5C990C, char *, gInputJoyStrB2);
ALIVE_VAR(0x0, 0x5C9910, char *, gInputJoyStrB3);
ALIVE_VAR(0x0, 0x5C9914, char *, gInputJoyStrB4);
ALIVE_VAR(0x0, 0x5C9918, char *, gInputJoyStrB5);
ALIVE_VAR(0x0, 0x5C991C, char *, gInputJoyStrB6);
ALIVE_VAR(0x0, 0x5C9920, char *, gInputJoyStrB7);
ALIVE_VAR(0x0, 0x5C9924, char *, gInputJoyStrB8);
ALIVE_VAR(0x0, 0x5C9928, char *, gInputJoyStrB9);
ALIVE_VAR(0x0, 0x5C992C, char *, gInputJoyStrB0);
ALIVE_VAR(0x0, 0x00562D38, char, gMainMenuStrX);

// Globals
ALIVE_VAR(0x0, 0x5C9F70, void *, gPtrPauseMenu);

//Level Info
ALIVE_VAR(0x0, 0x005C3030, unsigned short, gCurrentLevel);
ALIVE_VAR(0x0, 0x005C3032, unsigned short, gCurrentPath);
ALIVE_VAR(0x0, 0x005C3034, unsigned short, gCurrentCam);



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
		}
	}
	
	char r = Abe_LoadString_.Ptr()(src, dst, a3, a4);
	printf("Abe_LoadString: Src: %x Dst: %x Str: %s StrOut: %s a4: %i\n", src, dst, src, dst, a4);
	return r;
}

int __fastcall Abe_RenderText(void *font,void* ecx, int a2, char *text, int x, int y, int renderInGameLayer, int a7, int a8, int a9, char a10, char a11, char a12, int a13, int a14, int a15, int a16);
ALIVE_FUNC_IMPLEX(0x0, 0x004337D0, Abe_RenderText, true);
int __fastcall Abe_RenderText(void *font, void* ecx, int a2, char *text, int x, int y, int renderInGameLayer, int a7, int a8, int a9, char a10, char a11, char a12, int a13, int a14, int a15, int a16)
{
	printf("Abe_RenderText: Addr: %x %s\n", text, text);
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