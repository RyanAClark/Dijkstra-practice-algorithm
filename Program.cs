using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra
{
    public class Vertex
    {
        public int distance;
        public string index;
        
        public Vertex(string index)
        {
            this.index = index;
        }
    }

    public class Edges
    {
        public Vertex node1;
        public Vertex node2;
        public int cost;

        public Edges(Vertex node1, Vertex node2, int cost)
        {
            this.node1 = node1;
            this.node2 = node2;
            this.cost = cost;
        }
    }

    public class Dijkstra
    {
        public List<Vertex> dist;
        public List<Vertex> queue = new List<Vertex>();  //Queue of vertexes 

        public Dijkstra(graph g,string start)
        {
            int amount=g.vertex.Count;
            int amount2 = g.edges.Count;
            Initial(start, amount, g);

            while (queue.Count > 0)
            {
                Vertex u = FindVertex();

                for (int v = 0; v < amount2; v++)
                {
                    if (g.edges[v].cost > 0&& g.edges[v].node1==u) //Checking for edges where u is the first node in the edge
                    {
                        int alt = g.edges[v].node1.distance + g.edges[v].cost;

                        if(alt<g.edges[v].node2.distance){
                            g.edges[v].node2.distance = alt;
                        }
                    }
                }
            }
        }

        private void Initial(string start, int amount,graph g)
        {          
            dist = new List<Vertex>(g.vertex);
            for (int i = 0; i < amount ; i++)
            {
                if (dist[i].index == start)
                {
                    dist[i].distance = 0;
                }
                else
                {
                    dist[i].distance = int.MaxValue;
                }
                queue.Add(dist[i]);
            }
        }

        private Vertex FindVertex()  //Find Next Vertex
        {
            int min = int.MaxValue;
            Vertex adj = null;

            foreach (Vertex v in queue){
                if(v.distance<=min)
                {
                    min = v.distance;
                    adj = v;
                }
            }
            queue.Remove(adj);
            return adj;
        }
    }

    public class graph
    {
        public List<Vertex> vertex;
        public List<Edges> edges;

        public graph()
        {
            vertex = new List<Vertex>();
            edges = new List<Edges>();
        }

        public void Add(string x){             //Adds nodes to graph
            Vertex node = new Vertex(x);
            vertex.Add(node);
        }

        public void AddEdge(Vertex node1, Vertex node2,int cost){  //Adds edges to nodes
            Edges connect = new Edges(node1, node2, cost);
            Edges connect2 = new Edges(node2, node1, cost);

            edges.Add(connect);
            edges.Add(connect2);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            graph x = new graph();
            Console.WriteLine("Enter Starting Node (node0-4): ");
            string start = Console.ReadLine();
            x.Add("node0");
            x.Add("node1");
            x.Add("node2");
            x.Add("node3");
            x.Add("node4");
            x.AddEdge(x.vertex[0],x.vertex[1],5);
            x.AddEdge(x.vertex[0], x.vertex[2], 8);
            x.AddEdge(x.vertex[1], x.vertex[3], 7);
            x.AddEdge(x.vertex[2], x.vertex[4], 3);
            x.AddEdge(x.vertex[3], x.vertex[4], 5);
            Dijkstra test = new Dijkstra(x,start);
            foreach (Vertex e in test.dist)
            {
                Console.Write("Distance to "+e.index+": ");
                Console.WriteLine(e.distance);
            }
            Console.WriteLine();
        }
    }
}
