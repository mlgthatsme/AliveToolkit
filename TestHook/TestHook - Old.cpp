/*
* Copyright (C) 2016, Michael Grima
* All rights reserved.
*/

#include "stdafx.h"
#include "TestHook.h"
#include "MinHook.h"
#include <typeinfo.h>
#include <string>
#include <iostream>

#include "AliveApi.h"

#include "AeTypes.h"

#include "Script.h"

template<typename T>
T Lerp(T from, T to, float t)
{
	return (T)(from + ((to - from) * t));
}

//#define FIXED_TO_FLOAT(f) ((f) / (float)0x10000)
//#define FLOAT_TO_FIXED(f) ()

AE_abe ** hero = reinterpret_cast<AE_abe**>(0x005C1B68);
AE_abe ** currentlyControlled = reinterpret_cast<AE_abe**>(0x5C1B8C);
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

#define LOG_HOOK_CALL printf("[CALL] " __FUNCTION__ "\n")

typedef int16_t(*T_Abe_LoadResource)(char *name, int a2);
T_Abe_LoadResource Abe_LoadResource = reinterpret_cast<T_Abe_LoadResource>(0x00403274);

typedef char*(*T_Abe_GetLoadedResource)(int type, int a2, __int16 a3, __int16 a4);
T_Abe_GetLoadedResource Abe_GetLoadedResource = reinterpret_cast<T_Abe_GetLoadedResource>(0x00401AC8);

typedef int(__cdecl* T_SND_PlayEx)(int sampleID, int volume, int, float pitch, int, int, int);
FunctionHook<T_SND_PlayEx> H_SND_PlayEx(0x004EF740);
int __cdecl Hook_SND_PlayEx(int a1, int volume, int a3, float pitch, int a5, int a6, int a7)
{
	LOG_HOOK_CALL;
	return H_SND_PlayEx.Real()(a1, volume, a3, pitch, a5, a6, a7);
}

typedef int(__cdecl* T_SND_New)(AE_snd_sample * a1, int a2, int a3, int a4, int a5);
FunctionHook<T_SND_New> H_SND_New(0x004EEFF0);
int __cdecl Hook_SND_New(AE_snd_sample * a1, int a2, int a3, int a4, int a5)
{
	//LOG_HOOK_CALL;
	//printf("Create sample: %X\n", a1->dword8);
	return H_SND_New.Real()(a1, a2, a3, a4, a5);
}

typedef int(__cdecl* T_GPU_LoadImage)(int a1, int a2);
FunctionHook<T_GPU_LoadImage> H_GPU_LoadImage(0x004F5E90);
int __cdecl Hook_GPU_LoadImage(int a1, int a2)
{
	LOG_HOOK_CALL;
	return H_GPU_LoadImage.Real()(a1, a2);
}

typedef char(__thiscall* T_PauseMenuLoop)(void *thisPtr);
FunctionHook<T_PauseMenuLoop> H_PauseMenuLoop(0x004903E0);
char __fastcall Hook_PauseMenuLoop(void *thisPtr)
{
	LOG_HOOK_CALL;
	return H_PauseMenuLoop.Real()(thisPtr);
}

typedef int(__cdecl* T_Abe_DebugError)(char * title, int a2, int a3, char *message);
FunctionHook<T_Abe_DebugError> H_Abe_DebugError(0x004F2920);
int __cdecl Hook_Abe_DebugError(char * title, int a2, int a3, char *message)
{
	//LOG_HOOK_CALL;
	//printf("[Abe Error] %s %s\n", title, message);
	return H_Abe_DebugError.Real()(title, a2, a3, message);
}

typedef int(__cdecl* T_Abe_LoadString)(char *a1, char *a2, int a3, char a4);
FunctionHook<T_Abe_LoadString> H_Abe_LoadString(0x004969D0);
int __cdecl Hook_Abe_LoadString(char *a1, char *a2, int a3, char a4)
{
	//LOG_HOOK_CALL;
	printf("[Load String] %s\n", a1);
	return H_Abe_LoadString.Real()(a1,a2,a3,a4);
}

