#pragma once

#ifdef ExternalCPlusPlusLibrary_EXPORTS
#define ExternalCPlusPlusLibrary_API __declspec(dllexport)
#else
#define ExternalCPlusPlusLibrary_API __declspec(dllimport)
#endif

extern "C" ExternalCPlusPlusLibrary_API long CalculateSum(
    const unsigned long long a, const unsigned long long b);
