#include <stdio.h>
#include <assert.h>
#include <string.h>
#include "../../../Core/include/appcore.h"

void test_device_status_as_string_0(void)
{
    char *result = device_status_as_string(0);
    assert(strcmp(result, "Online") == 0);
}

void test_device_status_as_string_1(void)
{
    char *result = device_status_as_string(1);
    assert(strcmp(result, "Offline") == 0);
}

void test_device_status_as_string_2(void)
{
    char *result = device_status_as_string(2);
    assert(strcmp(result, "Maintenance") == 0);
}

int main(void)
{
    test_device_status_as_string_0();
    test_device_status_as_string_1();
    test_device_status_as_string_2();
    printf("All Tests Passed\n");
    return 0;
}