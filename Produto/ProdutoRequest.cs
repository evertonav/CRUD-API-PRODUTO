public record ProdutoRequest(
    string Codigo, 
    string Nome, 
    int CategoriaId, 
    List<string> Tags);
