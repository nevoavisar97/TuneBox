var url = new URLSearchParams(window.location.search);
var songId = url.get('id');
var faved = false;



$(document).ready(function () {
	if (sessionStorage["logged"] == null) { 
	$('#closeLoginPopup').on('click', function () {
		$('#loginPopup').css('display', 'none');
	});
		$('#loginPopup').on('click', function (event) {
			if (event.target === this) {
				$('#loginPopup').css('display', 'none');
			}
		});
	}


	const api = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/Song/GetSongById/" + songId;
	ajaxCall("GET", api, "", successCB2, errorCB2);


});

function successCB2(data) {

	const apiKey = "AIzaSyAHvjEzCFX9gAgKpTeNKGzCYDkLeoeifhU";
	const searchTerm = data.songName + " " + data.artistName; // Replace this with the term you want to search for on YouTube

		const apiUrl = `https://www.googleapis.com/youtube/v3/search?part=snippet&q=${searchTerm}&key=${apiKey}`;

		fetch(apiUrl)
			.then(response => response.json())
			.then(data => {

				let id = (data.items[0].id.videoId);
				const apiUrl = `https://www.googleapis.com/youtube/v3/videos?part=player&id=${id}&key=${apiKey}`;

				fetch(apiUrl)
					.then(response => response.json())
					.then(data => {
						console.log(data);
						// Extract the embed URL from the API response
						const embedUrl = data.items[0].player.embedHtml;
						const embedUrl2 = data.items[0].player.embedHtml;
						console.log(embedUrl);

						// Now you can use 'embedUrl' to embed the video on your page
						$('#iframe-video').html(embedUrl);
						$('#mobile-utube').html(embedUrl);
						$('#iframe-video').css("height", "215px !important");
						$('#mobile-utube').css("width", "50% !important");
					})
					.catch(error => {
						console.error('Error fetching data:', error);
					});
			})
			.catch(error => {
				// Handle errors here
				console.error('Error fetching data:', error);
			});
	
	const settings = {
		async: true,
		crossDomain: true,
		url: 'https://genius-song-lyrics1.p.rapidapi.com/song/details/?id='+data.api_id,
		method: 'GET',
		headers: {
			'X-RapidAPI-Key': 'fe9b11269dmsh333bf54c01c3437p117808jsn6a3267eb09ba',
			'X-RapidAPI-Host': 'genius-song-lyrics1.p.rapidapi.com'
		}
	};

	$.ajax(settings).done(function (response) {
		if (response.song.description_preview != "")
			$("#sp-desc").text(response.song.description_preview);
		else $("#sp-desc").text(data.songName + " by " + data.artistName+ " Released at: " + data.realeaseDate.split("T")[0]);
	});
	
	var songImg = $('#songImg');
	var appleFrame = $('#apple');
	var name = $('#sp-name');
	var artist = $('#sp-artist');
	var album = $('#albumName');
	var date = $('#releaseDate');
	var lyrics = $("#lyrics");
	let text =  data.text.replace(/\r\n/, "<br>");

	lyrics.html(text);
	name.text(data.songName);
	artist.text("By " + data.artistName);
	artist.attr("href", "ArtistPage.html?id=" + data.artistName);
	date.text(data.realeaseDate.split('T')[0]);
	// Change the src attribute
	songImg.attr('src', data.imgUrl);
	if (data.appleM != 0) {
		appleFrame.attr('src', data.appleM)
	}
	else appleFrame.css("display", "none");
	
	if (data.albumName != "0") {
		album.text(data.albumName);
	}
	else $("#al-name").css("display", "none");

	var logged = sessionStorage['logged'];
	if (logged != null) {
		var jsonObject = JSON.parse(logged);
		var avatar = jsonObject.avatar;
		$('#sp-avatar').attr("src", avatar)

		user = JSON.parse(sessionStorage['logged']).id;
		const api = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/Users/CheckUserFavorite/" + user + "/" + songId;
		ajaxCall("GET", api, "", successCBd, errorCB);
	}
	else $('#sp-avatar').attr("src", 'avatars/avatar5.png');
}
function errorCB2(err) {
	alert("Connection to server failed");
}

function successCBd(data) {
	if (data) {
		faved = true;
		$('#sp-fav').attr('src', 'images/heartFull.png');
	}
	else {
		$('#sp-fav').attr('src', 'images/heart.png');
	}
}

///////hover///////

function heartHover() {
    if (faved == false)
        $('#sp-fav').attr('src', 'images/heartFull.png')
} function heartOut() {
    if (faved == false)
        $('#sp-fav').attr('src', 'images/heart.png')
}

/////heart clicked//////////
function favorite() {
	// Hide the login popup when the close button is clicked

	if (sessionStorage['logged'] != null) {
		let user = JSON.parse(sessionStorage['logged']).id;
		//add to favorites
		if (faved == false) {
			$('#sp-fav').attr('src', 'images/heartFull.png');
			faved = true;
		}
		//remove from favorites
		else {
			$('#sp-fav').attr('src', 'images/heart.png');
			faved = false;
		}
		//update db
		const api = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/Users/AddRemoveFav/" + user + "/" + songId;
		ajaxCall("POST", api, "", successCBc, errorCB);
	}
	else {
			event.preventDefault(); // Prevent form submission
			$('#loginPopup').css('display', 'block');

    }
}


function successCBc(data) {
    console.log(data + ' success');
}

function errorCB(err) {
    alert("Could not complete your request, Server might be down");
}


function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}
function logOut() {
    sessionStorage.clear();
    location.reload();
}



// Close the dropdown if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('.dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}