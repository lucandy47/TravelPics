import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'travelpics-no-posts',
  templateUrl: './no-posts.component.html',
  styleUrls: ['./no-posts.component.scss']
})
export class NoPostsComponent implements OnInit {
  ngOnInit(): void {
  }

  @Input("message") message!: string;
}