typedef int(__thiscall* T_SligBrain)(void *thisPtr);
FunctionHook<T_SligBrain> H_SligBrain(0x004B17C0);
int __fastcall Hook_SligBrain(AE_slig * slig_obj)
{
	//LOG_HOOK_CALL;
	printf("Slig %X %d %d %d %d\n", slig_obj, slig_obj->word11C, slig_obj->dword120, slig_obj->word106, slig_obj->word108);
	return H_SligBrain.Real()(slig_obj);
}

typedef char(__thiscall* T_DDCheatTeleportLoop)(void *thisPtr);
FunctionHook<T_DDCheatTeleportLoop> H_DDCheatTeleportLoop(0x00415E20);
char __fastcall Hook_DDCheatTeleportLoop(void *thisPtr)
{
	LOG_HOOK_CALL;
	return H_DDCheatTeleportLoop.Real()(thisPtr);
}

typedef int( __thiscall* T_Abe_RenderText)(void *thisPtr, int a2, char *a3, int a4, short a5, int a6, int a7, int a8, int a9, char a10, char a11, char a12, int a13, int a14, int a15, int a16);
FunctionHook<T_Abe_RenderText> H_Abe_RenderText(0x004337D0);
int __fastcall Hook_Abe_RenderText(void *thisPtr, int a2, char *a3, int a4, short a5, int a6, int a7, int a8, int a9, char a10, char a11, char a12, int a13, int a14, int a15, int a16)
{
	LOG_HOOK_CALL;
	return H_Abe_RenderText.Real()(thisPtr, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16);
}


void Loop();

typedef char(__thiscall* T_DDCheatLoop)(void *thisPtr);
FunctionHook<T_DDCheatLoop> H_DDCheatLoop(0x00415780);
char __fastcall Hook_DDCheatLoop(void *thisPtr)
{
	//LOG_HOOK_CALL;
	Loop();

	/*short * thisBuffer = reinterpret_cast<short *>(thisPtr);
	thisBuffer[14] = keyboardInput[0];
	return H_DDCheatTeleportLoop.Real()(thisPtr);*/
	
	return H_DDCheatLoop.Real()(thisPtr);
}

typedef short(__thiscall* T_SetCam)(void * thisPtr, __int16 a1, __int16 a2, __int16 a3, __int16 a4, __int16 a5, __int16 a6);
T_SetCam SetCam = reinterpret_cast<T_SetCam>(0x480D30);

//#define ABE_FUNC(FuncName, Address, Params) { \
//typedef int(__thiscall* T_#FuncName)(Params);\
//T_#FuncName LoadLevel = reinterpret_cast<T_#FuncName>(Address);\
//}\
//
//ABE_FUNC(Test1, 0x004803F0, void * thisPtr, __int16 a2, __int16 a3, __int16 a4, __int16 a5, __int16 a6, __int16 a7);

typedef int(__thiscall* T_LoadLevel)(void * thisPtr, __int16 a2, __int16 a3, __int16 a4, __int16 a5, __int16 a6, __int16 a7);
T_LoadLevel LoadLevel = reinterpret_cast<T_LoadLevel>(0x004803F0);

typedef AE_abe *(__thiscall* T_CreateAbeObject)(void *thisPtr, int a2, int a3, int a4, int a5);
T_CreateAbeObject CreateAbeObject = reinterpret_cast<T_CreateAbeObject>(0x0044AD10);

typedef int *(__stdcall* T_AbeDebugPrintF)(char *format, ...);
T_AbeDebugPrintF AbeDebugPrintF = reinterpret_cast<T_AbeDebugPrintF>(0x004F5560);


typedef int *(__cdecl* T_AbePlaySound)(unsigned __int8 a1, int a2, int a3, int a4);
T_AbePlaySound AbePlaySound = reinterpret_cast<T_AbePlaySound>(0x0046FBA0);

