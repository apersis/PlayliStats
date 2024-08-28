using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace aspnetcoreapp.Pages_Musics
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public Dictionary<string,int> musiquesParArtistes { get;set; } = new Dictionary<string, int>();

        public async Task OnGetAsync()
        {


            var serializer = new JsonSerializer();
            Root jsonRoot = new();
            using (var streamReader = new StreamReader("..\\data.json"))
            using (var textReader = new JsonTextReader(streamReader))
            {
                jsonRoot = serializer.Deserialize<Root>(textReader);
            }

            List<Item> playlist = jsonRoot.items;

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
        }
    }
}
