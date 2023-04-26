import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Post } from './dtos/post';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  private readonly apiUrl = `${environment.baseDashboardApiUrl}/api/posts`;

  constructor(private httpClient: HttpClient) { }

  public addNewPost(formData: FormData): Observable<string>{
    return this.httpClient.post<string>(`${this.apiUrl}`, formData);
  }

  public getUserPosts(userId: number):Observable<Post[]>{
    return this.httpClient.get<Post[]>(`${this.apiUrl}/user/${userId}`);
  }
}
