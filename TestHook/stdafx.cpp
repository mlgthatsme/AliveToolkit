// stdafx.cpp : source file that includes just the standard includes
// TestHook.pch will be the pre-compiled header
// stdafx.obj will contain the pre-compiled type information

#include "stdafx.h"

// TODO: reference any additional headers you need in STDAFX.H
// and not in this file

std::vector<std::string> split(const std::string& s, char seperator)
{
	std::vector<std::string> output;

	std::string::size_type prev_pos = 0, pos = 0;

	while ((pos = s.find(seperator, pos)) != std::string::npos)
	{
		std::string substring(s.substr(prev_pos, pos - prev_pos));

		output.push_back(substring);

		prev_pos = ++pos;
	}

	output.push_back(s.substr(prev_pos, pos - prev_pos)); // Last word

	return output;
}

int GetGameType()
{
	char buffer[MAX_PATH + 1] = {};
	::GetModuleFileNameA(GetModuleHandle(NULL), buffer, _countof(buffer));

	const auto exeName = split(std::string(buffer), L'\\').back();
	if (strcmp(exeName.c_str(), "Exoddus.exe") == 0 || strcmp(exeName.c_str(), "Exoddus_AliveEdition.exe") == 0)
		return 2;
	else if (strcmp(exeName.c_str(), "AbeWin.exe") == 0)
		return 1;
	else
		return 0;
}