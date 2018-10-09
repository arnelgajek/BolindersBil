var formUtils = {
    files: [],
    buildPreview: function (dataUrl) {
        var html = ['<img src="' + dataUrl + '" class="img-thumbnail">'];
        var button = ['<a class="btn btn-warning btn-sm">Ta bort</a>'];
        var theDiv = ['<div class="imageThumbnailPreview">' + html + button + '</div>'];
        $('#filePreviews').append(theDiv);
    },
    removeImage: function (index) {
        formUtils.files.splice(index, 1);
        console.log(formUtils.files);
    }
};

$(document).ready(function () {

    // Sends the checked items value to the Vehicle controller EditVehicle action with the items value.
    // id="editButton" is in Admin.cshtml
    $('#editButton').on('click', function () {
        var checkedValue = $('.chk:checked').val();
        var url = '/Vehicle/EditVehicle?vehicleId=' + checkedValue;
        window.location.href = url;
    });
    
    $('#selectFilesBtn').click(function (e) {
        e.preventDefault();
        $('#images').trigger('click');
    });

    $(document).on('click', 'div.imageThumbnailPreview', function () {
        formUtils.removeImage($(this).index());
        $(this).remove();
    });

    $(document).on('change', '#usedCheckbox', function () {
        if ($(this).is(':checked')) {
            $(this).attr('value', 'true');
        } else {
            $(this).attr('value', 'false');
        }
    });

    $(document).on('change', '#leasableCheckbox', function () {
        if ($(this).is(':checked')) {
            $(this).attr('value', 'true');
        } else {
            $(this).attr('value', 'false');
        }
    });

    if ($('#editVehicleForm').length > 0) {

        $("#images").on("change", function (event) {
            formUtils.files = [];
            var files = event.originalEvent.target.files;
            $(files).each(function (i, file) {
                var reader = new FileReader();
                reader.addEventListener("load", function () {
                    formUtils.files.push(file);
                    var dataUrl = reader.result;
                    formUtils.buildPreview(dataUrl);
                }, false);
                reader.readAsDataURL(file);
            });
        });

        $('#editVehicleForm').submit(function (e) {
            e.preventDefault();
            $('#images').remove();
            var formData = new FormData();

            $('#editVehicleForm textarea').each(function () {
                formData.append($(this).attr('name'), $(this).val());
            });
            $('#editVehicleForm select').each(function () {
                formData.append($(this).attr('name'), $(this).val());
            });
            $('#editVehicleForm input').each(function () {
                formData.append($(this).attr('name'), $(this).val());
            });
            $(formUtils.files).each(function (i, file) {
                formData.append('images[' + i + ']', formUtils.files[i]);
            });

            $.ajax({
                url: $('#editVehicleForm').attr('action'),
                type: 'POST',
                data: formData,
                dataType: 'json',
                processData: false,
                contentType: false,
                success: function (data) {
                    console.log(data);
                    window.location.href = '/Vehicle/Admin';
                },
                error: function (error) {
                    console.log(error);
                }
            });

        });
    }


    
});