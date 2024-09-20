import { iCharacter } from "./i-character";
import { iNation } from "./i-nation";


export interface iGuild {
  id: number;
  name: string;
  description: string;
  modifier: number;
  members: iCharacter[];
  power: number;
  nation: iNation;


}
