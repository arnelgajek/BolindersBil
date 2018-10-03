$(function () {

    // Checks all the checkboxes for you:
    $('#checkAll').click(function () {
        // TODO: If the checkAll checked, enable button #bulkDeleteVehicle:

        $('input:checkbox').prop('checked',
            this.checked);
   });

    // Gets the value using class:
    $('#testTest').click(function () {
        getValueUsingClass();
    });

    $('#bulktest').click(function (e) {
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
