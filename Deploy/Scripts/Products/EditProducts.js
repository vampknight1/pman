$(document).ready(function () {

    $('#UnitPrice').live('keydown', currenciesOnly).live('blur', function () { $(this).formatCurrency(); });

    //$('#UnitPrice').removeAttr('data-val-number');

    $('#Upload').click(function () {
        var formData = new FormData();
        var totalFiles = document.getElementById('FileUpload').files.length;
        for (var i = 0; i < totalFiles; i++) {
            var file = document.getElementById('FileUpload').files[i];

            formData.append('FileUpload', file);
        }

        $.ajax({
            type: 'POST',
            url: '/Products/Upload',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (response) {
                alert('Upload Photo Succes!!');
            },
            error: function (error) {
                alert('Upload Process Errror !');
            }
        });
    });
});