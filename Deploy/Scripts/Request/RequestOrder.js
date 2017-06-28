$(document).ready(function () {
    $('#myTable').DataTable({
        ajax: {
            url: '/RequestOrder/LoadData',
            type: 'GET',
            datatype: 'json',
        },
        'columns': [
            //{
            //    data: 'RequestID', 'width': '1%', 'render': function (data) {
            //        return '<img src='/Content/images/details_open.png' alt='expand/collapse' rel='' +data+ ''/>'
            //    }
            //},
            {
                data: 'RequestID', 'width': '4%', 'render': function (data) {
                    return '<a class="btn btn-info" href="/RequestOrder/Edit/' + data + '"><i class="halflings-icon white edit"></i></a>' +
                            '<a class="btn btn-danger" id="btnDelete" href="/RequestOrder/Delete/' + data + '"><i class="halflings-icon white trash"></i></a>' +
                        //'<a class="btn btn-danger" id="btnDelete" href="/RequestOrder/Delete/#"><i class="halflings-icon white trash"></i></a>' +
                            '<a class="btn btn-success" href="javascript:;' + data + '"><i class="halflings-icon white zoom-in"></i></a>'
                            + '<img src="/Content/images/details_open.png" alt="expand/collapse" rel="' + data + '"/>'
                }
            },
            { data: 'RequestNo', 'width': '10%' },
            { data: 'DepartemenName', 'width': '5%' },
            { data: 'RequestDate', 'width': '11%' },
            { data: 'RequiredDate', 'width': '10%' },
            { data: 'TotalRequest', 'width': '5%', 'render': $.fn.dataTable.render.number(',', '.', 0, '') },
            { data: 'ReffNo', 'width': '5%' },
            { data: 'RequestType', 'width': '5%' },
            { data: 'Requester', 'width': '7%' },
            { data: 'Manager', 'width': '7%' },
            { data: 'GAMgr', 'width': '7%' },
            { data: 'GM', 'width': '7%' },
            { data: 'TotalPrice', 'width': '5%', 'render': $.fn.dataTable.render.number(',', '.', 0, '') },
            { data: 'CurrencyCode', 'width': '2%' },
            { data: 'AssetsType', 'width': '5%' },
            { data: 'Remarks', 'width': '5%' },
        ],
        pagingType: 'full_numbers',
        info: true,
        scrollCollapse: true,
        sScrollX: true,
        sScrollY: '55vh',
        sScrollCollapse: true,
        bSort: true,
        select: {
            style: 'os',
            selector: 'td:first-child'
        },
        buttons: [
            { extend: 'create', 'editor': 'editor' },
            { extend: 'edit', 'editor': 'editor' },
            { extend: 'remove', 'editor': 'editor' }
        ]
    });
});
var oTable;
$('#myTable tbody td img').click(function () {
    var nTr = this.parentNode.parentNode;
    if (this.src.match('details_close')) {
        /* This row is already open - close it */
        this.src = '/Content/images/details_open.png';
        oTable.fnClose(nTr);
    }
    else {
        /* Open this row */
        this.src = '/Content/images/details_close.png';
        var id = $(this).attr('rel');
        $.get('/RequestDetail/Index?id=' + id, function (e) {
            oTable.fnOpen(nTr, e, 'details');
        });
    }
});


//$(document).delegate('#btnDelete', 'click', function (e) {
$(document).ready(function () {
    $('#btnDelete').click(function () {
        alert('Are you want delete this ?');
    //debugger
    //e.preventDefault();
    //$(function () {

    //$('#myTable .a.btn.btn-success').click(function () {
    //    var id = $(this).closest('tr').find('td').eq(0).html();
        $.ajax({
            type: 'POST',
            url: '/RequestOrder/Delete' + data,
            data: RequestID,
            //contentType: 'application/json; charset=utf-8',
            dataType: 'text/html',
            async: true,
            cache: false,
            success: function (response) {
                //$('#dialog').html(response);
                //$('#dialog').dialog('open');
                alert('Success !!')
            },
            //failure: function (response) {
            //    alert(response.responseText);
            //},
            //error: function (response) {
            //    alert(response.responseText);
            error: function (data) {
                alert(data.x);
            }
        });
    });
});