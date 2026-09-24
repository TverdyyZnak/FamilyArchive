import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TreeFullResponse, TreeRequest, TreeShortResponse } from '../../models/ApiEndpoints/TreeContracts';



@Injectable({
  providedIn: 'root'
})
export class TreeService {


  private readonly apiUrl = 'api/Tree';


  constructor(
    private http: HttpClient
  ) { }

  getAllTrees(): Observable<TreeShortResponse[]> {

    return this.http.get<TreeShortResponse[]>(
      this.apiUrl
    );
  }

  getTreeById(id: string): Observable<TreeFullResponse> {

    const params = new HttpParams()
      .set('id', id);


    return this.http.get<TreeFullResponse>(
      `${this.apiUrl}/by-id`,
      { params }
    );
  }

  getTreesByUserId(userId: string): Observable<TreeShortResponse[]> {

    const params = new HttpParams()
      .set('id', userId);


    return this.http.get<TreeShortResponse[]>(
      `${this.apiUrl}/by-user-id`,
      { params }
    );
  }

  createTree(request: TreeRequest): Observable<string> {

    return this.http.post<string>(
      this.apiUrl,
      request
    );
  }

  updateTitle(id: string, title: string): Observable<string> {

    const params = new HttpParams()
      .set('id', id)
      .set('title', title);


    return this.http.put<string>(
      this.apiUrl,
      null,
      {
        params
      }
    );
  }

  addUserToTree(treeId: string, userId: string): Observable<string> {

    const params = new HttpParams()
      .set('treeId', treeId)
      .set('userId', userId);


    return this.http.post<string>(
      `${this.apiUrl}/add-user`,
      null,
      {
        params
      }
    );
  }

  addPersonToTree(treeId: string, personId: string): Observable<string> {
    const params = new HttpParams()
      .set('treeId', treeId)
      .set('personId', personId);

    return this.http.post<string>(
      `${this.apiUrl}/add-person`,
      null,
      {
        params
      }
    );
  }

  deleteTree(id: string): Observable<string> {

    const params = new HttpParams()
      .set('id', id);


    return this.http.delete<string>(
      this.apiUrl,
      {
        params
      }
    );
  }

}