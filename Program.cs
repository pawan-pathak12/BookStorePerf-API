using BookStorePerfApi.Data;
using BookStorePerfApi.Interfaces.Commands;
using BookStorePerfApi.Interfaces.Queries;
using BookStorePerfApi.Repositories.Commands;
using BookStorePerfApi.Repositories.Queries;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<DapperContext>();
builder.Services.AddAutoMapper(typeof(Program));

#region Register Services

#region Command 

builder.Services.AddScoped<IBookCommandRepository, BookCommandRepository>();
builder.Services.AddScoped<IAuthorCommandRepository, AuthorCommandRepository>();
builder.Services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();
builder.Services.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
builder.Services.AddScoped<IOrderItemCommandRepository, OrderItemCommandRepository>();
#endregion

#region Query 

builder.Services.AddScoped<IBookQueryRepository, BookQueryRepository>();
builder.Services.AddScoped<IAuthorQueryRepository, AuthorQueryRepository>();
builder.Services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
builder.Services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();
builder.Services.AddScoped<IOrderItemQueryRepository, OrderItemQueryRepository>();
#endregion


#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
