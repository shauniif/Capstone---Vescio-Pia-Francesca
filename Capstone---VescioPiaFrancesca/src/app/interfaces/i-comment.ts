import { iArticle } from "./i-article"
import { iUser } from "./i-user"

export interface iComment {
  id: number
  content: string
  author: iUser
  article: iArticle
  publicationDate: string
}
