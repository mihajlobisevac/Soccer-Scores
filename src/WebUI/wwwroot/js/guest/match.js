var app = new Vue({
    el: '#app',
    data: {
        matchId: 1,
        loading: false,
        matchModel: {
            id: 0,
            kickOff: "DateTime",
            gameWeek: 1,
            homeTeam: {
                id: 0,
                name: "Home Team",
                crest: "Crest",
            },
            awayTeam: {
                id: 0,
                name: "Away Team",
                crest: "Crest",
            },
            season: {
                id: 0,
                competitionId: 0,
                name: "Competition",
            },
            incidents: [],
            players: [],
        },
    },
    mounted() {
        this.getMatch(1)
    },
    methods: {
        getMatch(id) {
            this.loading = true;
            axios.get('/api/matches/' + id)
                .then(res => {
                    console.log(res);
                    var match = res.data;
                    this.matchModel = {
                        id: match.id,
                        kickOff: match.kickOff,
                        gameWeek: match.gameWeek,
                        homeTeam: match.homeTeam,
                        awayTeam: match.awayTeam,
                        season: match.season,
                        incidents: match.incidents,
                        players: match.players,
                    }
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
    },
    computed: {
        homeScore: function () {
            return this.matchModel
                .incidents
                .find(i => i.class === 'FT')
                .homeScore
        },
        awayScore: function () {
            return this.matchModel
                .incidents
                .find(i => i.class === 'FT')
                .awayScore
        },
        season: function () {
            return this.matchModel.season
        },
        incidents: function () {
            return this.matchModel.incidents
        }
    }
});
