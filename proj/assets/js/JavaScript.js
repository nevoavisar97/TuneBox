var coll = document.getElementsByClassName("collapsible");
var i;



$(document).ready(function () {
    ////initialize slider info
    const api = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/Song/GetNsongs/" + 9;
    ajaxCall("GET", api, "", successCB, errorCB);

    function successCB(data) {
        console.log(data);
        for (let i = 1; i <= data.length; i++) {
            let id = "#slide" + i;
            let img = $(id + " img");
            img.id = data[i - 1].id;
            img.attr("src", data[i - 1].imgUrl);
            img.click(function () {
                window.location.href = "songPage.html?id=" + img.id;
            });
            $(id+" h3").text(data[i-1].songName);
            $(id + " h5").text(data[i-1].artistName);

        }
    }
      ////initialize artist slider info
    const api1 = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/Artists/GetNartists/" + 9;
    ajaxCall("GET", api1, "", successCBa, errorCB);

    function successCBa(data) {
        console.log(data);
        for (let i = 0; i < 9; i++) {
            let j = i+10;
            let id = "#slide" + j;
            let img = $(id + " img");
            img.id = data[i].stageName;
            img.attr("src", data[i].imgUrl);
            img.click(function () {
                window.location.href = "ArtistPage.html?id=" + img.id;
            });
            $(id + " h3").text(data[i].stageName);
            if (data[i].fullName != " ")
                 $(id + " h5").text(data[i].fullName);

        }
    }


    for (i = 0; i < coll.length; i++) {
        coll[i].addEventListener("click", function () {
            this.classList.toggle("active");
            var content = this.nextElementSibling;
            if (content.style.display === "block") {
                content.style.display = "none";
            } else {
                content.style.display = "block";
            }
        });
    }

   


});

function errorCB(data) {
    console.log("failed to connect to server");
}

function initializeSlider(sliderContainer) {
    const sliderContent = sliderContainer.querySelector('.slider-content');
    const slides = sliderContainer.querySelectorAll('.slide');
    var slideWidthM = 0;
    var slideWidthD = 0;
    if (window.innerWidth <= 480) {
        slideWidthM = slides[0].offsetWidth;
    }
    else {
        slideWidthD = slides[0].offsetWidth - 10;
    }
    let currentIndex = 0;

    function slide(direction) {
        const nextIndex = currentIndex + direction;
        if (window.innerWidth <= 480) {
            // For mobile screens
            if (nextIndex >= 0 && nextIndex <= slides.length ) {
                currentIndex = nextIndex;
                sliderContent.style.transform = `translateX(-${currentIndex * slideWidthM}px)`;
            }
        } else {
            // For screens larger than 480px
            if (nextIndex >= 0 && nextIndex <= slides.length - 4) {
                currentIndex = nextIndex;
                sliderContent.style.transform = `translateX(-${currentIndex * slideWidthD}px)`;
            }
        }
        updateNavButtons();
    }


    function updateNavButtons() {
        const prevButton = sliderContainer.querySelector('.prev');
        const nextButton = sliderContainer.querySelector('.next');

        prevButton.disabled = currentIndex === 0;
        nextButton.disabled = currentIndex === slides.length - 1;
    }

    // Event listeners for navigation buttons
    const prevButton = sliderContainer.querySelector('.prev');
    const nextButton = sliderContainer.querySelector('.next');

    prevButton.addEventListener('click', () => slide(-1));
    nextButton.addEventListener('click', () => slide(1));

    // Call updateNavButtons initially
    updateNavButtons();
}

// Initialize sliders
const sliderContainers = document.querySelectorAll('.slider-container');
sliderContainers.forEach((sliderContainer) => {
    initializeSlider(sliderContainer);
});
