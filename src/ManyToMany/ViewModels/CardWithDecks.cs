using ManyToMany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyToMany.ViewModels
{
    public class CardWithDecks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int Cost { get; set; }
        public List<Deck> Decks { get; set; }

    }
}
