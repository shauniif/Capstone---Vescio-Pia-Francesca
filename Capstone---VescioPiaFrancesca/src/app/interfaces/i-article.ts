import { iComment } from "./i-comment"
import { iUser } from "./i-user"

export interface iArticle {
  id: number
  title: string
  image: string
  content: string
  author: iUser
  publicationDate: string
  comments: iComment[]
}
