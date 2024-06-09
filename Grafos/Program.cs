

using System;
class Program
{
    public static void Main(string[] args)

    {
        // Escolha do Algoritmo 
        Console.WriteLine("Digite o Algoritmo desejado:\n" +
     "1. Busca em Profundidade\n" +
     "2. Busca em Largura (D)\n" +
     "3. Algoritmo de Dijkstra (D)\n" +
     "4. Algoritmo de Jarník-Prim (G)\n" +
     "5. Ordenação Topológica (D)\n" +
     "6. Algoritmo de Kruskal (G)\n" +
     "7. Algoritmo de Fleury (G)\n" +
     "8. Algoritmo de König-Egerváry (G)\n" +
     "9. Algoritmo Guloso de Coloração (G)\n" +
     "10. Algoritmo de Welsh-Powell (G)\n" +
     "11. Algoritmo de Brélaz (G)\n" +
     "12. Algoritmo de Kosaraju (D)\n" +
     "13. Algoritmo de Kahn (D)\n" +
     "14. Algoritmo de Bellman-Ford (D)\n" +
     "15. Algoritmo de Ford-Fulkerson (D)");



        int algoritmo = int.Parse(Console.ReadLine());

        // Leitura de arestas e vertices
        Console.WriteLine("Insira a quantidade de vértices e arestas");
        string[] numVA = Console.ReadLine().Split(' ');

        //Definiçaõ dos arrays

        int NumVertices = int.Parse(numVA[0]);
        int NumArestas = int.Parse(numVA[1]);


        // Construção do grafo
        Grafo grafo = new Grafo(NumVertices);


      



        switch (algoritmo) 
        {
            case 1:
                BuscaProfundidade();
                break;

            case 2:
                BuscaLargura();
                break;

            case 3:
                AlgoritmoDijkstra();

                break;

            case 4:
                
                break;

            case 5: break;

            case 6: break;

            case 7: break;

            case 8: break;

            case 9: break;

            case 10: break;

            case 11: break;

            case 12: break;

            case 13: break;

            case 14: break;

            case 15: break;


            default:
                Console.WriteLine("Insira um Algoritmo válido");
                break;


        }


       

        void BuscaProfundidade()

        {

            //Armazenamento dos Dados


            for (int i = 0; i < NumArestas; i++)
            {
                string[] linha = Console.ReadLine().Split(' ');
                int origem = int.Parse(linha[0]);
                int destino = int.Parse(linha[1]);
                grafo.AdicionarAresta(origem, destino);

            }
            Console.WriteLine("Resultado da busca em profundidade (DFS):");
            bool[] visitado = new bool[NumVertices];
            for (int i = 0; i < NumVertices; i++)
            {
                if (!visitado[i])
                {
                    DFS(i, grafo, visitado);
                }
            }
            void DFS(int vertice, Grafo grafo, bool[] visitado)
            {
                visitado[vertice] = true;
                Console.Write(vertice + " ");

                foreach (int adjacente in grafo.GetAdjacentes(vertice))
                {
                    if (!visitado[adjacente])
                    {
                        DFS(adjacente, grafo, visitado);
                    }
                }
            }
        }

        void BuscaLargura()
        {
            // Armazenamento dos Dados
            for (int i = 0; i < NumArestas; i++)
            {
                string[] linha = Console.ReadLine().Split(' ');
                int origem = int.Parse(linha[0]);
                int destino = int.Parse(linha[1]);
                grafo.AdicionarAresta(origem, destino);
            }

            Console.WriteLine("Resultado da busca em largura (BFS):");

            // Array para marcar os vértices visitados
            bool[] visitado = new bool[NumVertices];

            // Fila para armazenar os vértices a serem visitados
            Queue<int> fila = new Queue<int>();

            for (int i = 0; i < NumVertices; i++)
            {
                if (!visitado[i])
                {
                    BFS(i, grafo, visitado, fila);
                }
            }

            void BFS(int vertice, Grafo grafo, bool[] visitado, Queue<int> fila)
            {
                visitado[vertice] = true;
                fila.Enqueue(vertice);

                while (fila.Count > 0)
                {
                    int atual = fila.Dequeue();
                    Console.Write(atual + " ");

                    foreach (int adjacente in grafo.GetAdjacentes(atual))
                    {
                        if (!visitado[adjacente])
                        {
                            visitado[adjacente] = true;
                            fila.Enqueue(adjacente);
                        }
                    }
                }
            }


        }

        void AlgoritmoDijkstra()
        {
            // Solicitar o vértice de origem
            Console.WriteLine("Digite o vértice de origem:");
            int origem = int.Parse(Console.ReadLine());

            // Array para armazenar as distâncias mínimas
            int[] distancias = new int[NumVertices];
            Array.Fill(distancias, int.MaxValue);
            distancias[origem] = 0;

            // Conjunto de vértices visitados
            HashSet<int> visitados = new HashSet<int>();

            // Algoritmo de Dijkstra
            while (visitados.Count < NumVertices)
            {
                int verticeAtual = -1;
                int menorDistancia = int.MaxValue;
                for (int i = 0; i < NumVertices; i++)
                {
                    if (!visitados.Contains(i) && distancias[i] < menorDistancia)
                    {
                        verticeAtual = i;
                        menorDistancia = distancias[i];
                    }
                }

                if (verticeAtual == -1)
                    break;

                visitados.Add(verticeAtual);

                foreach (int adjacente in grafo.GetAdjacentes(verticeAtual))
                {
                    int novaDistancia = distancias[verticeAtual] + 1; // Considerando todas as arestas de peso 1
                    if (novaDistancia < distancias[adjacente])
                    {
                        distancias[adjacente] = novaDistancia;
                    }
                }
            }

            // Exibindo distâncias mínimas
            Console.WriteLine("Distâncias mínimas a partir do vértice " + origem + ":");
            for (int i = 0; i < NumVertices; i++)
            {
                Console.WriteLine("Vértice " + i + ": " + (distancias[i] == int.MaxValue ? "Infinito" : distancias[i].ToString()));
            }
        }

        void AlgoritmoJarník()
        {

        }
    }
}





   

     

