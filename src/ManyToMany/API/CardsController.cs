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
        public List<Card> Get()
        {
            return _db.Cards.ToList();
        }

        [HttpGet("{id}")]
        public CardWithDecks Get(int id)
        {
            CardWithDecks card = (from c in _db.Cards
                                     where c.Id == id
                                     select new CardWithDecks
                                     {
                                         Id = c.Id,
                                         Name = c.Name,
                                         Class = c.Class,
                                         Cost = c.Cost,
                                         Decks = (from dc in _db.DeckCards
                                                   where dc.CardId == id
                                                   select dc.Deck).ToList()
                                     }).FirstOrDefault();
            return card;
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
                    Class = card.Class,
                    Cost = card.Cost,
                };
                _db.Cards.Add(tempCard);
                _db.SaveChanges();

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
