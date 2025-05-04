GCC = gcc

C_SRC = $(shell find ./src -name '*.test.c')
LIB_SRC = ../../Core/bin/shared

BIN = bin

test:
	@cp -r $(LIB_SRC) $(BIN)
	$(GCC) $(C_SRC) -L./$(BIN)/shared -lappcore -lprotobuf-c -o $(BIN)/test
	@export LD_LIBRARY_PATH=./$(BIN)/shared
	./$(BIN)/test