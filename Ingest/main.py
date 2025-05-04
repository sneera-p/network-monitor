import bin.appcore_py
from bin.appcore_py import py_device_status_as_string, py_device_status_parse

print(py_device_status_as_string(0))
print(py_device_status_parse("Offline"))