using API_Peliculas.Data;
using API_Peliculas.PeliculasMapper;
using API_Peliculas.Repositorio;
using API_Peliculas.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//4º - CONFIGURAMOS LA CONEXION A SQL SERVER Y AGREGAMOS REPOSITORIOS Y AUTOMAPPER -> siguiente paso es un txt
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));
});

builder.Services.AddResponseCaching(); //47º - AÑADIMOS LA CACHE PARA FACILITAR EL ACCESO A DATOS QUE SON SIEMPRE DEL MISMO VALOR

//AGREGAMOS LOS REPOSITORIOS
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<IPeliculaRepositorio, PeliculaRepositorio>(); //24º
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>(); //38º

var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta");//44º - 4/5

//AGREGAMOS EL AUTOMAPPER
builder.Services.AddAutoMapper(typeof(PeliculasMapper));

//44º - 5/5
//CONFIGURACION AUTENTICACION
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Add services to the container.

builder.Services.AddControllers(opcion => //49º - 1/3 - CREACION DE PERFIL DE CACHE GLOBAL
{
    opcion.CacheProfiles.Add("PorDefecto20Segundos", new CacheProfile() { Duration = 30 });
}); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => //46º - COMO TOMA LA API LA AUTENTICACION (creacion del boton de Authorize arriba a la derecha)
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = 
        "Autenticacion JWT usando  el esquema Bearer. \r\n\r\n " +
        "Ingresa la palabra 'Bearer' seguida de un [espacio] y despues su Token en el campo de abajo. \r\n\r\n " +
        "Ejemplo: \"Bearer sadfasdasdfasdfasdfasdfas\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string> ()
        }
    });
});

//44º - 1/5
//SOPORTE PARA CORS
//Se pueden habilitar: 1-Un dominnio, 2-multiples dominios, 3- cualquier dominio (cuidado la seguridad)
//Usamos de ejemplo el dominio http://localhost:3223, se debe cambiar por el correcto
//Se usa (*) para todos los dominios.
builder.Services.AddCors(p => p.AddPolicy("PolicyCors", build =>
{
    //CON ESTO HACEMOS QUE SOLO LOS QUE ESTEN EN ESE DOMINIO PODRAN CONSUMIR LA API
    //build.WithOrigins("http://localhost:3223").AllowAnyMethod().AllowAnyHeader(); //EJEMPLO 1
    //build.WithOrigins("http://localhost:3223", "http://localhost:5445").AllowAnyMethod().AllowAnyHeader(); //EJEMPLO 2
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); //EJEMPLO 3
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//44º - 2/5
//SOPORTE PARA CORS
app.UseCors("PolicyCors");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
