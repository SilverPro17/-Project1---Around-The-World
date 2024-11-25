using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using MyNamespace;

public class WeatherAPI : MonoBehaviour
{
    public string city; // Nome da cidade fornecida pelo usuário
    public string APIkey; // Sua chave da API OpenWeather

    public TextMeshPro messageText; // Referência para exibir os dados
    private double lat;
    private double lon;
    private double temp; // Temperatura
    private string weatherDescription = ""; // Descrição do clima
    private bool dataInitialized = false; // Para garantir que os dados foram inicializados

    private IEnumerator GetLatLon()
    {
        string url = "http://api.openweathermap.org/geo/1.0/direct?q=" + city + "&appid=" + APIkey;
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;

            CountryData[] dataDictionary = JsonConvert.DeserializeObject<CountryData[]>(responseText);
            if (dataDictionary.Length > 0)
            {
                CountryData data = dataDictionary[0];
                lat = data.lat;
                lon = data.lon;
                Debug.Log($"Latitude: {lat}, Longitude: {lon}");
                StartCoroutine(GetWeather()); // Chama o método para obter o clima após obter lat/lon
            }
            else
            {
                Debug.LogError("Nenhum dado encontrado para a cidade especificada.");
            }
        }
        else
        {
            Debug.LogError("Erro ao obter latitudes e longitudes: " + request.error);
        }
    }

    private IEnumerator GetWeather()
    {
        string url = "https://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&units=metric&appid=" + APIkey;
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            Debug.Log("Resposta da API: " + responseText);

            WeatherResponse data = JsonConvert.DeserializeObject<WeatherResponse>(responseText);

            // Atualiza os dados de temperatura e descrição do clima
            temp = data.main.temp;
            weatherDescription = data.weather[0].description; // Primeiro item da lista de condições
            dataInitialized = true; // Marca que os dados foram inicializados
        }
        else
        {
            Debug.LogError("Erro ao obter os dados do clima: " + request.error);
        }
    }

    void Start()
    {
        if (messageText == null)
        {
            Debug.LogError("messageText não está atribuído. Por favor, arraste o objeto correspondente no Inspector.");
        }

        // Inicia a obtenção da latitude e longitude
        StartCoroutine(GetLatLon());

        // Atualiza o clima a cada 60 segundos
        InvokeRepeating(nameof(UpdateWeather), 60f, 60f);
    }

    private void UpdateWeather()
    {
        StartCoroutine(GetWeather()); // Reobtém os dados climáticos em tempo real
    }

    void Update()
    {
        if (dataInitialized)
        {
            // Exibe os dados atualizados em tempo real
            messageText.text = $"Temperatura: {temp}°C\nClima: {weatherDescription}";
        }
        else
        {
            messageText.text = "Carregando dados do clima...";
        }
    }
}
