using FitTrack.Auth.Auth;
using FitTrack.Auth.Data.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PgSqlConnectionString")));


builder.Services.AddIdentityServer(options =>
{
    options.EmitStaticAudienceClaim = true;
})
#if DEBUG
.AddDeveloperSigningCredential()
#endif
.AddProfileService<ProfileService>();


var app = builder.Build();

app.UseIdentityServer();
app.Run();