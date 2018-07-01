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

#include <ddraw.h>
#include "windowed/window_hooks.hpp"
#include "windowed/ddraw7proxy.hpp"

#include <mmsystem.h>
#include <dsound.h>

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

			gPtrPauseMenu = j_Abe_CreatePauseMenu(abe_malloc(616));
		}
	}

	CLROnTick();
}

int __fastcall LoopHook(void *thisPtr);
ALIVE_FUNC_IMPLEX(0x40DD20, 0x45F040, LoopHook, true);
int __fastcall LoopHook(void *thisPtr)
{
	int v = LoopHook_.Ptr()(thisPtr);

	Loop();
	
	
	return v;
}

/*static*/ DirectSurface7Proxy* DirectSurface7Proxy::g_Primary;
/*static*/ DirectSurface7Proxy* DirectSurface7Proxy::g_BackBuffer;
/*static*/ DirectSurface7Proxy* DirectSurface7Proxy::g_FakePrimary;

HRESULT __stdcall Hook_DirectDrawCreate(GUID *lpGUID, LPDIRECTDRAW *lplpDD, IUnknown *pUnkOuter);
ALIVE_FUNC_IMPLEX(0x0052C75C, 0x0052C75C, Hook_DirectDrawCreate, true);
HRESULT __stdcall Hook_DirectDrawCreate(GUID *lpGUID, LPDIRECTDRAW *lplpDD, IUnknown *pUnkOuter)
{
    const HRESULT ret = Hook_DirectDrawCreate_.Ptr()(lpGUID, lplpDD, pUnkOuter);
    if (SUCCEEDED(ret))
    {
        *lplpDD = new DirectDraw7Proxy(*lplpDD);

        SubClassWindow();
        PatchWindowTitle();
    }
    return ret;
}

ALIVE_FUNC_IMPLEX(0x0052C6DE, 0x0052C6DE, Hook_SetWindowLongA, true);
LONG WINAPI Hook_SetWindowLongA(HWND hWnd, int nIndex, LONG dwNewLong)
{
    if (nIndex == GWL_STYLE)
    {
        dwNewLong = WS_OVERLAPPEDWINDOW | WS_VISIBLE;
    }
    return Hook_SetWindowLongA_.Ptr()(hWnd, nIndex, dwNewLong);
}

struct IDirectSoundVtbl
{
    HRESULT(__stdcall *QueryInterface)(IDirectSound *This, const IID *const, LPVOID *);
    ULONG(__stdcall *AddRef)(IDirectSound *This);
    ULONG(__stdcall *Release)(IDirectSound *This);
    HRESULT(__stdcall *CreateSoundBuffer)(IDirectSound *This, LPCDSBUFFERDESC, LPDIRECTSOUNDBUFFER *, LPUNKNOWN);
    HRESULT(__stdcall *GetCaps)(IDirectSound *This, LPDSCAPS);
    HRESULT(__stdcall *DuplicateSoundBuffer)(IDirectSound *This, LPDIRECTSOUNDBUFFER, LPDIRECTSOUNDBUFFER *);
    HRESULT(__stdcall *SetCooperativeLevel)(IDirectSound *This, HWND, DWORD);
    HRESULT(__stdcall *Compact)(IDirectSound *This);
    HRESULT(__stdcall *GetSpeakerConfig)(IDirectSound *This, LPDWORD);
    HRESULT(__stdcall *SetSpeakerConfig)(IDirectSound *This, DWORD);
    HRESULT(__stdcall *Initialize)(IDirectSound *This, LPGUID);
};

class DirectSound8Proxy : public IDirectSound
{
private:
    IDirectSound* mDSound = nullptr;
    HWND mHwnd = 0;
public:
    DirectSound8Proxy(IDirectSound* pDSound)
        : mDSound(pDSound)
    {

    }

    /*** IUnknown methods ***/
    STDMETHOD(QueryInterface) (THIS_ REFIID riid, LPVOID FAR * ppvObj)
    {
        return mDSound->QueryInterface(riid, ppvObj);
    }

    STDMETHOD_(ULONG, AddRef) (THIS)
    {
        return mDSound->AddRef();
    }

    STDMETHOD_(ULONG, Release) (THIS)
    {
        return mDSound->Release();
    }

    /*** IDirectDraw methods ***/
    STDMETHOD(CreateSoundBuffer)(THIS_ LPCDSBUFFERDESC a1, _Outptr_ LPDIRECTSOUNDBUFFER *ppDSBuffer, _Pre_null_ LPUNKNOWN pUnkOuter)
    {
        return mDSound->CreateSoundBuffer(a1, ppDSBuffer, pUnkOuter);
    }

    STDMETHOD(GetCaps)(THIS_ _Out_ LPDSCAPS pDSCaps)
    {
        return mDSound->GetCaps(pDSCaps);
    }

    STDMETHOD(DuplicateSoundBuffer)(THIS_ _In_ LPDIRECTSOUNDBUFFER pDSBufferOriginal, _Outptr_ LPDIRECTSOUNDBUFFER *ppDSBufferDuplicate)
    {
        return mDSound->DuplicateSoundBuffer(pDSBufferOriginal, ppDSBufferDuplicate);
    }

    STDMETHOD(SetCooperativeLevel)(THIS_ HWND hwnd, DWORD dwLevel)
    {
        return mDSound->SetCooperativeLevel(hwnd, DSSCL_PRIORITY);
    }

    STDMETHOD(Compact)(THIS)
    {
        return mDSound->Compact();
    }

    STDMETHOD(GetSpeakerConfig)(THIS_ _Out_ LPDWORD pdwSpeakerConfig)
    {
        return mDSound->GetSpeakerConfig(pdwSpeakerConfig);
    }

    STDMETHOD(SetSpeakerConfig)(THIS_ DWORD dwSpeakerConfig)
    {
        return mDSound->SetSpeakerConfig(dwSpeakerConfig);
    }

    STDMETHOD(Initialize)(THIS_ _In_opt_ LPCGUID pcGuidDevice)
    {
        return mDSound->Initialize(pcGuidDevice);
    }
};


HRESULT __stdcall Hook_DirectSoundCreate(LPGUID a1, LPDIRECTSOUND *a2, LPUNKNOWN a3);
ALIVE_FUNC_IMPLEX(0x0052C762, 0x0052C762, Hook_DirectSoundCreate, true);
HRESULT __stdcall Hook_DirectSoundCreate(LPGUID a1, LPDIRECTSOUND *a2, LPUNKNOWN a3)
{
    const HRESULT ret = Hook_DirectSoundCreate_.Ptr()(a1, a2, a3);
    if (SUCCEEDED(ret))
    {
        *a2 = new DirectSound8Proxy(*a2);
    }
    return ret;
}

signed int __cdecl SND_CreateDS_4EEAA0(unsigned int a1, int a2, int a3);
ALIVE_FUNC_IMPLEX(0x4EEAA0, 0x4EEAA0, SND_CreateDS_4EEAA0, true);
signed int __cdecl SND_CreateDS_4EEAA0(unsigned int a1, int a2, int a3)
{
    a1 = 8000;
    return SND_CreateDS_4EEAA0_.Ptr()(a1, a2, a3);
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