﻿"use strict";
//This should add the portion of HTML which has the chat interface as well as a script tag linking to chat.js.
//Thats cool. I like the "open instant chat button"
function openChat() {
    document.getElementById("chatApp").innerHTML = "<div class=\"container\"><div class=\"row\">&nbsp;</div><div class=\"row\"><div class=\"col-2\">User</div><div class=\"col-4\"><input type=\"text\" id=\"userInput\" /></div></div><div class=\"row\"><div class=\"col-2\">Message</div><div class=\"col-4\"><input type=\"text\" id=\"messageInput\" /></div></div><div class=\"row\">&nbsp;</div><div class=\"row\"><div class=\"col-6\"><input type=\"button\" id=\"sendButton\" value=\"Send Message\" /></div></div></div><div class=\"row\"><div class=\"col-12\"><hr /></div></div><div class=\"row\"><div class=\"col-6\"><ul id=\"messagesList\"></ul></div></div>";
    var scriptTag = document.createElement("SCRIPT");
    var srcForScriptTag = document.createAttribute("SRC");
    srcForScriptTag.value = "/js/chat.js";
    scriptTag.setAttributeNode(idForScriptTag);
    document.querySelector("body").appendChild(scriptTag);
}