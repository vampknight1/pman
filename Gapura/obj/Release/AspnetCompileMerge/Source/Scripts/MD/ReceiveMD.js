var Product = []
//fetch Product from database
function LoadProduct(element) {
    if (Product.length == 0) {
        //ajax function for fetch data
        $.ajax({
            type: "GET",
            url: '/Products/getProducts',
            success: function (data) {
                Product = data;
                //render Product
                renderProduct(element);
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
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(Product, function (i, val) {
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
        //render Products to the element
        renderUnit(element);
    }
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
        if ($('#ProductID').val() == "0") {
            isAllValid = false;
            $('#ProductID').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#ProductID').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#UnitPrice').val().trim() != '' && !isNaN($('#UnitPrice').val().trim()))) {
            isAllValid = false;
            $('#UnitPrice').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#UnitPrice').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#UnitID').val() == "0") {
            isAllValid = false;
            $('#UnitID').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#UnitID').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#Quantity').val().trim() != '' && !isNaN($('#Quantity').val().trim()))) {
            isAllValid = false;
            $('#Quantity').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Quantity').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#Amount').val().trim() != '' && !isNaN($('#Amount').val().trim()))) {
            isAllValid = false;
            $('#Amount').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Amount').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#Remark').val().trim() == '') {
            isAllValid = false;
            $('#Remark').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Remark').siblings('span.error').css('visibility', 'hidden');
        }


        if (isAllValid) {
            var $newRow = $('#mainrow').clone().removeAttr('id');
            $('.ProductID', $newRow).val($('#ProductID').val());
            $('.UnitID', $newRow).val($('#UnitID').val());

            //Replace add button with remove button
            $('#add', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#ProductID,#product,#quantity,#rate,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //append clone row
            $('#orderdetailsItems').append($newRow);

            //clear select data
            $('#ProductID,#product').val('0');
            $('#quantity,#rate').val('');
            $('#orderItemError').empty();
        }

    })

    //remove button click event
    $('#orderdetailsItems').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });

    $('#submit').click(function () {
        var isAllValid = true;

        //validate order items
        $('#orderItemError').text('');
        var list = [];
        var errorItemCount = 0;
        $('#orderdetailsItems tbody tr').each(function (index, ele) {
            if (
                $('select.product', this).val() == "0" ||
                (parseInt($('.quantity', this).val()) || 0) == 0 ||
                $('.rate', this).val() == "" ||
                isNaN($('.rate', this).val())
                ) {
                errorItemCount++;
                $(this).addClass('error');
            } else {
                var orderItem = {
                    ProductID: $('select.product', this).val(),
                    Quantity: parseInt($('.quantity', this).val()),
                    Rate: parseFloat($('.rate', this).val())
                }
                list.push(orderItem);
            }
        })

        if (errorItemCount > 0) {
            $('#orderItemError').text(errorItemCount + " invalid entry in order item list.");
            isAllValid = false;
        }

        if (list.length == 0) {
            $('#orderItemError').text('At least 1 order item required.');
            isAllValid = false;
        }

        if ($('#orderNo').val().trim() == '') {
            $('#orderNo').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#orderNo').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#orderDate').val().trim() == '') {
            $('#orderDate').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#orderDate').siblings('span.error').css('visibility', 'hidden');
        }

        if (isAllValid) {
            var data = {
                OrderNo: $('#orderNo').val().trim(),
                OrderDateString: $('#orderDate').val().trim(),
                Description: $('#description').val().trim(),
                OrderDetails: list
            }

            $(this).val('Please wait...');

            $.ajax({
                type: 'POST',
                url: '/home/save',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (data) {
                    if (data.status) {
                        alert('Successfully saved');
                        //here we will clear the form
                        list = [];
                        $('#orderNo,#orderDate,#description').val('');
                        $('#orderdetailsItems').empty();
                    }
                    else {
                        alert('Error');
                    }
                    $('#submit').text('Save');
                },
                error: function (error) {
                    console.log(error);
                    $('#submit').text('Save');
                }
            });
        }

    });

});

LoadProduct($('#ProductID'));
LoadProduct($('#UnitID'));