const char * GetAeTypeString(int type)
{
	static std::string strTemp;

	switch (type)
	{
	case 0:
		return "No ID";
	case 7:
		return "Animation";
	case 13:
		return "Brew Machine";
	case 30:
		return "Grinder";
	case 33:
		return "Door";
	case 34:
		return "Door Lock";
	case 35:
		return "Bird";
	case 39:
		return "Electrocute";
	case 48:
		return "Rock Spawner";
	case 50:
		return "Fleech";
	case 53:
		return "Item Count";
	case 54:
		return "Flying Slig";
	case 61:
		return "Locked Soul";
	case 64:
		return "Greeter";
	case 67:
		return "Gluckon";
	case 68:
		return "Help Phone";
	case 69:
		return "Hero";
	case 78:
		return "Pulley";
	case 83:
		return "Anti Chant";
	case 84:
		return "Meat";
	case 85:
		return "Meat Sack";
	case 88:
		return "Mine";
	case 91:
		return "Greeter Body";
	case 96:
		return "Paramite";
	case 103:
		return "Pull Rope";
	case 105:
		return "Rock";
	case 106:
		return "Rock Sack";
	case 110:
		return "Mudokon";
	case 111:
		return "Red Laser";
	case 112:
		return "Scrab";
	case 122:
		return "Gate";
	case 124:
		return "Snooz Particle";
	case 125:
		return "Slig";
	case 126:
		return "Slog";
	case 129:
		return "Slug";
	case 134:
		return "Particle";
	case 139:
		return "Lever";
	case 142:
		return "Trapdoor";
	case 143:
		return "UXB";
	case 146:
		return "Web";
	default:
		strTemp = std::to_string(type);
		return strTemp.c_str();
	}
}

typedef AE_abe *(__thiscall* T_CreateSligObject)(void * thisPtr, int a2, int a3);
T_CreateSligObject CreateSligObject = reinterpret_cast<T_CreateSligObject>(0x004B1370);

typedef AE_abe *(__thiscall* T_CreateSlogObject)(void * thisPtr, int a2, int a3);
T_CreateSlogObject CreateSlogObject = reinterpret_cast<T_CreateSlogObject>(0x004C54F0);

typedef _DWORD *(__thiscall* T_LoadFnt)(void * thisPtr, int a1, int a2, int a3);
T_LoadFnt LoadFnt = reinterpret_cast<T_LoadFnt>(0x00404390);

void SpawnSlig()
{
	void * params = malloc(16);
	memset(params, 255, 16);
	void * creatureData = malloc(664);
	AE_abe * creature = CreateSligObject(creatureData, (int)params, 0xffff);
	creature->position_x = (*hero)->position_x;
	creature->position_y = (*hero)->position_y - (100 * 0x10000);
	creature->velocity_y = -10 * 0x10000;
}

void * __cdecl Hook_abe_malloc(size_t a1);

void SpawnSlog()
{
	void * params = malloc(128);
	memset(params, 255, 128);
	void * creatureData = malloc(356);
	AE_abe * creature = CreateSlogObject(creatureData, (int)params, 0xffff);
	creature->position_x = (*hero)->position_x;
	creature->position_y = (*hero)->position_y - (100 * 0x10000);
	creature->velocity_y = -10 * 0x10000;
}

