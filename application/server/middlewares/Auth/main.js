const express = require('express');
const axios = require('axios');
const app = express();

const services = require('./services');
const hosts = require('./hosts');

app.use(express.json());

app.use((req, res, next) => {
    res.header('Access-Control-Allow-Origin', '*');
    res.header('Access-Control-Allow-Headers', '*');
    next();
});

app.options('*', (req, res) => {
    res.header('Access-Control-Allow-Origin', '*');
    res.header('Access-Control-Allow-Headers', '*');
    res.header('Access-Control-Allow-Methods', 'GET, POST, PATCH, PUT, DELETE');
    res.send();
});

async function validateToken(authToken) {
    const user_service = services.find((service) => {
        return service.name === 'user';
    });

    const user_service_url = `${user_service.protocol}://${user_service.dns_host}:${user_service.port}`; // service.host if common host
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

async function validateUrl(req) {
    const host = req.headers.host;
    const host_without_port = host.split(':')[0];

    return hosts.includes(host_without_port);
}

app.all('*', async (req, res) => {
    let url = req.url;

    if (!await validateUrl(req)) {
        return res.status(404).send('Host not found!');
    }

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
    const request_url = `${service.protocol}://${service.dns_host}:${service.port}${requestPath}`; // service.host if common host


    // Check if the request has a token
    if (req.headers.authorization) {
        let response = await validateToken(req.headers.authorization);

        if (!response.token_valid) {
            req.headers.Authorization = null;
            return res.status(401).send(response.error);
        } else {
            req.headers.Authorization = req.headers.authorization;
            req.headers.Userid = response.user_id;
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
