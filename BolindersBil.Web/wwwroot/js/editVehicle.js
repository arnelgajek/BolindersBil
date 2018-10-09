$(document).ready(function () {
    
    $('#editButton').on('click', function () {
        var checkedValue = $('.chk:checked').val();
        var url = '/Vehicle/EditVehicle?vehicleId=' + checkedValue;
        window.location.href = url;
    });
    
});