struct Ae_fonttype
{
	_BYTE gap0[40];
	__int16 int1628;
	_BYTE gap2A[10];
	_DWORD dword34;
};


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

	virtual void Func3(int a1)
	{
		bool isGamePaused = *(bool*)0x005C1B66;

		int renderOffsetX = *(int*)(*(int*)((*(int*)0x5BB5F4) + 32));
		int renderOffsetY = *(int*)((*(int*)((*(int*)0x5BB5F4) + 32)) + 4);

		int targetX = (*hero)->position_x;
		int targetY = (*hero)->position_y - FLOAT_TO_FIXED(50) - FLOAT_TO_FIXED((sinf(mGame->GetgnFrame() / 50.0f) * 5));

		if (!isGamePaused)
		{
			position_x = Lerp<int>(position_x, targetX, 0.1f);
			position_y = Lerp<int>(position_y, targetY, 0.1f);
		}

		if (mGame->GetgnFrame() % 50 == 0)
		{
			

			if (!Abe_GetLoadedResource('minA', 301, 0, 0))
				Abe_LoadResource("EXPLO2.BAN", 0);

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

					if (!Abe_GetLoadedResource('minA', 6005, 0, 0))
						Abe_LoadResource("GRENADE.BAN", 0);
					if (!Abe_GetLoadedResource(1835626049, 372, 0, 0))
						Abe_LoadResource("SMEXP.BAN", 0);
					if (!Abe_GetLoadedResource(1835626049, 365, 0, 0))
						Abe_LoadResource("METAL.BAN", 0);

					AE_abe* grenade = reinterpret_cast<AE_abe*(__thiscall*)(void * thisPtr, int a2, int a3, __int16 a4, __int16 a5, __int16 a6, void * a7)>(0x402874)(Hook_abe_malloc(316), obj->position_x, obj->position_y, 0, 1, 0, this);
					grenade->scale = obj->scale;
					grenade->layer = obj->layer;
					(*((void(__thiscall **)(void *, int, unsigned int))grenade->vtable + 24))((void*)grenade, 0, 0); // Sets velocity
					
					

					//obj->health = 1;
					break;
				}
			}
		}
		
		T_Abe_RenderText renderFunc = reinterpret_cast<T_Abe_RenderText>(0x004014C4);
		//renderFunc(&font, 0x005C11B0, "o", (position_x - renderOffsetX) / 0x10000, (position_y - renderOffsetY) / 0x10000, 0, 1, 0, 41, 233, 233, 233, 8, 0x10000, 640, 0);

		//renderFunc(&font, 0x005C11B0, "TEST", 100, 100, 1, 1, 0, 0x16, 127, 127, 127, 0, 0x10000, 0xA2, 0x32);

		//DBGPRINTFUNC;
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
		saveBuffer[0] = 1;
		return 60;
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

	//virtual int LoadObject(unsigned char * saveBuffer) // 6
	//{
	//	DBGPRINTFUNC;
	//	printf("Custom Object loaded first byte: %d\n", saveBuffer[0]);
	//	return 1;
	//}

	
};

// REPLACE OBJECTS HOOK
typedef int(__stdcall* T_ParsePathTlvs)(int a1, __int16 a2);
FunctionHook<T_ParsePathTlvs> H_ParsePathTlvs(0x00482C10);
int __stdcall Hook_ParsePathTlvs(int a1, __int16 a2) 
{
	LOG_HOOK_CALL;
	/*int objListOffset = *(int*)(*(int*)(0xBB47C0) + 12) + 28;
	printf("OBJECT SPAWN LIST: %X\n", objListOffset);
	int objReplaces[] = { 17 };
	int replacementObj = 32;
	for (int i = 0; i < sizeof(objReplaces) / 4; i++)
	{
		*reinterpret_cast<int*>(objListOffset + (objReplaces[i] * 4)) = *reinterpret_cast<int*>(objListOffset + (replacementObj * 4));
	}*/

	/*for (int i = 0; i < 111; i++)
	{
		if (i == 22)
			continue;
		*reinterpret_cast<int*>(objListOffset + (i * 4)) = *reinterpret_cast<int*>(objListOffset + (103 * 4));
	}*/
	return H_ParsePathTlvs.Real()(a1, a2);
}

typedef AE_abe *(__thiscall* T_CreateFunction)(void * thisPtr, int a2, int a3);
typedef AE_abe *(__thiscall* T_CreateFunction3)(void * thisPtr, int a2, int a3, __int16 a4);

typedef AE_abe *(__cdecl* T_PathCreateObject)(int a1, int a3, __int16 a4);

AE_abe * CreateObject(int functionPtr)
{
	T_CreateFunction func = reinterpret_cast<T_CreateFunction>(functionPtr);

	void * params = malloc(5120);
	memset(params, 255, 5120);

	void * creatureData = malloc(5120);
	AE_abe * creature = func(creatureData, (int)params, 0xffffffff);
	creature->position_x = (*hero)->position_x;
	creature->position_y = (*hero)->position_y - (100 * 0x10000);
	creature->velocity_y = -10 * 0x10000;
	free(params);

	return creature;
}

