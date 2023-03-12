import { Component } from '@angular/core';

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
export class AppComponent {
  title = 'TravelPics.UI';
}
