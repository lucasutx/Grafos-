using System;
using System.Collections.Generic;

class Program
{
    public static void Main(string[] args)
    {
        // Escolha do Algoritmo 
        Console.WriteLine("Digite o Algoritmo desejado:\n" +
        "1. Busca em Profundidade\n" +
        "2. Busca em Largura \n" +
        "3. Algoritmo de Dijkstra \n" +
        "4. Algoritmo de Jarník-Prim \n" +
        "5. Ordenação Topológica \n" +
        "6. Algoritmo de Kruskal \n" +
        "7. Algoritmo de Fleury \n" +
        "8. Algoritmo de König-Egerváry \n" +
        "9. Algoritmo Guloso de Coloração \n" +
        "10. Algoritmo de Welsh-Powell \n" +
        "11. Algoritmo de Brélaz \n" +
        "12. Algoritmo de Kosaraju \n" +
        "13. Algoritmo de Kahn \n" +
        "14. Algoritmo de Bellman-Ford \n" +
        "15. Algoritmo de Ford-Fulkerson ");

        int algoritmo = int.Parse(Console.ReadLine());

        // Leitura de arestas e vertices
        Console.WriteLine("Insira a quantidade de vértices e arestas");
        string[] numVA = Console.ReadLine().Split(' ');

        // Definição dos arrays
        int NumVertices = int.Parse(numVA[0]);
        int NumArestas = int.Parse(numVA[1]);

        // Criação do grafo
        List<Tuple<int, int>>[] grafo = new List<Tuple<int, int>>[NumVertices];
        for (int i = 0; i < NumVertices; i++)
        {
            grafo[i] = new List<Tuple<int, int>>();
        }

        // Leitura das arestas
        Console.WriteLine("Insira as arestas no formato 'origem destino peso'");
        for (int i = 0; i < NumArestas; i++)
        {
            string[] aresta = Console.ReadLine().Split(' ');
            int origem = int.Parse(aresta[0]);
            int destino = int.Parse(aresta[1]);
            int peso = int.Parse(aresta[2]);
            grafo[origem].Add(new Tuple<int, int>(destino, peso));

            // Se o algoritmo requer grafo não direcionado, adicionamos também a aresta inversa
            if (algoritmo == 4 || algoritmo == 6 || algoritmo == 7 || algoritmo == 9 || algoritmo == 10 || algoritmo == 11)
            {
                grafo[destino].Add(new Tuple<int, int>(origem, peso));
            }
        }

        switch (algoritmo)
        {
            case 1:
                BuscaProfundidade(grafo);
                break;

            case 2:
                BuscaLargura(grafo);
                break;

            case 3:
                AlgoritmoDijkstra(grafo, 0);  // Usar vértice 0 como início
                break;

            case 4:
                AlgoritmoJarnikPrim(grafo);
                break;

            case 5:
                OrdenacaoTopologica(grafo);
                break;
            case 6:
                //AlgoritmoKruskal(grafo, NumVertices);
                break;
            case 7:
                //AlgoritmoFleury(grafo);
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
                break;

            default:
                Console.WriteLine("Insira um Algoritmo válido");
                break;
        }
    }