char * ReadCreatureDump(const char * name)
{
	std::ifstream in = std::ifstream(std::string("creatures/" + std::string(name) + ".alive"));
	char * buffer = new char[in.tellg()];
	in.read(buffer, in.tellg());
	return buffer;
}

void DumpCreatureMemory(const char * name, void * mem, int size)
{
	std::ofstream out = std::ofstream(std::string("creatures/" + std::string(name) + ".alive"));
	out.write(reinterpret_cast<const char *>(mem), size);
}

static int ntemp1 = 0;
static int ntemp2 = 0;
__declspec(naked) int ThisCallFunction(unsigned int address, ...)
{
	__asm
	{
		pop ntemp1
		pop ntemp2
		add esp,4
		push ntemp1

		jmp ntemp2
	}
}

AE_abe * CreateMud()
{
	T_CreateFunction func = reinterpret_cast<T_CreateFunction>(0x00474F30);

	static char * params = ReadCreatureDump("mudmine");
	//DumpCreatureMemory("mudmine", (void*)0x005D7110, 1000);
	void * creatureData = malloc(412);
	//AE_abe * creature = (AE_abe * )ThisCallFunction(0x00474F30, creatureData, (int)0x005D7110, 0x1011368); //
	AE_abe * creature = func(creatureData, (int)0x005D7110, 0x1011368);
	creature->position_x = (*hero)->position_x;
	creature->position_y = (*hero)->position_y - (20 * 0x10000);
	creature->velocity_y = -10 * 0x10000;

	return creature;
}

