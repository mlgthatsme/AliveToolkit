#pragma once

enum AE_PathParams_EnumContinuePointScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_ContinuePoint
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumContinuePointScale Scale;
	_WORD SaveFileId;
};

enum AE_PathParams_EnumPathTransitionWipe : _WORD
{
	Movie = 0,
	Left = 1,
	Right = 3,
	Up = 4,
	Down = 5,
	Zoom = 6,
	Horizontal = 7,
	Vertical = 8,
	Moviefirst = 9,
};
enum AE_PathParams_EnumPathTransitionScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_PathTransition
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Level;
	_WORD Path;
	_WORD Camera;
	_WORD Movie;
	AE_PathParams_EnumPathTransitionWipe Wipe;
	AE_PathParams_EnumPathTransitionScale Scale;
};

enum AE_PathParams_EnumHoistHoistType : _WORD
{
	NextFloor = 0,
	NextEdge = 1,
	Offscreen = 2,
};
enum AE_PathParams_EnumHoistEdgeType : _WORD
{
	Right = 0,
	Left = 1,
	Both = 2,
};
enum AE_PathParams_EnumHoistScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_Hoist
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumHoistHoistType HoistType;
	AE_PathParams_EnumHoistEdgeType EdgeType;
	_WORD Id;
	AE_PathParams_EnumHoistScale Scale;
};

enum AE_PathParams_EnumEdgeType : _WORD
{
	Left = 0,
	Right = 1,
	Both = 2,
};
enum AE_PathParams_EnumEdgeCanGrab : _WORD
{
	Yes = 1,
	no = 0,
};
enum AE_PathParams_EnumEdgeScale : _DWORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_Edge
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumEdgeType Type;
	AE_PathParams_EnumEdgeCanGrab CanGrab;
	AE_PathParams_EnumEdgeScale Scale;
};

struct AE_PathParams_DeathDrop
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Animation;
	_WORD Sound;
	_WORD Id;
	_WORD Action;
	_DWORD Value;
};

enum AE_PathParams_EnumDoorScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumDoorType : _WORD
{
	NormalWood = 0,
	Wood = 1,
	Stone = 2,
	Tomb = 3,
};
enum AE_PathParams_EnumDoorStartState : _WORD
{
	Open = 0,
	Closed = 1,
};
enum AE_PathParams_EnumDoorWipeEffect : _WORD
{
	Movie = 0,
	Left = 1,
	Right = 2,
	Up = 3,
	Down = 4,
	Rect = 5,
	Vert = 6,
	Horz = 7,
	MovieFirst = 8,
};
enum AE_PathParams_EnumDoorAbeDirection : _WORD
{
	Left = 1,
	Right = 0,
};
enum AE_PathParams_EnumDoorCloseAfterUse : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumDoorCancelThrowables : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_Door
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Level;
	_WORD Path;
	_WORD Camera;
	AE_PathParams_EnumDoorScale Scale;
	_WORD DoorNumber;
	_WORD Id;
	_WORD TargetDoorNumber;
	AE_PathParams_EnumDoorType Type;
	AE_PathParams_EnumDoorStartState StartState;
	_WORD HubId1;
	_WORD HubId2;
	_WORD HubId3;
	_WORD HubId4;
	_WORD HubId5;
	_WORD HubId6;
	_WORD HubId7;
	_WORD HubId8;
	AE_PathParams_EnumDoorWipeEffect WipeEffect;
	_WORD MovieNumber;
	_WORD XOffset;
	_WORD YOffset;
	_WORD WipeXOrigin;
	_WORD WipeYOrigin;
	AE_PathParams_EnumDoorAbeDirection AbeDirection;
	AE_PathParams_EnumDoorCloseAfterUse CloseAfterUse;
	AE_PathParams_EnumDoorCancelThrowables CancelThrowables;
};

enum AE_PathParams_EnumShadowScale : _DWORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_Shadow
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD CenterW;
	_WORD CenterH;
	_BYTE Red1;
	_BYTE Green1;
	_BYTE Blue1;
	_BYTE Red2;
	_BYTE Green2;
	_BYTE Blue2;
	_WORD Id;
	AE_PathParams_EnumShadowScale Scale;
};

enum AE_PathParams_EnumLiftPointIsStartPosition : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumLiftPointLiftStopType : _WORD
{
	TopFloor = 0,
	BottomFloor = 1,
	MiddleFloor = 2,
	MiddleLockFloor = 3,
	NoFloor,StartPointOnly = 4,
};
enum AE_PathParams_EnumLiftPointScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumLiftPointIgnoreLiftMover : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_LiftPoint
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Id;
	AE_PathParams_EnumLiftPointIsStartPosition IsStartPosition;
	_WORD LiftType;
	AE_PathParams_EnumLiftPointLiftStopType LiftStopType;
	AE_PathParams_EnumLiftPointScale Scale;
	AE_PathParams_EnumLiftPointIgnoreLiftMover IgnoreLiftMover;
};

enum AE_PathParams_EnumWellLocalLeavesWhenOn : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_WellLocal
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Scale;
	_WORD TriggerId;
	_WORD WellId;
	_WORD ResourceId;
	_WORD OffDeltaX;
	_WORD OffDeltaY;
	_WORD OnDeltaX;
	_WORD OnDeltaY;
	AE_PathParams_EnumWellLocalLeavesWhenOn LeavesWhenOn;
	_WORD LeafX;
	_DWORD LeafY;
};

enum AE_PathParams_EnumDoveScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_Dove
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD NumberOfBirds;
	_WORD PixelPerfect;
	AE_PathParams_EnumDoveScale Scale;
};

enum AE_PathParams_EnumRockSackSide : _WORD
{
	Left = 0,
	Right = 1,
};
enum AE_PathParams_EnumRockSackScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_RockSack
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumRockSackSide Side;
	_WORD XVel;
	_WORD YVel;
	AE_PathParams_EnumRockSackScale Scale;
	_WORD NumberOfRocks;
};

enum AE_PathParams_EnumFallingItemScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_FallingItem
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Id;
	AE_PathParams_EnumFallingItemScale Scale;
	_WORD DelayTime;
	_WORD NumberOfItems;
	_WORD ResetId;
};

