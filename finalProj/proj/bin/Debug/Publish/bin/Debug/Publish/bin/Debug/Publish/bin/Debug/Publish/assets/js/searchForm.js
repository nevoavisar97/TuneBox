$(document).ready(function () {
    const searchForm = document.getElementById('search-form');
    const queryInput = document.getElementById('query');
    const searchSubmitButton = document.getElementById('searchSubmit');
    var query;
    // Function to handle search submission
    function submitSearch() {
        const category = document.getElementById('search-category').value;
        query = queryInput.value;

        if (category === 'artist') {
            const api = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/Song/SearchSongsByArtist/" + query;
            ajaxCall("GET", api, "", successCBa, errorCB);
        }

        else if (category === 'song') {
            const api = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/Song/SearchSongs/" + query;
            ajaxCall("GET", api, "", successCB, errorCB);
        }
        function errorCB(err) {
            alert("Connection to server failed");
        }

        //search by song success
        function successCB(data) {
            sessionStorage['searchRes'] = JSON.stringify(data);
            sessionStorage['searchWord'] = query;
            window.location.href = 'search-result.html';
        }

        //search by artist success
        function successCBa(data) {
            sessionStorage['searchRes'] = JSON.stringify(data);
            sessionStorage['searchWord'] = query;

            window.location.href = 'search-result.html';

        }

        queryInput.value = '';

        console.log('Search:', category, query);
    }

    // Event listener for form submission
    searchForm.addEventListener('submit', function (event) {
        event.preventDefault(); // Prevent form submission
        submitSearch();
    });

    // Event listener for search submit button click
    searchSubmitButton.addEventListener('click', function () {
        submitSearch();
    });


    // Function to perform the search on mobile





    const queryInput1 = document.getElementById('query1');
    var query1;
    // Function to handle search submission
    function submitSearch1() {
        const category = document.getElementById('search-category1').value;
        query1 = queryInput1.value;

        if (category === 'artist') {
            const api = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/Song/SearchSongsByArtist/" + query1;
            ajaxCall("GET", api, "", successCBa, errorCB);
        }

        else if (category === 'song') {
            const api = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/Song/SearchSongs/" + query1;
            ajaxCall("GET", api, "", successCB, errorCB);
        }
        function errorCB(err) {
            alert("Connection to server failed");
        }

        //search by song success
        function successCB(data) {
            sessionStorage['searchRes'] = JSON.stringify(data);
            sessionStorage['searchWord'] = query1;
            window.location.href = 'search-result.html';
        }

        //search by artist success
        function successCBa(data) {
            sessionStorage['searchRes'] = JSON.stringify(data);
            sessionStorage['searchWord'] = query1;

            window.location.href = 'search-result.html';

        }

        queryInput.value = '';

        return false;
    }

    // Event listener for form submission
    $('#search-form1').submit(submitSearch1);
    $("#searchSubmit1").click(submitSearch1)


    // Function to perform the search
 

});
