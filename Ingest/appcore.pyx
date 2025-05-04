from cpython.bytes cimport PyBytes_FromString
from cpython.unicode cimport PyUnicode_FromString
from libc.string cimport strdup
from libc.stdlib cimport free

cdef extern from "appcore.h":
    char* device_status_as_string(int value)
    int device_status_parse(char* value)

    char* incident_severity_as_string(int value)
    int incident_severity_parse(char* value)

def py_device_status_as_string(int value):
    cdef char* cstr = device_status_as_string(value)
    return PyUnicode_FromString(cstr)

def py_device_status_parse(str value):
    return device_status_parse(value.encode('utf-8'))

def py_incident_severity_as_string(int value):
    cdef char* cstr = incident_severity_as_string(value)
    return PyUnicode_FromString(cstr)

def py_incident_severity_parse(str value):
    return incident_severity_parse(value.encode('utf-8'))
