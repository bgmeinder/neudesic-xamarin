"use strict";

const env = process.env.NODE_ENV = process.env.NODE_ENV || 'development',
    express = require("express"),
    bodyParser = require('body-parser'),
    mongoose = require("mongoose"),
    session = require("express-session"),
    MongoStore = require("connect-mongo")(session),
    config = require("./config")[env],
    app = express();

app.use(session({ secret: config.secretKey, store: new MongoStore({ mongooseConnection: mongoose.connection }), saveUninitialized: true, resave: true }));
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

require("./db")(config);
require("./routes")(app);

const httpServer = app.listen(config.port, (err) => {
    if (err) {
        console.log(err);
        return;
    }
    console.log('Listening at http://localhost:' + config.port + '\n');
});

module.exports = httpServer;