import { iNation } from "./i-nation"

export interface iEco {
  id: number
  position: number
  name: string
  description: string
  pic: string
  modifier: number
  nation: iNation
}
