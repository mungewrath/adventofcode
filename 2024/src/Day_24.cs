using System.ComponentModel;
using Microsoft.Extensions.Logging;

public class Day_24 : IDay
{
    private ILogger<Day_24> _logger;
    private Dictionary<string, Wire> _wires = [];

    public Day_24(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_24>();
    }

    // Intuition: Build a graph representing the circuit flow.
    // Simulating it runs in O(E^2) time worst case, assuming no loops
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        List<Wire> startingWires = [];

        int i = 0;
        while (!string.IsNullOrWhiteSpace(lines[i]))
        {
            var nameAndVal = lines[i].Split(": ");
            startingWires.Add(GetOrCreateWire(nameAndVal[0], int.Parse(nameAndVal[1])));
            i++;
        }
        i++;

        while (i < lines.Length)
        {
            var rel = lines[i].Split(" ");
            var op = rel[1] switch
            {
                "AND" => Operation.AND,
                "XOR" => Operation.XOR,
                "OR" => Operation.OR,
                _ => throw new InvalidEnumArgumentException(),
            };
            i++;

            ConnectGate(rel[0], rel[2], rel[4], op);
        }

        foreach (Wire w in startingWires)
        {
            w.Update();
        }

        return GetBinaryResultFromWires("z");
    }

    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }

    private class Wire
    {
        public string Name { get; }
        public int? Value { get; set; }

        private Func<int?, int?, int?> gateOperation;
        private Wire input1;
        private Wire input2;
        private List<Wire> downstreamWires;

        public Wire(string name, int? value = null)
        {
            Name = name;
            Value = value;
            downstreamWires = [];
        }

        protected void AddChildWire(Wire w)
        {
            downstreamWires.Add(w);
        }

        public void Connect(Operation operation, Wire in1, Wire in2)
        {
            gateOperation = operation switch
            {
                Operation.AND => (a, b) => a.HasValue && b.HasValue ? (a.Value & b.Value) : (int?)null,
                Operation.OR => (a, b) => a.HasValue && b.HasValue ? (a.Value | b.Value) : (int?)null,
                Operation.XOR => (a, b) => a.HasValue && b.HasValue ? (a.Value ^ b.Value) : (int?)null,
                _ => throw new ArgumentException("Invalid operation")
            };
            input1 = in1;
            in1.AddChildWire(this);
            input2 = in2;
            in2.AddChildWire(this);
        }

        public void Update()
        {
            if (gateOperation != null && input1.Value.HasValue && input2.Value.HasValue)
            {
                Value = gateOperation(input1.Value, input2.Value);
            }
            foreach (Wire child in downstreamWires)
            {
                child.Update();
            }
        }
    }

    private enum Operation
    {
        AND,
        OR,
        XOR
    }


    private Wire GetOrCreateWire(string name, int? initialValue = null)
    {
        if (!_wires.ContainsKey(name))
        {
            _wires[name] = new Wire(name, initialValue);
        }

        if (initialValue.HasValue)
        {
            _wires[name].Value = initialValue;
        }

        return _wires[name];
    }

    private void ConnectGate(string input1Name, string input2Name, string outputName, Operation operation)
    {
        var input1 = GetOrCreateWire(input1Name);
        var input2 = GetOrCreateWire(input2Name);
        var output = GetOrCreateWire(outputName);

        output.Connect(operation, input1, input2);
    }

    private long GetBinaryResultFromWires(string prefix)
    {
        long result = 0;
        var power = 0;
        foreach (var kvp in _wires.Where(w => w.Key.StartsWith(prefix)).OrderBy(w => w.Key))
        {
            if (kvp.Value.Value.HasValue)
            {
                result += ((long)kvp.Value.Value.Value) << power;
            }
            power++;
        }
        return result;
    }
}