using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ManyToMany.Data;
using ManyToMany.Models;
using ManyToMany.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ManyToMany.API
{
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        ApplicationDbContext _db;

        public CardsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public List<CardWithDecks> Get()
        {

            List<CardWithDecks> cards = (from c in _db.Cards
                                         select new CardWithDecks
                                         {
                                             Id = c.Id,
                                             Class = c.Class,
                                             Cost = c.Cost,
                                             Name = c.Name
                                         }).ToList();


            return cards;
        }

        [HttpGet("{id}")]
        public DeckWithCards Get(int id) // ------------ I switched from cards with deck.
        {
            DeckWithCards deck = (from c in _db.Decks
                                  where c.Id == id
                                  select new DeckWithCards
                                  {
                                      Id = c.Id,
                                      Name = c.Name,
                                      Class = c.Class,
                                      Cards = (from cd in _db.DeckCards
                                               where cd.CardId == id
                                               select cd.Card).ToList()
                                  }).FirstOrDefault();
            return deck;
        }

        [HttpPost]
        public IActionResult Post([FromBody]CardWithDecks card)
        {
            if (card == null)
            {
                return BadRequest();
            }
            else if (card.Id == 0)
            {
                Card tempCard = new Card
                {
                    Name = card.Name,
                    Class = card.Class
                };
                _db.Cards.Add(tempCard);
                _db.SaveChanges();

                foreach (Deck d in card.Decks)
                {
                    _db.DeckCards.Add(new DeckCard
                    {
                        CardId = tempCard.Id,
                        DeckId = d.Id
                    });
                    _db.SaveChanges();
                }

                return Ok();

            }
            else if (card.Id !=0)
            {
                foreach (Deck d in card.Decks)
                {
                    _db.DeckCards.Add(new DeckCard
                    {
                        CardId = card.Id,
                        DeckId = d.Id
                    });
                    _db.SaveChanges();
                }

                return Ok();


            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Card cardToDelete = (from c in _db.Cards
                                 where c.Id == id
                                 select c).FirstOrDefault();

            _db.Cards.Remove(cardToDelete);


            //List<MovieActor> mvToDelete = (from mv in _db.MovieActors // not needed in this case because the entity framework handles it.
            //                               where mv.MovieId == id
            //                               select mv).ToList();
            //_db.MovieActors.RemoveRange(mvToDelete);
            _db.SaveChanges();

            return Ok();
        }
    }
}
