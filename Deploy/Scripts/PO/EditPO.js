$(function () {
    $('#ReffNo').focus();
    $('#PODate').datepicker({
        appendText: ' YYYY-MM-DD',
        dateFormat: 'yy-mm-dd',
        minDate: 0,
        changeMonth: true,
        changeYear: true,
        showOn: 'both'        //for button
    });
});

function totAmount() {
    var varUnitPrice = document.getElementById('unitprice').value;
    var varQuantity = document.getElementById('quantity').value;
    document.getElementById('amount').value = parseFloat(varUnitPrice) * parseInt(varQuantity);
}

$('.quantity').keyup(function () {
    var result = 0;
    $('#TotalRequest').attr('value', function () {
        $('.quantity').each(function () {
            if ($(this).val() !== '') {
                result += parseInt($(this).val());
            }
        });
        return result;
    });
});

$('.amount').keyup(function () {
    var result = 0;
    $('#TotalPrice').attr('value', function () {
        $('.amount').each(function () {
            if ($(this).val() !== '') {
                result += parseFloat($(this).val());
            }
        });
        return result;
    });
});