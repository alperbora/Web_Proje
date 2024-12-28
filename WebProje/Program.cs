using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;

var builder = WebApplication.CreateBuilder(args);

// Veritabanı bağlantısını SQLite olarak yapılandırma
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// HTTP istemcisi ve RazorPages hizmetlerini ekleyin
builder.Services.AddRazorPages();
builder.Services.AddHttpClient<ReplicateService>();
builder.Services.AddHttpClient();

// Authentication (Cookie) ve Authorization yapılandırması
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login"; // Giriş yapmamış kullanıcılar bu URL'ye yönlendirilecek
        options.AccessDeniedPath = "/Home/AccessDenied"; // Erişim reddedilen kullanıcılar buraya yönlendirilir
    });

// Authorization politikası ekleniyor (Admin rolüne sahip olanlar için)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

// MVC Controller'ları yapılandırma
builder.Services.AddControllers();

var app = builder.Build();

// Veritabanı ve seed verilerini eklemek için scope oluşturuluyor
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Veritabanı ve tabloların oluşturulmasını sağlıyoruz
    context.Database.EnsureCreated();
    Console.WriteLine("Veritabanı oluşturuldu (veya zaten mevcut).");

    // Seed verilerini ekleme işlemi
    try
    {
        SeedData.Initialize(scope.ServiceProvider, context);
        Console.WriteLine("Seed veriler başarıyla eklendi.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Seed verileri eklerken hata oluştu: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middleware sıralaması
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();  // Authentication middleware (Bu burada olmalı)
app.UseAuthorization();   // Authorization middleware (Bu da burada olmalı)

// Erişim izni olmayan kullanıcılar için yönlendirme
app.UseStatusCodePagesWithRedirects("/Home/AccessDenied");

// Default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
