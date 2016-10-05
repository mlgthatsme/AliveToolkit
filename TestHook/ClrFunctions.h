#pragma once

SCRIPT_FUNCTION void * Ae_CreateObject(int id, char * params);
SCRIPT_FUNCTION void Ae_PlaySound(int id, int vol, float pitch, int a4);
SCRIPT_FUNCTION void UpdateAllocationList();
SCRIPT_FUNCTION void Ae_LoadResource(char * filename);
SCRIPT_FUNCTION void Ae_QuikLoad(char * saveData);
SCRIPT_FUNCTION const char * Ae_QuikSave();

void AddAllocationEntry(int address, int size, int caller);
void RemoveAllocationEntry(int address);
void AddRaycastEntry(bool hit, int x1, int y1, int x2, int y2, int collidedLinePointer, int collisionX, int collisionY, int mode);

void Script_Init();

void CLROnTick();