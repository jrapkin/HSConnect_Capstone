"use strict";
var chatIsOpen = false;
function openChat(nameOfUser) {
    var scriptTag = document.createElement("SCRIPT");
    var srcForScriptTag = document.createAttribute("SRC");
    srcForScriptTag.value = "/js/chat.js";
    scriptTag.setAttributeNode(srcForScriptTag);

    if (!chatIsOpen) {
        document.getElementById("chatApp").innerHTML = "<div class=\"container\"><div class=\"row\">&nbsp;</div><div class=\"row\"><div class=\"col-2\">User</div><div class=\"col-4\"><input type=\"text\" id=\"userInput\" value=\"" + nameOfUser + "\" /></div></div><div class=\"row\"><div class=\"col-2\">Message</div><div class=\"col-4\"><input type=\"text\" id=\"messageInput\" /></div></div><div class=\"row\">&nbsp;</div><div class=\"row\"><div class=\"col-6\"><input type=\"button\" id=\"sendButton\" value=\"Send Message\" /></div></div></div><div class=\"row\"><div class=\"col-12\"><hr /></div></div><div class=\"row\"><div class=\"col-6\"><ul id=\"messagesList\"></ul></div></div>";
        document.getElementById("toggleChat").innerText = "Close Chat";
        document.querySelector("body script:last-child").setAttribute("SRC", "/js/chat.js");
        document.querySelector("body").appendChild(scriptTag);
        chatIsOpen = true;
    }
    else {
        document.getElementById("chatApp").innerHTML = "";
        document.getElementById("toggleChat").innerText = "Open Instant Chat";
        //document.getElementById("chatJs").setAttribute("SRC", "");
        chatIsOpen = false;
        //document.querySelector("body").removeChild(scriptTag);
    };
}