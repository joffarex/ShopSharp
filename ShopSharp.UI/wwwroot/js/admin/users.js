const app = new Vue({
    el: '#users',
    data: {
        loading: false,
        username: "",
        password: "",
    },
    mounted() {
        // TODO: get all users
    },
    methods: {
        createUser() {
            this.loading = true;
            axios.post('/users', {
                username: this.username,
                password: this.password,
            })
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