enum AE_PathParams_EnumPullRingRopeTargetAction : _WORD
{
	TurnOn = 0,
	TurnOff = 1,
	Toggle = 2,
	Increment = 3,
	Decrement = 4,
};
enum AE_PathParams_EnumPullRingRopeScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumPullRingRopeOnSound : _WORD
{
	None = 0,
	Well = 1,
	Bell = 2,
	Door = 3,
	ElectricWall = 4,
	MovingBomb = 5,
	Slamdoor = 6,
};
enum AE_PathParams_EnumPullRingRopeOffSound : _WORD
{
	None = 0,
	Well = 1,
	Bell = 2,
	Door = 3,
	ElectricWall = 4,
	MovingBomb = 5,
	Slamdoor = 6,
};
enum AE_PathParams_EnumPullRingRopeSoundDirection : _DWORD
{
	Center = 0,
	Left = 1,
	Right = 2,
};
struct AE_PathParams_PullRingRope
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Id;
	AE_PathParams_EnumPullRingRopeTargetAction TargetAction;
	_WORD LengthOfRope;
	AE_PathParams_EnumPullRingRopeScale Scale;
	AE_PathParams_EnumPullRingRopeOnSound OnSound;
	AE_PathParams_EnumPullRingRopeOffSound OffSound;
	AE_PathParams_EnumPullRingRopeSoundDirection SoundDirection;
};

enum AE_PathParams_EnumBackgroundAnimationIsSemiTrans : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumBackgroundAnimationSoundEffect : _WORD
{
	None = 0,
	Campfire = 1,
	Windmill = 2,
};
enum AE_PathParams_EnumBackgroundAnimationLayer : _WORD
{
	Background = 0,
	Normal = 1,
	Foreground = 2,
};
struct AE_PathParams_BackgroundAnimation
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD AnimationRes;
	AE_PathParams_EnumBackgroundAnimationIsSemiTrans IsSemiTrans;
	_WORD SemiTransMode;
	AE_PathParams_EnumBackgroundAnimationSoundEffect SoundEffect;
	_WORD Id;
	AE_PathParams_EnumBackgroundAnimationLayer Layer;
};

struct AE_PathParams_AOTimedMine
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Id;
	_WORD State;
	_WORD Scale;
	_WORD TicksBeforeExplode;
	_DWORD DisableResources;
};

enum AE_PathParams_EnumSligScale : _WORD
{
	Full = 0,
	Half = 1,
	_3Or4ths = 2,
};
enum AE_PathParams_EnumSligStartState : _WORD
{
	Listening = 0,
	Paused = 1,
	Sleeping = 2,
	Chase = 3,
	RunOffScreen = 4,
	GameEnder = 5,
};
enum AE_PathParams_EnumSligChaseAbe : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumSligStartDirection : _WORD
{
	Left = 0,
	Right = 1,
};
enum AE_PathParams_EnumSligTalkToAbe : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumSligDontShoot : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumSligStayAwake : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_Slig
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumSligScale Scale;
	AE_PathParams_EnumSligStartState StartState;
	_WORD PauseTime;
	_WORD PauseLeftMin;
	_WORD PauseLeftMax;
	_WORD PauseRightMin;
	_WORD PauseRightMax;
	_WORD ChalNumber;
	_WORD ChalTimer;
	_WORD NumberOfTimesToShoot;
	_WORD Unknown;
	_WORD Code1;
	_WORD Code2;
	AE_PathParams_EnumSligChaseAbe ChaseAbe;
	AE_PathParams_EnumSligStartDirection StartDirection;
	_WORD PanicTimeout;
	_WORD NumPanicSounds;
	_WORD PanicSoundTimeout;
	_WORD StopChaseDelay;
	_WORD TimeToWaitBeforeChase;
	_WORD SligID;
	_WORD ListenTime;
	_WORD PercentSayWhat;
	_WORD PercentBeatMud;
	AE_PathParams_EnumSligTalkToAbe TalkToAbe;
	AE_PathParams_EnumSligDontShoot DontShoot;
	_WORD ZShootDelay;
	AE_PathParams_EnumSligStayAwake StayAwake;
	_WORD DisableResources;
	_WORD NoiseWakeUpDistance;
	_DWORD Id;
};

enum AE_PathParams_EnumSlogScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumSlogDirection : _WORD
{
	Left = 0,
	Right = 1,
};
enum AE_PathParams_EnumSlogAsleep : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_Slog
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumSlogScale Scale;
	AE_PathParams_EnumSlogDirection Direction;
	AE_PathParams_EnumSlogAsleep Asleep;
	_WORD WakeUpAnger;
	_WORD BarkAnger;
	_WORD ChaseAnger;
	_WORD JumpDelay;
	_WORD DisableResources;
	_WORD AngryId;
	_WORD BoneEatingTime;
};

enum AE_PathParams_EnumSwitchTargetAction : _WORD
{
	TurnOn = 0,
	TurnOff = 1,
	Toggle = 2,
	Increment = 3,
	Decrement = 4,
};
enum AE_PathParams_EnumSwitchScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumSwitchOnSound : _WORD
{
	None = 0,
	Well = 1,
	Bell = 2,
	Door = 3,
	ElectricWall = 4,
	MovingBomb = 5,
	Slamdoor = 6,
};
enum AE_PathParams_EnumSwitchOffSound : _WORD
{
	None = 0,
	Well = 1,
	Bell = 2,
	Door = 3,
	ElectricWall = 4,
	MovingBomb = 5,
	Slamdoor = 6,
};
enum AE_PathParams_EnumSwitchSoundDirection : _WORD
{
	Center = 0,
	Left = 1,
	Right = 2,
};
enum AE_PathParams_EnumSwitchPersistOffscreen : _DWORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_Switch
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumSwitchTargetAction TargetAction;
	AE_PathParams_EnumSwitchScale Scale;
	AE_PathParams_EnumSwitchOnSound OnSound;
	AE_PathParams_EnumSwitchOffSound OffSound;
	AE_PathParams_EnumSwitchSoundDirection SoundDirection;
	_WORD TriggerId;
	AE_PathParams_EnumSwitchPersistOffscreen PersistOffscreen;
};

