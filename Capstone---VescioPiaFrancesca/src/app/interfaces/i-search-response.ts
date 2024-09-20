import { iCity } from "./i-city";
import { iEco } from "./i-eco";
import { iGuild } from "./i-guild";
import { iNation } from "./i-nation";

export interface iSearchResponse {
  ecos: iEco[];
  guilds: iGuild[];
  cities: iCity[];
  nations: iNation[];
}
