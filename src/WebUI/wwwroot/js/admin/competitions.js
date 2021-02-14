var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        countries: []
    },
    mounted() {
        this.getCountries();
    },
    methods: {
        getCountries() {
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
        }
    },
    computed: {
    }
});
