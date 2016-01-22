/*
 * kontrola existence webSocketu
 */
if (ws) {
    console.log("ws exists!");
} else {
    console.log("ws NOT exists!");
}
/*
 * Zalogování načtení souboru
 */
console.log("ApplicationStart.js");
/*
 * Napojení se na WebSocket instanci a naslouchat
 */
(function webSocketBinder(ws) {
    ws._send_ = ws.send;
    ws._onmessage_ = ws.onmessage;
    ws.send = function (msg) {
        bound.logWebSocketSend(msg);
        ws._send_(msg);
    };
    ws.onmessage = function (evt) {
        bound.logWebSocketData(evt.data);
        ws._onmessage_(evt);
    };
})(ws);