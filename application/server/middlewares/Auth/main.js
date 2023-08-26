const express = require('express');
const app = express();

app.get('*', (req, res) => {
    const fullUrl = req.url;

    // Якщо є auth значить мідлвар працює
    if(fullUrl.indexOf('/auth') === 0) {
        const url = fullUrl.split('/auth')[1];

        // Якщо є токен - звіряєм
        if (req.headers.token) {
            res.send(req.headers.token);
        }
        // Якщо немає - переадресовуєм на origin
        else {
            res.redirect(`${url}`);
        }
    }
    //
    else {

    }
});

app.listen(3000, () => {
    console.log('Example app listening on port 3000!');
});