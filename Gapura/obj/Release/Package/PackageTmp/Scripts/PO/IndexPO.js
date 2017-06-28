$(document).ready(function () {
    $('#myTable').DataTable({
        ajax: {
            url: '/POHeader/LoadData',
            type: 'GET',
            datatype: 'json',
        },
        columns: [
            {
                data: 'POID', 'width': '4%', 'render': function (data) {
                    return '<a class="btn btn-info" href="/POHeader/Edit/' + data + '"><i class="halflings-icon white edit"></i></a>' +
                            '<a class="btn btn-danger" href="/POHeader/Delete/' + data + '"><i class="halflings-icon white trash"></i></a>' +
                            '<a class="btn btn-success" href="/PODetail/Index/' + data + '"><i class="halflings-icon white zoom-in"></i></a>'
                }
            },
            { data: 'PONo', 'width': '10%' },
            { data: 'PODate', 'width': '10%' },
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
            { data: 'Remarks', 'width': '5%' }
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