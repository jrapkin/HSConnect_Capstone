"use strict";
var chatApp = document.getElementById("chatApp");
chatApp.style.display = "none";
function openChat() {
    if (chatApp.style.display == "none") {
        chatApp.style.display = "block";
    }
    else {
        chatApp.style.display = "none";
    };
}