enum AE_PathParams_EnumSecurityEyeScale : _DWORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_SecurityEye
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumSecurityEyeScale Scale;
};

enum AE_PathParams_EnumPulleyScale : _DWORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_Pulley
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumPulleyScale Scale;
};

enum AE_PathParams_EnumAbeStartScale : _DWORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_AbeStart
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumAbeStartScale Scale;
};

enum AE_PathParams_EnumWellExpressScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumWellExpressLeavesWhenOn : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_WellExpress
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumWellExpressScale Scale;
	_WORD Id;
	_WORD WellId;
	_WORD ResourceId;
	_WORD ExitX;
	_WORD ExitY;
	_WORD OffLevel;
	_WORD OffPath;
	_WORD OffCamera;
	_WORD OffWell;
	_WORD OnLevel;
	_WORD OnPath;
	_WORD OnCamera;
	_WORD OnWell;
	AE_PathParams_EnumWellExpressLeavesWhenOn LeavesWhenOn;
	_WORD LeafX;
	_WORD LeafY;
	_WORD Movie;
};

enum AE_PathParams_EnumMineScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumMinePersistsOffscreen : _DWORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_Mine
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD NumPatterns;
	_WORD Pattern;
	AE_PathParams_EnumMineScale Scale;
	_WORD DisabledResources;
	AE_PathParams_EnumMinePersistsOffscreen PersistsOffscreen;
};

enum AE_PathParams_EnumUxbScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumUxbState : _WORD
{
	Off = 1,
	On = 0,
};
struct AE_PathParams_Uxb
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD NumPatterns;
	_WORD Pattern;
	AE_PathParams_EnumUxbScale Scale;
	AE_PathParams_EnumUxbState State;
	_DWORD DisabledResources;
};

enum AE_PathParams_EnumParamiteScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumParamiteEntrance : _WORD
{
	OnGround = 0,
	DropIn = 1,
	CallFromAbove = 2,
	CallFromLeft = 3,
	CallFromRight = 4,
};
enum AE_PathParams_EnumParamiteHissBeforeAttack : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumParamiteDeleteWhenFarAway : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumParamiteDeadlyScratch : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_Paramite
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumParamiteScale Scale;
	AE_PathParams_EnumParamiteEntrance Entrance;
	_WORD AttackDelay;
	_WORD DropDelay;
	_WORD MeatEatingTime;
	_WORD AttackDuration;
	_WORD DisableResources;
	_WORD Id;
	AE_PathParams_EnumParamiteHissBeforeAttack HissBeforeAttack;
	AE_PathParams_EnumParamiteDeleteWhenFarAway DeleteWhenFarAway;
	AE_PathParams_EnumParamiteDeadlyScratch DeadlyScratch;
};

enum AE_PathParams_EnumMovieStoneScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_MovieStone
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD MovieNumber;
	AE_PathParams_EnumMovieStoneScale Scale;
	_WORD Id;
};

enum AE_PathParams_EnumBirdPortalSide : _WORD
{
	Left = 1,
	Right = 0,
};
enum AE_PathParams_EnumBirdPortalScale : _WORD
{
	Full = 0,
	Half = 1,
	_3Or4ths = 2,
};
enum AE_PathParams_EnumBirdPortalPortalType : _WORD
{
	Abe = 0,
	WorkerMud = 1,
	Shrykll = 2,
	MudTeleport = 3,
};
struct AE_PathParams_BirdPortal
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumBirdPortalSide Side;
	_WORD DestLevel;
	_WORD DestPath;
	_WORD DestCamera;
	AE_PathParams_EnumBirdPortalScale Scale;
	_WORD Movie;
	AE_PathParams_EnumBirdPortalPortalType PortalType;
	_WORD NumMudsForShrykll;
	_WORD CreateId;
	_WORD DeleteId;
};

enum AE_PathParams_EnumPortalExitSide : _WORD
{
	Left = 1,
	Right = 0,
};
enum AE_PathParams_EnumPortalExitScale : _WORD
{
	Full = 0,
	Half = 1,
	_3Or4ths = 2,
};
struct AE_PathParams_PortalExit
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumPortalExitSide Side;
	AE_PathParams_EnumPortalExitScale Scale;
};

enum AE_PathParams_EnumTrapDoorStartState : _WORD
{
	Open = 1,
	Closed = 0,
};
enum AE_PathParams_EnumTrapDoorSelfClosing : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumTrapDoorScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumTrapDoorDirection : _WORD
{
	Left = 0,
	Right = 1,
};
struct AE_PathParams_TrapDoor
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Id;
	AE_PathParams_EnumTrapDoorStartState StartState;
	AE_PathParams_EnumTrapDoorSelfClosing SelfClosing;
	AE_PathParams_EnumTrapDoorScale Scale;
	_WORD DestLevel;
	AE_PathParams_EnumTrapDoorDirection Direction;
	_WORD AnimOffset;
	_WORD StayOpenTime;
};

enum AE_PathParams_EnumAORollingBallScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumAORollingBallRollDirection : _WORD
{
	Left = 0,
	Right = 1,
};
struct AE_PathParams_AORollingBall
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumAORollingBallScale Scale;
	AE_PathParams_EnumAORollingBallRollDirection RollDirection;
	_WORD IdRelease;
	_WORD Speed;
	_DWORD Acceleration;
};

struct AE_PathParams_SligLeftBound
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD SligId;
	_WORD DisableResources;
};

struct AE_PathParams_InvisibleZone
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
};

enum AE_PathParams_EnumFootSwitchScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumFootSwitchAction : _WORD
{
	TurnOn = 0,
	TurnOff = 1,
	Toggle = 2,
	Inc = 3,
	Dec = 4,
};
enum AE_PathParams_EnumFootSwitchTriggerBy : _WORD
{
	AbeOnly = 0,
	Everyone = 1,
};
struct AE_PathParams_FootSwitch
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Id;
	AE_PathParams_EnumFootSwitchScale Scale;
	AE_PathParams_EnumFootSwitchAction Action;
	AE_PathParams_EnumFootSwitchTriggerBy TriggerBy;
};

