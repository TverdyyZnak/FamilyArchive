import { FileResource } from "./FileResource"

export interface Chapter{
    Id : string
    SerialNumber:number
    Title : string
    Description : string
    StartDate : Date
    EndDate : Date
    Files : FileResource[]
}