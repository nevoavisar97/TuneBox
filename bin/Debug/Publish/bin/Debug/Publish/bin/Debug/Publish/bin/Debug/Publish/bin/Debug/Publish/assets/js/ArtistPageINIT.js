var url = new URLSearchParams(window.location.search);
var name = url.get('id');
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

	const api = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/Artists/GetArtist/" + name;
	ajaxCall("GET", api, "", successCB9, errorCB);


	const api1 = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/Song/GetSongsByArtistName/" + name;
	ajaxCall("GET", api1, "", successCB10, errorCB);
});


//initializing artist songs//
function successCB10(songs) {
	let heading = $('#ap-song-head');
	heading.text(url.get('id') + " Hits");
	const songList = $('#ap-song-t');
	for (let i = 0; i < songs.length; i++) {
		const tr = $('<tr>');
		const td = $('<td>');
		const a = $('<a>');
		a.text( songs[i].songName + " | Album - " + songs[i].albumName);
		a.attr("href", "songPage.html?id=" + songs[i].id);
		a.css("border-bottom", "none");
		a.css("color", "#ff5c78");
		td.append(a);
		tr.append(td);
		songList.append(tr);
	}
}



//initializing artist info
function successCB9(data) {
	if (data.tw_id != "") {
		let contain = $("#desk-twit");
		let contain2 = $("#mobile-twitter");
		let src = data.tw_id;
		let twitter1 = $('<a class="twitter-timeline" data-width="460" data-height="550" data-theme="dark"></a>');
		let twitter = $('<a class="twitter-timeline" data-width="460" data-height="550" data-theme="dark"></a>');
		twitter.attr('id', 'desk-twit');
		twitter.attr("src", src);
		contain.append(twitter);
		contain2.append(twitter1);
	}

	var artistImg = $('#ArtistImg');
	var name = $('#ap-name');
	var full = $('#ap-full');
	var description = $('#ap-desc');
	var twiiter = $('.twitter-timeline');
	if (data.tw_id != 0)
		twiiter.attr('href', 'https://twitter.com/' + data.tw_id + "?ref_src=twsrc%5Etfw");
	console.log(twiiter.attr('href'));
	
	name.text(data.stageName);
	// Change the src attribute
	artistImg.attr('src', data.imgUrl);

	if (data.fullName != "0") {
		full.text(data.fullName);
	}
	if (data.description != "0") {
		description.text(data.description);
	}

	var logged = sessionStorage['logged'];
	if (logged != null) {
		var jsonObject = JSON.parse(logged);
		var avatar = jsonObject.avatar;
		$('#sp-avatar').attr("src", avatar)

	}
	else $('#sp-avatar').attr("src", 'avatars/avatar5.png');
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