#define _BYTE unsigned char
#define _WORD unsigned short
#define _DWORD unsigned int
#define __int8 char
#define __int16 short
#define __int32 int
#define __int64 long long

struct Ae_fonttype
{
	_BYTE gap0[40];
	__int16 int1628;
	_BYTE gap2A[10];
	_DWORD dword34;
};

struct __declspec(align(4)) AE_abe
{
	void *vtable;
	__int16 type;
	char objectMode;
	char field_7;
	char gap8;
	int field_C;
	char *field_10;
	int field_14;
	_BYTE gap18[8];
	int field_20;
	char field_24;
	__declspec(align(2)) char field_26;
	__declspec(align(2)) char field_28;
	char gap29;
	__int16 field_2A;
	__int16 field_2C;
	_BYTE gap2E[6];
	__int16 field_34;
	_BYTE gap36[6];
	int field_3C;
	_BYTE gap40[20];
	int field_54;
	__int16 gap58;
	_BYTE gap5A[88];
	char field_B2;
	_BYTE gapB3[5];
	_DWORD position_x;
	_DWORD position_y;
	char field_C0;
	int velocity_x;
	int velocity_y;
	int scale;
	char color_r;
	__declspec(align(2)) char color_g;
	__declspec(align(2)) char color_b;
	__declspec(align(2)) char layer;
	__int16 sprite_offset_x;
	__int16 sprite_offset_y;
	_BYTE gapDC[28];
	int field_F8;
	_BYTE gapFC[4];
	int field_100;
	__int16 gap104;
	__int16 alive_state;
	_BYTE gap108[4];
	int health;
	_BYTE gap110[32];
	int field_130;
	int field_134;
	__int16 field_138;
	_BYTE gap13A[48];
	char ring_ability;
	_BYTE gap16B[55];
	char rock_count;
	_BYTE gap1A3[148];
	 __declspec(align(1)) __int16 gap10A;
};
