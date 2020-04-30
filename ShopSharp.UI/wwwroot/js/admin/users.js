const app = new Vue({
    el: '#users',
    data: {
        loading: false,
        username: "",
    },
    mounted() {
        // TODO: get all users
    },
    methods: {
        createUser() {
            this.loading = true;
            axios.post('/users', {username: this.username})
                .then(res => {
                    console.log(res);
                })
                .catch(console.error)
                .then(() => {
                    this.loading = false;
                });
        },

    },
    computed: {},
});