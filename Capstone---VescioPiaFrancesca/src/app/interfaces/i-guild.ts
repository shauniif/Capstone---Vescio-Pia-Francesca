import { iCharacter } from "./i-character";
import { iNations } from "./nations";

export interface iGuild {
  id: number;
  name: string;
  description: string;
  modifier: number;
  members: iCharacter[];
  power: number;
  nation: iNations;


}
