using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyToMany.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public ICollection<DeckCard> DeckCards { get; set; }
    }
}
