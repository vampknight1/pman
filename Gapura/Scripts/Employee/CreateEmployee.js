$(function () {
    $('#TitleOfCourtesy').focus();
    $('#BirthDate').datepicker({
        dateFormat: 'yy-mm-dd',
        minYear: -16,
        maxDate: 0,
        changeMonth: true,
        changeYear: true
        //appendText: ' YYYY-MM-DD',
        //showOn: 'both'        //for button
    });

    $('#HireDate').datepicker({
        dateFormat: 'yy-mm-dd',
        maxDate: 0,
        changeMonth: true,
        changeYear: true
        //appendText: ' YYYY-MM-DD',
        //showOn: 'both'        //for button
    });
});

$(document).ready(function () {
    $('#Upload').click(function () {
        var formData = new FormData();
        var totalFiles = document.getElementById('FileUpload').files.length;
        for (var i = 0; i < totalFiles; i++) {
            var file = document.getElementById('FileUpload').files[i];

            formData.append('FileUpload', file);
        }
        $.ajax({
            type: 'POST',
            url: '/Employee/Upload',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (response) {
                alert('Upload Photo Succes!!');
            },
            error: function (error) {
                alert('Upload Process Error !');
            }
        });
    });
});