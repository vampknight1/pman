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
            url: '/ProductsInventory/LoadData',
            type: 'GET',
            datatype: 'json'
        },
        columns: [
            { data: 'DepartemenName', 'width': '17%' },
            { data: 'ProductCode', 'width': '13%' },
            { data: 'ProductName', 'width': '15%' },
            { data: 'UnitName', 'width': '5%' },
            { data: 'CategoryName', 'width': '13%' },
            { data: 'UnitsInStock', 'width': '7%' },
            { data: 'UnitsOnOrder', 'width': '7%' },
            //{
            //    mRender: function (data, type, row) {
            //        //var linkEdit = '@Html.ActionLink('Edit', 'Edit', new { ProductID = 1, DepartemenID = 11 })';
            //        var linkEdit = '<a href='/ProductsInventory/Edit/ProductID=1'+'11f'+'DepartemenID=11'>Edit</a>';
            //        linkEdit = linkEdit.replace('1', row.ProductID);
            //        linkEdit = linkEdit.replace('11', row.DepartemenID);

            //        //var linkDetails = '@Html.ActionLink('Details', 'Details', new { ProductID = 2, DepartemenID = 22 })';
            //        var linkDetails = '<a href='/ProductsInventory/Details/ProductID=2 DepartemenID=22'>Details</a>';
            //        linkDetails = linkDetails.replace('2', row.ProductID);
            //        linkDetails = linkDetails.replace('22', row.DepartemenID);

            //        //var linkDelete = '@Html.ActionLink('Delete', 'Delete', new { ProductID = 3, DepartemenID = 33 })';
            //        var linkDelete = '<a href='/ProductsInventory/Delete/ProductID=3 DepartemenID=33'>Delete</a>';
            //        linkDelete = linkDelete.replace('3', row.ProductID);
            //        linkDelete = linkDelete.replace('33', row.DepartemenID);

            //        //return linkDetails +
            //        //           '<br/>' + linkEdit +
            //        //           '<br/>' + linkDelete;

            //        //'render': function(data){
            //        //    return
            //        //    '<a class='btn btn-info' href='/ProductsInventory/Edit/' + linkEdit + ''><i class='halflings-icon white edit'></i></a>' +
            //        //    '<a class='btn btn-danger' href='/ProductsInventory/Delete/' + linkDelete + ''><i class='halflings-icon white trash'></i></a>' +
            //        //    '<a class='btn btn-success' href='/ProductsInventory/Details/' + linkDetails + ''><i class='halflings-icon white zoom-in'></i></a>';
            //        //}
            //    }
            //},
            {
                ////data: null,
                ////'className': 'center',
                data: '{ProductID}, {DepartemenID}',
                width: '13%',
                render: function (data) {
                    render: function (data) {
                        return '<a class="btn btn-info" href="/ProductsInventory/Edit/' + data + '"><i class="halflings-icon white edit"></i></a>' +
                                    '<a class="btn btn-danger" href="/ProductsInventory/Delete/' + data + '"><i class="halflings-icon white trash"></i></a>' +
                                    '<a class="btn btn-success" href="/ProductsInventory/Details/' + data + '"><i class="halflings-icon white zoom-in"></i></a>'
                }
            }
        ],
        pagingType: 'full_numbers',
        info: true,
        scrollCollapse: true,
        sScrollX: true,
        sScrollY: '55vh',
        sScrollCollapse: true,
        width: '100%',
        postion: 'absolute'
    });
    dataTable.table.on('draw', function () {
        $('button[data-type="delete"]').click(function () {
            var $button = $(this);
        });
        $('button[data-type="edit"]').click(function () {
        });
    });
});