using invoice.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<companyContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("companyContext") ?? throw new InvalidOperationException("Connection string 'companyContext' not found.")));
//builder.Services.AddDbContext<invoiceContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("invoiceContext") ?? throw new InvalidOperationException("Connection string 'invoiceContext' not found.")));

//Add Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddTransient<companyContext>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    //SeedData.Initialize(services);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
