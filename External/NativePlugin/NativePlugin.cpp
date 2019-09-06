// NativePlugin.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "NativePlugin.h"
#include <string>

//---------------------------------------------------------------------------------------------------------------------

UINT32Callback g_dataCallback = nullptr;
StringCallback g_logCallback = nullptr;

uint32_t g_data[] = { 0,1,2,3,4,5,6,7,8,9 };

//---------------------------------------------------------------------------------------------------------------------

void SetDataCallback(UINT32Callback callback) {
    g_dataCallback = callback;
}

//---------------------------------------------------------------------------------------------------------------------

void SetLogCallback(StringCallback callback) {
    g_logCallback = callback;
}

//---------------------------------------------------------------------------------------------------------------------

void Execute() {

    if (nullptr != g_dataCallback) {
        g_dataCallback(g_data);
    }

    if (nullptr != g_logCallback) {
        const char* LOG_MESSAGE = "This is a message from the native plugin.";
        g_logCallback(LOG_MESSAGE);
    }

}
