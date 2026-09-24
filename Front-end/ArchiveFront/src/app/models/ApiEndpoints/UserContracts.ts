export interface UserRequest {
  login: string;
  password: string;
  email: string;
}

export interface UserResponse {
  id: string;
  login: string;
  password: string;
  email: string;
}