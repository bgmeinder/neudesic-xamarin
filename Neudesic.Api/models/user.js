"use strict";

const mongoose = require("mongoose"),
    encryption = require("../utilities/encryption"),
    defaultPassword = "P@ssword123",
    userSchema = mongoose.Schema({
        firstName: String,
        lastName: String,
        username: {
            type: String,
            required: true,
            unique: true
        },
        salt: String,
        password: String,
    }),
    User = mongoose.model("User", userSchema),
    createNewUser = function (user) {
        user.salt = encryption.createSalt();
        user.password = encryption.hashPwd(user.salt, defaultPassword);

        return user;
    };

exports.createDefaults = function () {
    User.find({}, (err, collection) => {
        if (collection.length === 0) {
            const defaultUsers = [
                {
                    firstName: "Brent",
                    lastName: "Gmeinder",
                    username: "brent@gmeinder.net"
                },
                {
                    firstName: "John",
                    lastName: "Doe",
                    username: "jdoe@email.com",
                },
                {
                    firstName: "Tom",
                    lastName: "Jones",
                    username: "tj@email.com",
                },
                {
                    firstName: "Sarah",
                    lastName: "Connor",
                    username: "sconnor@skynet.com",
                }
            ];
            let newUser = null;

            defaultUsers.forEach(user => {
                newUser = createNewUser(user);
                User.create(newUser);
            });
        }
    });
};