AE_abe * CreateObject3(int functionPtr)
{
	T_CreateFunction3 func = reinterpret_cast<T_CreateFunction3>(functionPtr);

	void * params = malloc(1024);
	memset(params, 0, 1024);
	void * creatureData = malloc(2048);
	AE_abe * creature = func(creatureData, (int)params, 0xffffffff, 0);
	creature->position_x = (*hero)->position_x;
	creature->position_y = (*hero)->position_y - (100 * 0x10000);
	creature->velocity_y = -10 * 0x10000;

	return creature;
}


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

	static int prevX = 0;
	if (*currentlyControlled)
	{
		if (prevX != (*currentlyControlled)->position_x)
			printf("Player X Delta %f\n", static_cast<float>(((int)(*currentlyControlled)->position_x - (int)prevX) / static_cast<float>(0x10000)));
		prevX = (*currentlyControlled)->position_x;

		static int prevY = 0;
		if (prevY != (*currentlyControlled)->position_y)
			printf("Player Y Delta %f\n", static_cast<float>(((int)(*currentlyControlled)->position_y - (int)prevY) / static_cast<float>(0x10000)));
		prevY = (*currentlyControlled)->position_y;
	}
	
	/*if (GetAsyncKeyState(VK_SHIFT))
	{
		for (int i = 0; i < 10; i++)
		{
			if (GetAsyncKeyState('1' + i))
			{
				SetCam(reinterpret_cast<short*>(0x5C3030), i, 0, 1, 8, 0, 0);
			}
		}
	}*/
	if (GetAsyncKeyState('Y'))
	{
		for (int i = 0; i < mGame->mWorld->Objects->mCount; i++)
		{
			AE_abe * obj = (AE_abe *)mGame->mWorld->Objects->mData[i];
			if (mGame->mWorld->Objects->mData[i] != *hero && obj->type > 0 && obj->position_x != 0 && obj->position_y != 0)
			{
				obj->position_x = (*hero)->position_x + (100 << 16);
				obj->position_y = (*hero)->position_y;
			}
		}
	}
	if (GetAsyncKeyState('I'))
	{
		for (int i = 0; i <mGame->mWorld->Objects->mCount; i++)
		{
			AE_abe * obj = (AE_abe *)mGame->mWorld->Objects->mData[i];
			if (obj->type > 0 && obj->position_x != 0 && obj->position_y != 0)
			{
				obj->scale = (int)(0.5f * 0x10000);
			}
		}
	}
	if (GetAsyncKeyState('Q'))
	{
		/*char * switchStates = reinterpret_cast<char *>(0x005C1A28);
		for (int i = 0; i < 256; i++)
		{
			switchStates[i] = 1;
		}*/

		void * shake = malloc(48);
		reinterpret_cast<AE_abe *(__thiscall*)(void * a2, __int16 arg0, __int16 a3)>(0x403FB7)(shake, 0, 0);
	}
	if (GetAsyncKeyState('E'))
	{
		static bool createdObject = false;

		if (!createdObject)
		{
			// Create custom abe object
			AliveAPI::ObjectList * objList = *reinterpret_cast<AliveAPI::ObjectList **>(0x00BB47C4);
			AliveAPI::ObjectList * objList2 = *reinterpret_cast<AliveAPI::ObjectList **>(0x5C1124);

			AE_abe * newObj = (AE_abe*)(new CustomAEObject());
			newObj->position_x = (*currentlyControlled)->position_x;
			newObj->position_y = (*currentlyControlled)->position_y;

			objList->AddObject(newObj);
			objList2->AddObject(newObj);
			
			createdObject = true;
		}
		// Load Save

		// slig grenade
		/*if (!Abe_GetLoadedResource('minA', 6005, 0, 0))
			Abe_LoadResource("GRENADE.BAN", 0);
		if (!Abe_GetLoadedResource(1835626049, 372, 0, 0))
			Abe_LoadResource("SMEXP.BAN", 0);
		if (!Abe_GetLoadedResource(1835626049, 365, 0, 0))
			Abe_LoadResource("METAL.BAN", 0);*/

		// Spawn grenade
		//AE_abe* grenade = reinterpret_cast<AE_abe*(__thiscall*)(void * thisPtr, int a2, int a3, __int16 a4, __int16 a5, __int16 a6, void * a7)>(0x402874)(Hook_abe_malloc(316), (*currentlyControlled)->position_x, (*currentlyControlled)->position_y, 0, 1, 0, *currentlyControlled);
		//(*((void(__thiscall **)(void *, int, unsigned int))grenade->vtable + 24))((void*)grenade, -10 << 16, 0);

		//AE_abe* grenade = reinterpret_cast<AE_abe*(__thiscall*)(void * thisPtr, int a2, int a3, __int16 a4, int a5, int a6, int a7)>(0x402874)(Hook_abe_malloc(264), (*currentlyControlled)->position_x + (150 << 16), (*currentlyControlled)->position_y, 0x10000, 0, 0, 9);
		//(*((void(__thiscall **)(void *, int, unsigned int))grenade->vtable + 24))((void*)grenade, -10 << 16, 0);

		//CreateObject(0x004DE9A0);// uxb
		//CreateMud(); //MUDS
		//CreateObject(0x0046FD40);

		//int funcID = 15;

		//T_PathCreateObject func = reinterpret_cast<T_PathCreateObject>(reinterpret_cast<int*>(0x00589724)[funcID]);

		///////////////////////////////

		

		// Create pause menu
		/*void * pauseMenuObject = malloc(616);
		reinterpret_cast<void *(__thiscall*)(void * thisPtr)>(0x0048FB80)(pauseMenuObject);
		*reinterpret_cast<int*>(0x005C9300) = (int)pauseMenuObject;*/

		//reinterpret_cast<__int16 *(__thiscall*)(int thisPtr, __int16 a1, __int16 a2, __int16 a3, __int16 a4, __int16 a5, __int16 a6)>(0x004047E1)(0x005C3030, 5, 1, 1, 0, 0, 0);

		//reinterpret_cast<int (__thiscall*)(int thisPtr, __int16 a1, __int16 a2, __int16 a3, __int16 a4, __int16 a5, __int16 a6)>(0x004803F0)(0x005C3030, 5, 1, 1, 0, 0, 0);
	}
	if (GetAsyncKeyState('U'))
	{
		AbePlaySound(29, 24, 400, 0x10000);
		printf("---------------");
		for (int i = 0; i < mGame->mWorld->Objects->mCount; i++)
		{
			AE_abe * obj = (AE_abe *)mGame->mWorld->Objects->mData[i];
			printf("ADDR [%X] VPTR[%X] Type %d (%s) at %d %d\n", obj, obj->vtable, obj->type,GetAeTypeString(obj->type), obj->position_x >> 16, obj->position_y >> 16);
			//objList->objects[i]->position_x = (*hero)->position_x;
			//objList->objects[i]->position_y = (*hero)->position_y;
			//memcpy(objList->objects[i], *hero, sizeof(AE_abe));

			/*if (objList->objects[i]->field_4 == 110)
			{
				std::ofstream out = std::ofstream("creatures/mudokon.dump");
				out.write((const char *)objList->objects[i], 412);
			}*/
		}
		//printf("Teleporting");
		//SetCam(reinterpret_cast<short*>(0x5C3030), 1, 0, 4, 8, 1, 1);
		//SetCam(reinterpret_cast<short*>(0x5C3030), 1, 0, 4, 8, 0, 0);
		//SetCam(reinterpret_cast<short*>(0x5C3030), 1, 1, 4, 8, 0, 0);
		//*hero = CreateAbeObject(malloc(444), 58808, 85, 57, 0);

		
	}

	if (*ddcheatFlying)
	{
		int moveSpeed = 5 * 0x10000;
		if (GetAsyncKeyState(VK_DOWN))
			(*currentlyControlled)->position_y += moveSpeed;
		if (GetAsyncKeyState(VK_UP))
			(*currentlyControlled)->position_y -= moveSpeed;
		if (GetAsyncKeyState(VK_LEFT))
			(*currentlyControlled)->position_x -= moveSpeed;
		if (GetAsyncKeyState(VK_RIGHT))
			(*currentlyControlled)->position_x += moveSpeed;
	}

	return;
}

