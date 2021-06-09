# Train

For Code Test Prompt

# To Run

You should be able to simply pull, build, and run the code as-is.

If you want to use a different "railway system" for input, you may put that in a new file, following the same format as the input in the prompt, and pass that filepath as the only command-line argument.

There are extensive unit tests; you can run them to verify they pass, and they should provide good insight on the project structure.

# Design Overview

I split this solution into three projects -- a main project which runs the application, performs high-level parsing and business logic, and runs the calculation algorithms, a data library which contains the data structures, and a unit test project which tests the algorithms against data from the prompts and otherwise.

I hope the overall architecture and algorithm design is self-evident from looking at the code and the comments.

# Assumptions Made

I tried to stick as closely as possible to the specifications laid out in the prompt -- routes are allowed to be cyclical, stop names are all one letter, route distances may be any positive integer, etc.

There is fairly little ambiguity in the prompt; however, it should be easy to modify this project to answer other, similar questions, and obviously the "railway" itself can be configured by an input file.
