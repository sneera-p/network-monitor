VENV := venv
PYTHON := ./$(VENV)/bin/python
PIP := ./$(VENV)/bin/pip

PACKAGES = 	cython \
			setuptools \
			zabbix_utils

# Create and bootstrap the venv
activate:
	python3 -m venv $(VENV)

restore: activate
	$(PIP) install --upgrade pip
	$(PIP) install $(PACKAGES)
	@echo "Copying shared libraries..."
	@cp -r ../Core/bin/shared/* bin/

build:
	$(PYTHON) setup.py build_ext -b bin -t obj --cython-c-in-temp
	rm -r appcore.c

run: build
	$(PYTHON) main.py