enum AE_PathParams_EnumSecurityOrbScale : _DWORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_SecurityOrb
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumSecurityOrbScale Scale;
};

enum AE_PathParams_EnumMotionDetectorScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumMotionDetectorStartOn : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumMotionDetectorDrawFlare : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_MotionDetector
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumMotionDetectorScale Scale;
	_WORD DeviceX;
	_WORD DeviceY;
	_WORD SpeedX256;
	AE_PathParams_EnumMotionDetectorStartOn StartOn;
	AE_PathParams_EnumMotionDetectorDrawFlare DrawFlare;
	_WORD DisableId;
	_WORD AlarmId;
	_WORD AlarmTicks;
};

enum AE_PathParams_EnumSligSpawnerScale : _WORD
{
	Full = 0,
	Half = 1,
	_3Or4ths = 2,
};
enum AE_PathParams_EnumSligSpawnerStartState : _WORD
{
	Listening = 0,
	Paused = 1,
	Sleeping = 2,
	Chase = 3,
	RunOffScreen = 4,
	GameEnder = 5,
};
enum AE_PathParams_EnumSligSpawnerChaseAbe : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumSligSpawnerStartDirection : _WORD
{
	Left = 0,
	Right = 1,
};
enum AE_PathParams_EnumSligSpawnerTalkToAbe : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumSligSpawnerDontShoot : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumSligSpawnerStayAwake : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_SligSpawner
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumSligSpawnerScale Scale;
	AE_PathParams_EnumSligSpawnerStartState StartState;
	_WORD PauseTime;
	_WORD PauseLeftMin;
	_WORD PauseLeftMax;
	_WORD PauseRightMin;
	_WORD PauseRightMax;
	_WORD ChalNumber;
	_WORD ChalTimer;
	_WORD NumberOfTimesToShoot;
	_WORD Unknown;
	_WORD Code1;
	_WORD Code2;
	AE_PathParams_EnumSligSpawnerChaseAbe ChaseAbe;
	AE_PathParams_EnumSligSpawnerStartDirection StartDirection;
	_WORD PanicTimeout;
	_WORD NumPanicSounds;
	_WORD PanicSoundTimeout;
	_WORD StopChaseDelay;
	_WORD TimeToWaitBeforeChase;
	_WORD SligID;
	_WORD ListenTime;
	_WORD PercentSayWhat;
	_WORD PercentBeatMud;
	AE_PathParams_EnumSligSpawnerTalkToAbe TalkToAbe;
	AE_PathParams_EnumSligSpawnerDontShoot DontShoot;
	_WORD ZShootDelay;
	AE_PathParams_EnumSligSpawnerStayAwake StayAwake;
	_WORD DisableResources;
	_WORD NoiseWakeUpDistance;
	_WORD Id;
	_WORD SpawnMany;
};

enum AE_PathParams_EnumElectricWallScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumElectricWallStart : _WORD
{
	On = 1,
	Off = 0,
};
struct AE_PathParams_ElectricWall
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumElectricWallScale Scale;
	_WORD Id;
	AE_PathParams_EnumElectricWallStart Start;
};

enum AE_PathParams_EnumLiftMoverDirection : _WORD
{
	Up = 0,
	Down = 1,
};
struct AE_PathParams_LiftMover
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD SwitchId;
	_WORD LiftId;
	AE_PathParams_EnumLiftMoverDirection Direction;
};

enum AE_PathParams_EnumMeatSackSide : _WORD
{
	Left = 0,
	Right = 1,
};
enum AE_PathParams_EnumMeatSackScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_MeatSack
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumMeatSackSide Side;
	_WORD XVel;
	_WORD YVel;
	AE_PathParams_EnumMeatSackScale Scale;
	_WORD NumberOfItems;
};

enum AE_PathParams_EnumScrabScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumScrabPatrolType : _WORD
{
	Walk100Percent = 0,
	Walk75Percent = 1,
	Walk50Percent = 2,
	Walk25Percent = 3,
	Walk0Percent = 4,
};
enum AE_PathParams_EnumScrabRoarRandomly : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumScrabPersistant : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumScrabKillCloseFleech : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_Scrab
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumScrabScale Scale;
	_WORD AttackDelay;
	AE_PathParams_EnumScrabPatrolType PatrolType;
	_WORD LeftMinDelay;
	_WORD LeftMaxDelay;
	_WORD RightMinDelay;
	_WORD RightMaxDelay;
	_WORD AttackDuration;
	_WORD DisableResources;
	AE_PathParams_EnumScrabRoarRandomly RoarRandomly;
	AE_PathParams_EnumScrabPersistant Persistant;
	_WORD WhirlAttackDuration;
	_WORD WhirlAttackRecharge;
	AE_PathParams_EnumScrabKillCloseFleech KillCloseFleech;
};

struct AE_PathParams_ScrabLeftBound
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
};

struct AE_PathParams_ScrabRightBound
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
};

struct AE_PathParams_SligRightBound
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD SligId;
	_WORD DisableResources;
};

struct AE_PathParams_SligPersist
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD SligId;
	_WORD DisableResources;
};

enum AE_PathParams_EnumEnemyStopperStopDirection : _WORD
{
	Left = 0,
	Right = 1,
	Both = 2,
};
struct AE_PathParams_EnemyStopper
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumEnemyStopperStopDirection StopDirection;
	_WORD Id;
};

enum AE_PathParams_EnumInvisibleSwitchIdAction : _WORD
{
	TurnOn = 0,
	TurnOff = 1,
	Toggle = 2,
	Inc = 3,
	Dec = 4,
};
enum AE_PathParams_EnumInvisibleSwitchSetOffAlarm : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumInvisibleSwitchScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_InvisibleSwitch
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Id;
	AE_PathParams_EnumInvisibleSwitchIdAction IdAction;
	_WORD Delay;
	AE_PathParams_EnumInvisibleSwitchSetOffAlarm SetOffAlarm;
	AE_PathParams_EnumInvisibleSwitchScale Scale;
};

