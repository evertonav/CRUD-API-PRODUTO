public static class ProdutoRepositorio {
    public static List<Produto>  Produtos {get; set;} = new List<Produto>();

    public static void Init(IConfiguration configuration)
    {
        List<Produto> listaProdutos = configuration.GetSection("Produtos").Get<List<Produto>>();
        Produtos = listaProdutos;
    }

    public static void  Adicionar(Produto produto) {
        Produtos.Add(produto);
    }

    public static Produto GetProduto(string codigo) {
        return Produtos.FirstOrDefault(p => p.Codigo == codigo);
    }

    public static void Remover(Produto produto)
    {
        Produtos.Remove(produto);
    }
}
