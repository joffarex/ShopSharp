const app = new Vue({
    el: '#products',
    data: {
        loading: false,
        editing: false,
        productModel: {
            id: null,
            name: "",
            description: "",
            value: "",
        },
        products: [],
        objectIndex: null,

    },
    mounted() {
        this.getProducts();
    },
    methods: {
        getProducts() {
            this.loading = true;
            axios.get('/products')
                .then(res => {
                    this.products = res.data;
                    console.log(res.data);
                })
                .catch(console.error)
                .then(() => {
                    this.loading = false;
                });
        },
        getProduct(id) {
            this.loading = true;
            axios.get(`/products/${id}`)
                .then(res => {
                    const product = res.data;
                    this.productModel = {
                        id: product.id,
                        name: product.name,
                        description: product.description,
                        value: product.value.slice(1),
                    };
                    console.log(this.productModel);
                })
                .catch(console.error)
                .then(() => {
                    this.loading = false;
                });
        },
        createProduct() {
            this.loading = true;
            const body = {
                name: this.productModel.name,
                description: this.productModel.description,
                value: this.productModel.value,
            };
            axios.post('/products', body)
                .then(res => {
                    console.log(res.data);
                    this.products.push(res.data);
                })
                .catch(console.error)
                .then(() => {
                    this.clearInput();
                    this.editing = false;
                    this.loading = false;
                });
        },
        editProduct(id, index) {
            this.loading = true;
            this.editing = true;
            this.objectIndex = index;
            this.getProduct(id);
        },
        updateProduct() {
            this.loading = true;
            const body = {
                name: this.productModel.name,
                description: this.productModel.description,
                value: this.productModel.value,
            };
            axios.put(`/products/${this.productModel.id}`, body)
                .then(res => {
                    console.log(res.data);
                    this.products.splice(this.objectIndex, 1, res.data);
                })
                .catch(console.error)
                .then(() => {
                    this.clearInput();
                    this.editing = false;
                    this.loading = false;
                });
        },
        deleteProduct(id, index) {
            this.loading = true;
            axios.delete(`/products/${id}`)
                .then(res => {
                    console.log(res.data);
                    this.products.splice(index, 1);
                })
                .catch(console.error)
                .then(() => {
                    this.loading = false;
                });
        },
        newProduct() {
            this.editing = true;
            this.clearInput();
        },
        clearInput() {
            this.objectIndex = null;
            this.productModel = {
                id: null,
                name: "",
                description: "",
                value: "",
            };
        },
        cancel() {
            this.clearInput();
            this.editing = false;
        }
    },
    computed: {},
});