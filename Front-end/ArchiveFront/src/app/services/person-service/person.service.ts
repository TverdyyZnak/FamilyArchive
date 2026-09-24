import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PersonFullResponse, PersonRequest, PersonShortResponse } from '../../models/ApiEndpoints/PersonContracts';



@Injectable({
  providedIn: 'root'
})
export class PersonService {


  private readonly apiUrl = 'api/Person';


  constructor(
    private http: HttpClient
  ) {}

  getAllPersons(): Observable<PersonShortResponse[]> {

    return this.http.get<PersonShortResponse[]>(
      this.apiUrl
    );
  }

  getPersonById(id: string): Observable<PersonFullResponse> {

    const params = new HttpParams()
      .set('id', id);


    return this.http.get<PersonFullResponse>(
      `${this.apiUrl}/by-id`,
      {
        params
      }
    );
  }

  updatePerson(id: string, request: PersonRequest): Observable<string> {

    const params = new HttpParams()
      .set('id', id);


    return this.http.put<string>(
      this.apiUrl,
      request,
      {
        params
      }
    );
  }

  updateFatherId(personId: string, fatherId: string): Observable<string> {

    const params = new HttpParams()
      .set('personId', personId)
      .set('fatherId', fatherId);


    return this.http.put<string>(
      `${this.apiUrl}/father-id`,
      null,
      {
        params
      }
    );
  }

  updateMotherId(personId: string, motherId: string): Observable<string> {

    return this.http.put<string>(
      `${this.apiUrl}/${personId}/mother/${motherId}`,
      null
    );
  }

  addChapter(personId: string, chapterId: string): Observable<string> {

    return this.http.put<string>(
      `${this.apiUrl}/${personId}/chapter/${chapterId}`,
      null
    );
  }

  createPerson(request: PersonRequest, archiveId: string): Observable<string> {

    return this.http.post<string>(
      `${this.apiUrl}/${archiveId}`,
      request
    );
  }

  deletePerson(id: string): Observable<string> {

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