using Microsoft.EntityFrameworkCore;
using TodoAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ToDoDb>(options => options.UseInMemoryDatabase("TodoList"));
var app = builder.Build();

app.MapGet("/todoItems",async(ToDoDb db)
    =>await db.Todos.ToListAsync());
app.MapGet("/todoItem/{id}",async(int id, ToDoDb db)
    =>await db.Todos.FindAsync(id));
app.MapPost("/todoItem", async(TodoItem item,ToDoDb db) =>
{
    await db.Todos.AddAsync(item);
    await db.SaveChangesAsync();
    return Results.Created($"/todoItems/{item.Id}", item);
});
app.MapPut("/todoItem/{id}",async (int id, TodoItem item, ToDoDb db) => { 
    var itemFound = await db.Todos.FindAsync (id);
    if (itemFound == null)
        return Results.NotFound();
    itemFound.Name = item.Name;
    itemFound.IsComplete = item.IsComplete;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/todoItem/{id}", async (int id, ToDoDb db) =>
{
    var itemFound = await db.Todos.FindAsync(id);
    if (itemFound == null)
        return Results.NotFound();
    db.Todos.Remove(itemFound);
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.Run();
