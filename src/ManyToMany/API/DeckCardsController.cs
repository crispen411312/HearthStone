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
    public class DeckCardsController : Controller
    {

        ApplicationDbContext _db;

        public DeckCardsController(ApplicationDbContext db)
        {
            this._db = db;
        }

        // GET: api/values
        [HttpGet]
        public List<Card> Get()
        {
            return _db.Cards.ToList();
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public DeckWithCards Get(int id)
        {
            DeckWithCards deck = (from d in _db.Decks
                                  where d.Id == id
                                  select new DeckWithCards
                                  {
                                      Name = d.Name,
                                      Cards = (from dc in _db.DeckCards
                                               where dc.DeckId == id
                                               select dc.Card).ToList()
                                  }).FirstOrDefault();
            return deck;
        }

        //{
        //   // _db.Cards.Add(card);
        //   // _db.SaveChanges();

        //    _db.DeckCards.Add(new DeckCard
        //    {
        //        DeckId = id,
        //        CardId = card.Id
        //    });
        //    _db.SaveChanges();

        //    return Ok();
        //}


        [HttpPost]
        public IActionResult Post(int id)
        {

            if (id != 0)
            {
                DeckCard tempCard = new DeckCard
                {
                    // DeckId = id,
                    // CardId = card.Id
                };
                _db.DeckCards.Add(tempCard);
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
            DeckCard cardToDelete = (from dc in _db.DeckCards
                                     where dc.CardId == id
                                     select dc).FirstOrDefault();

            _db.DeckCards.Remove(cardToDelete);
            _db.SaveChanges();

            return Ok();
        }
    }
}




