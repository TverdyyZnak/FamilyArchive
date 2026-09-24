import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FileRequest, FileResponse } from '../../models/ApiEndpoints/FileResourceContracts';



@Injectable({
  providedIn: 'root'
})
export class FileService {


  private readonly apiUrl = 'api/File';


  constructor(
    private http: HttpClient
  ) {}

  getAllFiles(): Observable<FileResponse[]> {

    return this.http.get<FileResponse[]>(
      this.apiUrl
    );
  }

  getFileById(id: string): Observable<FileResponse> {


    return this.http.get<FileResponse>(
      `${this.apiUrl}/${id}`
    );
  }

  createFile(request: FileRequest): Observable<string> {


    return this.http.post<string>(
      this.apiUrl,
      request
    );
  }

  updateFile(id: string, request: FileRequest): Observable<string> {

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

  deleteFile(id: string): Observable<string> {

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