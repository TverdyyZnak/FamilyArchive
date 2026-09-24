import { inject, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserResponse, UserRequest } from '../../models/ApiEndpoints/UserContracts';
import { DOCUMENT } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class UserService {


  private readonly apiUrl = '/api/User';

  private document = inject(DOCUMENT)
  constructor(
    private http: HttpClient
  ) {}

  getAllUsers(): Observable<UserResponse[]> {
    return this.http.get<UserResponse[]>(this.apiUrl);
  }

  getUserById(id: string): Observable<UserResponse> {

    const params = new HttpParams()
      .set('id', id);

    return this.http.get<UserResponse>(
      `${this.apiUrl}/by-id`,
      { params }
    );
  }

  getUserByLogin(login: string): Observable<UserResponse> {

    const params = new HttpParams()
      .set('login', login);

    return this.http.get<UserResponse>(
      `${this.apiUrl}/by-login`,
      { params }
    );
  }

  registration(request: UserRequest): Observable<string> {

    return this.http.post<string>(
      `${this.apiUrl}/reg`,
      request
    );
  }

  login(log: string, pass: string): Observable<string> {
    const body = {
      login: log, 
      password: pass
    }

    return this.http.post<string>(`api/User/login`, body);
  }

  logout(): Observable<string> {

    return this.http.post<string>(
      `${this.apiUrl}/logout`,
      null
    );
  }


  deleteUser(id: string): Observable<string> {
    const params = new HttpParams()
      .set('id', id);

    return this.http.delete<string>(
      this.apiUrl,
      { params }
    );
  }

  getCookie(): string | null {
    const name: string = "MCook" 
    const matches = this.document.cookie.match(new RegExp(
      "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    
    return matches ? decodeURIComponent(matches[1]) : null;
  }
}