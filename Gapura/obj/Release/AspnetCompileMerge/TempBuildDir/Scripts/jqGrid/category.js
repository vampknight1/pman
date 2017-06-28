$(function () {
    $("#jqGrid").jqGrid({
        url: "/Categories/GetCategories",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['CategoryID', 'Category Name', 'Description' /*, 'Picture'*/],
        colModel: [
            { key: true, hidden: true, name: 'CategoryID', index: 'CategoryID', editable: false },
            { key: false, name: 'CategoryName', index: 'CategoryName', editable: true },
            { key: false, name: 'Description', index: 'Description', editable: true }
            //{ key: false, name: 'Gender', index: 'Gender', editable: true, edittype: 'select', editoptions: { value: { 'M': 'Male', 'F': 'Female', 'N': 'None' } } },
            //{ key: false, name: 'ClassName', index: 'ClassName', editable: true, edittype: 'select', editoptions: { value: { '1': '1st Class', '2': '2nd Class', '3': '3rd Class', '4': '4th Class', '5': '5th Class' } } },
            //{ key: false, name: 'DateOfAdmission', index: 'DateOfAdmission', editable: true, formatter: 'date', formatoptions: { newformat: 'd/m/Y' } }
            //{ key: false, name: 'Picture', index: 'Picture', editable: true }
        ],
        pager: 'pager',
        rowNum: 20,
        rowList: [10, 20, 30, 40, 50],
        height: '100%',
        //Weight: '110%', 
        viewrecords: true,
        loadonce: true,
        loadtext: 'Loading Master Category',
        caption: 'Master Categories YSID-GA Dept.',
        emptyrecords: 'No Categories Records are Available to Display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: true,
        viewrecords: true,
        sortname: 'CategoryName',
        sortorder: 'asc',
        imgpath: '../scripts/jqgrid/themes/basic/images',
    //}).navGrid('#jqControls', {

    })
    .navGrid('#pager', {
        edit: true,
        add: true,
        del: true,
        search: true,
        refresh: true,
        editText: 'Edit',
        addText: 'Add',
        delText: 'Delete',
        searchText: 'Search'
        //refreshText: 'Refresh',
    },
        {
            zIndex: 100,
            url: '/Categories/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            //recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 100,
            url: "/Categories/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 100,
            url: "/Categories/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want delete this Categories... ? ",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            //Search Options
            width: 600,
            multipleSearch: true,
        });
});