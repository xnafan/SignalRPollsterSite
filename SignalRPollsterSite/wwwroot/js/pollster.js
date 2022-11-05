"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/pollster").build();

//HOOK UP SIGNALS FROM SERVER TO LOCAL METHODS:
//updates UI after votes from any client
connection.on("voteUpdated", updateResults);
//is retrieved when page is first opened, to get current poll results
connection.on("receivePollInfo", receivePollInfo);

//retrieve current poll status from server when connection is up
connection.start().then(function () {
    joinGroup(pollId);
    getUpdatedPollInfo();
}).catch(function (err) {
    return console.error(err.toString());
});

function joinGroup(pollId) {
    //alert("joingroup, pollId:" + pollId)
    connection.invoke("JoinPoll", pollId).catch(function (err) {
        alert("Error joining polling group on startup");
    });
}

function getUpdatedPollInfo() {
    connection.invoke("GetPollInfo", pollId).catch(function (err) {
        alert("Error retrieving updated poll info");
    });
}

function updateResults(results) {
    for (let i = 0; i < results.length; i++) {
        let span = document.getElementById("choice" + i + "VoteCount");
        span.innerText = results[i];
    }
}

function receivePollInfo(poll) {
    createPollButtons(poll);
    updateResults(poll.results);
}

function createPollButtons(poll) {
    let pollTitleDiv = document.querySelector("h1#title");
    pollTitleDiv.innerText = poll.title;
    let pollDescriptionDiv = document.querySelector("div#description");
    pollDescriptionDiv.innerText = poll.description;
    let choiceNumber = 0;
    let pollDiv = document.getElementById("pollDiv");
    let template = document.getElementsByTagName("template")[0];

    poll.choices.forEach(function (choice) {
    
    let divResult = template.content.querySelector("div.result");
    let choiceText = divResult.querySelector("div.choiceText");
        choiceText.innerText = choice ;
        let choiceVoteCount = divResult.querySelector("div.choiceVoteCount");
        choiceVoteCount.id = "choice"+choiceNumber+"VoteCount";

    let updatedTemplate = document.importNode(divResult, true);
    pollDiv.appendChild(updatedTemplate);
        choiceNumber++;
    });

    //and hook up to the button click event
    document.querySelectorAll(".voteButton").forEach(
        (elem, index) => {
            elem.addEventListener("click", function () {
                connection.invoke("Vote", pollId, index).catch(function (err) {
                    return console.error(err.toString());
                });
            });
        }
    );

}