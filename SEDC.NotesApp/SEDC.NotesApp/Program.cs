using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.IdentityModel.Tokens;
using SEDC.NotesApp.Helpers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//read from appSettings.json, find the property AppSettings from the main object
var appSettings = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettings);

AppSettings appSettingsObject = appSettings.Get<AppSettings>();
//Server = MICA\\SQLEXPRESS; Database = NoteAuthDb; Trusted_Connection = True; TrustServerCertificate = true
//DEPENDENCY INJECTION
DependencyInjectionHelper.InjectDbContext(builder.Services, "Server=MICA\\SQLEXPRESS; Database = NoteAuthDb; Trusted_Connection = True; TrustServerCertificate = true");
DependencyInjectionHelper.InjectRepositories(builder.Services);
//DependencyInjectionHelper.InjectAdoRepositories(builder.Services, "Server=.;Database=NotesAppDb;Trusted_Connection=True");
//DependencyInjectionHelper.InjectDapperRepositories(builder.Services, appSettingsObject.ConnectionString);
DependencyInjectionHelper.InjectServices(builder.Services);

//Configure JWT
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Academy_academy_1234567890")),

    };

    builder.Services.AddAuthentication(x =>
    {
        //we will use JWT authentication
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    ).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        //we expect the token into the HttpContext

        x.SaveToken = true;

        //how to validate token
        x.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            //the secret key
            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettingsObject.SecretKey))
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Our very secret secret key"))
        };
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthentication(); // to be able to use JWT authentication
    app.UseAuthorization();

    app.MapControllers();

    app.Run();


});