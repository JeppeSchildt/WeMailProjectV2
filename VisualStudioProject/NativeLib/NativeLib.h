#ifndef _NATIVELIB_H_
#define _NATIVELIB_H_

#ifndef MYAPI
  #define MYAPI 
#endif

#ifdef __cplusplus
extern "C" {
#endif

MYAPI void print_line(const char* str);
// Exported functions go here.


#ifdef __cplusplus
}
#endif

#endif // _NATIVELIB_H_
