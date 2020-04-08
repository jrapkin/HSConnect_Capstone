"use strict";
var chatIsOpen = false;
function openChat(nameOfUserFrom, nameOfUserTo, savedMessages) {
    var scriptTag = document.createElement("SCRIPT");
    var srcForScriptTag = document.createAttribute("SRC");
    srcForScriptTag.value = "/js/chat.js";
    scriptTag.setAttributeNode(srcForScriptTag);

    if (!chatIsOpen) {
        document.getElementById("chatApp").innerHTML =
            "<div class=\"container\">" +
                "<div class=\"row\">&nbsp;</div>" +
                "<div class=\"row\">" +
                    "<div class=\"col-2\">From</div>" +
                    "<div class=\"col-4\" id=\"userFromInput\"<strong>" + nameOfUserFrom + "</strong></div>" +
                "</div>" +
                "<div class=\"row\">" +
                    "<div class=\"col-2\">To</div>" +
                    "<div class=\"col-4\">" +
                        "<input type=\"text\" id=\"userToInput\" value=\"" + nameOfUserTo + "\" />" + 
                    "</div>" +
                "</div >" +
                "<div class=\"row\">" +
                    "<div class=\"col-2\">Message</div>" +
                    "<div class=\"col-4\">" +
                        "<input type=\"text\" id=\"messageInput\" />" +
                    "</div>" +
                "</div >" +
            "</div >" +
            
            "<div class=\"row\">&nbsp;</div>" +
                "<div class=\"row\">" +
                    "<div class=\"col-6\">" +
                    "<input type=\"button\" id=\"sendButton\" value=\"Send Message\" />" + 
                    "</div >" + 
                "</div >" + 
            "</div >" +
            "<div class=\"row\">" +
                "<div class=\"col-12\">" +
                "<hr />" + 
                "</div >" +
            "</div >" +
            "<div class=\"row\"><div>" +
            "<ul id=\"messagesList\"></ul>" + 
            "</div >" +
            "</div > ";
        //for (let i = 0; i < savedMessages.length; i++) {
        //    let savedMessage = document.createElement("li");
        //    savedMessage.textContent = messages[i];
        //    document.getElementById("messagesList").appendChild(savedMessage);
        //};
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