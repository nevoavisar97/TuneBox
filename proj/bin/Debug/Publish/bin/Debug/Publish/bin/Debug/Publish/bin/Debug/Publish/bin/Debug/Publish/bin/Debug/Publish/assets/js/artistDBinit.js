function initArtist() {
    const api = "https://localhost:7248/api/Artists";
    ajaxCall("GET", api, "", successCB, errorCB);
}
let j = 0;
let names = [];

function successCB(data) {
    names = data;
    getInfo();
}
function getInfo() {

    const settings = {
        async: true,
        crossDomain: true,
        url: 'https://genius-song-lyrics1.p.rapidapi.com/search/?q='+names[j],
        method: 'GET',
        headers: {
            'X-RapidAPI-Key': '8b91ebb566msh0e078e83a13d95bp18e2f0jsn9bb9ba2ba813',
            'X-RapidAPI-Host': 'genius-song-lyrics1.p.rapidapi.com'
        }
    };

    $.ajax(settings).done(function (response) {
        let id = response.hits[0].result.primary_artist.id;
        const settings = {
            async: true,
            crossDomain: true,
            url: 'https://genius-song-lyrics1.p.rapidapi.com/artist/details/?id='+id,
            method: 'GET',
            headers: {
                'X-RapidAPI-Key': '8b91ebb566msh0e078e83a13d95bp18e2f0jsn9bb9ba2ba813',
                'X-RapidAPI-Host': 'genius-song-lyrics1.p.rapidapi.com'
            }
        };

        $.ajax(settings).done(function (response) {
            console.log(response);
            let artist = response.artist;
            let alt_name = " ";
            if (artist.alternate_names.length != 0) {
                alt_name = artist.alternate_names[0];
            }
            let fb = " ";
            if (artist.facebook_name != null) {
                fb = artist.facebook_name;
            }
             let ig = " ";
            if (artist.instagram_name != null) {
                ig = artist.instagram_name;
            }
              let tw = " ";
            if (artist.twitter_name != null) {
                tw = artist.twitter_name;
            }


            let artist1 = {
                stageName: artist.name,
                fullName: alt_name,
                description: artist.description_preview,
                imgUrl: artist.image_url,
                ig_id: ig,
                fb_id: fb,
                tw_id: tw,
            }
            const api = "https://localhost:7248/api/Artists";
            ajaxCall("POST", api, JSON.stringify(artist1), successCBb, errorCB);

        });
    });
}

function successCBb(data) {
    console.log("success");
    console.log(j);
    j++;
    if (j < 93)
        getInfo();
    else console.log("reached!");
}

function errorCB(data) {
    console.log("failed to connect to server");
}