enum AE_PathParams_EnumMudScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumMudState : _WORD
{
	Chisle = 0,
	Scrub = 1,
	AngryWorker = 2,
	DamageRingGiver = 3,
	HealthRingGiver = 4,
};
enum AE_PathParams_EnumMudDirection : _WORD
{
	Left = 0,
	Right = 1,
};
enum AE_PathParams_EnumMudDeaf : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumMudEmotion : _WORD
{
	Normal = 0,
	Angry = 1,
	Sad = 2,
	Wired = 3,
	Sick = 4,
};
enum AE_PathParams_EnumMudGetsDepressed : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumMudInstantPowerup : _DWORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_Mud
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumMudScale Scale;
	AE_PathParams_EnumMudState State;
	AE_PathParams_EnumMudDirection Direction;
	_WORD VoicePitch;
	_WORD RescueId;
	AE_PathParams_EnumMudDeaf Deaf;
	_WORD DisableResources;
	_WORD SaveState;
	AE_PathParams_EnumMudEmotion Emotion;
	_WORD Blind;
	_WORD AngryTrigger;
	_WORD StopTrigger;
	AE_PathParams_EnumMudGetsDepressed GetsDepressed;
	_WORD RingTimeout;
	AE_PathParams_EnumMudInstantPowerup InstantPowerup;
};

struct AE_PathParams_ZSligCover
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
};

enum AE_PathParams_EnumDoorFlameScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumDoorFlameColour : _DWORD
{
	Normal = 0,
	Red = 1,
	Green = 2,
	Blue = 3,
};
struct AE_PathParams_DoorFlame
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Id;
	AE_PathParams_EnumDoorFlameScale Scale;
	AE_PathParams_EnumDoorFlameColour Colour;
};

enum AE_PathParams_EnumMovingBombStartType : _WORD
{
	WhenCreated = 0,
	WhenAlarmSounds = 1,
};
enum AE_PathParams_EnumMovingBombScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumMovingBombPersistOffscreen : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_MovingBomb
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Speed;
	_WORD Id;
	AE_PathParams_EnumMovingBombStartType StartType;
	AE_PathParams_EnumMovingBombScale Scale;
	_WORD MaxRise;
	_WORD DisableResources;
	_WORD StartSpeed;
	AE_PathParams_EnumMovingBombPersistOffscreen PersistOffscreen;
};

struct AE_PathParams_MenuController
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
};

struct AE_PathParams_TimerTrigger
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Id;
	_WORD DelayTicks;
	_WORD Id1;
	_WORD Id2;
	_WORD Id3;
	_WORD Id4;
};

enum AE_PathParams_EnumSecurityDoorScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_SecurityDoor
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumSecurityDoorScale Scale;
	_WORD Id;
	_WORD Code1;
	_WORD Code2;
	_WORD XPos;
	_WORD YPos;
};

enum AE_PathParams_EnumGrenadeMachineScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumGrenadeMachineSpoutSide : _WORD
{
	Left = 0,
	Right = 1,
};
struct AE_PathParams_GrenadeMachine
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumGrenadeMachineScale Scale;
	AE_PathParams_EnumGrenadeMachineSpoutSide SpoutSide;
	_WORD DisabledResources;
	_WORD NumberOfGrenades;
};

struct AE_PathParams_LCDScreen
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Message1Id;
	_WORD MessageRandMin;
	_WORD MessageRandMax;
	_WORD Message2Id;
	_DWORD IdToSwitchMessageSets;
};

enum AE_PathParams_EnumHandStoneScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_HandStone
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumHandStoneScale Scale;
	_WORD Camera1;
	_WORD Camera2;
	_WORD Camera3;
	_DWORD TriggerId;
};

struct AE_PathParams_CreditsController
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
};

struct AE_PathParams_NullObject63
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
};

enum AE_PathParams_EnumLCDStatusBoardHidden : _DWORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_LCDStatusBoard
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD NumberOfMuds;
	_WORD ZulagNumber;
	AE_PathParams_EnumLCDStatusBoardHidden Hidden;
};

enum AE_PathParams_EnumWheelSyncerAction : _WORD
{
	TurnOn = 0,
	TurnOff = 1,
	Toggle = 2,
};
struct AE_PathParams_WheelSyncer
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Id1;
	_WORD Id2;
	_WORD TriggerId;
	AE_PathParams_EnumWheelSyncerAction Action;
	_WORD Id3;
	_WORD Id4;
	_WORD Id5;
	_WORD Id6;
};

enum AE_PathParams_EnumMusicType : _WORD
{
	Secret = 0,
	Negative = 1,
	Positive = 2,
	SligAttack = 3,
	SlogAttack = 4,
	Silence = 5,
};
enum AE_PathParams_EnumMusicStart : _WORD
{
	PlayerTouch = 0,
	ByTime = 1,
};
struct AE_PathParams_Music
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumMusicType Type;
	AE_PathParams_EnumMusicStart Start;
	_DWORD TimeTicks;
};

enum AE_PathParams_EnumAOLightEffectType : _WORD
{
	Star = 0,
	RedOrGoldGlow = 1,
	GreenGlow = 2,
	FlintGlow = 3,
	FlintDoor = 4,
	FlintHub = 5,
};
enum AE_PathParams_EnumAOLightEffectFlipX : _WORD
{
	Yes = 0,
	No = 1,
};
struct AE_PathParams_AOLightEffect
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumAOLightEffectType Type;
	_WORD Size;
	_WORD Id;
	AE_PathParams_EnumAOLightEffectFlipX FlipX;
};

enum AE_PathParams_EnumSlogSpawnerScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumSlogSpawnerDirection : _WORD
{
	Left = 0,
	Right = 1,
};
enum AE_PathParams_EnumSlogSpawnerListenToSligs : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_SlogSpawner
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumSlogSpawnerScale Scale;
	_WORD NumberOfSlogs;
	_WORD AtATime;
	AE_PathParams_EnumSlogSpawnerDirection Direction;
	_WORD TicksBetweenSlogs;
	_WORD Id;
	AE_PathParams_EnumSlogSpawnerListenToSligs ListenToSligs;
	_WORD JumpAttackDelay;
};

