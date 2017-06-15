namespace ManyToMany.Controllers {

    export class HomeController {
        public decks;
        public deleteDeck(id: number) {
            this.$http.delete(`/api/decks/` + id).then((response) => {
                this.$state.reload();
            })
        }

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService, ) {
            this.$http.get('/api/decks').then((response) => {
                this.decks = response.data;
            })
        }
    }

    export class AllCardsController {

        public cards;
        public card;

        public deleteCard(id: number) {
            this.$http.delete(`/api/cards/` + id).then((response) => {
                this.$state.reload();
            })
        }

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService, ) {
            this.$http.get('/api/cards').then((response) => {
                this.cards = response.data;
            })
        }
        public addCard() {
            this.$http.post('/api/cards/', this.card).then((response) => {
                this.$state.reload();
            })
        }
    }

    export class AddDeckController {
        public cards;
        public deck;

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService) {
            this.$http.get('/api/cards').then((response) => {
                this.cards = response.data;
            })
        }

        public addDeck() {
            this.$http.post('/api/decks/', this.deck).then((response) => {
                this.$state.go('home');
            })
        }
    }

    // -------------------------------------- This one has issues. 
    export class DeckCardsController {
      //  public deckResource;
        public deckWithCards;
        public cardsWithDecks;
       // public card;
        

        public addDeckCard(card) {

            card.decks = this.deckWithCards;
            console.log("Card id is " +card.id);
            this.$http.post('/api/cards', card).then((response) => {
                this.$state.reload();
            })
        }

        public deleteDeckCard(id: number) {
            this.$http.delete(`/api/cards/` + id).then((response) => {
                this.$state.reload();
            })
        }

        constructor($resource: angular.resource.IResourceService,
            private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            private $stateParams: ng.ui.IStateParamsService) { // --------- Injected $stateparams in order to get id.
           
            this.$http.get('/api/cards').then((response) => {
                this.cardsWithDecks = response.data;
            })

            let DeckId = $stateParams[`id`];  // ---------------------Gets deck id from URL
            this.$http.get('/api/cards' + DeckId).then((response) => {
                this.deckWithCards = response.data;
            })
            console.log("Deck id = " + DeckId);
            console.log("Deck Contents " + this.deckWithCards);
            console.log("Card Contents " + this.cardsWithDecks);
        }
    }



    export class AboutController {
        public message = 'Hello from the about page!';
    }
}

