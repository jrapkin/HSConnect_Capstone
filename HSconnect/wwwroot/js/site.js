"use strict";
//if this is working, whats the next step? getting it to the other user recieving the message?
//Am still getting an error on chat.js line 21 I think.
//is it when you click send? I cant see your errors unfortunately
//Do you see my Chrome console?
//yeah I'm looking now.
//Look's better now. Misplaced end parentheses.
function openChat() {
    document.getElementById("chatApp").innerHTML = "<div class=\"container\"><div class=\"row\">&nbsp;</div><div class=\"row\"><div class=\"col-2\">User</div><div class=\"col-4\"><input type=\"text\" id=\"userInput\" /></div></div><div class=\"row\"><div class=\"col-2\">Message</div><div class=\"col-4\"><input type=\"text\" id=\"messageInput\" /></div></div><div class=\"row\">&nbsp;</div><div class=\"row\"><div class=\"col-6\"><input type=\"button\" id=\"sendButton\" value=\"Send Message\" /></div></div></div><div class=\"row\"><div class=\"col-12\"><hr /></div></div><div class=\"row\"><div class=\"col-6\"><ul id=\"messagesList\"></ul></div></div>";
    var scriptTag = document.createElement("SCRIPT");
    var srcForScriptTag = document.createAttribute("SRC");
    srcForScriptTag.value = "/js/chat.js";
    scriptTag.setAttributeNode(srcForScriptTag);
    document.querySelector("body").appendChild(scriptTag);
}