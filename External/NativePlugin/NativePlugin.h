#pragma once

#include "stdint.h"

typedef void (__stdcall *UINT32Callback)(const uint32_t* data);
typedef void (__stdcall *StringCallback)(const char* str);

extern "C" __declspec(dllexport) void SetDataCallback(UINT32Callback callback);
extern "C" __declspec(dllexport) void SetLogCallback(StringCallback callback);

extern "C" __declspec(dllexport) void Execute();