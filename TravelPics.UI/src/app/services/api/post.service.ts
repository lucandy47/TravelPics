import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Post } from './dtos/post';
import { LikeModel } from './dtos/like-model';
import { MapPost } from './dtos/map-post';

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

  public getLatestPosts():Observable<Post[]>{
    return this.httpClient.get<Post[]>(`${this.apiUrl}`);
  }
  
  public likePost(likeModel: LikeModel): Observable<string>{
    return this.httpClient.post<string>(`${this.apiUrl}/like`, likeModel);
  }

  public dislikePost(likeModel: LikeModel): Observable<string>{
    return this.httpClient.post<string>(`${this.apiUrl}/dislike`, likeModel);
  }

  public getMapPosts():Observable<MapPost[]>{
    return this.httpClient.get<MapPost[]>(`${this.apiUrl}/map`);
  }
}
