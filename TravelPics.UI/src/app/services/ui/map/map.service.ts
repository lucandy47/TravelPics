import { Injectable } from '@angular/core';
import { MapPost } from '../../api/dtos/map-post';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MapService {

  constructor() { }
  private mapPosts$: BehaviorSubject<MapPost[]> = new BehaviorSubject<MapPost[]>([]);
  public mapPosts = this.mapPosts$.asObservable();

  public setPosts(posts: any[]): void {
    this.mapPosts$.next(posts);
  }
}
