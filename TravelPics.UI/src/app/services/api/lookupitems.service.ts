import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AvailableSearchItem } from './dtos/available-search-item';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LookupitemsService {
  private readonly apiUrl = `${environment.baseDashboardApiUrl}/api/lookupitems`;

  constructor(private httpClient: HttpClient) { }

  findLookupItems(searchKeyword: string):Observable<AvailableSearchItem[]>{
    let params = new HttpParams().set('searchKeyword', searchKeyword);
    return this.httpClient.get<AvailableSearchItem[]>(
      `${this.apiUrl}/search-lookupitems`,
      {params: params}
    );
  }
}
