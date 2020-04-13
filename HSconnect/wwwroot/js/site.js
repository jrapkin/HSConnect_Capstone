"use strict";
var chatApp = document.getElementById("chatApp");
chatApp.style.display = "none";
function openChat() {
    let chatWindow = document.getElementById("chatWindow");
    let openCloseButton = document.getElementById("toggleChat");
    if (chatApp.style.display == "none") {
        chatApp.style.display = "block";
        chatWindow.style.width = "50%";
        chatWindow.style.borderLeft = "1px solid #cccccc";
        chatWindow.style.backgroundColor = "white";
        openCloseButton.innerText = "Close Instant Chat";
    }
    else {
        chatApp.style.display = "none";
        chatWindow.style.width = "auto";
        chatWindow.style.border = "none";
        chatWindow.style.background = "none";
        openCloseButton.innerText = "Open Instant Chat";
    };
}
function openFilterResourcesMenu() {
    document.getElementById("filter").style.display = "block";
}