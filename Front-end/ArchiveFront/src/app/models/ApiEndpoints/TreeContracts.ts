import { PersonFullResponse } from "./PersonContracts";
import { UserResponse } from "./UserContracts";

export interface TreeFullResponse {
  id: string;
  title: string;
  mainUserId: string;
  users: UserResponse[];
  persons: PersonFullResponse[];
}

export interface TreeRequest {
  title: string;
  mainUserId: string;
}

export interface TreeShortResponse {
  id: string;
  title: string;
  mainUserId: string;
}