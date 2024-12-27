using Microsoft.EntityFrameworkCore;
using WebProje.Data;

var builder = WebApplication.CreateBuilder(args);

// API Controller'larını ekleyin
builder.Services.AddControllers();  // Bu satır, API controller'larını kullanabilmek için gerekli.

// Diğer servislerinizi eklemeyi unutmayın
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Diğer servisler
builder.Services.AddRazorPages(); // Razor Pages için
builder.Services.AddEndpointsApiExplorer(); // API dokümantasyonu için

var app = builder.Build();

// Middleware yapılandırması
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Razor Pages için

// API'ler için endpointleri tanımlayın
app.MapControllers(); // API controller'larını kullanabilmek için gerekli.

app.Run();
