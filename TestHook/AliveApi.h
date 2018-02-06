#include "stdafx.h"

#pragma once


inline float FIXED_TO_FLOAT(int f)
{
	return (f) / (float)0x10000;
}

inline int FLOAT_TO_FIXED(float f)
{
	return(int)((f) * 0x10000);
}

template<typename T>
T Lerp(T from, T to, float t)
{
	return (T)(from + ((to - from) * t));
}

typedef int16_t(*T_Abe_LoadResource)(char *name, int a2);
typedef char*(*T_Abe_GetLoadedResource)(int type, int a2, __int16 a3, __int16 a4);
typedef char*(*T_Abe_AddLoadedResourceToObject)(void *thisPtr, int type, int resourceID);

static T_Abe_LoadResource BanLoadResource = reinterpret_cast<T_Abe_LoadResource>(0x00403274);
static T_Abe_GetLoadedResource BanGetLoadedResource = reinterpret_cast<T_Abe_GetLoadedResource>(0x00401AC8);
static T_Abe_AddLoadedResourceToObject AddLoadedResourceToObject = reinterpret_cast<T_Abe_AddLoadedResourceToObject>(0x004DC130);

namespace AliveAPI
{
	struct ObjectList
	{
		void ** mData;
		uint16_t mCount;

		void AddObject(void * obj)
		{
			mData[mCount] = obj;
			mCount++;
		}
	};

	// Anything to do with creatures, objects
	class World
	{
	public:
		ObjectList * Objects = *reinterpret_cast<ObjectList **>(0x00BB47C4);

		void LoadQuikSave(const char * saveData)
		{
			memcpy((void*)0x00BAF7F8, saveData, 8192);
			reinterpret_cast<__int16*(__cdecl*)()>(0x004022A2)();
		}

		void SpawnShockLine(float x, float y, float xto, float yto)
		{
			if (!BanGetLoadedResource('minA', 355, 0, 0))
				BanLoadResource("SPLINE.BAN", 0);

			void * shock = malloc(154);
			reinterpret_cast<void *(__thiscall*)(void *thisPtr, int a2, int a3, int a4, int a5, int a6, int a7, int a8)>(0x0040150A)(shock, FLOAT_TO_FIXED(x), FLOAT_TO_FIXED(y), FLOAT_TO_FIXED(xto), FLOAT_TO_FIXED(yto), 8, 0, 28);
		}
	};

	class Screen
	{
	public:
		void SetActiveCam(int level, int path, int cam)
		{
			reinterpret_cast<__int16 *(__thiscall*)(int thisPtr, __int16 a1, __int16 a2, __int16 a3, __int16 a4, __int16 a5, __int16 a6)>(0x004047E1)(0x005C3030, level, path, cam, 0, 0, 0);
		}

		int GetLevel() { return aeScreen->level; }
		int GetPath() { return aeScreen->path; }
		int GetCam() { return aeScreen->cam; }

		int GetCameraOffsetX() { return *(int*)(*(int*)((*(int*)0x5BB5F4) + 32)); };
		int GetCameraOffsetY() { return *(int*)((*(int*)((*(int*)0x5BB5F4) + 32)) + 4); };

		char * SwitchStates = reinterpret_cast<char *>(0x005C1A28); // Max 256

	private:
		struct __declspec(align(2)) AE_screen
		{
			_WORD word0;
			_WORD word2;
			_WORD word4;
			_WORD word6;
			_BYTE gap8[2];
			_WORD level;
			_WORD path;
			_WORD cam;
			_WORD transition;
			_WORD word12;
		};

		AE_screen * aeScreen = reinterpret_cast<AE_screen *>(0x005C3030);
	};

	class Sound
	{
	public:
		void PlaySound(int a1, int a2, int a3, float a4)
		{
			reinterpret_cast<int *(__cdecl*)(int a1, int a2, int a3, int a4)>(0x004012AD)(a1, a2, a3, (int)(a4 * 0x10000));
		}
	};

	class Game
	{
	public:
		World * mWorld = new World();
		Screen * mScreen = new Screen();
		Sound * mSound = new Sound();

		AE_abe * GetControlledObject() { return *reinterpret_cast<AE_abe**>(0x5C1B8C); }
		AE_abe * GetAbe() { return *reinterpret_cast<AE_abe**>(0x005C1B68); }

		int GetgnFrame() {return *reinterpret_cast<int*>(0x005C1B84); }

		const char * GetObjectTypeName(int type)
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
	};
}