// ------------------------------------------------------
// Author: Mohammad Hafijur Rahman
// Github: https://github.com/mail4hafij/silly-calculator
// ------------------------------------------------------

Console.WriteLine("Graph Calculator (space-separated). Examples: 2 + 2 * 2");
Console.WriteLine("Type 'exit' to quit.\n");

while (true)
{
    Console.Write("Input your expression: ");
    var input = Console.ReadLine();

    if (input is null) continue;

    var cmd = input.Trim().ToLowerInvariant();
    if (cmd == "exit" || cmd == "quit" || cmd == "q")
        break;

    if (string.IsNullOrWhiteSpace(input))
        continue;

    try
    {
        var calc = new GraphCalculator(input);
        int result = calc.Calculate();
        Console.WriteLine(result);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"error: {ex.Message}");
    }
}


// ------------------------------------------------------------
// Graph-based calculator using nodes (numbers) and edges (ops)
// ------------------------------------------------------------
public class GraphCalculator
{
    private struct Node
    {
        public int Value;
        public int? LeftEdgeIndex;
        public int? RightEdgeIndex;
    }

    private struct Edge
    {
        public string Operator;
        public int Weight;
        public int LeftNodeIndex;
        public int RightNodeIndex;
        public int Pos;
        public bool Visited;
    }

    private readonly string _expression;
    private List<Node> _nodes = [];
    private List<Edge> _edges = [];

    public GraphCalculator(string Expression)
    {
        _expression = Expression ?? throw new ArgumentNullException(nameof(Expression));
        BuildGraph();
    }

    private void BuildGraph()
    {
        var tokens = _expression.Trim().Split([' '], StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length == 0)
        {
            throw new FormatException("Empty expression.");
        }

        for (int i = 0; i < tokens.Length; i++)
        {
            var t = tokens[i];
            if (int.TryParse(t, out int n))
            {
                // Node
                Node node = new Node 
                { 
                    Value = n, 
                    LeftEdgeIndex = _edges.Count() == 0 ? null : _edges.Count() - 1, 
                    RightEdgeIndex = i == (tokens.Length - 1) ? null : _edges.Count() 
                };
                _nodes.Add(node);
            }
            else if (t is "+" or "-")
            {
                // Edge
                Edge edge = new Edge 
                { 
                    Operator = t, 
                    Weight = 1, 
                    Pos = _edges.Count(), 
                    LeftNodeIndex = _edges.Count(), 
                    RightNodeIndex = _edges.Count + 1, Visited = false 
                };
                _edges.Add(edge);
            }
            else if (t is "*" or "/")
            {
                // Edge
                Edge edge = new Edge 
                { 
                    Operator = t, 
                    Weight = 2, 
                    Pos = _edges.Count(), 
                    LeftNodeIndex = _edges.Count(), 
                    RightNodeIndex = _edges.Count + 1, 
                    Visited = false 
                };
                _edges.Add(edge);
            }
            else
            {
                throw new FormatException($"Unexpected token: '{t}'");
            }
        }
    }

    public int Calculate()
    {
        int total = 0;
        while (_edges.Count(e => !e.Visited) > 0)
        {
            Edge edge = _edges.OrderByDescending(e => e.Weight).ThenBy(e => e.Pos).First(e => !e.Visited);
            edge.Visited = true;
            _edges[edge.Pos] = edge;

            Node leftNode = _nodes[edge.LeftNodeIndex];
            Node rightNode = _nodes[edge.RightNodeIndex];

            int result;
            switch (edge.Operator)
            {
                case "+":
                    result = leftNode.Value + rightNode.Value;
                    break;
                case "-":
                    result = leftNode.Value - rightNode.Value;
                    break;
                case "*":
                    result = leftNode.Value * rightNode.Value;
                    break;
                case "/":
                    result = leftNode.Value / rightNode.Value;
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported operator: {edge.Operator}");
            }

            Node node = new Node 
            { 
                Value = result, 
                LeftEdgeIndex = leftNode.LeftEdgeIndex, 
                RightEdgeIndex = rightNode.RightEdgeIndex 
            };
            _nodes.Add(node);


            if(leftNode.LeftEdgeIndex != null)
            {
                var leftEdge = _edges[leftNode.LeftEdgeIndex ?? 0];
                leftEdge.RightNodeIndex = _nodes.Count - 1;
                _edges[leftEdge.Pos] = leftEdge;
            }
            if (rightNode.RightEdgeIndex != null)
            {
                var rightEdge = _edges[rightNode.RightEdgeIndex ?? 0];
                rightEdge.LeftNodeIndex = _nodes.Count - 1;
                _edges[rightEdge.Pos] = rightEdge;
            }
            total = result;
        }
        return total;
    }
}


