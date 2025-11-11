#include <stdio.h>

// Define the context that will change its behavior
typedef struct {
    void (*stateHandler)(int); // Function pointer to the current state handler
} Context;

// Define the states
void state1(int arg) {
    printf("Open Dev\n");
}

void state2(int arg) {
    printf("Close Dev\n");
}

// Function to initialize the context with an initial state
void initializeContext(Context* context) {
    context->stateHandler = state1;
}

// Function to change the state of the context
void changeState(Context* context, void (*newState)(arg)) {
    context->stateHandler = newState;
}

// Function to perform some action using the current state
void performAction(Context* context, int handler_argument) {
    context->stateHandler(handler_argument);
}

// A simple addition function
int add(int a, int b) {
    return a + b;
}

// A simple subtraction function
int subtract(int a, int b) {
    return a - b;
}

void calc(int a, int b, int (*op)(int, int)) {
    printf("%d\n", op(a, b));
}

int main() {
    // Create a context and initialize it with state1
    Context myContext;
    initializeContext(&myContext);

    // Perform an action using the initial state
    performAction(&myContext, 0);

    // Change the state to state2
    changeState(&myContext, state2);

    // Perform an action using the updated state
    performAction(&myContext, 0);
	
	// Passing different 
    // functions to 'calc'
    calc(10, 5, add);
  	calc(10, 5, subtract);

    return 0;
}