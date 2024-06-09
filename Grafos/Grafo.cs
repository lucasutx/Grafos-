class Grafo
{
    private List<List<int>> adjacencia;

    public Grafo(int numVertices)
    {
        adjacencia = new List<List<int>>();
        for (int i = 0; i < numVertices; i++)
        {
            adjacencia.Add(new List<int>());
        }
    }

    public void AdicionarAresta(int origem, int destino)
    {
        adjacencia[origem].Add(destino);
        
        adjacencia[destino].Add(origem);
    }

    public IEnumerable<int> GetAdjacentes(int vertice)
    {
        return adjacencia[vertice];
    }

}