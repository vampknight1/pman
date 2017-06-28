$(function () {
    $("#jqGrid").jqGrid({
        url: "/Suppliers/GetSuppliers",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['SupplierID', 'Company Name', 'Contact Name', 'Contact Title', 'Address', 'City', 'Postal Code', 'Country', 'Phone', 'Fax', 'HomePage'],
        colModel: [
            { key: true, hidden: true, name: 'SupplierID', index: 'SupplierID', editable: true },
            { key: false, name: 'CompanyName', index: 'CompanyName', editable: true },
            { key: false, name: 'ContactName', index: 'ContactName', editable: true },
            //{ key: false, name: 'Gender', index: 'Gender', editable: true, edittype: 'select', editoptions: { value: { 'M': 'Male', 'F': 'Female', 'N': 'None' } } },
            //{ key: false, name: 'ClassName', index: 'ClassName', editable: true, edittype: 'select', editoptions: { value: { '1': '1st Class', '2': '2nd Class', '3': '3rd Class', '4': '4th Class', '5': '5th Class' } } },
            //{ key: false, name: 'DateOfAdmission', index: 'DateOfAdmission', editable: true, formatter: 'date', formatoptions: { newformat: 'd/m/Y' } }
            { key: false, name: 'ContactTitle', index: 'ContactTitle', editable: true },
            { key: false, name: 'Address', index: 'Address', editable: true },
            { key: false, name: 'City', index: 'City', editable: true },
            { key: false, name: 'PostalCode', index: 'PostalCode', editable: true },
            { key: false, name: 'Country', index: 'Country', editable: true },
            { key: false, name: 'Phone', index: 'Phone', editable: true },
            { key: false, name: 'Fax', index: 'Fax', editable: true },
            { key: false, name: 'HomePage', index: 'HomePage', editable: true }
            ],
        pager: jQuery('#jqControls'),
        rowNum: 10,
        rowList: [10, 20, 30, 40, 50],
        height: '100%',
        //Weight: '110%',
        viewrecords: true,
        caption: 'Master YSID Suppliers',
        emptyrecords: 'No Suppliers Records are Available to Display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#jqControls', { edit: true, add: true, del: true, search: false, refresh: true },
        {
            zIndex: 100,
            url: '/Suppliers/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 100,
            url: "/Suppliers/Create",
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
            url: "/Suppliers/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete Supplier... ? ",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        });
});