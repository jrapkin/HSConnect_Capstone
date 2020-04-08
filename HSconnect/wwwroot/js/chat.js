"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("sendButton").disabled = true;

//function populateMessageList() {
//    connection.invoke("GetArchivedMessages", document.getElementById("userFromInput").value, document.getElementById("userToInput").value).catch(function (err) {
//        return console.error(err.toString());
//    });
//    for (let i = 0; i < savedMessages.length; i++) {
//        let savedMessage = document.createElement("li");
//        savedMessage.textContent = savedMessages[i];
//        document.getElementById("messagesList").appendChild(savedMessage);
//    }
//};
function populateMessageList() {
    connection.invoke("GetArchivedMessages", document.getElementById("userFromInput").value, document.getElementById("userToInput").value).then(
        function (savedMessages) {
            for (let i = 0; i < savedMessages.length; i++) {
                let savedMessage = document.createElement("li");
                savedMessage.textContent = savedMessages[i];
                document.getElementById("messagesList").appendChild(savedMessage);
            }
        },
        function (err) {
        return console.error(err.toString());
    });
    
};

connection.on("ReceiveMessage", function (userFrom, userTo, message, timeStamp) {
    if (userTo === document.getElementById("userFromInput").value) {
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = timeStamp + " " + userFrom + " says " + msg;
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    };
});

connection.start().then(function () {
    populateMessageList();
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var userFrom = document.getElementById("userFromInput").value;
    var userTo = document.getElementById("userToInput").value;
    var message = document.getElementById("messageInput").value;
    var timeStamp = new Date(Date.now()).getHours() + ":" + new Date(Date.now()).getMinutes();
    connection.invoke("SendMessage", userFrom, userTo, message, timeStamp.toString()).catch(function (err) {
        return console.error(err.toString());
    });
    connection.invoke("ArchiveMessage", userFrom, userTo, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

