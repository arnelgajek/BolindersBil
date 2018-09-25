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

    VMOmarker.addListener('click', () => {
        window.open('https://goo.gl/maps/eZsFk7WEPhN2');
    });
    JKPGmarker.addListener('click', () => {
        window.open('https://goo.gl/maps/SXjBctWRvZR2');
    });
    GBGmarker.addListener('click', () => {
        window.open('https://goo.gl/maps/zxMHPXRp9QQ2');
    });

}
