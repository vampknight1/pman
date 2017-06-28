$(document).ready(function () {
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
            url: '/Employee/LoadData',
            type: 'GET',
            datatype: 'json'
        },
        columns: [
            { data: 'LastName', 'width': '15%' },
            { data: 'OfficeCode', 'width': '5%' },
            { data: 'DepartemenName', 'width': '20%' },
            { data: 'TitleName', 'width': '10%' },
            { data: 'BirthDate', 'width': '10%' },
            { data: 'HireDate', 'width': '10%' },
            { data: 'ReportsTo', 'width': '15%' },
            //{ data: 'FirstName', 'width': '10%' },
            //{ data: 'TitleOfCourtesy', 'width': '5%' },
            //{ data: 'Address', 'width': '20%' },
            //{ data: 'City', 'width': '7%' },
            //{ data: 'Region', 'width': '8%' },
            //{ data: 'PostalCode', 'width': '8%' },
            //{ data: 'Country', 'width': '8%' },
            //{ data: 'HomePhone', 'width': '5%' },
            //{ data: 'Extension', 'width': '5%' },
            //{ data: 'Photo', 'width': '5%' },
            //{ data: 'Notes', 'width': '5%' },
            //{ data: 'PhotoPath', 'width': '5%' },                               
            {
                //data: null,
                //'className': 'center',
                data: 'EmployeeID', 'width': '4%', 'render': function (data) {
                    return '<a class="btn btn-success" href="/Employee/Details/' + data + '"><i class="halflings-icon white zoom-in"></i></a>' +
                            '<a class="btn btn-info" href="/Employee/Edit/' + data + '"><i class="halflings-icon white edit"></i></a>' +
                            '<a class="btn btn-danger" href="/Employee/Delete/' + data + '"><i class="halflings-icon white trash"></i></a>'
                }
            }
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