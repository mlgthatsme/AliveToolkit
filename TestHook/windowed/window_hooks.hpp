#pragma once

#include <memory>
#include <Windows.h>

LRESULT CALLBACK NewWindowProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam);
void CenterWnd(HWND wnd);
void SubClassWindow();
void PatchWindowTitle();
LONG WINAPI Hook_SetWindowLongA(HWND hWnd, int nIndex, LONG dwNewLong);
