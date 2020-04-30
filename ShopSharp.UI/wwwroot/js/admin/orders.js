const app = new Vue({
    el: '#orders',
    data: {
        status: 0,
        loading: false,
        orders: [],
        selectedOrder: null,
    },
    mounted() {
        this.getOrders();
    },
    watch: {
        status: function () {
            this.getOrders();
        },
    },
    methods: {
        getOrders() {
            this.loading = true;
            axios.get(`/orders?status=${this.status}`)
                .then(res => {
                    this.orders = res.data;
                    console.log(res.data);
                })
                .catch(console.error)
                .then(() => {
                    this.loading = false;
                });
        },
        selectOrder(id) {
            this.loading = true;
            axios.get(`/orders/${id}`)
                .then(res => {
                    this.selectedOrder = res.data;
                    console.log(res.data);
                })
                .catch(console.error)
                .then(() => {
                    this.loading = false;
                });
        },
        updateOrder() {
            this.loading = true;
            axios.put(`/orders/${this.selectedOrder.id}`, null)
                .then(res => {
                    console.log(res);
                    this.exitOrder();
                    this.getOrders();
                })
                .catch(console.error)
                .then(() => {
                    this.loading = false;
                });
        },
        exitOrder() {
            this.selectedOrder = null;
        },
    },
    computed: {},
});