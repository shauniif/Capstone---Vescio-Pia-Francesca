import { iNations } from "./nations";

export interface iGuild {
  id: number;
  name: string;
  description: string;
  modifier: number;
  nation: iNations
}
