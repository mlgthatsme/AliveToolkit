/*
* Copyright (C) 2016, Michael Grima
* All rights reserved.
*/

#include "stdafx.h"
#include "TestHook.h"

#include "ClrFunctions.h"

AliveAPI::Game * mGame = new AliveAPI::Game();

typedef AE_abe *(__cdecl* T_PathCreateObject)(int a1, int a3, __int16 a4);

int * keyboardInput = reinterpret_cast<int*>(0x005BD4EC);
bool * ddcheatFlying = reinterpret_cast<bool*>(0x005C2C08); // DDCheat fly Enabled

template <typename T>
inline MH_STATUS MH_CreateHookEx(LPVOID pTarget, LPVOID pDetour, T** ppOriginal)
{
	return MH_CreateHook(pTarget, pDetour, reinterpret_cast<LPVOID*>(ppOriginal));
}

template <typename T>
inline MH_STATUS MH_CreateHookApiEx(
	LPCWSTR pszModule, LPCSTR pszProcName, LPVOID pDetour, T** ppOriginal)
{
	return MH_CreateHookApi(
		pszModule, pszProcName, pDetour, reinterpret_cast<LPVOID*>(ppOriginal));
}

// Types
template<class FunctionType>
class FunctionHook
{
public:
	FunctionHook(DWORD func) : funcPointer(reinterpret_cast<FunctionType>(func))
	{
		originalFunc = reinterpret_cast<FunctionType>(func);
	}

	void Install(FunctionType newFunc)
	{
		printf("Hook %s\n", typeid(FunctionHook).name());
		MH_STATUS r1 = MH_CreateHook(funcPointer, newFunc, (LPVOID*)&originalFunc);
		MH_STATUS r2 = MH_EnableHook(funcPointer);

		if (r1)
			printf("Failed to create hook: %s\n", MH_StatusToString(r1));
		if (r2)
			printf("Failed to enable hook: %s\n", MH_StatusToString(r2));
	}

	FunctionType Real() const
	{
		return reinterpret_cast<FunctionType>(originalFunc);
	}

private:

	FunctionType originalFunc;
	FunctionType funcPointer;
};

typedef int( __thiscall* T_Abe_RenderText)(void *thisPtr, int a2, char *a3, int a4, short a5, int a6, int a7, int a8, int a9, char a10, char a11, char a12, int a13, int a14, int a15, int a16);

void Loop();

typedef char(__thiscall* T_LoopHook)(void * thisptr);
FunctionHook<T_LoopHook> H_LoopHook(0x45F040);
int __fastcall Hook_LoopHook(void *thisPtr)
{
	Loop();
	return H_LoopHook.Real()(thisPtr);
}

typedef _DWORD *(__thiscall* T_LoadFnt)(void * thisPtr, int a1, int a2, int a3);
T_LoadFnt LoadFnt = reinterpret_cast<T_LoadFnt>(0x00404390);

class CustomAEObject
{
public:

#define DBGPRINTFUNC printf(__FUNCTION__ "\n")

	__int16 type = 180;
	_BYTE objectMode;
	char field_7;
	char gap8;
	int field_C;
	char *field_10;
	int field_14;
	_BYTE gap18[14];
	char field_26;
	__declspec(align(2)) char field_28;
	char gap29;
	__int16 field_2A;
	__int16 field_2C;
	_BYTE gap2E[6];
	__int16 field_34;
	_BYTE gap36[6];
	int field_3C;
	_BYTE gap40[20];
	int field_54;
	_BYTE gap58[96];
	int position_x;
	int position_y;
	char field_C0;
	int velocity_x;
	int velocity_y;
	int scale;
	char color_r;
	__declspec(align(2)) char color_g;
	__declspec(align(2)) char color_b;
	__declspec(align(2)) char layer;
	__int16 sprite_offset_x;
	__int16 sprite_offset_y;
	_BYTE gapDC[28];
	int field_F8;
	_BYTE gapFC[4];
	int field_100;
	__int16 gap104;
	__int16 alive_state;
	_BYTE gap108[40];
	int field_130;
	int field_134;
	__int16 field_138;
	_BYTE gap13A[253];
	__declspec(align(1)) __int16 gap10A;

	Ae_fonttype font;

	CustomAEObject()
	{
		objectMode = 8; // Sets mode to update

		LoadFnt(&font, 256, (int)0x554474, (int)0x5BC5C8);
	}

	virtual void * Func1(char a1) // Constructer?
	{
		DBGPRINTFUNC;
		if (a1)
			free(this);
		
		return this;
	}

	virtual int Func2()
	{
		DBGPRINTFUNC;
		return 0;
	}