struct AE_PathParams_DeathClock
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD StartTriggerId;
	_WORD Time;
	_DWORD StopTriggerId;
};

enum AE_PathParams_EnumGasEmitterGasColour : _WORD
{
	Yellow = 0,
	Red = 1,
	Blue = 2,
	Green = 3,
	White = 4,
};
struct AE_PathParams_GasEmitter
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD PortId;
	AE_PathParams_EnumGasEmitterGasColour GasColour;
};

enum AE_PathParams_EnumSlogHutScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_SlogHut
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumSlogHutScale Scale;
	_WORD Id;
	_DWORD TicksBetweenZs;
};

enum AE_PathParams_EnumGlukkonScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumGlukkonDirection : _WORD
{
	Right = 0,
	Left = 1,
};
enum AE_PathParams_EnumGlukkonCalmMotion : _WORD
{
	StandOnly = 0,
	BetweenTags = 1,
};
enum AE_PathParams_EnumGlukkonGlukkonType : _WORD
{
	Normal1 = 0,
	Aslik = 1,
	Drpik = 2,
	Phleg = 3,
	Normal2 = 4,
	Normal3 = 5,
};
struct AE_PathParams_Glukkon
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumGlukkonScale Scale;
	AE_PathParams_EnumGlukkonDirection Direction;
	AE_PathParams_EnumGlukkonCalmMotion CalmMotion;
	_WORD PreAlarmDelay;
	_WORD PostAlarmDelay;
	_WORD SpawnId;
	_WORD SpawnDirection;
	_WORD SpawnDelay;
	AE_PathParams_EnumGlukkonGlukkonType GlukkonType;
	_WORD StartGasId;
	_WORD PlayMovieId;
	_WORD MovieId;
};

struct AE_PathParams_AOKillUnsavedMuds
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
};

struct AE_PathParams_SoftLanding
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_DWORD Id;
};

struct AE_PathParams_Water
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD MaxDrops;
	_WORD Id;
	_WORD SplashTime;
	_WORD SplashXVelocity;
	_WORD SplashYVelocity;
	_WORD Timeout;
};

enum AE_PathParams_EnumWheelScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumWheelOffWhenStopped : _DWORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_Wheel
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumWheelScale Scale;
	_WORD Id;
	_WORD Duration;
	_WORD OffTime;
	AE_PathParams_EnumWheelOffWhenStopped OffWhenStopped;
};

enum AE_PathParams_EnumLaughingGasIsLaughingGas : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_LaughingGas
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumLaughingGasIsLaughingGas IsLaughingGas;
	_WORD GasId;
	_WORD RedPercent;
	_WORD GreenPercent;
	_DWORD BluePercent;
};

enum AE_PathParams_EnumFlyingSligScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumFlyingSligState : _WORD
{
	Patrol = 0,
	Paused = 1,
};
enum AE_PathParams_EnumFlyingSligDirection : _WORD
{
	Left = 0,
	Right = 1,
};
enum AE_PathParams_EnumFlyingSligPersistant : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_FlyingSlig
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumFlyingSligScale Scale;
	AE_PathParams_EnumFlyingSligState State;
	_WORD HiPauseTime;
	_WORD PatrolPauseMin;
	_WORD PatrolPauseMax;
	AE_PathParams_EnumFlyingSligDirection Direction;
	_WORD PanicDelay;
	_WORD GiveUpChaseDelay;
	_WORD PrechaseDelay;
	_WORD SligId;
	_WORD ListenTime;
	_WORD TriggerId;
	_WORD GrenadeDelay;
	_WORD MaxVelocity;
	_WORD LaunchId;
	AE_PathParams_EnumFlyingSligPersistant Persistant;
};

enum AE_PathParams_EnumFleechScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumFleechDirection : _WORD
{
	Left = 0,
	Right = 1,
};
enum AE_PathParams_EnumFleechAsleep : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumFleechHanging : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumFleechGoesToSleep : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumFleechPersistant : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_Fleech
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumFleechScale Scale;
	AE_PathParams_EnumFleechDirection Direction;
	AE_PathParams_EnumFleechAsleep Asleep;
	_WORD WakeUp;
	_WORD NotUsed;
	_WORD AttackAnger;
	_WORD AttackDelay;
	_WORD WakeUpId;
	AE_PathParams_EnumFleechHanging Hanging;
	_WORD LostTargetTimeout;
	AE_PathParams_EnumFleechGoesToSleep GoesToSleep;
	_WORD PatrolRangeGrids;
	_WORD Unused;
	_WORD AllowWakeUpId;
	AE_PathParams_EnumFleechPersistant Persistant;
};

enum AE_PathParams_EnumSlurgDirection : _WORD
{
	Right = 0,
	Left = 1,
};
enum AE_PathParams_EnumSlurgScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_Slurg
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD PauseDelay;
	AE_PathParams_EnumSlurgDirection Direction;
	AE_PathParams_EnumSlurgScale Scale;
	_WORD Id;
};

enum AE_PathParams_EnumSlamDoorStartsShut : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumSlamDoorHalfScale : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumSlamDoorInverted : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumSlamDoorDelete : _DWORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_SlamDoor
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumSlamDoorStartsShut StartsShut;
	AE_PathParams_EnumSlamDoorHalfScale HalfScale;
	_WORD Id;
	AE_PathParams_EnumSlamDoorInverted Inverted;
	AE_PathParams_EnumSlamDoorDelete Delete;
};

struct AE_PathParams_LevelLoader
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Id;
	_WORD DestLevel;
	_WORD DestPath;
	_WORD DestCamera;
	_DWORD Movie;
};

struct AE_PathParams_DemoSpawnPoint
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
};

enum AE_PathParams_EnumTeleporterHalfScale : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumTeleporterWipe : _WORD
{
	Movie = 0,
	Left = 1,
	Right = 2,
	Up = 3,
	Down = 4,
	Rect = 5,
	Vert = 6,
	Horz = 7,
	MovieFirst = 8,
};
struct AE_PathParams_Teleporter
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Id;
	_WORD TargetId;
	_WORD Camera;
	_WORD Path;
	_WORD Level;
	_WORD TriggerId;
	AE_PathParams_EnumTeleporterHalfScale HalfScale;
	AE_PathParams_EnumTeleporterWipe Wipe;
	_WORD MovieNumber;
	_WORD ElectricX;
	_DWORD ElectricY;
};

