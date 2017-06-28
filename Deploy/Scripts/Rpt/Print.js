function ItemsListPrint() {
    window.open('/Products/PrintListOfItems');
}

function SupplierListPrint() {
    window.open('/Suppliers/PrintListOfSuppliers');
}

function CategoryListPrint() {
    window.open('/Categories/PrintListOfCategories');
}

function EmployeeListPrint() {
    window.open('/Employee/PrintListOfEmployees');
}

function RFFormPrint(documentTitle) {
    var id = $('#RequestID').val().trim();
    window.open('/RequestHeader/PrintRFForm/' + id);

    var documentTitle = $('#RequestNo').val().trim();
    document.title = documentTitle;
}