"use strict";



if (document.getElementById("sendButton") !== null) {
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

    document.getElementById("sendButton").disabled = true;
    document.getElementById("userToInput").addEventListener("change", populateMessageList);

    function populateMessageList() {
        document.getElementById("messagesList").innerHTML = "";
        connection.invoke("GetArchivedMessages", document.getElementById("userFromInput").innerText, document.getElementById("userToInput").value).then(
            function (savedMessages) {
                let numberOfSavedMessagesToPrint;
                if (savedMessages.length > 0) {
                    appendElement("messagesList", "p", "Recent Messages:");
                    if (savedMessages.length >= 5) {
                        numberOfSavedMessagesToPrint = savedMessages.length - 5;
                    }
                    else {
                        numberOfSavedMessagesToPrint = 0;
                    }
                    for (let i = numberOfSavedMessagesToPrint; i < savedMessages.length; i++) {
                        let savedMessage = document.createElement("li");
                        savedMessage.textContent = savedMessages[i];
                        document.getElementById("messagesList").appendChild(savedMessage);
                    }
                }
                
                let hr = document.createElement("hr");
                document.getElementById("messagesList").appendChild(hr);
                appendElement("messagesList", "p", "New Messages:");
            },
            function (err) {
            return console.error(err.toString());
        });
    
    };
    function appendElement(idOfElementToAppendTo, elementType, textContent) {
        let currentMessagesLabel = document.createElement(elementType);
        currentMessagesLabel.textContent = textContent;
        document.getElementById(idOfElementToAppendTo).appendChild(currentMessagesLabel);
    }

    connection.on("ReceiveMessage", function (userFrom, userTo, message, timeStamp) {
        if (userTo === document.getElementById("userFromInput").innerText || userFrom === document.getElementById("userFromInput").innerText) {
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
        var userFrom = document.getElementById("userFromInput").innerText;
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
};

