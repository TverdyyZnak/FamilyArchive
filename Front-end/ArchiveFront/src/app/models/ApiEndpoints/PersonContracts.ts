import { ChapterResponse } from "./ChapterContracts";

export interface PersonFullResponse {
  id: string;
  firstName: string;
  lastName: string;
  surname: string;
  biography: string;
  birthday?: string | null; // DateOnly?
  death?: string | null;    // DateOnly?
  chapters: ChapterResponse[];
}

export interface PersonRequest {
  firstName: string;
  lastName: string;
  surname: string;
  biography: string;
  birthday?: string | null; // DateOnly?
  death?: string | null;    // DateOnly?
}

export interface PersonShortResponse {
  id: string;
  firstName: string;
  lastName: string;
  surname: string;
  biography: string;
  birthday?: string | null;
  death?: string | null;
}