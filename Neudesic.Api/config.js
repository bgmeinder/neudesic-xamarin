"use strict";

module.exports = {
    development: {
        server: "http://localhost",
        db: "mongodb://localhost/rest-api",
        dbName: "rest-api",
        secretKey: "From the first time to the last time.",
        port: process.env.PORT || 3080
    },
    production: {
        server: "http://gmeinder.net",
        db: "mongodb://localhost/rest-api",
        dbName: "rest-api",
        secretKey: "The third time's the charm!",
        port: process.env.PORT || 80
    }
};