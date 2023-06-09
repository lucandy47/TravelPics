import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-page-not-found',
  templateUrl: './page-not-found.component.html',
  styleUrls: ['./page-not-found.component.scss']
})
export class PageNotFoundComponent implements OnInit {
  public pageUri: string | null = null;

  constructor(private router: Router) {
    this.pageUri = this.router.getCurrentNavigation()?.extras.state?.pageUri;
  }
  ngOnInit(): void {}
}
