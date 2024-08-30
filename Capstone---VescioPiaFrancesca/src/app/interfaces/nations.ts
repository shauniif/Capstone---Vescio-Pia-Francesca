import { iCity } from "./i-city"
import { iEco } from "./i-eco"
import { iGuild } from "./i-guild"

export interface iNations {
  id: number
  name:string
  description: string
  formOfGovernment: string
  photo: string
  modfier: number
  ecos: iEco[]
  cities: iCity[]
  guilds: iGuild[]
}
