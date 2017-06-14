namespace ManyToMany {

    angular.module('ManyToMany', ['ui.router', 'ngResource', 'ui.bootstrap']).config((
        $stateProvider: ng.ui.IStateProvider,
        $urlRouterProvider: ng.ui.IUrlRouterProvider,
        $locationProvider: ng.ILocationProvider
    ) => {
        // Define routes
        $stateProvider
            .state('home', {
                url: '/',
                templateUrl: '/ngApp/views/home.html',
                controller: ManyToMany.Controllers.HomeController,
                controllerAs: 'controller'
            })
            .state(`cards`, {
                url: `/cards`,
                templateUrl: `ngApp/views/cards.html`,
                controller: ManyToMany.Controllers.AllCardsController,
                controllerAs: `controller`
            })
            .state('about', {
                url: '/about',
                templateUrl: '/ngApp/views/about.html',
                controller: ManyToMany.Controllers.AboutController,
                controllerAs: 'controller'
            })
            .state(`addDeck`, {
                url: `/addDeck`,
                templateUrl: `ngApp/views/addDeck.html`,
                controller: ManyToMany.Controllers.AddDeckController,
                controllerAs: `controller`
            })
            .state('details', {
                url: '/details/:id',
                templateUrl: '/ngApp/views/details.html',
                controller: ManyToMany.Controllers.DeckCardsController,
                controllerAs: 'controller'
            })
            .state('notFound', {
                url: '/notFound',
                templateUrl: '/ngApp/views/notFound.html'
            });

        // Handle request for non-existent route
        $urlRouterProvider.otherwise('/notFound');

        // Enable HTML5 navigation
        $locationProvider.html5Mode(true);
    });

    

}
