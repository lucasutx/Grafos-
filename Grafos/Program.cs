using System;
using System.Collections.Generic;
using System.Linq;

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
        "15. Algoritmo de Ford-Fulkerson \n "+
         "0. Para sair");

        int algoritmo = int.Parse(Console.ReadLine());



        while(algoritmo != 0)
        {
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
                grafo[i] = new List<Tuple<int, int>>(100);
            }

            // Lista para armazenar as arestas para Kruskal
            List<Tuple<int, int, int>> arestas = new List<Tuple<int, int, int>>();

            // Lista para armazenar as arestas de Welsh-Powell
            List<Tuple<int, int>> arestasW = new List<Tuple<int, int>>();

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
                    arestas.Add(new Tuple<int, int, int>(origem, destino, peso));
                    arestasW.Add(new Tuple<int, int>(origem, destino));
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
                    AlgoritmoKruskal(arestas, NumVertices);
                    break;

                case 7:
                    AlgoritmoFleury(grafo);
                    break;

                case 8:
                    AlgoritmoKonigEgervary(grafo, NumVertices);
                    break;

                case 9:
                    AlgoritmoGuloso(grafo, NumVertices);
                    break;

                case 10:
                    AlgoritmoWelshPowell(grafo, NumVertices);
                    break;

                case 11:
                    AlgoritmoBrelaz(grafo, NumVertices);
                    break;

                case 12:
                    AlgoritmoKosaraju(grafo, NumVertices);
                    break;

                case 13:
                    AlgoritmoKahn(grafo, NumVertices);
                    break;

                case 14:
                    AlgoritmoBellmanFord(arestas, NumVertices, 0);
                    break;

                case 15:
                    AlgoritmoFordFulkerson(grafo, 0, NumVertices - 1);
                    break;

                default:
                    Console.WriteLine("Insira um Algoritmo válido");
                    break;
            }

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
            "15. Algoritmo de Ford-Fulkerson \n " +
             "0. Para sair");

            algoritmo = int.Parse(Console.ReadLine());
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
    
    static void AlgoritmoKruskal(List<Tuple<int, int, int>> arestas, int NumVertices)
    {
        // Ordenar arestas pelo peso
        var arestasOrdenadas = arestas.OrderBy(aresta => aresta.Item3).ToList();

        // Inicializar o Union-Find
        int[] pai = new int[NumVertices];
        int[] rank = new int[NumVertices];
        for (int i = 0; i < NumVertices; i++)
        {
            pai[i] = i;
            rank[i] = 0;
        }

        // Função para encontrar o pai do elemento
        int Find(int i)
        {
            if (pai[i] != i)
                pai[i] = Find(pai[i]);
            return pai[i];
        }

        // Função para unir dois conjuntos
        void Union(int x, int y)
        {
            int raizX = Find(x);
            int raizY = Find(y);

            if (raizX != raizY)
            {
                if (rank[raizX] > rank[raizY])
                    pai[raizY] = raizX;
                else if (rank[raizX] < rank[raizY])
                    pai[raizX] = raizY;
                else
                {
                    pai[raizY] = raizX;
                    rank[raizX]++;
                }
            }
        }

        // Lista para armazenar a Árvore Geradora Mínima
        List<Tuple<int, int, int>> agm = new List<Tuple<int, int, int>>();

        foreach (var aresta in arestasOrdenadas)
        {
            int origem = aresta.Item1;
            int destino = aresta.Item2;
            int peso = aresta.Item3;

            // Se origem e destino não estão no mesmo conjunto, adiciona a aresta na AGM
            if (Find(origem) != Find(destino))
            {
                agm.Add(aresta);
                Union(origem, destino);
            }
        }

        // Exibir a Árvore Geradora Mínima
        Console.WriteLine("Arestas da Árvore Geradora Mínima (origem, destino, peso):");
        foreach (var aresta in agm)
        {
            Console.WriteLine($"{aresta.Item1} - {aresta.Item2} : {aresta.Item3}");
        }
    }
    
    static void AlgoritmoFleury(List<Tuple<int, int>>[] grafo)
    {
        // Verificar se o grafo possui um caminho ou ciclo Euleriano
        int oddDegreeVertices = 0;
        int startVertex = 0;

        for (int i = 0; i < grafo.Length; i++)
        {
            if (grafo[i].Count % 2 != 0)
            {
                oddDegreeVertices++;
                startVertex = i;
            }
        }

        // Se o número de vértices de grau ímpar for diferente de 0 ou 2, o grafo não possui caminho ou ciclo Euleriano
        if (oddDegreeVertices != 0 && oddDegreeVertices != 2)
        {
            Console.WriteLine("O grafo não possui caminho ou ciclo Euleriano.");
            return;
        }

        // Função auxiliar para verificar se a aresta u-v é uma ponte
        bool IsBridge(int u, int v)
        {
            // Se u-v é a única aresta de u, não é uma ponte
            if (grafo[u].Count == 1)
                return false;

            // Contar vértices alcançáveis a partir de u
            bool[] visited = new bool[grafo.Length];
            int count1 = DFSCount(u, visited);

            // Remover a aresta u-v e contar vértices alcançáveis novamente
            RemoveEdge(u, v);
            visited = new bool[grafo.Length];
            int count2 = DFSCount(u, visited);

            // Adicionar a aresta u-v de volta
            AddEdge(u, v);

            // Se count1 for maior que count2, u-v é uma ponte
            return count1 > count2;
        }

        // Função auxiliar para contar o número de vértices alcançáveis a partir de v
        int DFSCount(int v, bool[] visited)
        {
            visited[v] = true;
            int count = 1;

            foreach (var adj in grafo[v])
            {
                if (!visited[adj.Item1])
                    count += DFSCount(adj.Item1, visited);
            }

            return count;
        }

        // Função auxiliar para remover a aresta u-v
        void RemoveEdge(int u, int v)
        {
            grafo[u].RemoveAll(adj => adj.Item1 == v);
            grafo[v].RemoveAll(adj => adj.Item1 == u);
        }

        // Função auxiliar para adicionar a aresta u-v
        void AddEdge(int u, int v)
        {
            grafo[u].Add(new Tuple<int, int>(v, 0));
            grafo[v].Add(new Tuple<int, int>(u, 0));
        }

        // Função para imprimir o circuito ou caminho Euleriano usando o Algoritmo de Fleury
        void PrintEulerianPath(int u)
        {
            foreach (var adj in grafo[u].ToList())
            {
                int v = adj.Item1;

                // Se a aresta u-v não é uma ponte ou é a última aresta, percorra-a
                if (!IsBridge(u, v))
                {
                    Console.WriteLine($"{u} - {v}");
                    RemoveEdge(u, v);
                    PrintEulerianPath(v);
                }
            }
        }

        // Começar o Algoritmo de Fleury a partir do vértice inicial
        PrintEulerianPath(startVertex);
    }
    
    static void AlgoritmoKonigEgervary(List<Tuple<int, int>>[] grafo, int NumVertices)
    {
        // Converter o grafo em uma lista de adjacências simplificada para emparelhamento bipartido
        List<int>[] bipartido = new List<int>[NumVertices];
        for (int i = 0; i < NumVertices; i++)
        {
            bipartido[i] = new List<int>();
            foreach (var adj in grafo[i])
            {
                bipartido[i].Add(adj.Item1);
            }
        }

        int[] emparelhamentoU = new int[NumVertices];
        int[] emparelhamentoV = new int[NumVertices];
        for (int i = 0; i < NumVertices; i++)
        {
            emparelhamentoU[i] = -1;
            emparelhamentoV[i] = -1;
        }

        // Função de busca para encontrar um caminho aumentante
        bool BuscaCaminhoAumentante(int u, bool[] visitadosU)
        {
            foreach (var v in bipartido[u])
            {
                if (!visitadosU[v])
                {
                    visitadosU[v] = true;

                    if (emparelhamentoV[v] == -1 || BuscaCaminhoAumentante(emparelhamentoV[v], visitadosU))
                    {
                        emparelhamentoU[u] = v;
                        emparelhamentoV[v] = u;
                        return true;
                    }
                }
            }
            return false;
        }

        // Encontra o emparelhamento máximo
        int emparelhamentoMaximo = 0;
        for (int u = 0; u < NumVertices; u++)
        {
            bool[] visitadosU = new bool[NumVertices];
            if (BuscaCaminhoAumentante(u, visitadosU))
            {
                emparelhamentoMaximo++;
            }
        }

        // Exibe o emparelhamento máximo
        Console.WriteLine("Emparelhamento máximo:");
        for (int u = 0; u < NumVertices; u++)
        {
            if (emparelhamentoU[u] != -1)
            {
                Console.WriteLine($"{u} - {emparelhamentoU[u]}");
            }
        }
        Console.WriteLine("Número máximo de emparelhamentos: " + emparelhamentoMaximo);
    }
    
    static void AlgoritmoGuloso(List<Tuple<int, int>>[] grafo, int NumVertices)
    {
        // Vetor para armazenar as cores dos vértices
        int[] cores = new int[NumVertices];
        for (int i = 0; i < NumVertices; i++)
        {
            cores[i] = -1; // Inicializa todos os vértices com cor não atribuída
        }

        // Função para verificar se é seguro atribuir uma cor a um vértice
        bool PodeAtribuirCor(int vertice, int cor)
        {
            foreach (var adjacente in grafo[vertice])
            {
                if (cores[adjacente.Item1] == cor)
                    return false; // Já existe um vértice adjacente com a mesma cor
            }
            return true;
        }

        // Atribuição de cores
        for (int v = 0; v < NumVertices; v++)
        {
            // Verifica cores disponíveis
            bool[] coresDisponiveis = new bool[NumVertices];
            for (int i = 0; i < NumVertices; i++)
                coresDisponiveis[i] = true;

            // Considera cores dos vértices adjacentes
            foreach (var adjacente in grafo[v])
                if (cores[adjacente.Item1] != -1)
                    coresDisponiveis[cores[adjacente.Item1]] = false;

            // Encontra a primeira cor disponível
            int cor;
            for (cor = 0; cor < NumVertices; cor++)
                if (coresDisponiveis[cor])
                    break;

            cores[v] = cor; // Atribui a cor encontrada ao vértice
        }

        // Imprime a coloração dos vértices
        Console.WriteLine("Coloração dos vértices:");
        for (int i = 0; i < NumVertices; i++)
        {
            Console.WriteLine($"Vértice {i}: Cor {cores[i]}");
        }
    }
    
    static void AlgoritmoWelshPowell(List<Tuple<int, int>>[] grafo, int NumVertices)
    {
        // Lista para armazenar o grau de cada vértice
        int[] grau = new int[NumVertices];
        for (int i = 0; i < NumVertices; i++)
        {
            grau[i] = grafo[i].Count;
        }

        // Ordenar os vértices por grau decrescente
        int[] verticesOrdenados = Enumerable.Range(0, NumVertices).OrderByDescending(v => grau[v]).ToArray();

        // Array para armazenar as cores dos vértices
        int[] cores = new int[NumVertices];
        for (int i = 0; i < NumVertices; i++)
        {
            cores[i] = -1; // -1 significa que o vértice ainda não foi colorido
        }

        // Colorir os vértices
        int cor = 0;
        for (int i = 0; i < NumVertices; i++)
        {
            int v = verticesOrdenados[i];

            if (cores[v] == -1) // Se o vértice ainda não foi colorido
            {
                // Atribuir uma nova cor ao vértice
                cores[v] = cor;

                // Colorir todos os vértices adjacentes que ainda não foram coloridos e que não são adjacentes a vértices da mesma cor
                for (int j = 0; j < NumVertices; j++)
                {
                    int u = verticesOrdenados[j];

                    if (cores[u] == -1 && !grafo[v].Any(adj => cores[adj.Item1] == cor))
                    {
                        cores[u] = cor;
                    }
                }

                // Incrementar a cor para a próxima iteração
                cor++;
            }
        }

        // Exibir as cores atribuídas a cada vértice
        for (int i = 0; i < NumVertices; i++)
        {
            Console.WriteLine($"Vértice {i} -> Cor {cores[i]}");
        }
    }
    
    public static void AlgoritmoBrelaz(List<Tuple<int, int>>[] grafo, int NumVertices)
    {
        // Array para armazenar as cores dos vértices
        int[] cores = new int[NumVertices];
        for (int i = 0; i < NumVertices; i++)
        {
            cores[i] = -1; // -1 significa que o vértice ainda não foi colorido
        }

        // Array para armazenar a saturação de cada vértice
        int[] saturacao = new int[NumVertices];
        for (int i = 0; i < NumVertices; i++)
        {
            saturacao[i] = 0; // Iniciar com saturação 0
        }

        // Função auxiliar para obter a cor disponível mais baixa para um vértice
        int ObterCorDisponivel(int vertice)
        {
            bool[] coresUsadas = new bool[NumVertices];
            foreach (var adj in grafo[vertice])
            {
                if (cores[adj.Item1] != -1)
                {
                    coresUsadas[cores[adj.Item1]] = true;
                }
            }
            for (int i = 0; i < NumVertices; i++)
            {
                if (!coresUsadas[i])
                {
                    return i;
                }
            }
            return NumVertices; // Isso não deve acontecer
        }

        // Selecionar o primeiro vértice (o de maior grau)
        int[] grau = new int[NumVertices];
        for (int i = 0; i < NumVertices; i++)
        {
            grau[i] = grafo[i].Count;
        }

        int verticeInicial = Array.IndexOf(grau, grau.Max());
        cores[verticeInicial] = 0;

        // Atualizar a saturação dos vértices adjacentes
        foreach (var adj in grafo[verticeInicial])
        {
            saturacao[adj.Item1]++;
        }

        // Colorir os vértices restantes
        for (int i = 1; i < NumVertices; i++)
        {
            // Selecionar o vértice com maior saturação (em caso de empate, escolher o de maior grau)
            int verticeSelecionado = -1;
            int maiorSaturacao = -1;
            for (int j = 0; j < NumVertices; j++)
            {
                if (cores[j] == -1)
                {
                    if (saturacao[j] > maiorSaturacao)
                    {
                        maiorSaturacao = saturacao[j];
                        verticeSelecionado = j;
                    }
                    else if (saturacao[j] == maiorSaturacao && grau[j] > grau[verticeSelecionado])
                    {
                        verticeSelecionado = j;
                    }
                }
            }

            // Atribuir a cor disponível mais baixa ao vértice selecionado
            int corDisponivel = ObterCorDisponivel(verticeSelecionado);
            cores[verticeSelecionado] = corDisponivel;

            // Atualizar a saturação dos vértices adjacentes
            foreach (var adj in grafo[verticeSelecionado])
            {
                if (cores[adj.Item1] == -1)
                {
                    saturacao[adj.Item1]++;
                }
            }
        }

        // Exibir as cores atribuídas a cada vértice
        for (int i = 0; i < NumVertices; i++)
        {
            Console.WriteLine($"Vértice {i} -> Cor {cores[i]}");
        }
    }

    public static void AlgoritmoKosaraju(List<Tuple<int, int>>[] grafo, int NumVertices)
    {
        Stack<int> pilha = new Stack<int>();
        bool[] visitado = new bool[NumVertices];

        // Passo 1: Preencher a pilha com a ordem dos vértices finalizados
        for (int i = 0; i < NumVertices; i++)
        {
            if (!visitado[i])
            {
                PreencherOrdem(grafo, i, visitado, pilha);
            }
        }

        // Passo 2: Transpor o grafo
        List<int>[] grafoTransposto = TransporGrafo(grafo, NumVertices);

        // Passo 3: Processar os vértices na ordem dada pela pilha
        Array.Fill(visitado, false);
        while (pilha.Count > 0)
        {
            int v = pilha.Pop();
            if (!visitado[v])
            {
                List<int> componente = new List<int>();
                DFS(grafoTransposto, v, visitado, componente);
                Console.WriteLine("Componente fortemente conectada: " + string.Join(", ", componente));
            }
        }
    }
    private static void PreencherOrdem(List<Tuple<int, int>>[] grafo, int v, bool[] visitado, Stack<int> pilha)
    {
        visitado[v] = true;
        foreach (var adj in grafo[v])
        {
            if (!visitado[adj.Item1])
            {
                PreencherOrdem(grafo, adj.Item1, visitado, pilha);
            }
        }
        pilha.Push(v);
    }
    private static List<int>[] TransporGrafo(List<Tuple<int, int>>[] grafo, int NumVertices)
    {
        List<int>[] grafoTransposto = new List<int>[NumVertices];
        for (int i = 0; i < NumVertices; i++)
        {
            grafoTransposto[i] = new List<int>();
        }
        for (int v = 0; v < NumVertices; v++)
        {
            foreach (var adj in grafo[v])
            {
                grafoTransposto[adj.Item1].Add(v);
            }
        }
        return grafoTransposto;
    }
    private static void DFS(List<int>[] grafo, int v, bool[] visitado, List<int> componente)
    {
        visitado[v] = true;
        componente.Add(v);
        foreach (var adj in grafo[v])
        {
            if (!visitado[adj])
            {
                DFS(grafo, adj, visitado, componente);
            }
        }
    }

    public static void AlgoritmoKahn(List<Tuple<int, int>>[] grafo, int NumVertices)
    {
        int[] grauEntrada = new int[NumVertices];
        for (int i = 0; i < NumVertices; i++)
        {
            foreach (var adj in grafo[i])
            {
                grauEntrada[adj.Item1]++;
            }
        }

        Queue<int> fila = new Queue<int>();
        for (int i = 0; i < NumVertices; i++)
        {
            if (grauEntrada[i] == 0)
            {
                fila.Enqueue(i);
            }
        }

        List<int> ordenacao = new List<int>();
        while (fila.Count > 0)
        {
            int v = fila.Dequeue();
            ordenacao.Add(v);

            foreach (var adj in grafo[v])
            {
                grauEntrada[adj.Item1]--;
                if (grauEntrada[adj.Item1] == 0)
                {
                    fila.Enqueue(adj.Item1);
                }
            }
        }

        if (ordenacao.Count == NumVertices)
        {
            Console.WriteLine("Ordenação Topológica: " + string.Join(", ", ordenacao));
        }
        else
        {
            Console.WriteLine("O grafo contém um ciclo, portanto, a ordenação topológica não é possível.");
        }
    }

    public static void AlgoritmoBellmanFord(List<Tuple<int, int, int>> arestas, int NumVertices, int origem)
    {
        int[] distancia = new int[NumVertices];
        for (int i = 0; i < NumVertices; i++)
        {
            distancia[i] = int.MaxValue;
        }
        distancia[origem] = 0;

        for (int i = 0; i < NumVertices - 1; i++)
        {
            foreach (var aresta in arestas)
            {
                int u = aresta.Item1;
                int v = aresta.Item2;
                int peso = aresta.Item3;

                if (distancia[u] != int.MaxValue && distancia[u] + peso < distancia[v])
                {
                    distancia[v] = distancia[u] + peso;
                }
            }
        }

        // Verificação de ciclos negativos
        foreach (var aresta in arestas)
        {
            int u = aresta.Item1;
            int v = aresta.Item2;
            int peso = aresta.Item3;

            if (distancia[u] != int.MaxValue && distancia[u] + peso < distancia[v])
            {
                Console.WriteLine("O grafo contém um ciclo negativo.");
                return;
            }
        }

        Console.WriteLine("Distâncias mínimas a partir do vértice " + origem + ":");
        for (int i = 0; i < NumVertices; i++)
        {
            Console.WriteLine("Vértice " + i + ": " + (distancia[i] == int.MaxValue ? "Infinito" : distancia[i].ToString()));
        }
    }


    static void AlgoritmoFordFulkerson(List<Tuple<int, int>>[] grafo, int fonte, int sumidouro)
    {
        int numVertices = grafo.Length;

        // Inicializar matriz de capacidades residuais
        int[,] capacidadeResidual = new int[numVertices, numVertices];
        foreach (var adjacencias in grafo)
        {
            foreach (var adjacencia in adjacencias)
            {
                capacidadeResidual[adjacencia.Item1, adjacencias.IndexOf(adjacencia)] = adjacencia.Item2;
            }
        }

        // Inicializar vetor para armazenar o caminho de aumento
        int[] caminhoAumento = new int[numVertices];

        int fluxoMaximo = 0;  // Inicializar o fluxo máximo como 0

        // Enquanto houver um caminho de aumento, aumente o fluxo
        while (BFS(grafo, capacidadeResidual, fonte, sumidouro, caminhoAumento))
        {
            // Encontrar a capacidade residual mínima ao longo do caminho de aumento encontrado pela BFS
            int fluxoCaminho = int.MaxValue;
            for (int v = sumidouro; v != fonte; v = caminhoAumento[v])
            {
                int u = caminhoAumento[v];
                fluxoCaminho = Math.Min(fluxoCaminho, capacidadeResidual[u, v]);
            }

            // Atualizar as capacidades residuais ao longo do caminho e do caminho reverso
            for (int v = sumidouro; v != fonte; v = caminhoAumento[v])
            {
                int u = caminhoAumento[v];
                capacidadeResidual[u, v] -= fluxoCaminho;
                capacidadeResidual[v, u] += fluxoCaminho;
            }

            // Adicionar o fluxo do caminho ao fluxo máximo
            fluxoMaximo += fluxoCaminho;
        }

        // Imprimir o fluxo máximo encontrado
        Console.WriteLine($"Fluxo Máximo: {fluxoMaximo}");
    }

    // Função auxiliar para encontrar um caminho de aumento usando BFS
    static bool BFS(List<Tuple<int, int>>[] grafo, int[,] capacidadeResidual, int fonte, int sumidouro, int[] caminhoAumento)
    {
        int numVertices = grafo.Length;
        bool[] visitado = new bool[numVertices];
        Queue<int> fila = new Queue<int>();

        fila.Enqueue(fonte);
        visitado[fonte] = true;
        caminhoAumento[fonte] = -1;

        while (fila.Count > 0)
        {
            int u = fila.Dequeue();

            foreach (var adj in grafo[u])
            {
                int v = adj.Item1;
                int capacidade = capacidadeResidual[u, v];

                if (!visitado[v] && capacidade > 0)
                {
                    if (v == sumidouro)
                    {
                        caminhoAumento[v] = u;
                        return true;
                    }

                    fila.Enqueue(v);
                    caminhoAumento[v] = u;
                    visitado[v] = true;
                }
            }
        }

        return false;
    }

}









