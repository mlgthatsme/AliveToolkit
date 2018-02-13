#pragma once

#include <Windows.h>

#define _BYTE unsigned char
#define _WORD unsigned short
#define _DWORD unsigned int
#define __int8 char
#define __int16 short
#define __int32 int
#define __int64 long long

struct AE_ObjectList
{
	void *mArray;
	_WORD mCount;
	_WORD mMaxCount;
	_WORD mExpandSize;
	_WORD mFreeCount;
};

struct AE_pathLine
{
	_WORD X1;
	_WORD Y1;
	_WORD X2;
	_WORD Y2;
	_BYTE Mode;
	_BYTE Gap;
	_WORD Unknown1;
	_WORD Unknown2;
	_WORD Unknown3;
	_WORD Unknown4;
	_WORD Unknown5;
};

struct AE_PathList
{
	AE_pathLine * Data;
	char gap[8];
	int Count;
};

struct AE_ScreenShake
{
	void *vTable;
	_WORD objectID;
	_BYTE objectMode;
	_BYTE gap7[57];
	_WORD shakeLength;
	_WORD word42;
	_WORD word44;
};

struct AE_Rect
{
	_WORD X;
	_WORD Y;
	_WORD Width;
	_WORD Height;
};

struct AE_Font
{
	_BYTE gap0[40];
	AE_Rect rect;
	_WORD v3;
	_WORD v4;
	_WORD v5;
};



struct HalfFloatVector
{
	_DWORD X;
	_DWORD Y;
};


struct __declspec(align(4)) AE_abe
{
	void *vtable;
	__int16 type;
	char objectMode;
	char field_7;
	char gap8;
	int field_C;
	AE_ObjectList loadedBanList;
	char unknown;
	void *mCurrentAnim;
	_BYTE gap24[11];
	_BYTE gap30[4];
	int field_34;
	_BYTE gap38[4];
	int field_3C;
	_BYTE gap40[20];
	int field_54;
	__int16 gap58;
	_BYTE gap5A[88];
	__int16 field_B2;
	_BYTE gapB4[4];
	HalfFloatVector mPosition;
	__int16 field_C0;
	HalfFloatVector mVelocity;
	int mScale;
	__int16 color_r;
	__int16 color_g;
	__int16 color_b;
	__int16 mLayer;
	__int16 sprite_offset_x;
	__int16 sprite_offset_y;
	char gapDC;
	void *mShadowPtr;
	_BYTE gapE4[20];
	int field_F8;
	_BYTE gapFC[4];
	int mCurrentCollider;
	__int16 gap104;
	__int16 mAliveStateIndex;
	_BYTE gap108[4];
	int health;
	_BYTE gap110[4];
	_BYTE mFlags;
	_BYTE gap115[27];
	int field_130;
	int field_134;
	__int16 field_138;
	__int16 abe_hi_type;
	_BYTE gap13C[46];
	char ring_ability;
	_BYTE gap16B[55];
	char rock_count;
	_BYTE gap1A3[148];
	__declspec(align(1)) __int16 gap10A;
};

struct __declspec(align(4)) InputPadObject
{
	DWORD mPressed;
	BYTE field_4_dir;
	BYTE field_5;
	WORD field_6_padding;
	DWORD field_8_previous;
	DWORD field_C_held;
	DWORD field_10_released;
	DWORD field_14_padding;
};

struct __declspec(align(4)) InputObject
{
	InputPadObject mPads[2];
	DWORD **field_30_pDemoRes;
	DWORD field_34_demo_command_index;
	WORD field_38_bDemoPlaying;
	WORD field_3A_pad_idx;
	DWORD field_3C_command;
	DWORD field_40_command_duration;
};
