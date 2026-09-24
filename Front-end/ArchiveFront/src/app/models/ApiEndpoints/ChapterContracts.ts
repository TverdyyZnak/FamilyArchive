import { FileResource } from "../FileResource";
import { FileRequest } from "./FileResourceContracts";

export interface ChapterRequest {
  serial: number;
  title: string;
  description: string;
  start?: string | null; // DateOnly?
  end?: string | null;   // DateOnly?
}

export interface ChapterResponse {
  id: string;
  serial: number;
  title: string;
  description: string;
  start?: string | null;
  end?: string | null;
  files: FileRequest[];
}