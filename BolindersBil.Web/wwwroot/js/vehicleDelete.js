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
<<<<<<< HEAD

=======
    
>>>>>>> 1085564e3313107e558b27c5e4d9704ac9601d4e
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