	virtual void Update(int a1)
	{
		bool isGamePaused = *(bool*)0x005C1B66;

		int renderOffsetX = *(int*)(*(int*)((*(int*)0x5BB5F4) + 32));
		int renderOffsetY = *(int*)((*(int*)((*(int*)0x5BB5F4) + 32)) + 4);

		int targetX = mGame->GetAbe()->position_x;
		int targetY = mGame->GetAbe()->position_y - FLOAT_TO_FIXED(50) - FLOAT_TO_FIXED((sinf(mGame->GetgnFrame() / 50.0f) * 5));

		if (!isGamePaused)
		{
			position_x = Lerp<int>(position_x, targetX, 0.1f);
			position_y = Lerp<int>(position_y, targetY, 0.1f);
		}

		if (mGame->GetgnFrame() % 50 == 0)
		{
			if (!BanGetLoadedResource('minA', 301, 0, 0))
				BanLoadResource("EXPLO2.BAN", 0);

			for (int o = 0; o < mGame->mWorld->Objects->mCount; o++)
			{
				AE_abe *obj = (AE_abe *)mGame->mWorld->Objects->mData[o];

				if ((int)obj->vtable == 0x005462E4 || (int)obj->vtable == 0x00547460)
				{
					// Shock Line
					void * shock = malloc(154);
					mGame->mWorld->SpawnShockLine(FIXED_TO_FLOAT(position_x), FIXED_TO_FLOAT(position_y), FIXED_TO_FLOAT(obj->position_x), FIXED_TO_FLOAT(obj->position_y));
					
					// Sparks
					void * sparks = malloc(264);
					reinterpret_cast<AE_abe *(__thiscall*)(void *thisPtr, int a2, int a3, __int16 a4, int a5, int a6, int a7)>(0x00403373)(sparks, position_x, position_y, 10, obj->scale, 3, 11);

					if (!BanGetLoadedResource('minA', 6005, 0, 0))
						BanLoadResource("GRENADE.BAN", 0);
					if (!BanGetLoadedResource(1835626049, 372, 0, 0))
						BanLoadResource("SMEXP.BAN", 0);
					if (!BanGetLoadedResource(1835626049, 365, 0, 0))
						BanLoadResource("METAL.BAN", 0);

					AE_abe* grenade = reinterpret_cast<AE_abe*(__thiscall*)(void * thisPtr, int a2, int a3, __int16 a4, __int16 a5, __int16 a6, void * a7)>(0x402874)(malloc(316), obj->position_x, obj->position_y, 0, 1, 0, this);
					grenade->scale = obj->scale;
					grenade->layer = obj->layer;
					(*((void(__thiscall **)(void *, int, unsigned int))grenade->vtable + 24))((void*)grenade, 0, 0);
					break;
				}
			}
		}
		
		static T_Abe_RenderText renderFunc = reinterpret_cast<T_Abe_RenderText>(0x004014C4);
		renderFunc(&font, 0x005C11B0, "o", (position_x - renderOffsetX) / 0x10000, (position_y - renderOffsetY) / 0x10000, 0, 1, 0, 41, 233, 233, 233, 8, 0x10000, 640, 0);
	}

	virtual short Func4()
	{
		DBGPRINTFUNC;
		//objectMode = 4; // Sets to be destroyed
		
		return *(int*)(0x005C3030);
	}

	virtual void Func5()
	{
		DBGPRINTFUNC;
	}

	virtual int SaveObject(unsigned char * saveBuffer) // 6
	{
		DBGPRINTFUNC;
		return 0;
	}

	virtual short Func7(int a2, int a3, int a4, int a5, int(__thiscall *a6)(_WORD *, int))
	{
		DBGPRINTFUNC;
		return 0;
	}

	virtual int Func8(int a2, __int16 a3)
	{
		DBGPRINTFUNC;
		return 0;
	}

	virtual void NullThing()
	{
		DBGPRINTFUNC;
	}

	virtual int Func10(int a1)
	{
		DBGPRINTFUNC;
		return 0;
	}

	virtual void NullThing2()
	{
		DBGPRINTFUNC;
	}

	virtual int Func12(int a1)
	{
		DBGPRINTFUNC;
		return 0;
	}

	virtual int Func13(unsigned short a1)
	{
		DBGPRINTFUNC;
		return 0;
	}

	virtual void Func14()
	{
		DBGPRINTFUNC;
	}

	virtual void Func15()
	{
		DBGPRINTFUNC;
	}

	virtual int Func16()
	{
		DBGPRINTFUNC;
		return 0;
	}

	virtual int Func17()
	{
		DBGPRINTFUNC;
		return 0;
	}

