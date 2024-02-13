//initSongInfo();
function initSongInfo() {
    const api = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/Song/GetSongsNames";
    ajaxCall("GET", api, "", successCB, errorCB);
}
let k = 0;
let names1 = [];

function successCB(data) {
    names1 = data;
    getInfo();
}

function getInfo() {
    const settings = {
        async: true,
        crossDomain: true,
        url: 'https://genius-song-lyrics1.p.rapidapi.com/search/?q=' + names1[k],
        method: 'GET',
        headers: {
            'X-RapidAPI-Key': '8b91ebb566msh0e078e83a13d95bp18e2f0jsn9bb9ba2ba813',
            'X-RapidAPI-Host': 'genius-song-lyrics1.p.rapidapi.com'
        }
    };

    $.ajax(settings).done(function (response) {
        let id;
        if (response.hits.length > 0)
            id = response.hits[0].result.id;
        else {
            k++;
            getInfo();
        }

        const settings = {
            async: true,
            crossDomain: true,
            url: 'https://genius-song-lyrics1.p.rapidapi.com/song/details/?id=' + id,
            method: 'GET',
            headers: {
                'X-RapidAPI-Key': '8b91ebb566msh0e078e83a13d95bp18e2f0jsn9bb9ba2ba813',
                'X-RapidAPI-Host': 'genius-song-lyrics1.p.rapidapi.com'
            }
        };
      
   
        $.ajax(settings).done(function (response) {
            let song = response.song;
            let utb = " ";
            if (song.youtube_url != null) {
                utb = song.youtube_url;
            }

            let albumI = 0;
            let albumN = "";
            if (song.album != null) {
                albumI = song.album.id;
                albumN = song.album.name;
            }
            let date = "2001-01-01";
            if (song.release_date != null) {
                date = song.release_date;
            }

           

            let song1 = {
                artistName : "name",
                link : "link",
                text : "text",
                api_id: song.id,
                songName: names1[k].split(" by")[0],
                imgUrl: song.song_art_image_url,
                realeaseDate: date,
                artist_id: song.primary_artist.id,
                album_id: albumI,
                albumName: albumN,
                uTube: utb,
                appleM: song.apple_music_player_url,
            }

            console.log(song1);
            const api = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/Song/UpdateSong";
            ajaxCall("POST", api, JSON.stringify(song1), successCBb, errorCB);

        });
    });
}



function successCBb(data) {
    console.log("success");
    console.log(k);
    k++;
    if (k < 500)
        getInfo();
    else console.log("reached!");
}

function errorCB(data) {
    console.log("failed to connect to server");
}