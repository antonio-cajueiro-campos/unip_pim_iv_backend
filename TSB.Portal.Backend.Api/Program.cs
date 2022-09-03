using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using TSB.Portal.Backend.Application.UseCases.Authenticate;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

//JWT
var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
	opt.TokenValidationParameters = new()
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = configuration["Jwt:ValidIssuer"],
		ValidAudience = configuration["Jwt:ValidAudience"],
		IssuerSigningKey = chaveSimetrica
	};
});


builder.Services.AddControllers();

// Dependecy Injection
builder.Services.AddDependecies();

builder.Services.AddDataContext(configuration).AddDbServices();

builder.Services.AddControllersWithViews()
	.AddJsonOptions(options =>
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.EnableAnnotations();
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Portal TSB", Version = "v1" });

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter \'Bearer\' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement {
		{
			new OpenApiSecurityScheme {
				Reference = new OpenApiReference {
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});

// Authorization
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
	options.AddPolicy("Collaborators", policy => policy.RequireRole("Admin", "Commercial", "Financial", "Support", "Technician"));
});

// Cors
builder.Services.AddCors(c =>
{
	c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	app.UseHttpsRedirection();
	app.UseCors(x => x
		.AllowAnyMethod().AllowAnyHeader()
		.SetIsOriginAllowed(origin => true)
		.AllowCredentials()
	);
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
