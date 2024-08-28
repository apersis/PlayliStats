// See https://aka.ms/new-console-template for more information
using System.Collections.ObjectModel;
using Newtonsoft.Json;

Console.WriteLine("Hello World!");

//Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

var serializer = new JsonSerializer();
Root jsonRoot = new();
using (var streamReader = new StreamReader("C:\\Users\\Pc\\Documents\\SpotifyAPI\\data.json"))
using (var textReader = new JsonTextReader(streamReader))
{
    jsonRoot = serializer.Deserialize<Root>(textReader);
}

List<Item> playlist = jsonRoot.items;

Dictionary<string,int> musiquesParArtistes = new Dictionary<string, int>();

// Decompte du nombre de musiques par artiste dans cette playlist
foreach (Item music in playlist){
    int nbrArtistes = music.track.artists.Count();
    for (int i=0; i < nbrArtistes; i++){
        string artiste = music.track.artists[i].name;
        if (artiste != null){
            if(musiquesParArtistes.ContainsKey(artiste)){
                musiquesParArtistes[artiste]++;
            }else{
                musiquesParArtistes.Add(artiste,1);
            }
        }
    }
}

var musiquesParArtistesCroissant = musiquesParArtistes.OrderBy(pair => pair.Value);

// Affichage de mon dictionaire
foreach (var pair in musiquesParArtistesCroissant){
    Console.WriteLine($"{pair.Key} : {pair.Value}");
}

Console.WriteLine("End Of Program");