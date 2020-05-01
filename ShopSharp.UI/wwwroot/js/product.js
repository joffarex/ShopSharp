const toggleStock = function (event) {
    let stockToHide = document.querySelectorAll('.low-stock');

    stockToHide.forEach(el => {
        el.classList.add('is-hidden');
    });

    showLowStock(event.target.value);
};

const showLowStock = function (id) {
    let stockToShow = document.getElementById(`low-stock-${id}`);

    if (stockToShow !== null && stockToShow !== undefined) {
        stockToShow.classList.remove('is-hidden');
    }
};

showLowStock(document.getElementById('CartDto_StockId').value);