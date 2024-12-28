using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;


public class AIController : Controller
{
    private readonly ReplicateService _replicateService;

    public AIController(ReplicateService replicateService)
    {
        _replicateService = replicateService;
    }

    [HttpPost,Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> GenerateImage(string imageUrl, string prompt)
    {
        // Replicate API'yi çağırın ve yanıtı alın
        var result = await _replicateService.CallReplicateApiAsync(imageUrl, prompt);

        // JSON yanıtını manuel olarak işleyip 'output' değerini alın
        var jsonResponse = JsonDocument.Parse(result);
        var outputUrl = jsonResponse.RootElement.GetProperty("output").GetString();

        // Output URL'yi ViewData'ya ekleyin
        ViewData["GeneratedImageOutput"] = outputUrl;

        return View("Result");
    }

    [Authorize(Roles = "User,Admin")]
    public IActionResult Index()
    {
        return View();
    }
}

