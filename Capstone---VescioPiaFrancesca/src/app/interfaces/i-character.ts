import { iCity } from "./i-city"
import { iEco } from "./i-eco"
import { iGuild } from "./i-guild"
import { iGuildRole } from "./i-guild-role"
import { iRace } from "./i-race"
import { iUser } from "./i-user"

export interface iCharacter {
  id: number
  name: string
  background: string
  image: string
  score: number
  guild?: iGuild
  city: iCity
  race: iRace
  eco?: iEco
  user: iUser
  guildRole?: iGuildRole
}
