import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ChapterRequest, ChapterResponse } from '../../models/ApiEndpoints/ChapterContracts';



@Injectable({
  providedIn: 'root'
})
export class ChapterService {


  private readonly apiUrl = 'api/Chapter';


  constructor(
    private http: HttpClient
  ) {}

  getAllChapters(): Observable<ChapterResponse[]> {

    return this.http.get<ChapterResponse[]>(
      this.apiUrl
    );
  }

  getChapterById(id: string): Observable<ChapterResponse> {

    return this.http.get<ChapterResponse>(
      `${this.apiUrl}/${id}`
    );
  }

  createChapter(request: ChapterRequest, personId: string): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/new/${personId}`, request);
  }

  updateChapter(id: string, request: ChapterRequest): Observable<string> {

    const params = new HttpParams()
      .set('id', id);

    return this.http.put<string>(
      this.apiUrl,
      request,
      { params }
    );
  }

  deleteChapter(id: string): Observable<string> {

    const params = new HttpParams()
      .set('id', id);

    return this.http.delete<string>(
      this.apiUrl,
      { params }
    );
  }

}