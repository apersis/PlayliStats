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
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace aspnetcoreapp.Pages_Musics
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public ObservableCollection<ArtistInfo> obsArtistInfo = new ObservableCollection<ArtistInfo>();

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
                    string nameArtiste = music.track.artists[i].name;
                    if (nameArtiste != null){
                        if(obsArtistInfo.Any(a => a.name == nameArtiste)){
                            int nbrSons = obsArtistInfo.First(a => a.name == nameArtiste).nbrSons;
                            obsArtistInfo.First(a => a.name == nameArtiste).nbrSons = nbrSons + 1;
                        }else{
                            ArtistInfo nouvelArtiste = new ArtistInfo(nameArtiste, 1, music.track.album.images[0].url);
                            obsArtistInfo.Add(nouvelArtiste);
                        }
                    }
                }
            }
            obsArtistInfo = new ObservableCollection<ArtistInfo>(obsArtistInfo.OrderByDescending(p => p.nbrSons));
        }
    }
}
