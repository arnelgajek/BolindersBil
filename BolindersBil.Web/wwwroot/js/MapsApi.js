function initMap() {
    let vmo = { lat: 57.1833107, lng: 14.048138 };
    let jkpg = { lat: 57.7841876, lng: 14.1624033 };
    let gbg = { lat: 57.7085714, lng: 11.9719767 };
    let centerMap = { lat: 57.4958893, lng: 13.0902289 };
    let map = new google.maps.Map(
        document.getElementById('Map'), { zoom: 7, center: centerMap }); //lista ut hur man får flera markers på samma karta
    let VMOmarker = new google.maps.Marker({ position: vmo, map: map, title: 'Värnamo', animation: google.maps.Animation.DROP });
    let JKPGmarker = new google.maps.Marker({ position: jkpg, map: map, title: 'Jönköping', animation: google.maps.Animation.DROP });
    let GBGmarker = new google.maps.Marker({ position: gbg, map: map, title: 'Göteborg', animation: google.maps.Animation.DROP });


    var VMOstring = '<div id="content">' +
        '<div id="siteNotice">' +
        '</div>' +
        '<h3 id="firstHeading" class="firstHeading">Bolinders bil Värnamo</h3>' +
        '<p><a href="https://goo.gl/maps/LVnPDnKNfeC2" target="_blank">Klicka här<a/> för att öppna i Google maps<p/>' +
        '</div>' +
        '</div>';

    var JKPGstring = '<div id="content">' +
        '<div id="siteNotice">' +
        '</div>' +
        '<h3 id="firstHeading" class="firstHeading">Bolinders bil Jönköping</h3>' +
        '<p><a href="https://goo.gl/maps/C7ACTUndTft" target="_blank">Klicka här<a/> för att öppna i Google maps<p/>' +
        '</div>' +
        '</div>';

    var GBGstring = '<div id="content">' +
        '<div id="siteNotice">' +
        '</div>' +
        '<h3 id="firstHeading" class="firstHeading">Bolinders bil Göteborg</h3>' +
        '<p><a href="https://goo.gl/maps/uRwdVnCYWsE2" target="_blank">Klicka här<a/> för att öppna i Google maps<p/>' +
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
