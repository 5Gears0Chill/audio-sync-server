"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/connectionHub").build();

connection.on("ReceiveMessage", function(message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = message;
});

/*
    Subscribe to 'ReceiveObject' notification from server
*/
connection.on("ReceiveObject", function(message) {
    console.log('Received Object: ', message);
});

/*
    Start connection to hub
*/
connection.start().then(function() {
    console.log('Connection started');
}).catch(function(err) {
    return console.error(err.toString());
});


var audiControl = document.getElementById("audiControl");
audiControl.onplay = function() {
    connection.invoke("PlaySound").catch(function(err) {
        return console.error(err.toString());
    });
};