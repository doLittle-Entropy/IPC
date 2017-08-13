var net = require("net");
var socket = net.connect("/tmp/dolittle.sock");

socket.on("error", function (err) {
    console.log(err);
});

socket.on("connect", function () {
    console.log("Connected");
    socket.write("Hello from node<EOF>");
});

socket.on("data", function (data) {
    socket.end();
    console.log(data.toString("UTF-8"));
});