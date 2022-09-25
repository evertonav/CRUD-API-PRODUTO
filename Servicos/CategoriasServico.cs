public class CategoriasServico {
    private readonly ApplicationDbContext Context;
    public CategoriasServico(ApplicationDbContext context)
    {
        Context = context;
    }

    public Categoria BuscarPeloId(int id)
    {
        return Context.Categorias.Where(x => x.Id == id).FirstOrDefault();
    }

    public Categoria Adicionar(CategoriaRequest categoriaRequest)
    {
        Categoria categoria = new Categoria() { Nome = categoriaRequest.Nome };
        Context.Categorias.Add(categoria);
        Context.SaveChanges();

        return categoria;
    }
}