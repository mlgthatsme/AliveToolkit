#include "stdafx.h"
#include "ClrFunctions.h"

ref class ManagedGlobals {
public:
	static System::Collections::Generic::List<AliveAPIDotNet::MemoryAllocation>^ mMemAllocations = gcnew System::Collections::Generic::List<AliveAPIDotNet::MemoryAllocation>();
};

bool gIsScriptInit = false;

int GetGameType()
{
	char windowTitle[200];
	GetWindowTextA(FindWindowA("ABE_WINCLASS", NULL), windowTitle, 200);

	std::string title(windowTitle);

	if (title.find("Oddysee") != std::string::npos)
		return 1;
	else if (title.find("Exoddus") != std::string::npos)
		return 2;
	else
		return 0;
}

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

void * Ae_CreateObject(int id, char * params)
{
	switch (AliveAPIDotNet::GameConfiguration::Instance->GameType)
	{
	case AliveAPIDotNet::GameTypes::Oddysee:
		//reinterpret_cast<void *(__cdecl*)(void * params, int a2, int a3, __int16 a4)>(*(int*)(0x004D14CC + (id * 4)))(params, *(int*)(0x00507BA8), 0, 1);
		return reinterpret_cast<void *(__cdecl*)(void * params, int a2, int a3, __int16 a4)>(*(int*)(0x004D14CC + (id * 4)))(params, *(int*)(0x00507BA8), 0, 0);
	case AliveAPIDotNet::GameTypes::Exoddus:
		return reinterpret_cast<void *(__cdecl*)(void * params, int a2, int a3, __int16 a4)>(*(int*)(0x00589724 + (id * 4)))(params, *(int*)(0x00BB47C0), 0, 0);
	}
}

void AddRaycastEntry(bool hit, int x1, int y1, int x2, int y2, int collidedLinePointer, int collisionX, int collisionY, int mode)
{
	AliveAPIDotNet::DebugWindow::RaycastHit ray;
	ray.Hit = hit;
	ray.X1 = x1 >> 16;
	ray.Y1 = y1 >> 16;
	ray.X2 = x2 >> 16;
	ray.Y2 = y2 >> 16;
	ray.CX = collisionX >> 16;
	ray.CY = collisionY >> 16;
	ray.CollidedObject = collidedLinePointer;
	ray.Mode = mode >> 1;
	System::Threading::Monitor::Enter(AliveAPIDotNet::DebugWindow::mRaycastHits);
	AliveAPIDotNet::DebugWindow::mRaycastHits->Add(ray);
	System::Threading::Monitor::Exit(AliveAPIDotNet::DebugWindow::mRaycastHits);
}

void Ae_QuikLoad(char * saveData)
{
	memcpy((void*)0x00BAF7F8, saveData, 8192);
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

	System::Threading::Monitor::Enter(AliveAPIDotNet::DebugWindow::mRaycastHits);
	AliveAPIDotNet::DebugWindow::mRaycastHits->Clear();
	System::Threading::Monitor::Exit(AliveAPIDotNet::DebugWindow::mRaycastHits);
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