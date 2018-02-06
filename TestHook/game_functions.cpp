#include "stdafx.h"

#include "game_functions.hpp"

std::map<DWORD, BaseFunction*>& BaseFunction::FunctionTable()
{
	static std::map<DWORD, BaseFunction*> funcTable;
	return funcTable;
}

void BaseFunction::HookAll()
{
	LONG err = DetourTransactionBegin();

	if (err != NO_ERROR)
	{
		ALIVE_HOOK_FATAL("DetourTransactionBegin failed");
	}

	err = DetourUpdateThread(GetCurrentThread());

	if (err != NO_ERROR)
	{
		ALIVE_HOOK_FATAL("DetourUpdateThread failed");
	}

	for (auto& fn : FunctionTable())
	{
		fn.second->Apply();
	}

	err = DetourTransactionCommit();
	if (err != NO_ERROR)
	{
		ALIVE_HOOK_FATAL("DetourTransactionCommit failed");
	}
}