const addOneToCart = function (event) {
    const stockId = event.target.dataset.stockId;

    axios.post(`/Cart/AddOne/${stockId}`)
        .then(res => {
            updateCart();
        })
        .catch(err => {
            console.error(err);
            alert(err.message);
        });
};

const removeFromCart = function (stockId, quantity) {
    axios.post(`/Cart/Remove/${stockId}/${quantity}`)
        .then(res => {
            updateCart();
        })
        .catch(err => {
            console.error(err);
            alert(err.message);
        });
};

const removeOneFromCart = function (event) {
    const stockId = event.target.dataset.stockId;

    removeFromCart(stockId, 1)
};

const removeAllFromCart = function (event) {
    const stockId = event.target.dataset.stockId;
    let el = document.getElementById(`stock-quantity-${stockId}`);
    let quantity = Number(el.innerText);

    removeFromCart(stockId, quantity);
};

const updateCart = function () {
    axios.get('/Cart/GetCartComponent')
        .then(res => {
            let html = res.data;
            let el = document.getElementById('cart-nav');
            el.outerHTML = html;
        })
        .catch(console.error);

    axios.get('/Cart/GetCartMain')
        .then(res => {
            let html = res.data;
            let el = document.getElementById('cart-main');
            el.outerHTML = html;
        })
        .catch(console.error);
};