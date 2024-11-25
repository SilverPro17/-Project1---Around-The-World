using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using MyNamespace;

public class CityNameAPI : MonoBehaviour
{
    public string city; // Nome da cidade fornecido pelo usuário
    public string APIkey; // Sua chave da API OpenWeather

    public TextMeshPro messageText; // Referência para o TextMeshPro
    private string cityName; // Nome da cidade obtido da API
    private string countryName; // Nome do país obtido da API

    private IEnumerator GetLatLon() {
        string url = "http://api.openweathermap.org/geo/1.0/direct?q=" + city + "&appid=" + APIkey;
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success) {
            string responseText = request.downloadHandler.text;

            // Desserializar a resposta da API
            CountryData[] dataDictionary = JsonConvert.DeserializeObject<CountryData[]>(responseText);
            if (dataDictionary.Length > 0) {
                CountryData data = dataDictionary[0];
                cityName = data.name; // Armazena o nome da cidade
                countryName = data.country; // Armazena o código do país
            } else {
                Debug.Log("Nenhum dado encontrado para a cidade especificada.");
            }
        } else {
            Debug.Log("Erro ao buscar latitude e longitude: " + request.error);
        }
    }

    // Start é chamado antes do primeiro frame update
    void Start()
    {
        StartCoroutine(GetLatLon());
    }

    // Update é chamado a cada frame
    void Update()
    {
        // Atualiza o texto com o nome da cidade e país
        if (!string.IsNullOrEmpty(cityName) && !string.IsNullOrEmpty(countryName)) {
            messageText.text = "Cidade: " + cityName + "\nPaís: " + countryName;
        }
    }
}

