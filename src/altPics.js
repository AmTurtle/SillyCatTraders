"use strict";
/**
 * Copyright: jbs4bmx, revingly
*/
Object.defineProperty(exports, "__esModule", { value: true });
const tsyringe_1 = require("C:/snapshot/project/node_modules/tsyringe");
const logger = tsyringe_1.container.resolve("WinstonLogger");
const imageRouter = tsyringe_1.container.resolve("ImageRouter");
const preAkiModLoader = tsyringe_1.container.resolve("PreAkiModLoader");
class TraderPics {
    constructor() {
        this.path = require('path');
        this.modName = this.path.basename(this.path.dirname(__dirname.split('/').pop()));
        this.fs = require('fs');
    }
    postAkiLoad(container) {
        this.pkg = require("../package.json");
        const { extension, modTraders, updateAllTraders, updateModTraders, updatePrapor, updateTherapist, updateFence, updateSkier, updatePeacekeeper, updateMechanic, updateRagman, updateJaeger, updateLightKeeper } = require('./config.json');
        const filepath = `${preAkiModLoader.getModPath(this.modName)}res/`;
        this.fs.readdir(filepath, (err, files) => {
            files.forEach(file => {
                const traderName = file.split('/').pop().split('.').shift();
                if (updateAllTraders) {
                    imageRouter.addRoute(`/files/trader/avatar/${traderName}`, `${filepath}${traderName}.${extension}`);
                }
                else {
                    if (updatePrapor) {
                        if (traderName === "59b91ca086f77469a81232e4") {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`, `${filepath}${traderName}.${extension}`);
                        }
                    }
                    if (updateTherapist) {
                        if (traderName === "59b91cab86f77469aa5343ca") {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`, `${filepath}${traderName}.${extension}`);
                        }
                    }
                    if (updateFence) {
                        if (traderName === "579dc571d53a0658a154fbec") {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`, `${filepath}${traderName}.${extension}`);
                        }
                    }
                    if (updateSkier) {
                        if (traderName === "59b91cb486f77469a81232e5") {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`, `${filepath}${traderName}.${extension}`);
                        }
                    }
                    if (updatePeacekeeper) {
                        if (traderName === "59b91cbd86f77469aa5343cb") {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`, `${filepath}${traderName}.${extension}`);
                        }
                    }
                    if (updateMechanic) {
                        if (traderName === "5a7c2ebb86f7746e324a06ab") {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`, `${filepath}${traderName}.${extension}`);
                        }
                    }
                    if (updateRagman) {
                        if (traderName === "5ac3b86a86f77461491d1ad8") {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`, `${filepath}${traderName}.${extension}`);
                        }
                    }
                    if (updateJaeger) {
                        if (traderName === "5c06531a86f7746319710e1b") {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`, `${filepath}${traderName}.${extension}`);
                        }
                    }
                    if (updateLightKeeper) {
                        if (traderName === "638f541a29ffd1183d187f57") {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`, `${filepath}${traderName}.${extension}`);
                        }
                    }
                    if (updateModTraders) {
                        for (const modTrader in modTraders) {
                            imageRouter.addRoute(`/files/trader/avatar/${modTrader}`, `${filepath}${modTrader}.${extension}`);
                        }
                    }
                }
            });
        });
        logger.info(`${this.pkg.author}-${this.pkg.name} v${this.pkg.version}:Cached Successfully`);
    }
}
module.exports = { mod: new TraderPics() };