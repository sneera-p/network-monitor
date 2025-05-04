#include <stdlib.h>
#include <string.h>
#include "../obj/proto/shared/status.pb-c.h"
#include "../include/status.h"

LIVE EXPORT char* device_status_as_string(int value)
{
    char* status = NULL;
    switch (value)
    {
        case CORE__DEVICE_STATUS__ONLINE:
            status = "Online";
            break;
        case CORE__DEVICE_STATUS__OFFLINE:
            status = "Offline";
            break;
        case CORE__DEVICE_STATUS__MAINTENANCE:
            status = "Maintainance";
            break;
        default:
            status = "";
            break;
    }

    // Allocate new string
    char* result = malloc(strlen(status) + 1);
    if (result)
        strcpy(result, status);
    return result;
}

LIVE EXPORT int device_status_parse(char* value)
{
    if (strcmp(value, "Online") == 0)
    {
        return CORE__DEVICE_STATUS__ONLINE;
    }
    else if (strcmp(value, "Offline") == 0)
    {
        return CORE__DEVICE_STATUS__OFFLINE;
    }
    else if (strcmp(value, "Maintenance") == 0)
    {
        return CORE__DEVICE_STATUS__MAINTENANCE;
    }
    else
    {
        return -1;
    }
}

LIVE EXPORT char* incident_severity_as_string(int value)
{
    char* severity = NULL;

    switch (value)
    {
        case CORE__INCIDENT_SEVERITY__INFORMATION:
            severity = "Information";
            break;
        case CORE__INCIDENT_SEVERITY__LOW:
            severity = "Low";
            break;
        case CORE__INCIDENT_SEVERITY__AVERAGE:
            severity = "Average";
            break;
        case CORE__INCIDENT_SEVERITY__HIGH:
            severity = "High";
            break;
        case CORE__INCIDENT_SEVERITY__CRITICAL:
            severity = "Critical";
            break;
        default:
            severity = "";
            break;
    }

    // Allocate new string
    char* result = malloc(strlen(severity) + 1);
    if (result)
        strcpy(result, severity);
    return result;
}

LIVE EXPORT int incident_severity_parse(char* value)
{
    if (strcmp(value, "Information"))
    {
        return CORE__INCIDENT_SEVERITY__INFORMATION;
    }
    else if (strcmp(value, "Low"))
    {
        return CORE__INCIDENT_SEVERITY__LOW;
    }
    else if (strcmp(value, "Average"))
    {
        return CORE__INCIDENT_SEVERITY__AVERAGE;
    }
    else if (strcmp(value, "High"))
    {
        return CORE__INCIDENT_SEVERITY__HIGH;
    }
    else if (strcmp(value, "Critical"))
    {
        return CORE__INCIDENT_SEVERITY__CRITICAL;
    }
    else
    {
        return -1;
    }
}