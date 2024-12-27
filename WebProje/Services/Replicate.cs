using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ReplicateService
{
    private readonly HttpClient _httpClient;
    private readonly string _replicateApiKey = " "; // API anahtarınızı buraya koyun

    public ReplicateService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> CallReplicateApiAsync(string imageUrl, string prompt, double strength = 0.5)
    {
        var requestUrl = "https://api.replicate.com/v1/predictions";

        // JSON veri içeriği
        var requestBody = new
        {
            version = "51778c7522eb99added82c0c52873d7a391eecf5fcc3ac7856613b7e6443f2f7",
            input = new
            {
                image = imageUrl, // Fotoğraf URL'si
                steps = 50,
                prompt = prompt,
                negative_prompt = "(deformed iris, deformed pupils, semi-realistic, cgi, 3d, render, sketch, cartoon, drawing, anime:1.4), text, close up, cropped, out of frame, worst quality, low quality, jpeg artifacts, ugly, duplicate, morbid, mutilated, extra fingers, mutated hands, poorly drawn hands, poorly drawn face, mutation, deformed, blurry, dehydrated, bad anatomy, bad proportions, extra limbs, cloned face, disfigured, gross proportions, malformed limbs, missing arms, missing legs, extra arms, extra legs, fused fingers, too many fingers, long neck", // Negative prompt ekleniyor
                strength = 1.5,
                max_width = 612,
                max_height = 612,
                guidance_scale = 15
            }
        };

        // JSON formatına çevir
        var jsonContent = JsonSerializer.Serialize(requestBody);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Authorization başlıkları ekle
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_replicateApiKey}");
        _httpClient.DefaultRequestHeaders.Add("Prefer", "wait");

        try
        {
            // POST isteği gönder
            var response = await _httpClient.PostAsync(requestUrl, httpContent);

            // Yanıtı kontrol et
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent; // JSON yanıtı döndür
            }
            else
            {
                return $"API Error: {response.StatusCode} - {response.ReasonPhrase}";
            }
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}
