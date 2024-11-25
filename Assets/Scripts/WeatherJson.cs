using System.Collections.Generic;

namespace MyNamespace
{
    // Classe representando a resposta geral de clima
    public class WeatherResponse
    {
        public MainInfo main { get; set; } // Dados principais (temperatura, umidade, etc.)
        public List<WeatherDescription> weather { get; set; } // Lista de condições climáticas
    }

    // Classe representando os dados principais de clima
    public class MainInfo
    {
        public double temp { get; set; } // Temperatura
        public double feels_like { get; set; } // Sensação térmica
        public double temp_min { get; set; } // Temperatura mínima
        public double temp_max { get; set; } // Temperatura máxima
        public int pressure { get; set; } // Pressão atmosférica
        public int humidity { get; set; } // Umidade
    }

    // Classe representando a descrição do clima
    public class WeatherDescription
    {
        public string main { get; set; } // Categoria principal do clima (ex.: "Clear")
        public string description { get; set; } // Descrição detalhada (ex.: "clear sky")
    }

    // Classe representando os dados de latitude e longitude
    public class CountryData
    {
        public string name { get; set; } // Nome da cidade
        public double lat { get; set; } // Latitude
        public double lon { get; set; } // Longitude
        public string country { get; set; } // Código do país
    }

    // Classe representando o fuso horário e horário atual
    public class CountryTime
    {
        public Sys sys { get; set; } // Sistema de informações (inclui país)
        public int timezone { get; set; } // Fuso horário em segundos
        public int dt { get; set; } // Horário atual no formato UNIX
    }

    // Classe representando informações de sistema
    public class Sys
    {
        public string country { get; set; } // Código do país
    }
}
