
![lau4](https://github.com/user-attachments/assets/bc55189c-aa4a-494a-940b-159a36543c9d)

# Project1 - Around The World

## Descrição do Projeto
Este projeto Unity utiliza a **API OpenWeather** para exibir informações climáticas e a hora local em tempo real. A interface é construída com **objetos 3D** e **TextMeshPro**, oferecendo uma experiência visual interativa e informativa. O projeto foi implementado no **Unity 2022.2.21f1**, com suporte opcional para **Vuforia (10.27.3)**, permitindo futuras expansões para realidade aumentada (AR).

---

## Principais Funcionalidades
1. **Exibição de Informações Climáticas:**
   - Exibe temperatura atual, descrição do clima e condições gerais para a cidade especificada.
   
2. **Hora Local em Tempo Real:**
   - Calcula e exibe o horário local ajustado ao fuso horário da cidade.

3. **Localização Dinâmica:**
   - Obtém automaticamente a latitude e longitude da cidade especificada para buscar os dados de clima e hora.

4. **Interface Visual:**
   - Informações são exibidas em texto dinâmico utilizando **TextMeshPro** integrado a objetos 3D.

---
## Requisitos
- **Unity**: 2022.2.21f1.
- **API OpenWeather**: Uma chave de API válida (registre-se em [OpenWeather](https://openweathermap.org/)).
- **Pacotes Unity Necessários**:
  - **TextMeshPro** (padrão no Unity).
  - **Newtonsoft.Json** (para manipular JSON retornado pela API).

---

## Configuração do Projeto

### **1. Configurando o Unity**
1. Crie um novo projeto Unity (ou use um existente).
2. Certifique-se de que o **TextMeshPro** está configurado:
   - Adicione o pacote via **Window > Package Manager**.
3. Adicione o pacote **Newtonsoft.Json**:
   - Em **Package Manager**, clique em "Add package by name".
   - Insira `com.unity.nuget.newtonsoft-json`.

### **2. Criando a Cena**
1. Adicione **objetos 3D** na cena:
   - Um **Quad** para exibir o fundo ou informações.
   - Um **Cubo** (opcional) para interação visual.
2. Adicione objetos de texto:
   - Utilize **TextMeshPro** para exibir as informações climáticas e o horário.
     
![lau2](https://github.com/user-attachments/assets/4cf0278e-8a6f-4422-b97d-55c51bd8f696)

### **3. Configuração da API OpenWeather**
1. Cadastre-se em [OpenWeather](https://openweathermap.org/) e obtenha uma chave de API.
2. Insira a chave de API nos campos apropriados nos scripts Unity.

![eloelo](https://github.com/user-attachments/assets/8ad42192-31ee-49e5-b55f-cf1bba520122)

---

## Estrutura do Projeto

### **Scripts Principais**
1. **CityNameAPI.cs**
   - Obtém o nome da cidade e do país com base no input do usuário.
   - Exibe informações básicas da localização.

2. **TimeAPI.cs**
   - Obtém o horário local ajustado ao fuso horário da cidade.
   - Exibe a hora atualizada em tempo real.

3. **WeatherAPI.cs**
   - Obtém e exibe a temperatura atual e uma descrição do clima com base na localização.

4. **Classes de Modelo (MyNamespace):**
   - Define as estruturas para manipular as respostas JSON da API OpenWeather, incluindo clima, hora e localização.

---

## Como Usar
1. **Configuração Inicial:**
   - No Unity, importe os scripts fornecidos.
   - Adicione os scripts aos objetos da cena (ex.: `CityNameAPI.cs` para um objeto TextMeshPro).

2. **Inserção da Chave API:**
   - Em cada script, insira sua chave da API OpenWeather nos campos públicos `APIkey` via Inspector.

3. **Especificação da Cidade:**
   - No campo `city`, insira o nome da cidade para a qual deseja obter informações (ex.: "New York").

4. **Execução:**
   - Pressione **Play** no Unity.
   - Veja as informações exibidas em tempo real.
---

## Exemplo de Exibição
Informações exibidas no Unity durante a execução:
- **Clima Atual:**
  - "Temperatura: 25°C"
  - "Clima: Céu limpo"
- **Hora Local:**
  - "Hora local: 14:35:50"
- **Localização:**
  - "Cidade: New York"
  - "País: US"

---

## Scripts em Destaque
### **CityNameAPI.cs**
Exemplo de como o nome da cidade e do país é obtido:
```csharp
if (!string.IsNullOrEmpty(cityName) && !string.IsNullOrEmpty(countryName)) {
    messageText.text = "Cidade: " + cityName + "\nPaís: " + countryName;
}
````
### **WeatherAPI.cs**
Exemplo de exibição de temperatura e clima:
```csharp
if (dataInitialized) {
    messageText.text = $"Temperatura: {temp}°C\nClima: {weatherDescription}";
} else {
    messageText.text = "Carregando dados do clima...";
}
```
### **TimeAPI.cs**
Exemplo de cálculo da hora local em tempo real:
```csharp
localTime = localTime.AddSeconds(Time.deltaTime);
messageText.text = "Hora local: " + localTime.ToString("HH:mm:ss");
```
## Autor
**Silvano Rodrigues**  
Estudante universitário  
Desenvolvedor entusiasta com foco em aplicações interativas e integração de APIs.

