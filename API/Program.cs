using Application.Mappings;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração básica
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuração do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RDODigital API",
        Version = "v1",
        Description = "API para o sistema de fiscalização mobile",
        Contact = new OpenApiContact
        {
            Name = "Wesley Moura",
            Email = "wesley.moura@logictel.com.br"
        }
    });
    
    // Configuração JWT no Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }
    };
    
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, Array.Empty<string>() }
    });
});

// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configurações personalizadas usando os métodos de extensão
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddDependencyInjectionConfiguration();

var app = builder.Build();

// Pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RDODigital API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();