using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;
using MyNamespace;
public class TimeAPI : MonoBehaviour
{
    public string city; // Nome da cidade inserida pelo usuário
    public string APIkey; // Chave da API OpenWeather

    public TextMeshPro messageText; // Referência para exibir a hora
    public TextMeshPro cityText; // Referência para exibir cidade e país

    private DateTime localTime; // Armazena o horário local
    private string country = "--"; // Inicializa com placeholder
    private bool timeInitialized = false; // Verifica se o tempo foi inicializado

    private IEnumerator GetTimeFromAPI()
    {
        // URL da API com o nome da cidade
        string url = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + APIkey;
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            Debug.Log("Resposta da API: " + responseText);

            // Desserializar os dados retornados pela API
            CountryTime data = JsonConvert.DeserializeObject<CountryTime>(responseText);
            int timezone = data.timezone; // Fuso horário em segundos
            int currentTimeUnix = data.dt; // Horário atual no formato UNIX
            country = data.sys.country; // Código do país

            // Converter o horário atual para UTC e ajustar pelo fuso horário
            DateTime utcTime = DateTimeOffset.FromUnixTimeSeconds(currentTimeUnix).UtcDateTime;
            localTime = utcTime.AddSeconds(timezone);

            timeInitialized = true; // Marca que o tempo foi inicializado
        }
        else
        {
            Debug.LogError("Erro ao obter dados da API: " + request.error);
        }
    }

    void Start()
    {
        if (messageText == null)
        {
            Debug.LogError("messageText não está atribuído. Por favor, arraste o objeto correspondente no Inspector.");
        }
        if (cityText == null)
        {
            Debug.LogError("cityText não está atribuído. Por favor, arraste o objeto correspondente no Inspector.");
        }

        // Inicia a obtenção da hora pela API
        StartCoroutine(GetTimeFromAPI());
    }

    void Update()
    {
        if (messageText == null || cityText == null)
        {
            Debug.LogError("messageText ou cityText não está atribuído. Verifique no Inspector.");
            return;
        }

        if (timeInitialized)
        {
            // Incrementa a hora em tempo real
            localTime = localTime.AddSeconds(Time.deltaTime);

            // Atualiza os textos
            messageText.text = "Hora local: " + localTime.ToString("HH:mm:ss");
            cityText.text = "Cidade: " + city + "\nPaís: " + country;
        }
        else
        {
            messageText.text = "Obtendo horário...";
            cityText.text = "Cidade: " + city + "\nPaís: --";
        }
    }
}

