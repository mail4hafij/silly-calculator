// ------------------------------------------------------
// Author: Mohammad Hafijur Rahman
// Github: https://github.com/mail4hafij/silly-calculator
// ------------------------------------------------------


import readline from "readline";
import { GraphCalculator } from "./graph-calculator.js";

console.log("Graph Calculator (space-separated). Examples: 2 + 2 * 2");
console.log("Type 'exit' to quit.\n");

const rl = readline.createInterface({ input: process.stdin, output: process.stdout });

const ask = () =>
  rl.question("Input your expression: ", (line) => {
    if (!line) return ask();
    if (["exit", "quit", "q"].includes(line.trim().toLowerCase())) {
      rl.close();
      return;
    }
    try {
      const calc = new GraphCalculator(line);
      console.log(calc.calculate());
    } catch (err) {
      console.log(`error: ${err.message}`);
    }
    ask();
  });

ask();
