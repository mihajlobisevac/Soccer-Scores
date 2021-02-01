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
                    console.log(res);
                    this.competitions = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        matchIsFinished(result) {
            if (typeof (result) !== 'undefined' || result !== null) {
                return true;
            }
            else {
                return false;
            }
        }
    },
    computed: {
    }
});
