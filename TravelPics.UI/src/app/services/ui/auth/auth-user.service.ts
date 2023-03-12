import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthUserService {

  private loggedIn$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    true
  );
  public loggedIn = this.loggedIn$.asObservable();

  constructor() { }

  logout(): void{

  }
}
