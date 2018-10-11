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

         $("#modalCarousel").find("img").attr("src", theSrc);
    });
});