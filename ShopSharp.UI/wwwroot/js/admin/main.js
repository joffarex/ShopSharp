const app = new Vue({
    el: '#app',
    data: {
        loading: false,
        productModel: {
            name: "vue",
            description: "vuevue",
            value: "1.79",
        },
        products: [],
    },
    methods: {
        getProducts() {
            this.loading = true;
            axios.get('/Admin/products')
                .then(res => {
                    this.products = res.data;
                    console.log(res.data);
                })
                .catch(console.error)
                .then(() => {
                    this.loading = false;
                });
        },
        createProduct() {
            this.loading = true;
            axios.post('/Admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.push(res.data.result);
                })
                .catch(console.error)
                .then(() => {
                    this.loading = false;
                });
        },
    },
    computed: {},
});