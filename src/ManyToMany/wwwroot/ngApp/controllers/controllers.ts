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

    export class DeckCardsController {
      //  public deckResource;
        public deckCards;
        public cards;
        public card;

        public addDeckCard(card: number) {
           // return this.deckResource.save({ id: deckId }, card);
            this.$http.post('/api/deckCards', this.card).then((response) => {
                this.$state.reload();
            })
        }

        public deleteDeckCard(id: number) {
            this.$http.delete(`/api/deckCards/` + id).then((response) => {
                this.$state.reload();
            })
        }

        constructor($resource: angular.resource.IResourceService, private $http: ng.IHttpService, private $state: ng.ui.IStateService) {
            //this.deckResource = $resource('/api/decks/:id');
            this.$http.get('/api/cards').then((response) => {
                this.cards = response.data;
            })
            this.$http.get('/api/deckCards/:id').then((response) => {
                this.deckCards = response.data;
            })

        }
    }



    export class AboutController {
        public message = 'Hello from the about page!';
    }
}

