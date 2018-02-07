#pragma once

#include <Xinput.h>

float vibrationAmount = 0;

__int16 __fastcall Abe_ScreenShakeUpdate(AE_ScreenShake *thisPtr);
ALIVE_FUNC_IMPLEX(0x0, 0x004AD0E0, Abe_ScreenShakeUpdate, true)
__int16 __fastcall Abe_ScreenShakeUpdate(AE_ScreenShake * thisPtr)
{
	float v = thisPtr->shakeLength / 16.0f;
	if (v > vibrationAmount)
		vibrationAmount = v;

	return Abe_ScreenShakeUpdate_.Ptr()(thisPtr);
}

void UpdateVibration()
{
	if (vibrationAmount < 0)
		vibrationAmount = 0;
	else if (vibrationAmount > 1)
		vibrationAmount = 1;

	bool pauseMenuCreated = gPtrPauseMenu != 0;
	bool pauseMenuActive = false;
	if (pauseMenuCreated)
		pauseMenuActive = (((char*)(*(int*)&gPtrPauseMenu))[300] & 1) > 1;

	XINPUT_VIBRATION vib;
	USHORT vibLR = vibrationAmount * 65535;

	if (pauseMenuCreated && pauseMenuActive)
	{
		vibLR = 0;
	}

	vib.wLeftMotorSpeed = vibLR;
	vib.wRightMotorSpeed = vibLR;
	XInputSetState(0, &vib);
	vibrationAmount -= 1 / 16.0f;
}

void SetupXInputHooks()
{
	gInputJoyStrB1 = "X";
	gInputJoyStrB2 = "A";
	gInputJoyStrB3 = "B";
	gInputJoyStrB4 = "Y";
	gInputJoyStrB5 = "LB";
	gInputJoyStrB6 = "RB";
	gInputJoyStrB7 = "LT";
	gInputJoyStrB8 = "RT";
	gInputJoyStrB9 = "";
	gInputJoyStrB0 = "";

	const char * newGamepadName = "Xbox Controller";
	memcpy(&gInputGamepadString, newGamepadName, strlen(newGamepadName));

	gInputJoystickEnabled = true;
}

int Abe_InitInput();
ALIVE_FUNC_IMPLEX(0x0, 0x00491BC0, Abe_InitInput, true);
int Abe_InitInput()
{
	int r = Abe_InitInput_.Ptr()();
	SetupXInputHooks();
	return r;
}

void __cdecl Abe_LoadControllerSettings();
ALIVE_FUNC_IMPLEX(0x0, 0x00492D40, Abe_LoadControllerSettings, true);
void __cdecl Abe_LoadControllerSettings()
{
	Abe_LoadControllerSettings_.Ptr()();
	SetupXInputHooks();
}

void __cdecl Abe_GetJoystickInput(float *X1, float *Y1, float *X2, float *Y2, DWORD *Buttons);
ALIVE_FUNC_IMPLEX(0x0, 0x00460280, Abe_GetJoystickInput, true);
void __cdecl Abe_GetJoystickInput(float *X1, float *Y1, float *X2, float *Y2, DWORD *Buttons)
{
	XINPUT_STATE state;
	ZeroMemory(&state, sizeof(XINPUT_STATE));

	float deadzone = 0.2f;

	*Buttons = 0;
	*X1 = 0;
	*Y1 = 0;
	*X2 = 0;
	*Y2 = 0;

	if (XInputGetState(0, &state) == ERROR_SUCCESS)
	{
		float f_LX = state.Gamepad.sThumbLX / 32767.0f;
		float f_LY = state.Gamepad.sThumbLY / 32767.0f;

		float f_RX = state.Gamepad.sThumbRX / 32767.0f;
		float f_RY = state.Gamepad.sThumbRY / 32767.0f;

		// Joysticks
		if (abs(f_LX) > deadzone)
			*X1 = f_LX;
		if (abs(f_LY) > deadzone)
			*Y1 = -f_LY;

		if (abs(f_RX) > deadzone)
			*X2 = f_RX;
		if (abs(f_RY) > deadzone)
			*Y2 = -f_RY;

		// DPad Movement
		if (state.Gamepad.wButtons & XINPUT_GAMEPAD_DPAD_RIGHT)
			*X1 = 1;
		else if (state.Gamepad.wButtons & XINPUT_GAMEPAD_DPAD_LEFT)
			*X1 = -1;
		if (state.Gamepad.wButtons & XINPUT_GAMEPAD_DPAD_UP)
			*Y1 = -1;
		else if (state.Gamepad.wButtons & XINPUT_GAMEPAD_DPAD_DOWN)
			*Y1 = 1;

#define M_XINPUT_BIND(BIT, PAD_BUTTON){if (state.Gamepad.wButtons & PAD_BUTTON) *Buttons |= (1 << BIT); }


		M_XINPUT_BIND(0, XINPUT_GAMEPAD_X);
		M_XINPUT_BIND(1, XINPUT_GAMEPAD_A);
		M_XINPUT_BIND(2, XINPUT_GAMEPAD_B);
		M_XINPUT_BIND(3, XINPUT_GAMEPAD_Y);
		M_XINPUT_BIND(4, XINPUT_GAMEPAD_LEFT_SHOULDER);
		M_XINPUT_BIND(5, XINPUT_GAMEPAD_RIGHT_SHOULDER);
		M_XINPUT_BIND(8, XINPUT_GAMEPAD_BACK);
		M_XINPUT_BIND(9, XINPUT_GAMEPAD_START);

		if (state.Gamepad.bLeftTrigger > 32)
			*Buttons |= (1 << 6);
		if (state.Gamepad.bRightTrigger > 32)
			*Buttons |= (1 << 7);

		// 0 Square
		// 1 Cross
		// 2 Circle
		// 3 Triangle
		// 4 L1
		// 5 R1
		// 6 L2
		// 7 R2
		// 8 Back
		// 9 Start

		UpdateVibration();
	}
}