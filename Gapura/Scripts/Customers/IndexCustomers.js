$(document).ready(function () {

    var oTable = $('#myTable').DataTable({
        ajax: {
            url: '/Customers/LoadData',
            type: 'GET',
            datatype: 'json',
            processing: true,
            serverSide: true
        },
        columns: [
            {
                //'data': null,
                //'className': 'center',
                data: 'CustomerID', 'width': '11%', 'render': function (data) {
                    return '<a class="btn btn-info" href="/Customers/Edit/' + data + '"><i class="halflings-icon white edit"></i></a>' +
                            '<a class="btn btn-danger" href="/Customers/Delete/' + data + '"><i class="halflings-icon white trash"></i></a>' +
                            '<a class="btn btn-success" href="/Customers/Details/' + data + '"><i class="halflings-icon white zoom-in"></i></a>'
                }
            },
            { data: 'CompanyName', 'width': '20%' },
            { data: 'ContactName', 'width': '15%' },
            { data: 'ContactTitle', 'width': '10%' },
            { data: 'Address', 'width': '11%' },
            //{ 'data': 'City', 'autoWidth': true },
            //{ 'data': 'Region', 'autoWidth': true },
            //{ 'data': 'PostalCode', 'autoWidth': true },
            //{ 'data': 'Country', 'autoWidth': true },
            { data: 'Phone', 'width': '10%' },
            { data: 'Fax', 'width': '10%' },
        ],
        pagingType: 'full_numbers',
        info: true,
        scrollCollapse: true,
        sScrollX: true,
        sScrollY: '55vh',
        sScrollCollapse: true,
        width: '100%'
    });


    ////$('#myTable').on('click', 'a.btn.btn-danger', function (e) {
    ////    e.preventDefault();
    ////        $('#hiddenCustomerID').val(CustomerID);
    ////        $('#deleteModal').modal('show');
    ////})

    //var confirmDelete = function (CustomerID) {
    //    $('#hiddenCustomerID').val(CustomerID);
    //    $('#deleteModal').modal('show');
    //}

    //var deleteCostumer = function () {

    //    $('#divLoader').show();
    //    var custID = $('#hiddenCustomerID').val();

    //    $.ajax({
    //        type: 'POST',
    //        url: '/Customers/Delete',
    //        data: { id: custID },
    //        //data: data,
    //        success: function (status) {
    //            alert('Data Deleted ! ');
    //            $('#divLoader').hide();
    //        }
    //    })
    //    .fail(function (xhr, status, errorThrown) {
    //        alert("Sorry, there was a problem !");
    //        console.log("Error: " + errorThrown);
    //        console.log("Status: " + status);
    //        console.dir(xhr);
    //    });
    //}


    $('#myTable').on('click', 'a.btn.btn-danger', function (e) {
        e.preventDefault();
        OpenPopup($(this).attr('href'));
    })

    function OpenPopup(pageUrl) {
        var $pageContent = $('<div/>');
        $pageContent.load(pageUrl, function () {
            $('#popupForm', $pageContent).removeData('validator');
            $('#popupForm', $pageContent).removeData('unobtrusiveValidation');
            $.validator.unobtrusive.parse('form');

        });

        $dialog = $('<div class="popupWindow" style="overflow:auto"></div>')
                  .html($pageContent)
                  .dialog({
                      draggable: true,
                      autoOpen: false,
                      resizable: false,
                      model: true,
                      title: ' Delete Confirmation',
                      height: 250,
                      width: 400,
                      close: function () {
                          $dialog.dialog('destroy').remove();
                      }
                  });

        $('.popupWindow').on('submit', '#popupForm', function (e) {
            var url = $('#popupForm')[0].action;
            $.ajax({
                type: "POST",
                url: url,
                data: $('#popupForm').serialize(),
                success: function (data) {
                    if (data.status) {
                        $dialog.dialog('close');
                        oTable.ajax.reload();
                    }
                }
            })
            .fail(function (xhr, status, errorThrown) {
                alert("Sorry, there was a problem !");
                console.log("Error: " + errorThrown);
                console.log("Status: " + status);
                console.dir(xhr);
            });
            e.preventDefault();
        });

        $dialog.dialog('open');
    }
});