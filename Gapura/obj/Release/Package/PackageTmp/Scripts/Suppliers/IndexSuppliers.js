$(document).ready(function () {
    //var item;
    $('a.editor_create').on('click', function (e) {
        e.preventDefault();

        editor.create({
            title: 'Create new record',
            buttons: 'Add'
        });
    });
    // Edit record
    $('#myTable').on('click', 'a.editor_edit', function (e) {
        e.preventDefault();

        editor.edit($(this).closest('tr'), {
            title: 'Edit record',
            buttons: 'Update'
        });
    });
    // Delete a record
    $('#myTable').on('click', 'a.editor_remove', function (e) {
        e.preventDefault();

        editor.remove($(this).closest('tr'), {
            title: 'Delete record',
            message: 'Are you sure you wish to remove this record ?',
            buttons: 'Delete'
        });
    });

    $('#myTable').DataTable({
        ajax: {
            url: '/Suppliers/LoadData',
            type: 'GET',
            datatype: 'json'
        },
        columns: [
            {
                //data: null,
                //'className': 'center',
                data: "SupplierID", "width": "4%", "render": function (data) {
                    return '<a class="btn btn-info" href="/Suppliers/Edit/' + data + '"><i class="halflings-icon white edit"></i></a>' +
                            '<a class="btn btn-danger" href="/Suppliers/Delete/' + data + '"><i class="halflings-icon white trash"></i></a>' +
                            '<a class="btn btn-success" href="/Suppliers/Details/' + data + '"><i class="halflings-icon white zoom-in"></i></a>'
                }
            },
            { data: 'SupplierCode', 'width': '10%' },
            { data: 'CategoryName', 'width': '10%' },
            { data: 'CompanyName', 'width': '10%' },
            { data: 'Address', 'width': '20%' },
            { data: 'City', 'width': '7%' },
            { data: 'Region', 'width': '8%' },
            { data: 'ContactName', 'width': '8%' },
            { data: 'Phone', 'width': '5%' },
            { data: 'CellPhone', 'width': '5%' },
            { data: 'Npwp', 'width': '8%' },
            { data: 'TermDays', 'width': '5%' }
        ],
        pagingType: 'full_numbers',
        info: true,
        scrollCollapse: true,
        sScrollX: true,
        sScrollY: '55vh',
        sScrollCollapse: true,
        width: '100%'
    });
    dataTable.table.on('draw', function () {
        $('button[data-type="delete"]').click(function () {
            var $button = $(this);
        });

        $('button[data-type="edit"]').click(function () {
        });
    });
});