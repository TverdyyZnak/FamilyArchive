import { Chapter } from "./Chapter"

export interface Person{
    Id : string
    TreeLVL : number
    FirstName : string
    LastName : string
    Surname: string
    ShortBiography : string
    Birthday : Date
    DayOfDeath : Date
    FatherId : string
    MotherId : string
    Chapters : Chapter[]
}