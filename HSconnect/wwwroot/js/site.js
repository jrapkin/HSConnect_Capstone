"use strict";
var chatApp = document.getElementById("chatApp");
function openChat() {
    if (chatApp.style.display === "none") {
        chatApp.style.display = "block";
    }
    else {
        chatApp.style.display = "none";
    };
}