using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyToMany.Models
{
    public class DeckCard
    {
        public Deck Deck { get; set; }
        public int DeckId { get; set; }
        public Card Card { get; set; }
        public int CardId { get; set; }

    }
}
