import { iNation } from "./i-nation";

export interface iCity {
  id: number;
  name: string;
  description: string;
  nation: iNation
}
