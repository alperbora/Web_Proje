using Microsoft.EntityFrameworkCore;
using WebProje.Data;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();  


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var apiKey = builder.Configuration.GetValue<string>("Replicate:ApiKey");


builder.Services.AddRazorPages(); 
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddHttpClient<ReplicateService>();
builder.Services.AddHttpClient();


var app = builder.Build();


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

app.MapRazorPages(); 


app.MapControllers(); 

app.Run();