    static void BuscaProfundidade(List<Tuple<int, int>>[] grafo)
    {
        bool[] visitados = new bool[grafo.Length];
        for (int i = 0; i < grafo.Length; i++)
        {
            if (!visitados[i])
            {
                DFS(grafo, i, visitados);
            }
        }
    }
    static void DFS(List<Tuple<int, int>>[] grafo, int v, bool[] visitados)
    {
        visitados[v] = true;
        Console.WriteLine("Ordem de visita vértice: " + v);

        foreach (var adj in grafo[v])
        {
            if (!visitados[adj.Item1])
            {
                DFS(grafo, adj.Item1, visitados);
            }
        }
    }
    static void BuscaLargura(List<Tuple<int, int>>[] grafo)
    {
        bool[] visitados = new bool[grafo.Length];
        Queue<int> fila = new Queue<int>();

        for (int i = 0; i < grafo.Length; i++)
        {
            if (!visitados[i])
            {
                fila.Enqueue(i);
                visitados[i] = true;

                while (fila.Count > 0)
                {
                    int v = fila.Dequeue();
                    Console.WriteLine("Ordem de visita vértice: " + v);

                    foreach (var adj in grafo[v])
                    {
                        if (!visitados[adj.Item1])
                        {
                            fila.Enqueue(adj.Item1);
                            visitados[adj.Item1] = true;
                        }
                    }
                }
            }
        }
    }
    static void AlgoritmoDijkstra(List<Tuple<int, int>>[] grafo, int origem)
    {
        int n = grafo.Length;
        int[] dist = new int[n];
        bool[] visitados = new bool[n];

        for (int i = 0; i < n; i++)
        {
            dist[i] = int.MaxValue;
            visitados[i] = false;
        }

        dist[origem] = 0;
        PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
        pq.Enqueue(origem, 0);

        while (pq.Count > 0)
        {
            int v = pq.Dequeue();
            if (visitados[v])
                continue;
            visitados[v] = true;

            foreach (var adj in grafo[v])
            {
                int u = adj.Item1;
                int peso = adj.Item2;
                if (dist[v] + peso < dist[u])
                {
                    dist[u] = dist[v] + peso;
                    pq.Enqueue(u, dist[u]);
                }
            }
        }

        Console.WriteLine("Distâncias a partir do vértice " + origem + ":");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Vértice " + i + ": " + (dist[i] == int.MaxValue ? "Infinito" : dist[i].ToString()));
        }
    }
    static void AlgoritmoJarnikPrim(List<Tuple<int, int>>[] grafo)
    {
        int n = grafo.Length;
        int[] pai = new int[n]; // Array para armazenar o pai de cada vértice na MST
        int[] pesoMinimo = new int[n]; // Array para armazenar o peso mínimo da aresta que conecta cada vértice à MST
        bool[] incluido = new bool[n]; // Array para marcar se um vértice já está incluído na MST

        for (int i = 0; i < n; i++)
        {
            pesoMinimo[i] = int.MaxValue;
            incluido[i] = false;
        }

        // Primeiro vértice é escolhido como raiz da MST
        pesoMinimo[0] = 0;
        pai[0] = -1; // Não possui pai na MST

        // Construir a MST com n-1 arestas
        for (int count = 0; count < n - 1; count++)
        {
            // Escolher o vértice com o menor peso mínimo que ainda não está incluído na MST
            int u = -1;
            for (int i = 0; i < n; i++)
            {
                if (!incluido[i] && (u == -1 || pesoMinimo[i] < pesoMinimo[u]))
                {
                    u = i;
                }
            }

            // Incluir o vértice u na MST
            incluido[u] = true;

            // Atualizar os pesos mínimos dos vértices adjacentes de u que ainda não estão incluídos na MST
            foreach (var adj in grafo[u])
            {
                int v = adj.Item1;
                int peso = adj.Item2;
                if (!incluido[v] && peso < pesoMinimo[v])
                {
                    pai[v] = u;
                    pesoMinimo[v] = peso;
                }
            }
        }

        // Imprimir a MST construída
        Console.WriteLine("Arestas da Árvore Geradora Mínima (MST) usando Prim:");

        for (int i = 1; i < n; i++)
        {
            Console.WriteLine("Aresta " + pai[i] + " - " + i + " Peso: " + pesoMinimo[i]);
        }
    }
    static void OrdenacaoTopologica(List<Tuple<int, int>>[] grafo)
    {
        int n = grafo.Length;
        int[] grauEntrada = new int[n];
        Queue<int> fila = new Queue<int>();
        List<int> resultado = new List<int>();

        // Calcular o grau de entrada de cada vértice
        for (int i = 0; i < n; i++)
        {
            foreach (var adj in grafo[i])
            {
                grauEntrada[adj.Item1]++;
            }
        }

        // Adicionar vértices com grau de entrada zero à fila
        for (int i = 0; i < n; i++)
        {
            if (grauEntrada[i] == 0)
            {
                fila.Enqueue(i);
            }
        }

        // Processar vértices na fila
        while (fila.Count > 0)
        {
            int v = fila.Dequeue();
            resultado.Add(v);

            foreach (var adj in grafo[v])
            {
                grauEntrada[adj.Item1]--;
                if (grauEntrada[adj.Item1] == 0)
                {
                    fila.Enqueue(adj.Item1);
                }
            }
        }

        // Verificar se houve um ciclo no grafo
        if (resultado.Count != n)
        {
            Console.WriteLine("O grafo possui um ciclo e, portanto, não é um DAG.");
        }
        else
        {
            Console.WriteLine("Ordenação Topológica:");
            foreach (var v in resultado)
            {
                Console.Write(v + " ");
            }
        }

    }
}

 
    



