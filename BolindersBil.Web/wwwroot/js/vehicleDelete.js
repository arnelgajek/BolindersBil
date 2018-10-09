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

    //$('#confirmVehicleEdit').click(function () {
    //    getValueUsingClass();
    //});

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
