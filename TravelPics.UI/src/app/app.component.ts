import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthUserService } from './services/ui/auth/auth-user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss',
  '../styles/primeng-overwrites/menubar.scss',
  '../styles/primeng-overwrites/table.scss',
  '../styles/primeng-overwrites/typography.scss',
  '../styles/primeng-overwrites/overlay.scss',
]
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'TravelPics.UI';

  constructor(
    private _authUserService: AuthUserService
  ){}
  ngOnDestroy(): void {
  }

  ngOnInit(): void {
    this._authUserService.tryResoreUserSession();
  }
}
