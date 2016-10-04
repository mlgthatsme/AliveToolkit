#pragma once

SCRIPT_FUNCTION void * Ae_CreateObject(int id, char * params);
SCRIPT_FUNCTION void Ae_PlaySound(int id, int vol, float pitch, int a4);
SCRIPT_FUNCTION void UpdateAllocationList();
SCRIPT_FUNCTION void Ae_LoadResource(char * filename);
SCRIPT_FUNCTION void Ae_QuikLoad(char * saveData);

void AddAllocationEntry(int address, int size, int caller);
void RemoveAllocationEntry(int address);

void Script_Init();

void CLROnTick();