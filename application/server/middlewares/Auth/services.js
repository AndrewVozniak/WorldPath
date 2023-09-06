const services = [
    {
        name: 'auth',
        protocol: 'http',
        localhost: '127.0.0.1',
        dns_host: 'auth-middleware',
        docker_dev_host: 'host.docker.internal',
        port: 3000,
    },
    {
        name: 'weather',
        protocol: 'http',
        localhost: '127.0.0.1',
        dns_host: 'weather-service',
        docker_dev_host: 'host.docker.internal',
        port: 3001,
    },
    {
        name: 'routes',
        protocol: 'http',
        localhost: '127.0.0.1',
        dns_host: 'routes-service',
        docker_dev_host: 'host.docker.internal',
        port: 3002,
    },
    {
        name: 'places',
        protocol: 'http',
        localhost: '127.0.0.1',
        dns_host: 'places-service',
        docker_dev_host: 'host.docker.internal',
        port: 3003,
    },
    {
        name: 'travels',
        protocol: 'http',
        localhost: '127.0.0.1',
        dns_host: 'travels-service',
        docker_dev_host: 'host.docker.internal',
        port: 3004,
    },
    {
        name: 'community',
        protocol: 'http',
        localhost: '127.0.0.1',
        dns_host: 'community-service',
        docker_dev_host: 'host.docker.internal',
        port: 3005,
    },
    {
        name: 'support',
        protocol: 'http',
        localhost: '127.0.0.1',
        dns_host: 'support-service',
        docker_dev_host: 'host.docker.internal',
        port: 3006,
    },
    {
        name: 'reviews',
        protocol: 'http',
        localhost: '127.0.0.1',
        dns_host: 'reviews-service',
        docker_dev_host: 'host.docker.internal',
        port: 3007,
    },
    {
        name: 'user',
        protocol: 'http',
        localhost: '127.0.0.1',
        dns_host: 'user-service',
        docker_dev_host: 'host.docker.internal',
        port: 3008,
    },
    {
        name: 'dwelling',
        protocol: 'http',
        localhost: '127.0.0.1',
        dns_host: 'dwelling-service',
        docker_dev_host: 'host.docker.internal',
        port: 3009,
    },
    {
        name: 'search',
        protocol: 'http',
        localhost: '127.0.0.1',
        dns_host: 'search-service',
        docker_dev_host: 'host.docker.internal',
        port: 3010,
    },
    {
        name: 'recommendation',
        protocol: 'http',
        localhost: '127.0.0.1',
        dns_host: 'recommendation-service',
        docker_dev_host: 'host.docker.internal',
        port: 3011,
    },
    {
        name: 'notification',
        protocol: 'http',
        localhost: '127.0.0.1',
        dns_host: 'notification-service',
        docker_dev_host: 'host.docker.internal',
        port: 3012,
    },
    {
        name: 'transport',
        protocol: 'http',
        localhost: '127.0.0.1',
        dns_host: 'transport-service',
        docker_dev_host: 'host.docker.internal',
        port: 3013,
    }
]

module.exports = services;