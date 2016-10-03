#pragma once

#define SCRIPT_FUNCTION extern "C" __declspec(dllexport)

#define _CRT_SECURE_NO_WARNINGS

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN 

// Windows Header Files:
#include <windows.h>

#include <iostream>
#include <fstream>
#include <stdio.h>
#include <typeinfo.h>
#include <string>
#include <iostream>

#include "MinHook.h"

#include "AeTypes.h"
#include "AliveApi.h"