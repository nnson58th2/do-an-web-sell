$(document).ready(function () {
    let money = $('.price').val();
    let price = new Intl.NumberFormat('de-DE', { style: 'currency', currency: 'VND' }).format(money);
    console.log(price);
});