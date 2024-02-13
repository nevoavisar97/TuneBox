$(document).ready(function () {


//lyrics to sentence arr
function textToSentences(text) {
    // Split the text into an array of sentences using "\r\n" as the delimiter
    const sentences = text.split('\r\n');

    // Filter out any empty strings from the array
    const filteredSentences = sentences.filter(sentence => sentence.trim() !== '');

    return filterArray(filteredSentences);
}



function filterArray(arr) {
    const filteredArray = [];
    for (const item of arr) {
        if (!item.includes('[') && !item.includes(']'))
            if(item.length > 25) 
             filteredArray.push(item);
    }
    return filteredArray;
}





// pick random questions from the JSON 
function getRandomQuestions(data, num) {
    // Shuffle  questions 
    const shuffledQuestions = data.sort(() => Math.random() - 0.5);
    // Return  questions from the shuffled array
    return shuffledQuestions.slice(0, num);
}

// Number of questions
const numQuestionsToPick = 7;

// Get 7 random questions
 quiz = getRandomQuestions(questions, numQuestionsToPick);

// Display the selected questions
console.log(quiz);


//generate rand int between 0 to 24, this will be the correct song
function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
} 


//initialize 25 random songs 
const api = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/Quiz/" + 150;
ajaxCall("GET", api, "", successCB, errorCB);

var readyQuiz;
function successCB(data) {
    console.log(data);
    readyQuiz = initQuiz(data);

}

function initQuiz(quizData) {
    let ready = [];
    let options = [];
    let data;
    let question;
    let answers;
    let questionObj;
    for (let i = 0; i < quiz.length; i++) {
        switch (quiz[i].type) {
            case "song-artist":
                options = [];

                data = selectFour(quizData);
                console.log(data);
                //setting data[0] to be the song we ask about;
                question = quiz[i].question + " " + data[0].songName + "?";
                options[0] = data[1].artistName;
                options[1] = data[2].artistName;
                options[2] = data[3].artistName;
                //correct ans
                options[3] = data[0].artistName;

                questionObj = {
                    "question": question,
                    "options": options,
                    "type": "none"
                };
                ready.push(questionObj);
                break;

            case "fullname-artist":
                data = selectFour(quizData);
                options = [];

                //artistName == stagename
                //setting data[0] to be the song we ask about;
                question = quiz[i].question + " " + data[0].fullName + "?";
                options[0] = data[1].artistName;
                options[1] = data[2].artistName;
                options[2] = data[3].artistName;
                //correct ans
                options[3] = data[0].artistName;

                questionObj = {
                    "question": question,
                    "options": options,
                    "type": "none"
                };
                ready.push(questionObj);
                break;

            case "img-artist":
                // Handle the "img-artist" type question
                data = selectFour(quizData);
                options = [];

                //artistName == stagename
                //setting data[0] to be the song we ask about;
                question = quiz[i].question;
                options[0] = data[1].artistName;
                options[1] = data[2].artistName;
                options[2] = data[3].artistName;
                //correct ans
                options[3] = data[0].artistName;

                questionObj = {
                    "question": question,
                    "options": options,
                    "type": "img-artist",
                    "img": data[0].artistImg
                };
                ready.push(questionObj);
                break;

            case "song-album":
                // Handle the "song-album" type question
                data = selectFour(quizData);
                options = [];

                //setting data[0] to be the song we ask about;
                question = quiz[i].question + " " + data[0].songName + "?";
                options[0] = data[1].albumName;
                options[1] = data[2].albumName;
                options[2] = data[3].albumName;
                //correct ans
                options[3] = data[0].albumName;

                questionObj = {
                    "question": question,
                    "options": options,
                    "type": "none"
                };
                ready.push(questionObj);
                break;

            case "song-lyrics":
                // Handle the "song-lyrics" type question
                data = selectFour(quizData);
                options = [];

                //setting data[0] to be the song we ask about;
                question = quiz[i].question + " " + data[0].songName + "?";
                answers = textToSentences(data[1].text);
                options[0] = answers[getRandomInt(1, answers.length - 1)];
                answers = textToSentences(data[2].text);
                options[1] = answers[getRandomInt(1, answers.length - 1)];
                answers = textToSentences(data[3].text);
                options[2] = answers[getRandomInt(1, answers.length - 1)];
                //correct ans
                answers = textToSentences(data[0].text);
                options[3] = answers[getRandomInt(1, answers.length - 1)];

                questionObj = {
                    "question": question,
                    "options": options,
                    "type": "none"
                };
                ready.push(questionObj);
                break;

            case "song-year":
                // Handle the "song-year" type question
                data = selectFour(quizData);
               
                //setting data[0] to be the song we ask about;
                question = quiz[i].question + " " + data[0].songName + " released?";
                options = [];

                options[0] = data[1].releaseYear;
                options[1] = data[2].releaseYear;
                options[2] = data[3].releaseYear;
                //correct ans
                options[3] = data[0].releaseYear;

                questionObj = {
                    "question": question,
                    "options": options,
                    "type": "none"
                };
                ready.push(questionObj);
                break;

            case "song-img":
                // Handle the "song-img" type question
                data = selectFour(quizData);
               
                //setting data[0] to be the song we ask about;
                question = quiz[i].question + " " + data[0].songName + "?";
                options = [];
                options[0] = data[1].imgUrl;
                options[1] = data[2].imgUrl;
                options[2] = data[3].imgUrl;
                //correct ans
                options[3] = data[0].imgUrl;

                questionObj = {
                    "question": question,
                    "options": options,
                    "type": "song-img"
                };
                ready.push(questionObj);
                break;

            case "album-artist":
                // Handle the "album-stagename" type question
                data = selectFour(quizData);
                options = [];

                //setting data[0] to be the song we ask about;
                question = quiz[i].question + " " + data[0].albumName + "?";
                options[0] = data[1].artistName;
                options[1] = data[2].artistName;
                options[2] = data[3].artistName;
                //correct ans
                options[3] = data[0].artistName;

                questionObj = {
                    "question": question,
                    "options": options,
                    "type": "none"
                };
                ready.push(questionObj);
                break;

            //case "word-song":
            //    // Handle the "word-song" type question
            //    data = selectFour(quizData);
            //
            //    //setting data[0] to be the song we ask about;
            //    question = quiz[i].question + " " + data[0].albumName + "?";
            //    options[0] = data[1].artistName;
            //    options[1] = data[2].artistName;
            //    options[2] = data[3].artistName;
            //    //correct ans
            //    options[3] = data[0].artistName;

            //    break;

            case "lyric-song":
                // Handle the "lyric-song" type question
                data = selectFour(quizData);
                options = [];

                //setting data[0] to be the song we ask about;
                answers = textToSentences(data[0].text);
                question = quiz[i].question + '"...' + answers[getRandomInt(1, answers.length - 1)] + '..."?';
                options[0] = data[1].songName;
                options[1] = data[2].songName;
                options[2] = data[3].songName;
                //correct ans
                options[3] = data[0].songName;

                questionObj = {
                    "question": question,
                    "options": options,
                    "type": "none"
                };
                ready.push(questionObj);
                break;

            case "artist-album":
                // Handle the "artist-album" type question
                data = selectFour(quizData);
                options = [];

                //setting data[0] to be the song we ask about;
                question = quiz[i].question + " " + data[0].artistName + "?";
                options[0] = data[1].albumName;
                options[1] = data[2].albumName;
                options[2] = data[3].albumName;
                //correct ans
                options[3] = data[0].albumName;

                questionObj = {
                    "question": question,
                    "options": options,
                    "type": "none"
                };
                ready.push(questionObj);
                break;

            case "artist-song":
                // Handle the "artist-song" type question
                data = selectFour(quizData);
                options = [];

                //setting data[0] to be the song we ask about;
                question = quiz[i].question + " " + data[0].artistName;
                options[0] = data[1].songName;
                options[1] = data[2].songName;
                options[2] = data[3].songName;
                //correct ans
                options[3] = data[0].songName;

                questionObj = {
                    "question": question,
                    "options": options,
                    "type": "none"
                };
                ready.push(questionObj);
                break;

            default:
                // Handle the case when the questionType is not recognized
                break;
        }
    }
    return ready;

}

// Shuffle an array using the Fisher-Yates shuffle algorithm
function shuffleArray(array) {
    for (let i = array.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [array[i], array[j]] = [array[j], array[i]];
    }
    return array;
}


function selectFour(quizData) {
    // Shuffle the quizData array using the sort method
    const shuffledData = shuffleArray(quizData);

    const selectedSongs = [];
    const selectedArtists = new Set();

    for (const song of shuffledData) {
        const artist = song.artistName;

        if (!selectedArtists.has(artist)) {
            selectedSongs.push(song);
            selectedArtists.add(artist);
        }
        if (selectedArtists.size === 4) {
            break;
            // Stop when we have 4 different artists - this will assure different values (album names..)
        }
    }
    return selectedSongs;
}

var currentQuestionIndex = 0;

//func to get leaderboard
function getLeaderBoard() {
    //initialize leaderboard
    $('#leaderboard').empty();
    const api = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/LeaderBoard/GetL";
    ajaxCall("GET", api, "", successCB1, errorCB);
}

function successCB1(data) {
    let container = $('#leaderboard');
    for (let i = 0; i < data.length; i++) {

        let tr = $('<tr>');
        let td = $('<td>');
        let a = $('<p>');
        a.css('display', 'flex');
        a.css('align-items', 'center');
        let num = i + 1 + ".  ";
        let name = data[i].nickName;
        let img = $('<img>');
        img.attr("src", data[i].avatar);
        img.css("height", "50px");
        img.css("margin-left", "25px");
        img.css("margin-right", "10px");
        img.css("border-radius", "5px");
        a.append(num);
        a.append(img);
        a.append(name + " | ");
        a.append("Score: " + data[i].score);
        a.css("color", "#ff5c78");
        td.append(a);
        tr.append(td);
        container.append(tr);
    }
}



    $('#logIn-form2').submit(login)
    function login() {
        var logged = sessionStorage['logged'];
        if (logged != null) {
            alert("You are currently logged in to your account");
        }
        else {
            const api = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/Users/UserLogIn/" + $('#email4').val() + "/" + $('#password4').val();
            //const api = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/User/UserLogIn/" + val[0] + "%40" + val[1] + "/" + $('#password').val();
            ajaxCall("POST", api, { email: $('#email4').val(), password: $('#password4').val() }, successCBh, errorCB);
            return false;
        }
    }
    function successCBh(user) {
        sessionStorage['logged'] = JSON.stringify(user);
        alert("connected!")
        location.reload();

    }

    function errorCB(err) {
        if (err.status === 500) {
            alert("Error: " + err.responseText);
        } else {
            alert("Sorry, we could not complete your request because the server connection might have been lost or the data was undefined.");
        }
    }



    getLeaderBoard();
    let score = 0;
    let timerInterval;

    $("#start-btn").click(function () {
        if (sessionStorage['logged'] != null)
            startQuiz();
        else {
            $('#closeLoginPopup').on('click', function () {
                $('#loginPopup').css('display', 'none');
            });
            $('#loginPopup').on('click', function (event) {
                if (event.target === this) {
                    $('#loginPopup').css('display', 'none');
                }
            });

            $('#loginPopup').css('display', 'block');
        }
    });

    $("#quiz").submit(submitAns);

    function startQuiz() {
        $("#start-btn").hide();
        $("#timer-area").show();
        $("#start-btn").prop("disabled", true);
        $("#question-container").show();
        $("#score").text("Score: 0");
        currentQuestionIndex = 0;
        score = 0;
        userAnswer = null;
        showQuestion();
        startTimer(30);
    }

    function showQuestion() {
        if (currentQuestionIndex < readyQuiz.length) {
            const question = readyQuiz[currentQuestionIndex].question;
            const options1 = readyQuiz[currentQuestionIndex].options.slice(); // slice to prevent changes on origin arr
            const options = shuffleArray(options1);
            if (readyQuiz[currentQuestionIndex].type == "img-artist") {
                $("#to-show").show();
                $("#questionHead").append(question);
                $('#quiz-img').attr('src', readyQuiz[currentQuestionIndex].img);
            }
            $("#questionHead").text(question);
            $("#options").empty();
            
            //if (readyQuiz[currentQuestionIndex].type == 'img-song')
            for (let i = 0; i < options.length; i++) {
                if (readyQuiz[currentQuestionIndex].type == "song-img") {
                    let img = $('<img>');
                    img.attr("src", options[i]);
                    $("#label" + i).html(" ");
                    $("#label" + i).append(img);
                }
                else
                  $("#label" + i).text(options[i]);
                $("#opt" + i).val(options[i]);
            }  
        }
        else {
            endQuiz();
        }
    }

    function submitAns() {
        var userAns = $("input[name='opt']:checked").val();
        if (userAns) 
            checkAnswer(userAns);
        else alert("You must choose an answer before submitting");
        return false;
    }

    function checkAnswer(selectedOption) {
        const timeLeft = parseInt($("#timer").text().split(" ")[2]);
        clearInterval(timerInterval);
        const correctAnswer = readyQuiz[currentQuestionIndex].options[3];
        console.log(correctAnswer);
        if (selectedOption === correctAnswer) {
            const points = Math.max(0, 50 - (30 - timeLeft));
            score += points;
            $("#score").text(`Score: ${score}`);
        }
        userAnswer = null;
        currentQuestionIndex++;
        showQuestion();
        $("#to-show").hide();
        $("#quiz")[0].reset();
        startTimer(30);
    }

    function startTimer(seconds) {
        let timeLeft = seconds;
        $("#timer").text(`Time Left: ${timeLeft}`);

        timerInterval = setInterval(function () {
            timeLeft--;
            $("#timer").text(`Time Left: ${timeLeft}`);
            if (timeLeft === 0) {
                clearInterval(timerInterval);
                currentQuestionIndex++;
                userAnswer = null;
                showQuestion();
                startTimer(30);
            }
        }, 1000);
    }

    function endQuiz() {
        $("#start-btn").prop("disabled", false);
        $("#question-container").hide();
        $("#timer").text("Time Left: 30");
        alert(`Quiz Finished!\nYour final score: ${score}`);
        const leaderBoard = {
            nickName: JSON.parse(sessionStorage['logged']).userName,
            avatar: JSON.parse(sessionStorage['logged']).avatar,
            score: score
        };
        const api1 = "https://proj.ruppin.ac.il/cgroup40/test2/tar1/api/LeaderBoard";
        $.ajax({
            type: "POST",
            url: api1,
            data: JSON.stringify(leaderBoard),
            contentType: "application/json",
            dataType: "json",
            success: successCB20,
            error: errorCB
        });
    }



});

function successCB20(data) {
    console.log(data);
    if (data == true)
        alert("Congrats, your score has been updated on our leader board, the top 100 players will be displayed on game page!");
    else alert("Great effort - you can improve your score on our leader board anytime!");
    location.reload();
}
function errorCB(err) {
    alert("Sorry, we could not complete your request because the server connection might have been lost or the data was undefined.");
}
