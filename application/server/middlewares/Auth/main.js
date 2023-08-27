const express = require('express');
const axios = require('axios');
const app = express();
const services = require('./services');

app.use(express.json());

async function validateToken(authToken) {
    const user_service = services.find((service) => {
        return service.name === 'user';
    });

    const user_service_url = `${user_service.protocol}://${user_service.host}:${user_service.port}`;
    const request_url = `${user_service_url}/validate_token`;

    try {
        console.log(`Making request to ${request_url} with token ${authToken}`);
        const response = await axios.post(request_url, {}, { headers: { Authorization: authToken } });
        return response.data;
    } catch (error) {
        console.log(error);
        return null;
    }
}

app.all('*', async (req, res) => {
    let url = req.url;

    if (url.indexOf('/auth/') !== 0) {
        return res.send('Authentication not completed!');
    }

    url = url.split('/auth')[1];

    const service = services.find((service) => {
        return url.indexOf(`/${service.name}`) === 0;
    });

    if (!service) {
        return res.send('Service not found!');
    }

    const servicePath = `/${service.name}`;
    const urlParts = url.split(servicePath);

    if (urlParts.length < 2) {
        return res.send('Invalid URL format');
    }

    const requestPath = urlParts.slice(1).join(servicePath);
    const request_url = `${service.protocol}://${service.host}:${service.port}${requestPath}`;


    // Check if the request has a token
    if (req.headers.authorization) {
        let response = await validateToken(req.headers.authorization);

        if (!response.token_valid) {
            req.headers.Authorization = null;
        } else {
            req.headers.Authorization = req.headers.authorization;
            req.headers.Userid = response.user_id;
            console.log(response);
        }
    }

    try {
        let response = await axios({
            method: req.method,
            url: request_url,
            data: req.body,
            headers: {
                'Content-Type': 'application/json',
                'Userid': req.headers['Userid'],
                'Authorization': req.headers['Authorization']
            },
            params: req.query
        });

        res.send(response.data);
    } catch (error) {
        res.status(500).send('Error while making the request');
        console.log(error);
    }
});


app.listen(services[0].port, () => {
    console.log(`Auth Middleware listening on port ${services[0].port}!`);
});
