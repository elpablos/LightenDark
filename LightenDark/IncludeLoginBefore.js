
/*
 * Oprava chyby v JS, kdy pak padal cely script
 */
if (typeof log == "undefined") { log = function () { } };

if (ws) {
    console.log("ws exists!");
} else {
    console.log("ws NOT exists!");
}

/*
 * Zalogování načtení souboru
 */
console.log("IncludeLoginBefore.js");

/*
 * Přetížený connect
 */
function connect() {
    console.log('Start listening..');
    var target = 'ws://' + window.location.host + '/websocket/serverListener';

    if ('WebSocket' in window) {
        ws = new WebSocket(target);
    } else if ('MozWebSocket' in window) {
        ws = new MozWebSocket(target);
    } else {
        alert('WebSocket is not supported by this browser.');
        return;
    }
    ws.onopen = function () {
        
        log('Info: WebSocket connection opened.');
        //  requestLogin(16);
    };
    ws.onmessage = function (event) {
        // log('Received: ' + event.data);
        bound.logWebSocketData(event.data);
        var msg = JSON.parse(event.data);
        //Ulozim do fronty pokud to neni login
        //  if (messageCounter > 5 && msg.t != 32) {
        //    messageQueue.push(msg);
        //    messageCounter++;
        //  } else {
        processMessage(msg);
        //    messageCounter++;
        //   }
    };
    ws.onclose = function () {
        log('Info: WebSocket connection closed.');
    };
}

// a poslouchej (jinak se skript neuloží)
connect();

/*
 * Přetížený send
 */
function sendMessage(msg) {
    if (window.worldSave) {
        log("WORLD SAVE BLOCKED COMMUNICATION");
        return;
    }

    if (ws != null) {
        var finalMsg = JSON.stringify(msg);
        bound.logWebSocketSend(finalMsg);
        ws.send(finalMsg);
    } else {
        alert('Websocket is CLOSED !');
    }
}