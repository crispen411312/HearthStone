using ManyToMany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyToMany.ViewModels
{
    public class DeckWithCards
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public List<Card> Cards { get; set; }

    }
}
