using Microsoft.EntityFrameworkCore;
public class ProdutosServico
{
    private readonly ApplicationDbContext Context;
    public ProdutosServico(ApplicationDbContext context)
    {
        Context = context;
    }

   public Produto BuscarPeloId(int id)
   {
        return Context
                 .Produtos
                 .Include(x => x.Tags)
                 .Include(x => x.Categoria)
                 .Where(x => x.Id == id)
                 .FirstOrDefault();
   } 

   public void Atualizar(int id, ProdutoRequest produtoRequest)
   {    
        Produto produto = BuscarPeloId(id);    
        Categoria categoria = new CategoriasServico(Context).BuscarPeloId(produto.CategoriaId);
    
        produto.Codigo = produtoRequest.Codigo;
        produto.Nome = produtoRequest.Nome;
        produto.Categoria = categoria;
        
        if (produtoRequest.Tags != null)
        {
            produto.Tags = new List<Tag>();
            foreach(var item in produtoRequest.Tags)
            {
                produto.Tags.Add(new Tag{ Nome = item });
            }
        }

        Context.SaveChanges();
   }

   public void Remover(int id)
   {
        Context.Produtos.Remove(BuscarPeloId(id));
        Context.SaveChanges();
   }

   public Produto Adicionar(ProdutoRequest produtoRequest)
   {
        Categoria categoria = new CategoriasServico(Context).BuscarPeloId(produtoRequest.CategoriaId);

        Produto produto = new Produto {
            Codigo = produtoRequest.Codigo,
            Nome = produtoRequest.Nome,
            Categoria = categoria
        };

        if (produtoRequest.Tags != null)
        {
            produto.Tags = new List<Tag>();            
            foreach(var item in produtoRequest.Tags)
            {
                produto.Tags.Add(new Tag{ Nome = item });
            }
        }                       
    
        Context.Produtos.Add(produto);
        Context.SaveChanges();

        return produto;
   }
}