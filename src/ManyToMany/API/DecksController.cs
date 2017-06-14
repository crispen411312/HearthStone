using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ManyToMany.Data;
using ManyToMany.ViewModels;
using ManyToMany.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ManyToMany.API
{
    [Route("api/[controller]")]
    public class DecksController : Controller
    {
        // this is only here bacuse we are not using repo or services 
        private ApplicationDbContext _db;
        //Constructor
        public DecksController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public List<Deck> Get()
        {
            return _db.Decks.ToList();
        }

        [HttpGet("{id}")]
        public DeckWithCards Get(int id)
        {
            DeckWithCards Deck = (from d in _db.Decks
                                   where d.Id == id
                                   select new DeckWithCards
                                   {
                                       Id = d.Id,
                                       Name = d.Name,
                                       Class = d.Class,
                                       Cards = (from dc in _db.DeckCards // this is the join table
                                                 where dc.DeckId == d.Id // where the id's match
                                                 select dc.Card).ToList() // select the actor and add to list
                                   }).FirstOrDefault(); // return first or default like normal.
            return Deck;
        }

        [HttpPost]
        public IActionResult Post([FromBody]DeckWithCards deck)
        {
            if (deck == null)
            {
                return BadRequest();
            } else if (deck.Id == 0)
            {
                Deck tempDeck = new Deck
                {
                    Name = deck.Name,
                    Class = deck.Class,
                };
                _db.Decks.Add(tempDeck);
                _db.SaveChanges();

                /*foreach (Card card in deck.Cards) // --------------- to select 
                {
                    _db.DeckCards.Add(new DeckCard
                    {
                        DeckId = tempDeck.Id, //-------------------this needs to be the newly 
                        CardId = card.Id
                    });
                    _db.SaveChanges();
                }*/
                

                return Ok();

            } else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Deck deckToDelete = (from d in _db.Decks
                                 where d.Id == id
                                 select d).FirstOrDefault();

            _db.Decks.Remove(deckToDelete);


            //List<MovieActor> mvToDelete = (from mv in _db.MovieActors // not needed in this case because the entity framework handles it.
            //                               where mv.MovieId == id
            //                               select mv).ToList();
            //_db.MovieActors.RemoveRange(mvToDelete);
            _db.SaveChanges();

            return Ok();
        }

    }
}