typedef BOOL(WINAPI* F_TextOutA)(_In_ HDC hdc, _In_ int x, _In_ int y, _In_reads_(c) LPCSTR lpString, _In_ int c);
F_TextOutA TextOutAOriginal;
typedef BOOL(WINAPI* F_GetTextMetricsA)(_In_ HDC hdc, _Out_ LPTEXTMETRICA lptm);
F_GetTextMetricsA GetTextMetricsAOriginal;

BOOL WINAPI GetTextMetricsA_Hook(_In_ HDC hdc, _Out_ LPTEXTMETRICA lptm)
{
	HFONT hFont = CreateFont(4, 6, 0, 0, FW_REGULAR, 0, 0, 0, 0, 0, 0, DRAFT_QUALITY, 0, L"Small Fonts");
	HFONT hTemp = (HFONT)SelectObject(hdc, hFont);
	bool b = GetTextMetricsAOriginal(hdc, lptm);
	DeleteObject(SelectObject(hdc, hTemp));
	return b;
}

BOOL WINAPI TextOutA_Hook(_In_ HDC hdc, _In_ int x, _In_ int y, _In_reads_(c) LPCSTR lpString, _In_ int c)
{
	HFONT hFont = CreateFont(12,5, 0, 0, FW_REGULAR, 0, 0, 0, 0, 0, 0, DRAFT_QUALITY, 0, L"Small Fonts");
	HFONT hTemp = (HFONT)SelectObject(hdc, hFont);
	bool b = TextOutAOriginal(hdc, x, y, lpString, c);
	DeleteObject(SelectObject(hdc, hTemp));
	return b;
}

struct register_store
{
	unsigned int eax;
	unsigned int ebx;
	unsigned int ecx;
	unsigned int edx;
};

register_store register_backup;
#define SAVE_REGISTERS {__asm\
{\
	mov register_backup.eax, eax\
	mov register_backup.ebx, ebx\
	mov register_backup.ecx, ecx\
	mov register_backup.edx, edx\
}}

