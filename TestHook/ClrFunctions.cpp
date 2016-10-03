#include "stdafx.h"
#include "ClrFunctions.h"

ref class ManagedGlobals {
public:
	static System::Collections::Generic::List<AliveAPIDotNet::MemoryAllocation>^ mMemAllocations = gcnew System::Collections::Generic::List<AliveAPIDotNet::MemoryAllocation>();
};

bool gIsScriptInit = false;

void Script_Init()
{
	if (gIsScriptInit)
		return;

	AliveAPIDotNet::AlivePlugin::Initialize();
	gIsScriptInit = true;
}

void Ae_LoadResource(char * filename)
{
	BanLoadResource(filename, 0);
}

void * Ae_CreateObject(int id, char * params)
{
	return reinterpret_cast<void *(__cdecl*)(void * params, int a2, int a3, __int16 a4)>(*(int*)(0x00589724 + (id * 4)))(params, *(int*)(0x00BB47C0), 0, 0);
}

void CLROnTick()
{
	AliveAPIDotNet::AliveAPI::FireOnGameTick();
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