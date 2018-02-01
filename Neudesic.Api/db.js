"use strict";

const mongoose = require("mongoose"),
    userModel = require("./models/user");

mongoose.set("debug", function (collectionName, method) {
    console.info("mongo collection: %s method: %s", collectionName, method);
});

module.exports = function (config) {
    mongoose.connect(config.db).then(
        () => {
            console.info("Rest API db opened");
            userModel.createDefaults();
        },
        err => {
            console.error(`connection error: ${err}`);
        });
};