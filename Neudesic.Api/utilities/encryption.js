"use strict";
const crypto = require("crypto"),
    encoding = "base64";

module.exports = {
    createSalt: function () {
        const buffer = crypto.randomBytes(256);

        return buffer.toString(encoding);
    },
    hashPwd: function (salt, pwd) {
        const hmac = crypto.createHmac("sha256", salt);

        return hmac.update(pwd).digest(encoding);
    },


};