"use strict";
let pollId = "4ER-8IK";
var connection = new signalR.HubConnectionBuilder().withUrl("/pollster").build();

//Disable the send button until connection is established.
//and hook up to the button click event
document.querySelectorAll(".voteButton").forEach(
    (elem, index) => {
        elem.disabled = true;
        elem.addEventListener("click", function () {
            connection.invoke("Vote", pollId, index).catch(function (err) {
                return console.error(err.toString());
            });
        });
    }
);

//HOOK UP SIGNALS FROM SERVER TO LOCAL METHODS:
//updates UI after votes from any client
connection.on("voteUpdated", updateResults);
//is retrieved when page is first opened, to get current poll results
connection.on("receivePollInfo", receivePollInfo);

//retrieve current poll status from server when connection is up
connection.start().then(function () {
    getUpdatedPollInfo();
    //and enable all vote buttons
    document.querySelectorAll(".voteButton").forEach((elem) => elem.disabled = false);
}).catch(function (err) {
    return console.error(err.toString());
});

function getUpdatedPollInfo() {
    connection.invoke("GetPollInfo", pollId).catch(function (err) {
        alert("Error retrieving updated poll info");
    });
}

function updateResults(results) {
    for (let i = 0; i < results.length; i++) {
        let span = document.getElementById("option" + i + "score");
        span.innerText = results[i];
    }
}

function receivePollInfo(poll) { updateResults(poll.results); }