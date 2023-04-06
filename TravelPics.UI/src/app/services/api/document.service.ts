import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  private readonly apiUrl = `${environment.baseDashboardApiUrl}/api/documents`;

  constructor(private httpClient: HttpClient) { }

  public uploadPhoto(formData: FormData): Observable<string>{
    return this.httpClient.post<string>(`${this.apiUrl}`,formData);
  }
}
