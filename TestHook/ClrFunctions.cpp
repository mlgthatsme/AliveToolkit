#include "ClrFunctions.h"
#include <vector>

#include "AeTypes.h"

bool gAppEnableLog = false;

ref class ManagedGlobals {
public:
	static System::Collections::Generic::List<AliveAPIDotNet::MemoryAllocation>^ mMemAllocations = gcnew System::Collections::Generic::List<AliveAPIDotNet::MemoryAllocation>();
};

bool gIsScriptInit = false;

void Script_Init()
{
	if (gIsScriptInit)
		return;

	switch (GetGameType())
	{
	case 1:
		AliveAPIDotNet::AlivePlugin::Initialize(AliveAPIDotNet::GameTypes::Oddysee);
		break;
	case 2:
		AliveAPIDotNet::AlivePlugin::Initialize(AliveAPIDotNet::GameTypes::Exoddus);
		break;
	default:
		printf("Unknown Game Type. Alive Toolkit will not be loaded.\n");
		break;
	}
	
	gIsScriptInit = true;
}

void Ae_LoadResource(char * filename)
{
	switch (AliveAPIDotNet::GameConfiguration::Instance->GameType)
	{
	case AliveAPIDotNet::GameTypes::Oddysee:
		//reinterpret_cast<short(__cdecl*)(char *name, int a2)>(0x00403274)(filename, 0);
		break;
	case AliveAPIDotNet::GameTypes::Exoddus:
		reinterpret_cast<short(__cdecl*)(char *name, int a2)>(0x00403274)(filename, 0);
		break;
	}
}

bool createObjIds[160];

void * Ae_CreateObject(int id, char * params)
{
	switch (AliveAPIDotNet::GameConfiguration::Instance->GameType)
	{
	case AliveAPIDotNet::GameTypes::Oddysee:
		//reinterpret_cast<void *(__cdecl*)(void * params, int a2, int a3, __int16 a4)>(*(int*)(0x004D14CC + (id * 4)))(params, *(int*)(0x00507BA8), 0, 1);
		return reinterpret_cast<void *(__cdecl*)(void * params, int a2, int a3, __int16 a4)>(*(int*)(0x004D14CC + (id * 4)))(params, *(int*)(0x00507BA8), 0, 0);
	case AliveAPIDotNet::GameTypes::Exoddus:
		if (!createObjIds[id])
		{
			reinterpret_cast<void *(__cdecl*)(void * params, int a2, int a3, __int16 a4)>(*(int*)(0x00589724 + (id * 4)))(params, *(int*)(0x00BB47C0), 0, 2);
			createObjIds[id] = true;
		}
		return reinterpret_cast<void *(__cdecl*)(void * params, int a2, int a3, __int16 a4)>(*(int*)(0x00589724 + (id * 4)))(params, *(int*)(0x00BB47C0), 0, 0);
	}

	return 0;
}

void AddRaycastEntry(bool hit, int x1, int y1, int x2, int y2, int collidedLinePointer, int collisionX, int collisionY, int mode)
{
	AliveAPIDotNet::RaycastHit ray;
	ray.Hit = hit;
	ray.X1 = x1 >> 16;
	ray.Y1 = y1 >> 16;
	ray.X2 = x2 >> 16;
	ray.Y2 = y2 >> 16;
	ray.CX = collisionX >> 16;
	ray.CY = collisionY >> 16;
	ray.CollidedObject = collidedLinePointer;
	ray.Mode = mode >> 1;
	System::Threading::Monitor::Enter(AliveAPIDotNet::AliveAPI::RaycastHits);
	AliveAPIDotNet::AliveAPI::RaycastHits->Add(ray);
	System::Threading::Monitor::Exit(AliveAPIDotNet::AliveAPI::RaycastHits);
}

void Ae_QuikLoad()
{
	reinterpret_cast<__int16*(__cdecl*)()>(0x004022A2)();
}

static char mTempSaveData[8192];

const char * Ae_QuikSave()
{
	reinterpret_cast<void*(__cdecl*)(char *)>(0x004C91A0)(mTempSaveData);
	return mTempSaveData;
}

void CLROnTick()
{
	AliveAPIDotNet::AliveAPI::FireOnGameTick();
	System::Threading::Monitor::Enter(AliveAPIDotNet::AliveAPI::RaycastHits);
	AliveAPIDotNet::AliveAPI::RaycastHits->Clear();
	System::Threading::Monitor::Exit(AliveAPIDotNet::AliveAPI::RaycastHits);
}

int CLROnInput(int r, int index)
{
	if (AliveAPIDotNet::AliveAPI::Input != nullptr)
	{
		AliveAPIDotNet::AliveAPI::Input->Pads[index]->Pressed->Value = r;
		AliveAPIDotNet::AliveAPI::FireOnInput(index);
		return AliveAPIDotNet::AliveAPI::Input->Pads[index]->Pressed->Value;
	}
	else
	{
		return r;
	}
}

