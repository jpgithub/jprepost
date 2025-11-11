#include <stdio.h>

// Define the context that will change its behavior
typedef struct {
    void (*stateHandler)(); // Function pointer to the current state handler
} Context;

// Define the states
void state1() {
    printf("State 1\n");
}

void state2() {
    printf("State 2\n");
}

// Function to initialize the context with an initial state
void initializeContext(Context* context) {
    context->stateHandler = state1;
}

// Function to change the state of the context
void changeState(Context* context, void (*newState)()) {
    context->stateHandler = newState;
}

// Function to perform some action using the current state
void performAction(Context* context) {
    context->stateHandler();
}

int main() {
    // Create a context and initialize it with state1
    Context myContext;
    initializeContext(&myContext);

    // Perform an action using the initial state
    performAction(&myContext);

    // Change the state to state2
    changeState(&myContext, state2);

    // Perform an action using the updated state
    performAction(&myContext);

    return 0;
}