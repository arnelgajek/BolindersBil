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
            //console.log($(this).val());
        } else {
            $(this).attr('value', 'false');
            //console.log($(this).val());
        }
    });

    $(document).on('change', '#leasableCheckbox', function () {
        if ($(this).is(':checked')) {
            $(this).attr('value', 'true');
            //console.log($(this).val());
        } else {
            $(this).attr('value', 'false');
            //console.log($(this).val());
        }
    });

    if ($('#addNewVehicleForm').length > 0) {

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

        $('#addNewVehicleForm').submit(function (e) {
            e.preventDefault();
            $('#images').remove();
            var formData = new FormData();

            $('#addNewVehicleForm textarea').each(function () {
                formData.append($(this).attr('name'), $(this).val());
            });
            $('#addNewVehicleForm select').each(function () {
                formData.append($(this).attr('name'), $(this).val());
            });
            $('#addNewVehicleForm input').each(function () {
                formData.append($(this).attr('name'), $(this).val());
            });
            $(formUtils.files).each(function (i, file) {
                formData.append('images[' + i + ']', formUtils.files[i]);
            });

            $.ajax({
                url: $('#addNewVehicleForm').attr('action'),
                type: 'POST',
                data: formData,
                dataType: 'json',
                processData: false,
                contentType: false,
                success: function (data) {
                    console.log(data);
                    window.location.href = '/Vehicle/Admin';
                    console.log('Congrats! You have added ' + data + '.');
                },
                error: function (error) {
                    console.log(error);
                }
            });

        });
    }
});
function initMap() {
    let vmo = { lat: 57.1833107, lng: 14.048138 };
    let jkpg = { lat: 57.7841876, lng: 14.1624033 };
    let gbg = { lat: 57.7085714, lng: 11.9719767 };
    let centerMap = { lat: 57.4958893, lng: 13.0902289 };
    let map = new google.maps.Map(
        document.getElementById('Map'), { zoom: 7, center: centerMap }); 

    //let offices = [vmo, jkpg, gbg]; // Egen tid, få markörerna att droppa olika tid

    let VMOmarker = new google.maps.Marker({ position: vmo, map: map, title: 'Värnamo', animation: google.maps.Animation.DROP, draggable: true });
    let JKPGmarker = new google.maps.Marker({ position: jkpg, map: map, title: 'Jönköping', animation: google.maps.Animation.DROP, draggable: true });
    let GBGmarker = new google.maps.Marker({ position: gbg, map: map, title: 'Göteborg', animation: google.maps.Animation.DROP, draggable: true });

    let markers = [VMOmarker, JKPGmarker, GBGmarker];
    
    var VMOstring = '<div class="content">' +
        '<div class="siteNotice">' +
        '</div>' +
        '<h3 class="firstHeading">Bolinders bil Värnamo</h3>' +
        '<p><a href="https://goo.gl/maps/LVnPDnKNfeC2" target="_blank">Klicka här</a> för att öppna i Google maps</p>' +
        '</div>' +
        '</div>';

    var JKPGstring = '<div class="content">' +
        '<div class="siteNotice">' +
        '</div>' +
        '<h3 class="firstHeading">Bolinders bil Jönköping</h3>' +
        '<p><a href="https://goo.gl/maps/C7ACTUndTft" target="_blank">Klicka här</a> för att öppna i Google maps</p>' +
        '</div>' +
        '</div>';

    var GBGstring = '<div class="content">' +
        '<div class="siteNotice">' +
        '</div>' +
        '<h3 class="firstHeading">Bolinders bil Göteborg</h3>' +
        '<p><a href="https://goo.gl/maps/uRwdVnCYWsE2" target="_blank">Klicka här</a> för att öppna i Google maps</p>' +
        '</div>' +
        '</div>';
    

    var VMOinfowindow = new google.maps.InfoWindow({
        content: VMOstring
    });
    var JKPGinfowindow = new google.maps.InfoWindow({
        content: JKPGstring
    });
    var GBGinfowindow = new google.maps.InfoWindow({
        content: GBGstring
    });


    VMOmarker.addListener('click', () => {
        VMOinfowindow.open(map, VMOmarker);
    });
    JKPGmarker.addListener('click', () => {
        JKPGinfowindow.open(map, JKPGmarker);
    });
    GBGmarker.addListener('click', () => {
        GBGinfowindow.open(map, GBGmarker);
    });

}


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
$(function () {

    // Checks all the checkboxes for you:
    $('#checkAll').click(function () {

        $('input:checkbox').prop('checked',
            this.checked);
    });

    // Gets the value using class:
    $('#confirmVehicleDelete').click(function () {
        getValueUsingClass();
    });

    $('#confirmVehicleBulkDelete').click(function () {
        getValueUsingClass();
    });

    function getValueUsingClass() {
        
        // Declaring checkbox array:
        var chkArray = [];

        // Loop through all the classes with chk and see if they are checked:
        $('.chk:checked').each(function () {
            chkArray.push($(this).val());
        });

        // Join the array and separate the Ids with a comma:
        var selected;
        selected = chkArray.join(',');

        $('input[name="vehicleId"]').val(selected);
    }
});

$(function () {

    // Sets the carousel-item to active:
    $('.carousel-item').first().addClass('active');

    // Find the img in the carousel-item:
    var carouselElem = $('.carousel-item').find("img");
    console.log(carouselElem);

    // Find the id of the img and pass it on to the modal:
    $('#vehicleModal').on('show.bs.modal', function (e) {
        var activeElement = $('div.carousel-inner').find('.active');
        console.log(activeElement);

        var index = activeElement.index();
        console.log(index);

        var theSrc = $('.thumbs img').eq(index).attr('src');

        // Find the id from .thumbs img and use it to the modalCarousel:
         $("#modalCarousel").find("img").attr("src", theSrc);
    });
});
$(() => {
    console.log('it works now');
    
});