void CLROnDebugDraw()
{
	int ddHdc = 0x00C1D160;
	int screenHdc = reinterpret_cast<int(__cdecl*)(void * ddrawPtr)>(0x004F2150)((void*)ddHdc);
	AliveAPIDotNet::AliveAPI::ScreenHdc = System::IntPtr(screenHdc);
	AliveAPIDotNet::AliveAPI::FireOnDebugDraw();
	reinterpret_cast<int(__cdecl*)(int a1, int a2)>(0x4F21A0)(ddHdc, screenHdc);
}

void AddAllocationEntry(int address, int size, int caller)
{
	AliveAPIDotNet::MemoryAllocation alloc;
	alloc.Address = System::IntPtr(address);
	alloc.Size = size;
	alloc.Time = System::DateTime::Now;
	alloc.Caller = System::IntPtr(caller);
	ManagedGlobals::mMemAllocations->Add(alloc);

	AliveAPIDotNet::AliveAPI::FireOnMemoryAllocate(alloc);
}

void RemoveAllocationEntry(int address)
{
	AliveAPIDotNet::MemoryAllocation found;
	bool isFound = false;
	for each(AliveAPIDotNet::MemoryAllocation alloc in ManagedGlobals::mMemAllocations)
	{
		if (alloc.Address.ToInt32() == address)
		{
			found = alloc;
			isFound = true;
			break;
		}
	}

	if (isFound)
		ManagedGlobals::mMemAllocations->Remove(found);
}

void UpdateAllocationList()
{
	AliveAPIDotNet::AliveAPI::MemoryAllocations = ManagedGlobals::mMemAllocations;
}

void Ae_PlaySound(int id, int vol, float pitch, int a4)
{
	reinterpret_cast<int *(__cdecl*)(int a1, int a2, int a3, int a4)>(0x0046FBA0)(id, vol, (int)(pitch * 0x10000), a4);
}

bool IsMusicEnabled()
{
	return AliveAPIDotNet::AliveAPI::MusicEnabled;
}

// Dev Functions

SCRIPT_FUNCTION void Ae_SetMusic(int song, void * object, __int16 unknown1, bool unknown2)
{
	reinterpret_cast<__int16*(__cdecl*)(__int16 a1, int a2, __int16 a3, bool a4)>(0x47FD60)(song, (int)object, unknown1, unknown2);
}

SCRIPT_FUNCTION bool IsCustomDemoPlaying()
{
	return AliveAPIDotNet::Demos::DemoPlayer::IsDemoPlaying;
}

SCRIPT_FUNCTION void PlayDemo(const char * filePath)
{
	AliveAPIDotNet::Forms::RecordWindow::mPlayer = gcnew AliveAPIDotNet::Demos::DemoPlayer();
	AliveAPIDotNet::Forms::RecordWindow::mPlayer->Open(System::IO::File::Open(gcnew System::String(filePath), System::IO::FileMode::Open));
}

SCRIPT_FUNCTION void HideUI()
{
	AliveAPIDotNet::AlivePlugin::HideUI();
}

SCRIPT_FUNCTION void ShowUI()
{
	AliveAPIDotNet::AlivePlugin::ShowUI();
}

SCRIPT_FUNCTION void EnableVerboseLog()
{
	gAppEnableLog = true;
}

SCRIPT_FUNCTION void PlayMidiNote(int program, int note, float pan, float volume)
{
	int vLeft = 127;
	int vRight = 127;
	if (pan < 0)
		vRight *= (1.0f - abs(pan));
	if (pan > 0)
		vLeft *= (1.0f - abs(pan));

	reinterpret_cast<int(__cdecl*)(int a1, int program, int note, int leftVolume, int rightVolume, int volume)>(0x004FCB30)(0, program, note << 8, vLeft, vRight, (int)(volume * 127));
}

SCRIPT_FUNCTION void Lazors()
{
}

void SaveImage16(AE_Rect * rect, char * data)
{
	/*System::Drawing::Bitmap ^ bitmap = gcnew System::Drawing::Bitmap(rect->Width, rect->Height);
	System::Drawing::Imaging::BitmapData ^ bitData = bitmap->LockBits(System::Drawing::Rectangle(0, 0, rect->Width, rect->Height), System::Drawing::Imaging::ImageLockMode::ReadWrite, System::Drawing::Imaging::PixelFormat::Format16bppRgb555);

	memcpy((void*)bitData->Scan0, data, rect->Width * rect->Height * 2);

	bitmap->UnlockBits(bitData);

	bitmap->Save(System::String::Format("export/{0:X}.png", (int)data));*/
}