#define RESTORE_REGISTERS {__asm\
{\
	mov eax, register_backup.eax\
	mov ebx, register_backup.ebx\
	mov ecx, register_backup.ecx\
	mov edx, register_backup.edx\
}}

typedef void *(__cdecl* T_abe_malloc)(size_t a1);
FunctionHook<T_abe_malloc> H_abe_malloc(0x005212C0);
void * __cdecl Hook_abe_malloc(size_t a1)
{
	//void ** puEBP = NULL;
	//__asm { mov puEBP, ebp };
	//void * pvReturn = puEBP[1]; // this is the caller
	void * pvReturn = 0;

	void * allocatedMem = malloc(a1 + sizeof(int));
	((int *)allocatedMem)[0] = a1;

	//printf("%X\n", (int)pvReturn);
	AddAllocationEntry((int)allocatedMem + sizeof(int), (int)a1, (int)pvReturn);

	return (void*)((int)allocatedMem + sizeof(int));
}

typedef void *(__cdecl* T_abe_calloc)(size_t a1, size_t a2);
FunctionHook<T_abe_calloc> H_abe_calloc(0x00528DD7);
void * __cdecl Hook_abe_calloc(size_t num, size_t size)
{
	int convertedSize = num * size;
	void ** puEBP = NULL;
	__asm { mov puEBP, ebp };
	void * pvReturn = puEBP[1]; // this is the caller

	void * allocatedMem = malloc(convertedSize + sizeof(int));
	((int *)allocatedMem)[0] = convertedSize;

	//printf("%X\n", (int)pvReturn);
	AddAllocationEntry((int)allocatedMem + sizeof(int), (int)convertedSize, (int)pvReturn);

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

void ReplaceLCDText()
{
	for (int i = 0x0055576C; i < 0x005558F8; i += 4)
	{
		*reinterpret_cast<int*>(i) = (int)("                     This experience brought to you by mlgthatsme's Alive Debugger!                     ");
	}
}

void MLG_InitHook()
{
	MH_Initialize();

	//Enable DDCHEAT
	*reinterpret_cast<bool*>(0x005CA4B5) = true; // DDCheat Enabled
	*reinterpret_cast<bool*>(0x005BC000) = true; // Always Render
	*reinterpret_cast<bool*>(0x005C1BD8) = false; // Extra Ai Info

	
	//MH_CreateHookApi(L"gdi32.dll", "TextOutA", TextOutA_Hook, (LPVOID *)&TextOutAOriginal);
	/*MH_CreateHookApi(L"gdi32.dll", "GetTextMetricsA", GetTextMetricsA_Hook, (LPVOID *)&GetTextMetricsAOriginal);
	MH_EnableHook(MH_ALL_HOOKS);
*/
	H_DDCheatLoop.Install(reinterpret_cast<T_DDCheatLoop>(Hook_DDCheatLoop));
	//H_SND_PlayEx.Install(Hook_SND_PlayEx);
	H_SND_New.Install(Hook_SND_New);
	H_GPU_LoadImage.Install(Hook_GPU_LoadImage);
	H_ParsePathTlvs.Install(Hook_ParsePathTlvs);

	bool hookMemory = false;

	if (hookMemory)
	{
		H_abe_malloc.Install(Hook_abe_malloc);
		H_abe_calloc.Install(Hook_abe_calloc);
		H_abe_realloc.Install(Hook_abe_realloc);
		H_abe_free.Install(Hook_abe_free);
	}

	//H_PauseMenuLoop.Install(reinterpret_cast<T_PauseMenuLoop>(Hook_PauseMenuLoop));
	//H_SligBrain.Install(reinterpret_cast<T_SligBrain>(Hook_SligBrain));
	//H_Abe_RenderText.Install(reinterpret_cast<T_Abe_RenderText>(Hook_Abe_RenderText));
	//H_Abe_DebugError.Install(Hook_Abe_DebugError);
	//H_Abe_LoadString.Install(Hook_Abe_LoadString);

	//ReplaceLCDText();
}