enum AE_PathParams_EnumSlurgSpawnerDirection : _WORD
{
	Right = 0,
	Left = 1,
};
enum AE_PathParams_EnumSlurgSpawnerScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_SlurgSpawner
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD PauseDelay;
	AE_PathParams_EnumSlurgSpawnerDirection Direction;
	AE_PathParams_EnumSlurgSpawnerScale Scale;
	_WORD Id;
	_WORD DelayBetweenSlurgs;
	_WORD MaxSlurgs;
	_DWORD SpawnerId;
};

enum AE_PathParams_EnumGrinderScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumGrinderBehavior : _WORD
{
	Normal = 0,
	IdOnOroff = 1,
	SwitchControl = 2,
};
enum AE_PathParams_EnumGrinderStartState : _WORD
{
	On = 1,
	Off = 0,
};
enum AE_PathParams_EnumGrinderStartPosition : _WORD
{
	Top = 1,
	Bottom = 0,
};
enum AE_PathParams_EnumGrinderDirection : _WORD
{
	Down = 0,
	Right = 1,
	Left = 2,
};
struct AE_PathParams_Grinder
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumGrinderScale Scale;
	_WORD MinOffTime;
	_WORD MaxOffTime;
	_WORD Id;
	AE_PathParams_EnumGrinderBehavior Behavior;
	_WORD Speed;
	AE_PathParams_EnumGrinderStartState StartState;
	_WORD OffSpeed;
	_WORD MinOffTime2;
	_WORD MaxOffTime2;
	AE_PathParams_EnumGrinderStartPosition StartPosition;
	AE_PathParams_EnumGrinderDirection Direction;
};

struct AE_PathParams_ColorfulMeter
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Id;
	_WORD NumberOfMeterBars;
	_WORD Timer;
	_WORD StartsFull;
};

enum AE_PathParams_EnumFlyingSligSpawnerScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumFlyingSligSpawnerState : _WORD
{
	Patrol = 0,
	Paused = 1,
};
enum AE_PathParams_EnumFlyingSligSpawnerDirection : _WORD
{
	Left = 0,
	Right = 1,
};
enum AE_PathParams_EnumFlyingSligSpawnerPersistant : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_FlyingSligSpawner
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumFlyingSligSpawnerScale Scale;
	AE_PathParams_EnumFlyingSligSpawnerState State;
	_WORD HiPauseTime;
	_WORD PatrolPauseMin;
	_WORD PatrolPauseMax;
	AE_PathParams_EnumFlyingSligSpawnerDirection Direction;
	_WORD PanicDelay;
	_WORD GiveUpChaseDelay;
	_WORD PrechaseDelay;
	_WORD SligId;
	_WORD ListenTime;
	_WORD TriggerId;
	_WORD GrenadeDelay;
	_WORD MaxVelocity;
	_WORD LaunchId;
	AE_PathParams_EnumFlyingSligSpawnerPersistant Persistant;
};

enum AE_PathParams_EnumMineCarScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_MineCar
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumMineCarScale Scale;
	_WORD MaxDamage;
};

enum AE_PathParams_EnumSlogFoodSackSide : _WORD
{
	Left = 0,
	Right = 1,
};
enum AE_PathParams_EnumSlogFoodSackScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_SlogFoodSack
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumSlogFoodSackSide Side;
	_WORD XVel;
	_WORD YVel;
	AE_PathParams_EnumSlogFoodSackScale Scale;
	_WORD NumberOfBones;
};

enum AE_PathParams_EnumExplosionSetStartInstantly : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumExplosionSetBigRocks : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumExplosionSetSequnceDirection : _WORD
{
	LeftToRight = 1,
	RightToLeft = 0,
};
enum AE_PathParams_EnumExplosionSetScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_ExplosionSet
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumExplosionSetStartInstantly StartInstantly;
	_WORD Id;
	AE_PathParams_EnumExplosionSetBigRocks BigRocks;
	_WORD StartDelay;
	AE_PathParams_EnumExplosionSetSequnceDirection SequnceDirection;
	_WORD SequnceDelay;
	_WORD SpacingGrids;
	AE_PathParams_EnumExplosionSetScale Scale;
};

struct AE_PathParams_MultiswitchController
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
};

enum AE_PathParams_EnumRedGreenStatusLightScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumRedGreenStatusLightSnapToGrid : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_RedGreenStatusLight
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Id;
	AE_PathParams_EnumRedGreenStatusLightScale Scale;
	_WORD OtherId1;
	_WORD OtherId2;
	_WORD OtherId3;
	_WORD OtherId4;
	_WORD OtherId5;
	AE_PathParams_EnumRedGreenStatusLightSnapToGrid SnapToGrid;
};

enum AE_PathParams_EnumSlapLockScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumSlapLockIsPersistant : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumSlapLockHasGhost : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumSlapLockHasPowerup : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_SlapLock
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumSlapLockScale Scale;
	_WORD TargetTombId1;
	_WORD TargetTombId2;
	AE_PathParams_EnumSlapLockIsPersistant IsPersistant;
	AE_PathParams_EnumSlapLockHasGhost HasGhost;
	AE_PathParams_EnumSlapLockHasPowerup HasPowerup;
	_WORD PowerupId;
	_WORD OptionId;
};

enum AE_PathParams_EnumParamiteNetScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_ParamiteNet
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumParamiteNetScale Scale;
};

struct AE_PathParams_Alarm
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD Id;
	_WORD Duration;
};

struct AE_PathParams_FartMachine
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD NumberOfBrews;
};

