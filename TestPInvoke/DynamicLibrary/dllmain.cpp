﻿// dllmain.cpp : DLL 애플리케이션의 진입점을 정의합니다.
#include "pch.h"

#define MY_DECLSPEC __declspec(dllexport)

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}


extern "C" MY_DECLSPEC int Matches(char* buffer, int bufferSize, int* indexArray, int indexArraySize)
{
    int cond;
    for (size_t i = 0; i < bufferSize; i++)
    {
        if (buffer[i] == 1)
        {
            indexArray[i % 100] = i;
        }
        else
        {
            indexArray[i % 100] = i;
        }
    }
    
    
    //*data2[0] = 777;

    //std::cout << ((int)intArray[0]) <<std::endl;

    return 1;
}