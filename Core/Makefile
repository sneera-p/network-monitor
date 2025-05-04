# Compilers
GCC = gcc
EMCC = emcc
PROTOC = protoc

## ----------------------------- Settings -----------------------------

# Input settings
PROTO_SRC = ../Protos
PROTO_GOOGLE = /usr/include

# Output Settings
OUTDIR = bin
OUTNAME = libappcore

# Output Targets
PROTO_TARGET = obj/proto
WASM_TARGET = $(OUTDIR)/web
SO_TARGET = $(OUTDIR)/shared


## --------------------------- Source Files ---------------------------

# protos
PROTO_FILES_SRC = $(shell find $(PROTO_SRC) -name '*.proto')

PROTO_FILES_DEF = $(PROTO_GOOGLE)/google/protobuf/timestamp.proto

PROTO_FILES_ALL = $(PROTO_FILES_SRC) $(PROTO_FILES_DEF)

# C files 
C_SRC = $(shell find ./src -name '*.c') \
		$(shell find ./obj -name '*.c')

C_INC = -I./include \
		-I$(PROTO_TARGET) \

PROTOBUF_C = ./obj/protobuf-c


EXPORTED_FUNCS ='_device_status_as_string', \
				'_device_status_parse', \
				'_incident_severity_as_string', \
				'_incident_severity_parse'


## -------------------------- Compiler Flags --------------------------

PROTO_FLAGS =   -I$(PROTO_GOOGLE) \
                -I../Protos

WASM_FLAGS = 	-s EXPORTED_FUNCTIONS="[$(EXPORTED_FUNCS)]" \
             	-s EXPORTED_RUNTIME_METHODS="['cwrap', 'UTF8ToString']" \
             	-s MODULARIZE=1 \
             	-s EXPORT_NAME='$(OUTNAME)Module' \
             	-O3 \
				$(C_INC) \
				-I$(PROTOBUF_C)

SO_FLAGS =  	-shared \
            	-fPIC \
            	-O3 \
				$(C_INC) \
				$(shell pkg-config --libs --cflags libprotobuf-c)


## ------------------------------ Builds ------------------------------

.PHONY: all clean proto wasm so

# --- Default Target ---
all: proto wasm so

proto: $(PROTO_FILES_ALL)
	@mkdir -p $(PROTO_TARGET)
	$(PROTOC) $(PROTO_FLAGS) --c_out=$(PROTO_TARGET) $(PROTO_FILES_ALL)

so: proto $(C_SRC)
	@mkdir -p $(SO_TARGET)
	$(GCC) $(SO_FLAGS) -o $(SO_TARGET)/$(OUTNAME).so $(C_SRC)

wasm: proto $(C_SRC)
	@mkdir -p $(WASM_TARGET)
	@cp -r /usr/include/protobuf-c $(PROTOBUF_C)
	$(EMCC) $(WASM_FLAGS) -o $(WASM_TARGET)/$(OUTNAME).js $(C_SRC)

clean:
	@rm -rf $(OUTDIR) $(PROTO_TARGET) $(PROTOBUF_C)