enum AE_PathParams_EnumScrabSpawnerScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumScrabSpawnerPatrolType : _WORD
{
	Walk100Percent = 0,
	Walk75Percent = 1,
	Walk50Percent = 2,
	Walk25Percent = 3,
	Walk0Percent = 4,
};
enum AE_PathParams_EnumScrabSpawnerRoarRandomly : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumScrabSpawnerPersistant : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumScrabSpawnerKillCloseFleech : _WORD
{
	Yes = 1,
	No = 0,
};
enum AE_PathParams_EnumScrabSpawnerAppearFrom : _WORD
{
	ObjectPosition = 0,
	OffScreenLeft = 1,
	OffScreenRight = 2,
};
struct AE_PathParams_ScrabSpawner
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumScrabSpawnerScale Scale;
	_WORD AttackDelay;
	AE_PathParams_EnumScrabSpawnerPatrolType PatrolType;
	_WORD LeftMinDelay;
	_WORD LeftMaxDelay;
	_WORD RightMinDelay;
	_WORD RightMaxDelay;
	_WORD AttackDuration;
	_WORD DisableResources;
	AE_PathParams_EnumScrabSpawnerRoarRandomly RoarRandomly;
	AE_PathParams_EnumScrabSpawnerPersistant Persistant;
	_WORD WhirlAttackDuration;
	_WORD WhirlAttackRecharge;
	AE_PathParams_EnumScrabSpawnerKillCloseFleech KillCloseFleech;
	_WORD Id;
	AE_PathParams_EnumScrabSpawnerAppearFrom AppearFrom;
};

enum AE_PathParams_EnumNakedSligScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumNakedSligDirection : _WORD
{
	Left = 0,
	Right = 1,
};
enum AE_PathParams_EnumNakedSligState : _WORD
{
	Awake = 0,
	Asleep = 1,
};
enum AE_PathParams_EnumNakedSligLockerDirection : _WORD
{
	Left = 0,
	Right = 1,
	Search = 2,
};
enum AE_PathParams_EnumNakedSligResetOnDeath : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_NakedSlig
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumNakedSligScale Scale;
	AE_PathParams_EnumNakedSligDirection Direction;
	AE_PathParams_EnumNakedSligState State;
	AE_PathParams_EnumNakedSligLockerDirection LockerDirection;
	_WORD PanicId;
	AE_PathParams_EnumNakedSligResetOnDeath ResetOnDeath;
};

struct AE_PathParams_SligGetPants
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
};

enum AE_PathParams_EnumSligGetWingsScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumSligGetWingsState : _WORD
{
	Patrol = 0,
	Paused = 1,
};
enum AE_PathParams_EnumSligGetWingsDirection : _WORD
{
	Left = 0,
	Right = 1,
};
enum AE_PathParams_EnumSligGetWingsPersistant : _WORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_SligGetWings
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumSligGetWingsScale Scale;
	AE_PathParams_EnumSligGetWingsState State;
	_WORD HiPauseTime;
	_WORD PatrolPauseMin;
	_WORD PatrolPauseMax;
	AE_PathParams_EnumSligGetWingsDirection Direction;
	_WORD PanicDelay;
	_WORD GiveUpChaseDelay;
	_WORD PrechaseDelay;
	_WORD SligId;
	_WORD ListenTime;
	_WORD TriggerId;
	_WORD GrenadeDelay;
	_WORD MaxVelocity;
	_WORD LaunchId;
	AE_PathParams_EnumSligGetWingsPersistant Persistant;
};

enum AE_PathParams_EnumGreeterScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumGreeterDirection : _WORD
{
	Left = 0,
	Right = 1,
};
struct AE_PathParams_Greeter
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumGreeterScale Scale;
	_WORD MotionDetectorSpeed;
	AE_PathParams_EnumGreeterDirection Direction;
};

enum AE_PathParams_EnumNakedSligButtonScale : _WORD
{
	Full = 0,
	Half = 1,
};
enum AE_PathParams_EnumNakedSligButtonIdAction : _WORD
{
	TurnOn = 0,
	TurnOff = 1,
	Toggle = 2,
	Inc = 3,
	Dec = 4,
};
enum AE_PathParams_EnumNakedSligButtonOnSound : _WORD
{
	None = 0,
	Well = 1,
	Bell = 2,
	Door = 3,
	ElectricWall = 4,
	MovingBomb = 5,
	Slamdoor = 6,
};
enum AE_PathParams_EnumNakedSligButtonOffSound : _WORD
{
	None = 0,
	Well = 1,
	Bell = 2,
	Door = 3,
	ElectricWall = 4,
	MovingBomb = 5,
	Slamdoor = 6,
};
enum AE_PathParams_EnumNakedSligButtonSoundDirection : _WORD
{
	Center = 0,
	Left = 1,
	Right = 2,
};
struct AE_PathParams_NakedSligButton
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumNakedSligButtonScale Scale;
	_WORD Id;
	AE_PathParams_EnumNakedSligButtonIdAction IdAction;
	AE_PathParams_EnumNakedSligButtonOnSound OnSound;
	AE_PathParams_EnumNakedSligButtonOffSound OffSound;
	AE_PathParams_EnumNakedSligButtonSoundDirection SoundDirection;
};

enum AE_PathParams_EnumGlukkonSecurityDoorScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_GlukkonSecurityDoor
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumGlukkonSecurityDoorScale Scale;
	_WORD OkId;
	_WORD FailId;
	_WORD XPos;
	_DWORD YPos;
};

enum AE_PathParams_EnumDoorBlockerScale : _WORD
{
	Full = 0,
	Half = 1,
};
struct AE_PathParams_DoorBlocker
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumDoorBlockerScale Scale;
	_WORD Id;
};

struct AE_PathParams_TorturedMudokon
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	_WORD SpeedId;
	_WORD ReleaseId;
};

enum AE_PathParams_EnumTrainDoorXFlip : _DWORD
{
	Yes = 1,
	No = 0,
};
struct AE_PathParams_TrainDoor
{
	_WORD HeaderUnknown1;
	_WORD HeaderUnknown2;
	_WORD HeaderUnknown3;
	_WORD HeaderUnknown4;
	_WORD RectTopLeftX;
	_WORD RectTopLeftY;
	_WORD RectBottomRightX;
	_WORD RectBottomRightY;
	AE_PathParams_EnumTrainDoorXFlip XFlip;
};

