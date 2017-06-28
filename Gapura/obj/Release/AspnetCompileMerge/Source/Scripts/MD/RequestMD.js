var Products = []
//fetch Product from database
function LoadProduct(element) {
    if (Products.length == 0) {
        //ajax function for fetch data
        $.ajax({
            type: "GET",
            url: '/Products/getProducts',
            success: function (data) {
                Products = data;
                //render Product
                renderProduct(element);
                //console.log(data);
            }
        })
    }
    else {
        //render Product to the element
        renderProduct(element);
    }
}

function renderProduct(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('- Select Item -'));
    $.each(Products, function (i, val) {
        $ele.append($('<option/>').val(val.ProductID).text(val.ProductName));
    })
}

var Units = []
//fetch Units from database
function LoadUnit(element) {
    if (Units.length == 0) {
        //ajax function for fetch data
        $.ajax({
            type: "GET",
            url: '/Units/getUnits',
            success: function (data) {
                Units = data;
                //render Units
                renderUnit(element);
            }
        })
    }
    else {
        //render Units to the element
        renderUnit(element);
    }
}

function renderUnit(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('-Select-'));
    $.each(Units, function (i, val) {
        $ele.append($('<option/>').val(val.UnitID).text(val.UnitName));
    })
}

/*
//fetch products
function LoadProduct(categoryDD) {
    $.ajax({
        type: "GET",
        url: "/Products/LoadData",
        data: { 'categoryID': $(categoryDD).val() },
        success: function (data) {
            //render products to appropriate dropdown
            renderProduct($(categoryDD).parents('.mycontainer').find('select.product'), data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}

function renderProduct(element, data) {
    //render product
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(data, function (i, val) {
        $ele.append($('<option/>').val(val.ProductID).text(val.ProductName));
    })
}
*/
$(document).ready(function () {
    //Add button click event
    $('#add').click(function () {
        //validation and add Request items
        var isAllValid = true;
        if (!($('#itemname').val().trim() != '' && (parseInt($('#itemname').val()) || 0))) {
            isAllValid = false;
            $('#itemname').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#itemname').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#unitprice').val().trim() != '' && !isNaN($('#unitprice').val().trim()))) {
            isAllValid = false;
            $('#unitprice').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#unitprice').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#unitname').val() == "0") {
            isAllValid = false;
            $('#unitname').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#unitname').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#quantity').val().trim() != '' && (parseInt($('#quantity').val()) || 0))) {
            isAllValid = false;
            $('#quantity').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#quantity').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#amount').val().trim() != '' && !isNaN($('#amount').val().trim()))) {
            isAllValid = false;
            $('#amount').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#amount').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#remark').val().trim() == '') {
            isAllValid = false;
            $('#remark').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#remark').siblings('span.error').css('visibility', 'hidden');
        }

        if (isAllValid) {
            var $newRow = $('#mainrow').clone().removeAttr('id');
            $('.itemname', $newRow).val($('#itemname').val());
            $('.unitname', $newRow).val($('#unitname').val());

            //Replace add button with remove button
            $('#add', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#sitemname, #unitprice, #unitname, #quantity, #amount, #remark, #add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //append clone row
            $('#orderdetailsItems').append($newRow);

            //clear select data
            $('#unitname').val('0');
            $('#sitemname, #unitprice, #quantity, #amount, #remark').val('');
            $('#orderItemError').empty();
            $('#sitemname').focus();
        }

    })

    //remove button click event
    $('#orderdetailsItems').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });

    $('#submit').click(function () {
        var isAllValid = true;

        //validate Request items
        $('#orderItemError').text('');
        var list = [];
        var errorItemCount = 0;
        $('#orderdetailsItems tbody tr').each(function (index, ele) {
            if (
                $('.itemname', this).val() == "" ||
                $('.unitname', this).val() == "0" ||
                (parseInt($('.quantity', this).val()) || 0) == 0 ||
                $('.remark', this).val() == "" ||
                $('.unitprice', this).val() == "" ||
                isNaN($('.unitprice', this).val()) ||
                $('.amount', this).val() == "" ||
                isNaN($('.amount', this).val())
                ) {
                errorItemCount++;
                $(this).addClass('error');
            } else {
                var orderItem = {
                    ProductID: $('.itemname', this).val(),
                    UnitPrice: parseFloat($('.unitprice', this).val()),
                    UnitID: $('.unitname', this).val(),
                    Quantity: parseInt($('.quantity', this).val()),
                    Amount: parseFloat($('.amount', this).val()),
                    Remarks: $('.remark', this).val()
                }
                list.push(orderItem);
                //// Cek Item List
                //console.log(orderItem);
                //console.log(list);
                //console.log(errorItemCount);
                //console.log(index);
                //console.log(ele);
            }
        })

        if (errorItemCount > 0) {
            $('#orderItemError').text(errorItemCount + " invalid entry in Requested item list.");
            isAllValid = false;
        }

        if (list.length == 0) {
            $('#orderItemError').text('At least, 1 item required !');
            isAllValid = false;
        }

        if ($('#RequestNo').val().trim() == '') {
            $('#RequestNo').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#RequestNo').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#RequestDate').val().trim() == '') {
            $('#RequestDate').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#RequestDate').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#RequiredDate').val().trim() == '') {
            $('#RequiredDate').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#RequiredDate').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#ReffNo').val().trim() == '') {
            $('#ReffNo').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#ReffNo').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#RequestTypeID').val().trim() == '') {
            $('#RequestTypeID').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#RequestTypeID').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#AssetsTypeID').val().trim() == '') {
            $('#AssetsTypeID').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#AssetsTypeID').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#sEmployeeID').val().trim() == '') {
            $('#sEmployeeID').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#EmployeeID').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#sMgrID').val().trim() == '') {
            $('#sMgrID').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#MgrID').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#DepartemenID').val().trim() == '') {
            $('#DepartemenID').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#DepartemenID').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#CurrencyID').val().trim() == '') {
            $('#CurrencyID').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#CurrencyID').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#TotalRequest').val().trim() == '') {
            $('#TotalRequest').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#TotalRequest').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#TotalPrice').val().trim() == '') {
            $('#TotalPrice').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#TotalPrice').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#Remarks').val().trim() == '') {
            $('#Remarks').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#Remarks').siblings('span.error').css('visibility', 'hidden');
        }

        if (isAllValid) {
            var data = {
                RequestNo: $('#RequestNo').val().trim(),
                RequestDate: $('#RequestDate').val().trim(),
                RequiredDate: $('#RequiredDate').val().trim(),
                ReffNo: $('#ReffNo').val().trim(),
                RequestTypeID: $('#RequestTypeID').val().trim(),
                AssetsTypeID: $('#AssetsTypeID').val().trim(),
                EmployeeID: $('#EmployeeID').val().trim(),
                MgrID: $('#MgrID').val().trim(),
                DepartemenID: $('#DepartemenID').val().trim(),
                CurrencyID: $('#CurrencyID').val().trim(),
                TotalRequest: $('#TotalRequest').val().trim(),
                TotalPrice: $('#TotalPrice').val().trim(),
                Remarks: $('#Remarks').val().trim(),
                RequestDetails: list
            }
            // console.log(data);           //cek

            $(this).val('Please wait..');

            $.ajax({
                type: 'POST',
                url: '/RequestHeader/Save',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (data) {
                    if (data.status) {
                        alert('Requested Item Successfully Saved');
                        //Clear the form
                        list = [];
                        $('#RequestNo,#RequestDate,#RequiredDate,#ReffNo,#RequestTypeID,#AssetsTypeID,#EmployeeID,#MgrID,#DepartemenID,#CurrencyID,#TotalRequest,#TotalPrice,#Remarks').val('');
                        $('#orderdetailsItems').empty();
                        //window.location = '/RequestHeader/UpdatePayments?json=' + JSON.stringify(data);   // Ex. for Update
                        window.location = '/RequestHeader/Index';
                    }
                    else {
                        alert('Error');
                    }
                    $('#submit').text('Save');
                },
                error: function (error) {
                    //console.log(error);
                    $('#submit').text('Save');
                }
            });
        }
        //return JavaScript("location.reload(true)");
    });
});

LoadProduct($('#itemname'));
LoadUnit($('#unitname'));