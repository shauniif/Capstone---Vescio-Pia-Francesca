import { iGuild } from "./i-guild";

export interface iGuildRole {
  id: number;
  name: string;
  modifier: number
  guild: iGuild;
}
