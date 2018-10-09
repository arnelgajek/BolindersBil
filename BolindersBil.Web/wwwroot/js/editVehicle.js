$(document).ready(function () {

    // Sends the checked items value to the Vehicle controller EditVehicle action with the items value.
    $('#editButton').on('click', function () {
        var checkedValue = $('.chk:checked').val();
        var url = '/Vehicle/EditVehicle?vehicleId=' + checkedValue;
        window.location.href = url;
    });
    
});