const app = new Vue({
    el: '#stock',
    data: {
        loading: false,
        products: [],
        selectedProduct: null,
        newStock: {
            productId: null,
            description: "",
            quantity: null,
        }
    },
    mounted() {
        this.getStock();
    },
    methods: {
        getStock() {
            this.loading = true;
            axios.get('/Admin/stocks')
                .then(res => {
                    this.products = res.data;
                    console.log(res.data);
                })
                .catch(console.error)
                .then(() => {
                    this.loading = false;
                });
        },
        selectProduct(product) {
            this.selectedProduct = product;
            this.newStock.productId = product.id;
        },
        updateStock() {
            this.loading = true;
            axios.put('/Admin/stocks', {
                stocks: this.selectedProduct.stocks.map(x => {
                    return {
                        id: x.id,
                        description: x.description,
                        quantity: x.quantity,
                        productId: this.selectedProduct.id,
                    };
                })
            })
                .then(res => {
                    console.log(res.data);
                })
                .catch(console.error)
                .then(() => {
                    this.loading = false;
                });
        },
        addStock() {
            this.loading = true;
            axios.post('/Admin/stocks', this.newStock)
                .then(res => {
                    this.selectedProduct.stocks.push(res.data);
                    console.log(res.data);
                })
                .catch(console.error)
                .then(() => {
                    this.loading = false;
                });
        },
        deleteStock(id, index) {
            this.loading = true;
            axios.delete(`/Admin/stocks/${id}`)
                .then(res => {
                    this.selectedProduct.stocks.splice(index, 1);
                    console.log(res.data);
                })
                .catch(console.error)
                .then(() => {
                    this.loading = false;
                });
        },
    },
    computed: {},
});