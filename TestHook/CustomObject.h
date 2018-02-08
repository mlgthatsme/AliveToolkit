#pragma once

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

		//Abe_ObjectInitFont(&font, 256, (int)0x554474, (int)0x5BC5C8); // Todo reimplement this
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

		int targetX = mGame->GetAbe()->mPosition.X;
		int targetY = mGame->GetAbe()->mPosition.Y - FLOAT_TO_FIXED(50) - FLOAT_TO_FIXED((sinf(mGame->GetgnFrame() / 50.0f) * 5));

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
					mGame->mWorld->SpawnShockLine(FIXED_TO_FLOAT(position_x), FIXED_TO_FLOAT(position_y), FIXED_TO_FLOAT(obj->mPosition.X), FIXED_TO_FLOAT(obj->mPosition.X));

					// Sparks
					void * sparks = malloc(264);
					reinterpret_cast<AE_abe *(__thiscall*)(void *thisPtr, int a2, int a3, __int16 a4, int a5, int a6, int a7)>(0x00403373)(sparks, position_x, position_y, 10, obj->mScale, 3, 11);

					if (!BanGetLoadedResource('minA', 6005, 0, 0))
						BanLoadResource("GRENADE.BAN", 0);
					if (!BanGetLoadedResource(1835626049, 372, 0, 0))
						BanLoadResource("SMEXP.BAN", 0);
					if (!BanGetLoadedResource(1835626049, 365, 0, 0))
						BanLoadResource("METAL.BAN", 0);

					AE_abe* grenade = reinterpret_cast<AE_abe*(__thiscall*)(void * thisPtr, int a2, int a3, __int16 a4, __int16 a5, __int16 a6, void * a7)>(0x402874)(malloc(316), obj->mPosition.X, obj->mPosition.Y, 0, 1, 0, this);
					grenade->mScale = obj->mScale;
					grenade->mLayer = obj->mLayer;
					(*((void(__thiscall **)(void *, int, unsigned int))grenade->vtable + 24))((void*)grenade, 0, 0);
					break;
				}
			}
		}

		//Abe_RenderText(&font, 0x005C11B0, "o", (position_x - renderOffsetX) / 0x10000, (position_y - renderOffsetY) / 0x10000, 0, 1, 0, 41, 233, 233, 233, 8, 0x10000, 640, 0);
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

	virtual short Func7(void * a2, __int16 a3)
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

/////////////////////////
// CUSTOM OBJECT 
/////////////////////////

int CustomObjectVTable[32];

void CustomObjectUpdate(AE_abe * thisPtr, int a1)
{
	printf("Anim: %X\n", thisPtr->mCurrentAnim);
}

int nopFunc(AE_abe * thisPtr)
{
	printf("Func Called!\n");
	return 0;
}

int CustomObjectSaveObject(AE_abe * thisPtr, unsigned char * saveBuffer)
{
	return 0;
}

void SetupCustomObjectVTable()
{
	for (int i = 0; i < 32; i++)
	{
		CustomObjectVTable[i] = (int)nopFunc;
	}

	CustomObjectVTable[0] = 0x402351;
	CustomObjectVTable[1] = 0x403B8E;
	CustomObjectVTable[2] = (int)CustomObjectUpdate;
	CustomObjectVTable[3] = 0x404115;
	CustomObjectVTable[5] = (int)CustomObjectSaveObject;
}

void * CreateCustomObject()
{
	SetupCustomObjectVTable();

	AE_abe * object = (AE_abe *)reinterpret_cast<void *(__thiscall*)(size_t size)>(0x004024AA)(305);
	reinterpret_cast<void *(__thiscall*)(AE_abe *thisPtr, int a2)>(0x00408240)(object, 0); // AbeInitPersistantObject

	object->vtable = (void*)0x00545F3C; // CustomObjectVTable;
	object->type = 83;
	object->mPosition = mGame->GetAbe()->mPosition;
	object->objectMode |= 0x40u;

	// AddLoadedResourceToObject
	char * resourceData = reinterpret_cast<char *(__fastcall*)
		(void *thisPtr, void * eax, int type, int resourceID)>(0x004DC130)
		(object, nullptr, 'minA', 1037);

	// sub_403AF8
	//reinterpret_cast<void *(__fastcall*)
	//	(void *thisPtr,void * eax, int a2, int a3, int a4, char * rd, int a6, int a7)>(0x00403AF8)
	//	(object, 0, 2228, 53, 28, resourceData, 1, 1);

	//// sub_401ED3
	//reinterpret_cast<int(__cdecl*)
	//	(int a1, int a2)>(0x00401ED3)((int)0x55C1EC, 0);

	auto v3 = object;
	*(_DWORD *)&v3->gap115[3] = 0;
	v3->mScale = 0x10000;
	v3->mLayer = 1;
	*(_WORD *)&v3->gap24[8] = 27;
	v3->gapDC |= 2u;
	*(_WORD *)&v3->gap115[7] = 0;
	*(_DWORD *)&v3->gap115[15] = 0;

	return object;
}

/////////////////////////
/////////////////////////
/////////////////////////

void Lazors()
{
	if (GetGameType() == 2)
	{
		AliveAPI::ObjectList * objList = *reinterpret_cast<AliveAPI::ObjectList **>(0x00BB47C4);
		AliveAPI::ObjectList * objList2 = *reinterpret_cast<AliveAPI::ObjectList **>(0x5C1124);

		AE_abe * newObj = (AE_abe*)(new CustomAEObject());
		newObj->mPosition.X = mGame->GetAbe()->mPosition.X;
		newObj->mPosition.Y = mGame->GetAbe()->mPosition.Y;

		objList->AddObject(newObj);
		objList2->AddObject(newObj);
	}
}