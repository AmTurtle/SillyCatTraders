/* eslint-disable @typescript-eslint/naming-convention */
/* eslint-disable @typescript-eslint/brace-style */
import { container, DependencyContainer } from "tsyringe";
import { PreSptModLoader } from "@spt/loaders/PreSptModLoader";
import { IPreSptLoadMod } from "@spt/models/external/IPreSptLoadMod";
import { IPostSptLoadMod } from "@spt/models/external/IPostSptLoadMod";
import { ILogger } from "@spt/models/spt/utils/ILogger";
import { ImageRouter } from "@spt/routers/ImageRouter";

const logger = container.resolve<ILogger>("WinstonLogger");
const imageRouter = container.resolve<ImageRouter>("ImageRouter");
const preSptModLoader = container.resolve<PreSptModLoader>("PreSptModLoader");

class Mod implements IPreSptLoadMod, IPostSptLoadMod
{
    private container: DependencyContainer;
    private pkg;
    private path = require("path");
    private modName = this.path.basename(this.path.dirname(__dirname.split("/").pop()));
    private fs = require("fs");

    public postSptLoad(container: DependencyContainer) {
        this.pkg = require("../package.json");
        const { extension, updateAllTraders, updatePrapor, updateTherapist, updateFence, updateSkier, updatePeacekeeper, updateMechanic, updateRagman, updateJaeger, updateLightKeeper, updateRef, AIOTrader, AKGuy, AnastasiaSvetlana, ARSHoppe, ArtemTrader, Bootlegger, DRIP, GearGal, GoblinKing, Gunsmith, IProject, KatarinaBlack, KeyMaster, MFACShop, Priscilu, Questor, TheBroker, cuteTrader, zeroTrader, sashahimik} = require("./config.json");
        const filepath = `${preSptModLoader.getModPath(this.modName)}res/`;

        this.fs.readdir(filepath, (err: any, files: any[]) => {
            files.forEach(file => {
                const traderName = file.split("/").pop().split(".").shift()
                if ( updateAllTraders ) {
                    // Updates all supported traders, both default and mod traders.
                    imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);

                } else {
                    // Updates only the selected traders as supported by this mod.
                    if ( updatePrapor ) {
                        if ( traderName === "59b91ca086f77469a81232e4" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( updateTherapist ) {
                        if ( traderName === "59b91cab86f77469aa5343ca" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( updateFence ) {
                        if ( traderName === "579dc571d53a0658a154fbec" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( updateSkier ) {
                        if ( traderName === "59b91cb486f77469a81232e5" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( updatePeacekeeper ) {
                        if ( traderName === "59b91cbd86f77469aa5343cb" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( updateMechanic ) {
                        if ( traderName === "5a7c2ebb86f7746e324a06ab" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( updateRagman ) {
                        if ( traderName === "5ac3b86a86f77461491d1ad8" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( updateJaeger ) {
                        if ( traderName === "5c06531a86f7746319710e1b" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( updateLightKeeper ) {
                        if ( traderName === "638f541a29ffd1183d187f57" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( updateRef ) {
                        if ( traderName === "6617beeaa9cfa777ca915b7c") {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( AIOTrader ) {
                        if ( traderName === "aiotrader" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                        if ( traderName === "blueheadtrader" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( AKGuy ) {
                        if ( traderName === "AKGUY" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( AnastasiaSvetlana ) {
                        if ( traderName === "Anastasia" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                        if ( traderName === "Svetlana" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( ARSHoppe ) {
                        if ( traderName === "armalite" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( ArtemTrader ) {
                        if ( traderName === "ArtemTrader" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( Bootlegger ) {
                        if ( traderName === "bootlegger" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( DRIP ) {
                        if ( traderName === "moron" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( GearGal ) {
                        if ( traderName === "GearGal" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( GoblinKing ) {
                        if ( traderName === "GoblinKing" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( Gunsmith ) {
                        if ( traderName === "gunsmith" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( IProject ) {
                        if ( traderName === "PRTS" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( KatarinaBlack ) {
                        if ( traderName === "kat" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                        if ( traderName === "sepi1" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( KeyMaster ) {
                        if ( traderName === "keymaster" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( MFACShop ) {
                        if ( traderName === "MFAC" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( Priscilu ) {
                        if ( traderName === "Priscilu" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( Questor ) {
                        if ( traderName === "questor" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( TheBroker ) {
                        if ( traderName === "broker_portrait1" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( cuteTrader ) {
                        if ( traderName === "kwmKYUUTO" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( zeroTrader ) {
                        if ( traderName === "kwmZERO" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                    if ( sashahimik ) {
                        if ( traderName === "himik" ) {
                            imageRouter.addRoute(`/files/trader/avatar/${traderName}`,`${filepath}${traderName}.${extension}`);
                        }
                    }
                }
            });
        });
        logger.info(`${this.pkg.author}-${this.pkg.name} v${this.pkg.version}:Cached Successfully`);
    }

}

export const mod = new Mod();
