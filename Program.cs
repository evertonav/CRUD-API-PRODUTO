using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddNpgsql<ApplicationDbContext>(builder.Configuration["Database:Postgres"]);
builder.Services.AddScoped<CategoriasServico>();
builder.Services.AddScoped<ProdutosServico>();

var app = builder.Build();

app.MapPost("/Categoria", (CategoriaRequest categoriaRequest, ApplicationDbContext context, CategoriasServico categoriasServico) =>
{
    Categoria categoria = categoriasServico.Adicionar(categoriaRequest);

    return Results.Created($"/Produto/{categoria.Id}", categoria);
});

app.MapGet("/Categoria/{id:int}", ([FromRoute] int Id, ApplicationDbContext context, CategoriasServico categoriasServico) =>
{
    Categoria categoria = categoriasServico.BuscarPeloId(Id);

    if (categoria == null)
        return Results.NotFound();

    return Results.Ok(categoria);
});

app.MapGet("/Produto/{id}", ([FromRoute] int id, ApplicationDbContext context, ProdutosServico produtosServico) => {
    Produto produto = produtosServico.BuscarPeloId(id);

    if (produto == null)
        return Results.NotFound();
    else
        return Results.Ok(produto);
    });

app.MapPost("/Produto", (ProdutoRequest produtoRequest, ApplicationDbContext context, ProdutosServico produtosServico) => {

    Produto produto = produtosServico.Adicionar(produtoRequest);

    return Results.Created($"/Produto/{produto.Codigo}", produto);
});

app.MapPut("/Produto/{id:int}", ([FromRoute] int id, ProdutoRequest produtoRequest, ApplicationDbContext context, ProdutosServico produtosServico) => {
    produtosServico.Atualizar(id, produtoRequest);

    return Results.Ok();
});

app.MapDelete("/Produto/{id:int}", ([FromRoute] int id, ApplicationDbContext context, ProdutosServico produtosServico) => {
    produtosServico.Remover(id);

    Results.Ok();
});

app.Run();
