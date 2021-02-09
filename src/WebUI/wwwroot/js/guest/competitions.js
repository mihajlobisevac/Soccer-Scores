var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        countries: []
    },
    mounted() {
        this.getCountriesWithCompetitions();
    },
    methods: {
        getCountriesWithCompetitions() {
            this.loading = true;
            axios.get('/api/competitions/all')
                .then(res => {
                    this.countries = res.data
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false
                })
        },
        getCompetitions(countryId) {
            let competitions = this.countries
                .find(c => c.id === countryId)
                .competitions.forEach(c => {
                    c.latestSeason = 1
                })

            return competitions
        },
    },
    computed: {
        // countriesWithCompetitions: function () {
        //     let countries = this.countries
            
        //     if(countries) {
        //         countries.forEach(country => {
        //             country.competitions.forEach(c => {
        //                 c.latestSeason = 1
        //             })
        //         })
        //     }

        //     return countries
        // },
    }
});
