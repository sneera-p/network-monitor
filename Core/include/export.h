#ifdef __EMSCRIPTEN__
#define LIVE  EMSCRIPTEN_KEEPALIVE
#include <emscripten/emscripten.h>
#else
#define LIVE 
#endif

#ifdef _WIN32
#define EXPORT __declspec(dllexport)
#else
#define EXPORT __attribute__((visibility("default")))
#endif