import { Person } from "./Person"
import { User } from "./User"

export interface Tree{
    Id : string
    Title : string
    MainUserId : string
    Users : User[]
    Persons : Person[]
}