"use strict";
let pollId = "4ER-8IK";
var connection = new signalR.HubConnectionBuilder().withUrl("/pollster").build();

//Disable the send button until connection is established.
document.querySelectorAll(".voteButton").forEach((elem) => elem.disabled = true);
connection.on("VoteUpdated", function (results) {
    update(results);
});

connection.start().then(function () {
    document.querySelectorAll(".voteButton").forEach((elem)=> elem.disabled = false);
}).catch(function (err) {
    return console.error(err.toString());
});
document.querySelectorAll(".voteButton").forEach(
    (elem, index) => {
        elem.addEventListener("click", function (event) {
            connection.invoke("Vote", pollId, index).catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();
        });
    }
);
function update(results) {
    for (let i = 0; i < results.length; i++) {
        let span = document.getElementById("option" + i + "score");
        span.innerText = results[i];
    }
}