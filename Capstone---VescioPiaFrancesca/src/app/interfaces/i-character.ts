import { iCity } from "./i-city"
import { iEco } from "./i-eco"
import { iGuild } from "./i-guild"
import { iRace } from "./i-race"
import { iUser } from "./i-user"

export interface iCharacter {
  id: number
  name: string
  image: string
  score: number
  guild?: iGuild
  city: iCity
  race: iRace
  eco?: iEco
  user: iUser
}
