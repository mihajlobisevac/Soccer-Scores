var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        competitions: []
    },
    mounted() {
        this.getMatches();
    },
    methods: {
        getMatches() {
            this.loading = true;
            axios.get('/api/matches/fixtures')
                .then(res => {
                    this.competitions = res.data
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false
                })
        }
    },
    computed: {
    }
});
