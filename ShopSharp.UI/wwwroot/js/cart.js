const addOneToCart = function (event) {
    const stockId = event.target.dataset.stockId;

    axios.post(`/Cart/AddOne/${stockId}`)
        .then(res => {
            console.log(res.data);
            let el = document.getElementById(`stock-${stockId}`);

            let quantity = Number(el.innerText);
            el.innerText = (quantity + 1);
            updateTotalValue(stockId, 'add');
        })
        .catch(err => {
            console.error(err);
            alert(err.message);
        });
};

const removeOneFromCart = function (event) {
    const stockId = event.target.dataset.stockId;

    axios.post(`/Cart/RemoveOne/${stockId}`)
        .then(res => {
            console.log(res.data);
            let el = document.getElementById(`stock-${stockId}`);

            let quantity = Number(el.innerText);
            el.innerText = (quantity - 1);
            updateTotalValue(stockId, 'remove');
        })
        .catch(err => {
            console.error(err);
            alert(err.message);
        });
};

const removeAllFromCart = function (event) {
    const stockId = event.target.dataset.stockId;

    axios.post(`/Cart/RemoveAll/${stockId}`)
        .then(res => {
            console.log(res.data);
            let el = document.getElementById(`product-${stockId}`);
            let value = document.getElementById(`product-value-${stockId}`).innerText.split('$')[1];
            let quantity = document.getElementById(`stock-${stockId}`).innerText;

            let totalValue = Number(value) * Number(quantity);

            el.outerHTML = "";
            updateTotalValue(stockId, 'removeAll', totalValue);
        })
        .catch(err => {
            console.error(err);
            alert(err.message);
        });
};

const updateTotalValue = function (stockId, type, removeAllTotalValue = null) {
    let value;
    if (removeAllTotalValue === null) {
        value = document.getElementById(`product-value-${stockId}`).innerText.split('$')[1];
    } else {
        value = removeAllTotalValue.toFixed(2);
    }

    let el = document.getElementById('total-value');
    let totalValue = Number(el.innerText.substr(1));
    switch (type) {
        case 'remove':
            el.innerText = `$${(totalValue - Number(value)).toFixed(2)}`;
            break;
        case 'add':
            el.innerText = `$${(totalValue + Number(value)).toFixed(2)}`;
            break;
        case 'removeAll':
            const displayValue = (totalValue - Number(value)).toFixed(2);
            el.innerText = `$${displayValue}`;
            break;
        default:
            break;
    }
};