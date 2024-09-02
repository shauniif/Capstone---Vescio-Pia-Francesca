import { iNations } from "./nations";

export interface iCity {
  id: number;
  name: string;
  description: string;
  nation: iNations
}
