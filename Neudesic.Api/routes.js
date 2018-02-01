"use strict";

const userRepository = require("./repositories/userRepository");

module.exports = function (app) {
    app.get("/api/users", userRepository.get);
    app.get("/api/users/:id", userRepository.getById);
    app.post("/api/users", userRepository.create);
    app.put("/api/users/:id", userRepository.update);
    app.delete("/api/users/:id", userRepository.delete);
};