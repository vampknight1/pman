//var editor;
$(document).ready(function () {
    //'editor' = new $.fn.dataTable.editor({
    //    'ajax': '/Categories/LoadData',
    //    'table': '#myTable',
    //    'fields': [{
    //        'label': 'Start date:',
    //        'name': 'CategoryName',
    //        'type': 'string'
    //    }, {
    //        'label': 'Description:',
    //        'name': 'Description',
    //        'type': 'string'
    //    }]
    //});

    $('#myTable').DataTable({
        //'dom': 'Bfrtip',
        ajax: {
            url: '/Categories/LoadData',
            type: 'GET',
            datatype: 'json'
        },
        columns: [
            { data: 'CategoryName', 'width': '32%' },
            { data: 'Description', 'width': '50%' },
            {
                data: 'CategoryID', 'width': '13%', 'render': function (data) {
                    return '<a class="btn btn-success" href="/Categories/Details/" + data + ""><i class="halflings-icon white zoom-in"></i></a>' +
                            '<a class="btn btn-info" href="/Categories/Edit/" + data + ""><i class="halflings-icon white edit"></i></a>' +
                            '<a class="btn btn-danger" href="/Categories/Delete/" + data + ""><i class="halflings-icon white trash"></i></a>'
                }
            }
        ],
        pagingType: 'full_numbers',
        //'bJQueryUI': true,
        //'bProcessing': true,
        //'bServerSide': true,
        scrollCollapse: true,
        sScrollX: true,
        sScrollY: '55vh',
        sScrollCollapse: true,
        bSort: true,
        select: {
            style: 'os',
            selector: 'td:first-child'
        },
        //'buttons': [
        //    { 'extend': 'create', 'editor': 'editor' },
        //    { 'extend': 'edit', 'editor': 'editor' },
        //    { 'extend': 'remove', 'editor': 'editor' }
        //]
    }).makeEditable({
        sUpdateURL: '/RequestHeader/Edit',
        sAddURL: '/RequestHeader/Detail',
        sDeleteURL: '/RequestHeader/Delete',
    });
});