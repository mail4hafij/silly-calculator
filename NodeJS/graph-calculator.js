// ------------------------------------------------------
// Author: Mohammad Hafijur Rahman
// Github: https://github.com/mail4hafij/silly-calculator
// ------------------------------------------------------


export class GraphCalculator {
  constructor(expression) {
    if (expression == null) {
      throw new Error("expression is required");
    }

    const pattern = /[+\-*/]/;
    if (!pattern.test(expression)) {
      throw new Error("The expression must contain at least one operator (+, -, *, /).");
    }

    this._expression = expression;
    this._nodes = [];
    this._edges = [];
    this._buildGraph();
  }

  _buildGraph() {
    const tokens = this._expression.trim().split(/\s+/);
    const isEmpty = tokens.length === 0 || (tokens.length === 1 && tokens[0] === "");
    if (isEmpty) throw new Error("Empty expression.");

    for (let i = 0; i < tokens.length; i++) {
      const t = tokens[i];
      const n = Number(t);

      if (!Number.isNaN(n)) {
        // Node
        this._nodes.push({
          value: n,
          leftEdgeIndex: this._edges.length === 0 ? null : this._edges.length - 1,
          rightEdgeIndex: i === tokens.length - 1 ? null : this._edges.length,
        });
      } else if (t === "+" || t === "-") {
        // Edge: low precedence
        this._edges.push({
          operator: t,
          weight: 1,
          pos: this._edges.length,
          leftNodeIndex: this._edges.length,
          rightNodeIndex: this._edges.length + 1,
          visited: false,
        });
      } else if (t === "*" || t === "/") {
        // Edge: high precedence
        this._edges.push({
          operator: t,
          weight: 2,
          pos: this._edges.length,
          leftNodeIndex: this._edges.length,
          rightNodeIndex: this._edges.length + 1,
          visited: false,
        });
      } else {
        throw new Error(`Unexpected token: '${t}'`);
      }
    }
  }

  calculate() {
    let total = 0;
    const hasUnvisited = () => this._edges.some(e => !e.visited);

    const intDiv = (a, b) => {
      if (b === 0) throw new Error("Division by zero.");
      return Math.trunc(a / b); // integer division
    };

    while (hasUnvisited()) {
      const edge = this._edges
        .filter(e => !e.visited)
        .sort((a, b) => b.weight - a.weight || a.pos - b.pos)[0];

      edge.visited = true;
      this._edges[edge.pos] = edge;

      const leftNode = this._nodes[edge.leftNodeIndex];
      const rightNode = this._nodes[edge.rightNodeIndex];

      const result =
        edge.operator === "+"
          ? leftNode.value + rightNode.value
          : edge.operator === "-"
          ? leftNode.value - rightNode.value
          : edge.operator === "*"
          ? leftNode.value * rightNode.value
          : intDiv(leftNode.value, rightNode.value);

      const newNode = {
        value: result,
        leftEdgeIndex: leftNode.leftEdgeIndex,
        rightEdgeIndex: rightNode.rightEdgeIndex,
      };

      this._nodes.push(newNode); 

      if (leftNode.leftEdgeIndex !== null) {
        const leftEdge = this._edges[leftNode.leftEdgeIndex];
        leftEdge.rightNodeIndex = this._nodes.length - 1;
        this._edges[leftEdge.pos] = leftEdge;
      }

      if (rightNode.rightEdgeIndex !== null) {
        const rightEdge = this._edges[rightNode.rightEdgeIndex];
        rightEdge.leftNodeIndex = this._nodes.length - 1;
        this._edges[rightEdge.pos] = rightEdge;
      }

      total = result;
    }

    return total;
  }
}
