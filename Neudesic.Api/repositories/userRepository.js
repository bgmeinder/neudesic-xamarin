"use strict";
const mongoose = require("mongoose"),
    User = mongoose.model("User"),
    encryption = require("../utilities/encryption");

exports.get = function (req, res) {
    User.find({}, (err, collection) => {
        res.send(collection);
    });
};

exports.getById = function (req, res) {
    User.findOne({ "_id": req.params.id }, (err, result) => {
        if (err) {
            res.status(500);
            return res.send({ reason: err.toString() });
        }

        res.send(result);
    });
};

exports.getByUserName = function (req, res) {
    User.findOne({ "userName": req.params.userName }, (err, result) => {
        if (err) {
            res.status(500);
            return res.send({ reason: err.toString() });
        }

        res.send(result);
    });
};

exports.update = function (req, res) {
    User.findOne({ _id: req.params.id }, (err, result) => {
        result.firstName = req.body.firstName;
        result.lastName = req.body.lastName;
        result.username = req.body.username.toLowerCase();
        result.salt = encryption.createSalt();
        result.password = encryption.hashPwd(result.salt, req.body.password);
        result.save(function (err) {
            if (err) {
                res.status(500);
                return res.send({ reason: err.toString() });
            }

            res.send(200);
        });
    });
};

exports.delete = function (req, res) {
    User.remove({ "_id": req.params.id }, (err) => {
        if (err) {
            res.status(500);
            res.send({ reason: err.toString() });
        }

        res.sendStatus(204);
    });
};

exports.create = function (req, res) {
    const userData = req.body;
    userData._id = mongoose.Types.ObjectId();
    userData.username = userData.username.toLowerCase();
    userData.salt = encryption.createSalt();
    userData.password = encryption.hashPwd(userData.salt, userData.password);
    User.create(userData, (err, user) => {
        if (err) {
            if (err.toString().indexOf("E11000") > -1) {
                err = new Error("Duplicate Username");
            }
            res.status(400);
            return res.send({ reason: err.toString() });
        }

        res.send(user);
    });
};