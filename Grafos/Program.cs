using System;
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

        //Definiçaõ dos arrays

        int NumVertices = int.Parse(numVA[0]);
        int NumArestas = int.Parse(numVA[1]);

        // Criação do grafo
        List<int>[] grafo = new List<int>[NumVertices];
        for (int i = 0; i < NumVertices; i++)
        {
            grafo[i] = new List<int>();
        }

        // Leitura das arestas
        Console.WriteLine("Insira as arestas no formato 'origem destino'");
        for (int i = 0; i < NumArestas; i++)
        {
            string[] aresta = Console.ReadLine().Split(' ');
            int origem = int.Parse(aresta[0]);
            int destino = int.Parse(aresta[1]);
            grafo[origem].Add(destino);

           /* // Se o algoritmo requer grafo não direcionado, adicionamos também a aresta inversa
            if (algoritmo == 4 || algoritmo == 6 || algoritmo == 7 || algoritmo == 9 || algoritmo == 10 || algoritmo == 11)
            {
                grafo[destino].Add(origem);
            }*/
        }
        switch (algoritmo)
        {
            case 1:
                BuscaProfundidade(grafo);
                break;

            case 2:

                break;

            case 3:


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

        static void BuscaProfundidade(List<int>[] grafo)
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

        static void DFS(List<int>[] grafo, int v, bool[] visitados)
        {
            visitados[v] = true;
            Console.WriteLine("Visitado vértice: " + v);

            foreach (var adj in grafo[v])
            {
                if (!visitados[adj])
                {
                    DFS(grafo, adj, visitados);
                }
            }
        }
    }
}