import { iCharacter } from "./i-character";

export interface iUser {
  id: number;
  firstName: string;
  lastName: string;
  username: string;
  dateBirth: Date;
  email: string;
  password: string;
  image?: string;
  characters: iCharacter[];
}
