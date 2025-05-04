from setuptools import setup, Extension
from Cython.Build import cythonize
import os

ext_modules = [
    Extension(
        name="appcore_py",
        sources=["appcore.pyx"],
        libraries=["appcore", "protobuf-c"],
        library_dirs=["./bin"],
        include_dirs=["../Core/include"],
        runtime_library_dirs=["./bin"]  # For Linux; helps find libappcore.so at runtime
    )
]

setup(
    name="appcore_wrapper",
    ext_modules=cythonize(ext_modules),
    # build_lib='bin',
    # build_temp='obj'
)