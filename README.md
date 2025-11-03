# silly-calculator

## Problem 
Create a function that calculates and returns the value of an integer expression given as a space separated string. Avoid using third-party libraries for the core functionality. Standard libraries may be used as needed. It should support the following -

Example 1: 
```
calculate("2 + 3")
```
It should support the 4 basic operators: addition (+), subtraction (-), multiplication (*) and division (/)

Example 2: 
```
calculate("3 * -2 + 6") 
```
It should support both positive and negative integers.

## Solution:
Let's turn this problem into a graph problem where numbers are nodes and operators are edges. After we build the graph, we pick edges according to precedence and compute the nodes and **add new nodes** until we reach all the edges and resolve the expression. 
This repository contains **two separate implementations** of the same graph-based calculator:

1. **C# Version** — written using .NET (strongly typed, object-oriented)
2. **Node.js Version** — written in modern JavaScript (lightweight, CLI-based)

Both versions use the **same underlying graph model and logic**, adapted to the idioms of each language.

```
/GraphCalculator
├── /C#
    ├── /Calculator
        └── Calculator.csproj
        └── Program.cs
    └── Calculator.sln  
└── /NodeJS
    └── graph-calculator.js
    └── cli.js
    └── package.json
```

## Conceptual Progression
### Expression: 
2 + 3 * 4
### Nodes (initially):
[2] [3] [4]
### Edges:
- [+] between 2 and 3, weight = 1
- [*] between 3 and 4, weight = 2

### Process:
1. Pick highest precedence edge: *
    - Compute: 3 * 4 = 12 
    - Add new node: [12]
    - Update "+" edge to connect [2] and [12]
2. Next edge: +
    - Compute: 2 + 12 = 14
    - Add new node: [14]
3. Edges exhausted → Final result: 14

## How to Run
### C# Project
1. Open the `.sln` file in **Visual Studio**
2. Build and run the project

### NodeJS Project
1. Open the `NodeJS` folder in **VS Code**
2. Install Node.js if you haven't already: https://nodejs.org/
3. Open a terminal in the folder and run: ```node .```

## Improvement Opportunities
This project implements a basic graph-based integer expression calculator with support for operators (`+`, `-`, `*`, `/`) and negative values via both C# and Node.js.

Here’s what has been done so far:

- Parses **space-separated** expressions (e.g. `3 * -2 + 6`)
- Builds a **graph model** of the expression where numbers are nodes and operations are edges
- Computes expressions strictly by **operator precedence**
- Supports CLI execution in both C# and Node.js
- Handles positive and negative integers
- Validates empty expressions and missing operators

### Possible Enhancements
These features could be added to make the calculator more robust:

- **Parentheses support** (e.g. `(2 + 3) * 4`)
- Support for **non-space-separated input** (e.g. `3*-2+6`)
- Floating-point number support (e.g. `3.5 * 2.1`)
- Improved error handling and messages (e.g. malformed expressions, division by zero)
- Add more operators (e.g. exponentiation `^`, modulo `%`)
- Unit tests for both implementations

These improvements would help expand the capabilities and usability of the calculator while keeping the core graph-based approach intact.

ENJOY!