	virtual int Func18(__int16 a1)
	{
		DBGPRINTFUNC;
		gap108[12];
		alive_state = a1;
		return a1;
	}

	virtual int Func19(__int16 a2, __int16 a3, int a4)
	{
		DBGPRINTFUNC;
		return 0;
	}

	virtual short Func20(int a1)
	{
		DBGPRINTFUNC;
		return 0;
	}

	virtual void Func21(int a1)
	{
		DBGPRINTFUNC;
	}

	virtual char Func22(short a1)
	{
		DBGPRINTFUNC;
		return 0;
	}

	virtual int Func23(short a1)
	{
		DBGPRINTFUNC;
		return 0;
	}

	virtual int Func24()
	{
		DBGPRINTFUNC;
		return 0;
	}

	virtual bool Func25()
	{
		DBGPRINTFUNC;
		return false;
	}

	virtual int Func26()
	{
		DBGPRINTFUNC;
		return 0;
	}

	virtual int Func27()
	{
		DBGPRINTFUNC;
		return 0;
	}
};

void Loop()
{
	Script_Init();

	static bool loadedSave = false;

	if (!loadedSave)
	{
		std::ifstream stream = std::ifstream("debug.sav");
		char saveData[8192];
		stream.read(saveData, 8192);
		mGame->mWorld->LoadQuikSave(saveData);
		stream.close();
		loadedSave = true;
	}

	CLROnTick();
}

typedef void *(__cdecl* T_abe_malloc)(size_t a1);
FunctionHook<T_abe_malloc> H_abe_malloc(0x005212C0);
void * __cdecl Hook_abe_malloc(size_t a1)
{
	void * allocatedMem = malloc(a1 + sizeof(int));
	((int *)allocatedMem)[0] = a1;
	AddAllocationEntry((int)allocatedMem + sizeof(int), (int)a1, 0);
	return (void*)((int)allocatedMem + sizeof(int));
}

typedef void *(__cdecl* T_abe_calloc)(size_t a1, size_t a2);
FunctionHook<T_abe_calloc> H_abe_calloc(0x00528DD7);
void * __cdecl Hook_abe_calloc(size_t num, size_t size)
{
	int convertedSize = num * size;
	void * allocatedMem = malloc(convertedSize + sizeof(int));
	((int *)allocatedMem)[0] = convertedSize;
	AddAllocationEntry((int)allocatedMem + sizeof(int), (int)convertedSize, 0);
	return (void*)((int)allocatedMem + sizeof(int));
}

typedef void (__cdecl* T_abe_free)(void * a1);
FunctionHook<T_abe_free> H_abe_free(0x00521334);
void __cdecl Hook_abe_free(void * a1)
{
	if (a1)
	{
		RemoveAllocationEntry((int)a1);
		H_abe_free.Real()((void*)((int)a1));
	}
}

typedef void*(__cdecl* T_abe_realloc)(void *lpMem, size_t a2);
FunctionHook<T_abe_realloc> H_abe_realloc(0x00522335);
void * __cdecl Hook_abe_realloc(void *lpMem, size_t a2)
{
	RemoveAllocationEntry((int)lpMem);

	void * newMem = realloc((void*)((int)lpMem - sizeof(int)), a2 + sizeof(int));
	((int *)newMem)[0] = a2;

	AddAllocationEntry((int)newMem + sizeof(int), (int)a2, 0);

	return (void*)((int)newMem + sizeof(int));
}

typedef __int16(__cdecl* T_ResourceLoadHack)(char *a1, int a2, int a3, __int16 a4, __int16 a5);
FunctionHook<T_ResourceLoadHack> H_resourceHack(0x004DBE00);
__int16 __cdecl Hook_resourceHack(char *file, int type, int id, __int16 a4, __int16 a5)
{
	BanLoadResource(file, 0);
	
	return H_resourceHack.Real()(file, type, id, a4, a5);
}

void MLG_InitHook()
{
	MH_Initialize();

	//Enable DDCHEAT
	*reinterpret_cast<bool*>(0x005CA4B5) = true; // DDCheat Enabled
	*reinterpret_cast<bool*>(0x005BC000) = true; // Always Render
	*reinterpret_cast<bool*>(0x005C1BD8) = false; // Extra Ai Info

	H_LoopHook.Install(reinterpret_cast<T_LoopHook>(Hook_LoopHook));

	//H_resourceHack.Install(Hook_resourceHack);

	bool hookMemory = false;

	if (hookMemory)
	{
		H_abe_malloc.Install(Hook_abe_malloc);
		H_abe_calloc.Install(Hook_abe_calloc);
		H_abe_realloc.Install(Hook_abe_realloc);
		H_abe_free.Install(Hook_abe_free);
	}
}