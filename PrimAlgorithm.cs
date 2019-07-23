using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prim_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class PrimAlgorithm
    {
        static void Main(string[] args)
        {
            Graph Nodes = new Graph();

            Node H1 = new Node("H1");
            Node H2 = new Node("H2");
            Node H3 = new Node("H3");
            Node H4 = new Node("H4");
            Node H5 = new Node("H5");
            Node H6 = new Node("H6");
            Node H7 = new Node("H7");
            Node H8 = new Node("H8");
            Node H9 = new Node("H9");
            Node H10 = new Node("H10");

            Nodes.Add(H1);
            Nodes.Add(H2);
            Nodes.Add(H3);
            Nodes.Add(H4);
            Nodes.Add(H5);
            Nodes.Add(H6);
            Nodes.Add(H7);
            Nodes.Add(H8);
            Nodes.Add(H9);
            Nodes.Add(H10);

            H1.AddNeighbour(H3, 45);
            H1.AddNeighbour(H10, 45);
            H1.AddNeighbour(H2, 20);

            H2.AddNeighbour(H1, 20);
            H2.AddNeighbour(H3, 30);
            H2.AddNeighbour(H10, 30);
            H2.AddNeighbour(H5, 25);

            H3.AddNeighbour(H1, 45);
            H3.AddNeighbour(H2, 30);
            H3.AddNeighbour(H4, 45);

            H4.AddNeighbour(H3, 45);
            H4.AddNeighbour(H5, 75);
            H4.AddNeighbour(H6, 40);

            H5.AddNeighbour(H2, 25);
            H5.AddNeighbour(H4, 75);
            H5.AddNeighbour(H6, 75);
            H5.AddNeighbour(H8, 90);

            H6.AddNeighbour(H4, 40);
            H6.AddNeighbour(H5, 75);
            H6.AddNeighbour(H7, 80);
            H6.AddNeighbour(H9, 40);

            H7.AddNeighbour(H6, 80);
            H7.AddNeighbour(H8, 15);

            H8.AddNeighbour(H7, 15);
            H8.AddNeighbour(H9, 45);
            H8.AddNeighbour(H5, 90);
            H8.AddNeighbour(H2, 100);
            H8.AddNeighbour(H10, 50);


            H9.AddNeighbour(H6, 40);
            H9.AddNeighbour(H8, 45);

            H10.AddNeighbour(H8, 50);
            H10.AddNeighbour(H2, 30);
            H10.AddNeighbour(H1, 45);

            MinimumSpanningTreeFinder tree = new MinimumSpanningTreeFinder(Nodes);
            tree.Find();
        }
    }

    class MinimumSpanningTreeFinder
    {
        private List<Node> ConnectedNodes;
        private List<Node> DisconnectedNodes;
        private Graph graph;
        private SortedDictionary<int, List<KeyValuePair<Node,Node>>> Edges;

        public MinimumSpanningTreeFinder(Graph g)
        {
            DisconnectedNodes = g.GetNodes();
            ConnectedNodes = new List<Node>();
            this.graph = g;
            Edges = new SortedDictionary<int, List<KeyValuePair<Node, Node>>>();
        }

        public void Find()
        {
            var StartNode = graph.GetNodes().Select(n=>n).FirstOrDefault();
            ConnectedNodes.Add(StartNode);
            DisconnectedNodes.Remove(StartNode);
            UpdateEdges(StartNode);

            while (DisconnectedNodes.ToList().Count != 0)
            {
                Node LeastExpensiveNode = getLeastExpensiveNode();
                ConnectedNodes.Add(LeastExpensiveNode);
                DisconnectedNodes.Remove(LeastExpensiveNode);
                UpdateEdges(LeastExpensiveNode);
            }

            Print();
        }

        private void UpdateEdges(Node Current)
        {
            foreach (var neighbor in Current.getNeighbors())
            {
                if (Edges.ContainsKey(neighbor.Value))
                    Edges[neighbor.Value].Add(new KeyValuePair<Node, Node>(Current, neighbor.Key));
                else
                    Edges.Add(neighbor.Value, new List<KeyValuePair<Node,Node>>(){new KeyValuePair<Node,Node>(Current, neighbor.Key)});
            }
        }

        private void Print()
        {
            foreach (var n in ConnectedNodes)
                Console.WriteLine(n.getName());
            Console.ReadKey();
        }

        private Node getLeastExpensiveNode()
        {
            //1. iterate through edges and get the least value
            //2. iterate through the values of edges and check for the first KeyValue Pair that falls within the Connected/Disconnected criteria
            //3. return that one

            foreach (var e in Edges)
            {
                foreach (var n in e.Value)
                {
                    if (ConnectedNodes.Contains(n.Key) && DisconnectedNodes.Contains(n.Value))
                    {
                        return n.Value;
                    }
                }
            }
            return null;
        }
    }

    class Node
    {
        private string Name;
        private Dictionary<Node, int> Neighbors;

        public Node(string NodeName)
        {
            this.Name = NodeName;
            Neighbors = new Dictionary<Node, int>();
        }

        public void AddNeighbour(Node n, int cost)
        {
            Neighbors.Add(n, cost);
        }

        public string getName()
        {
            return Name;
        }

        public Dictionary<Node, int> getNeighbors()
        {
            return Neighbors;
        }
    }

    class Graph
    {
        private List<Node> Nodes;

        public Graph()
        {
            Nodes = new List<Node>();
        }

        public void Add(Node n)
        {
            Nodes.Add(n);
        }

        public void Remove(Node n)
        {
            Nodes.Remove(n);
        }

        public List<Node> GetNodes()
        {
            return Nodes.ToList();
        }

        public int getCount()
        {
            return Nodes.Count;
        }
    }
}




